using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Piously.Game.Configuration;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Cursor;
using Piously.Game.Online.API;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input;
using osu.Framework.Logging;
using Piously.Game.Database;
using Piously.Game.Input;
using Piously.Game.Input.Bindings;
using Piously.Game.IO;
using osuTK.Input;
using RuntimeInfo = osu.Framework.RuntimeInfo;

namespace Piously.Game
{
    public class PiouslyGameBase : osu.Framework.Game
    {
        public const string CLIENT_STREAM_NAME = "Main";

        public const int SAMPLE_CONCURRENCY = 6;

        protected PiouslyConfigManager LocalConfig;

        protected FileStore FileStore;

        protected KeyBindingStore KeyBindingStore;

        protected SettingsStore SettingsStore;

        protected IAPIProvider API;

        protected MenuCursorContainer MenuCursorContainer;

        private Container content;

        protected override Container<Drawable> Content => content;

        protected Storage Storage { get; set; }

        private Bindable<bool> fpsDisplayVisible;

        public virtual Version AssemblyVersion => Assembly.GetEntryAssembly()?.GetName().Version ?? new Version();

        public string VersionHash { get; private set; }

        public bool IsDeployedBuild => AssemblyVersion.Major > 0;

        public virtual string Version
        {
            get
            {
                if (!IsDeployedBuild)
                    return @"local " + (DebugUtils.IsDebugBuild ? @"debug" : @"release");

                var version = AssemblyVersion;
                return $@"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        public PiouslyGameBase()
        {
            Name = @"Piously";
        }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        private DatabaseContextFactory contextFactory;

        protected override UserInputManager CreateUserInputManager() => new PiouslyUserInputManager();

        [BackgroundDependencyLoader]
        private void load()
        {
            using (var str = File.OpenRead(typeof(PiouslyGameBase).Assembly.Location))
                VersionHash = str.ComputeMD5Hash();

            Resources.AddStore(new DllResourceStore(@"Piously.dll"));

            dependencies.Cache(contextFactory = new DatabaseContextFactory(Storage));

            dependencies.CacheAs(Storage);

            var largeStore = new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, @"Textures")));
            largeStore.AddStore(Host.CreateTextureLoaderStore(new OnlineStore()));
            dependencies.Cache(largeStore);

            dependencies.CacheAs(this);
            dependencies.Cache(LocalConfig);

            AddFont(Resources, @"Fonts/osuFont");

            AddFont(Resources, @"Fonts/Torus-Regular");
            AddFont(Resources, @"Fonts/Torus-Light");
            AddFont(Resources, @"Fonts/Torus-SemiBold");
            AddFont(Resources, @"Fonts/Torus-Bold");

            AddFont(Resources, @"Fonts/Noto-Basic");
            AddFont(Resources, @"Fonts/Noto-Hangul");
            AddFont(Resources, @"Fonts/Noto-CJK-Basic");
            AddFont(Resources, @"Fonts/Noto-CJK-Compatibility");
            AddFont(Resources, @"Fonts/Noto-Thai");

            AddFont(Resources, @"Fonts/Venera-Light");
            AddFont(Resources, @"Fonts/Venera-Bold");
            AddFont(Resources, @"Fonts/Venera-Black");

            Audio.Samples.PlaybackConcurrency = SAMPLE_CONCURRENCY;

            runMigrations();

            API ??= new APIAccess(LocalConfig);

            dependencies.CacheAs(API);

            dependencies.Cache(FileStore = new FileStore(contextFactory, Storage));

            dependencies.Cache(KeyBindingStore = new KeyBindingStore(contextFactory));
            dependencies.Cache(SettingsStore = new SettingsStore(contextFactory));
            dependencies.Cache(new SessionStatics());
            dependencies.Cache(new PiouslyColor());

            // tracks play so loud our samples can't keep up.
            // this adds a global reduction of track volume for the time being.
            Audio.Tracks.AddAdjustment(AdjustableProperty.Volume, new BindableDouble(0.8));

            FileStore.Cleanup();

            if (API is APIAccess apiAccess)
                AddInternal(apiAccess);

            MenuCursorContainer = new MenuCursorContainer { RelativeSizeAxes = Axes.Both };

            GlobalActionContainer globalBindings;

            MenuCursorContainer.Child = globalBindings = new GlobalActionContainer(this)
            {
                RelativeSizeAxes = Axes.Both,
                Child = content = new PiouslyTooltipContainer(MenuCursorContainer.Cursor) { RelativeSizeAxes = Axes.Both }
            };

            base.Content.Add(CreateScalingContainer().WithChild(MenuCursorContainer));

            KeyBindingStore.Register(globalBindings);
            dependencies.Cache(globalBindings);
        }

        protected virtual Container CreateScalingContainer() => new DrawSizePreservingFillContainer();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // TODO: This is temporary until we reimplement the local FPS display.
            // It's just to allow end-users to access the framework FPS display without knowing the shortcut key.
            fpsDisplayVisible = LocalConfig.GetBindable<bool>(PiouslySetting.ShowFpsDisplay);
            fpsDisplayVisible.ValueChanged += visible => { FrameStatistics.Value = visible.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None; };
            fpsDisplayVisible.TriggerChange();

            FrameStatistics.ValueChanged += e => fpsDisplayVisible.Value = e.NewValue != FrameStatisticsMode.None;
        }

        private void runMigrations()
        {
            try
            {
                using (var db = contextFactory.GetForWrite(false))
                    db.Context.Migrate();
            }
            catch (Exception e)
            {
                Logger.Error(e.InnerException ?? e, "Migration failed! We'll be starting with a fresh database.", LoggingTarget.Database);

                // if we failed, let's delete the database and start fresh.
                // todo: we probably want a better (non-destructive) migrations/recovery process at a later point than this.
                contextFactory.ResetDatabase();

                Logger.Log("Database purged successfully.", LoggingTarget.Database);

                // only run once more, then hard bail.
                using (var db = contextFactory.GetForWrite(false))
                    db.Context.Migrate();
            }
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            // may be non-null for certain tests
            Storage ??= host.Storage;

            LocalConfig ??= new PiouslyConfigManager(Storage);
        }

        protected override Storage CreateStorage(GameHost host, Storage defaultStorage) => new PiouslyStorage(host, defaultStorage);

        private readonly List<ICanAcceptFiles> fileImporters = new List<ICanAcceptFiles>();

        /// <summary>
        /// Register a global handler for file imports. Most recently registered will have precedence.
        /// </summary>
        /// <param name="handler">The handler to register.</param>
        public void RegisterImportHandler(ICanAcceptFiles handler) => fileImporters.Insert(0, handler);

        /// <summary>
        /// Unregister a global handler for file imports.
        /// </summary>
        /// <param name="handler">The previously registered handler.</param>
        public void UnregisterImportHandler(ICanAcceptFiles handler) => fileImporters.Remove(handler);

        public async Task Import(params string[] paths)
        {
            var extension = Path.GetExtension(paths.First())?.ToLowerInvariant();

            foreach (var importer in fileImporters)
            {
                if (importer.HandledExtensions.Contains(extension))
                    await importer.Import(paths);
            }
        }

        public IEnumerable<string> HandledExtensions => fileImporters.SelectMany(i => i.HandledExtensions);

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            LocalConfig?.Dispose();

            contextFactory.FlushConnections();
        }

        private class PiouslyUserInputManager : UserInputManager
        {
            protected override MouseButtonEventManager CreateButtonEventManagerFor(MouseButton button)
            {
                switch (button)
                {
                    case MouseButton.Right:
                        return new RightMouseManager(button);
                }

                return base.CreateButtonEventManagerFor(button);
            }

            private class RightMouseManager : MouseButtonEventManager
            {
                public RightMouseManager(MouseButton button)
                    : base(button)
                {
                }

                public override bool EnableDrag => true; // allow right-mouse dragging for absolute scroll in scroll containers.
                public override bool EnableClick => false;
                public override bool ChangeFocusOnClick => false;
            }
        }

        public void Migrate(string path)
        {
            contextFactory.FlushConnections();
            (Storage as PiouslyStorage)?.Migrate(Host.GetStorage(path));
        }
    }
}
