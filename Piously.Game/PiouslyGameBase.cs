using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Input;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Piously.Game.Configuration;
using Piously.Game.Graphics;
using Piously.Game.Graphics.Cursor;
using Piously.Game.Input;
using Piously.Game.Input.Bindings;
using Piously.Game.IO;
using osuTK.Input;

namespace Piously.Game
{
    //This class does not load UI, just everything else
    public class PiouslyGameBase : osu.Framework.Game
    {
        protected Storage Storage { get; set; }

        protected KeyBindingStore KeyBindingStore;

        protected PiouslyConfigManager LocalConfig;

        protected MenuCursorContainer MenuCursorContainer;

        private Bindable<bool> fpsDisplayVisible;

        public PiouslyGameBase()
        {
            Name = @"Piously";
        }
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            // may be non-null for certain tests
            Storage ??= host.Storage;

            LocalConfig ??= new PiouslyConfigManager(Storage);
        }

        protected override Storage CreateStorage(GameHost host, Storage defaultStorage) => new PiouslyStorage(host, defaultStorage);

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        protected override UserInputManager CreateUserInputManager() => new PiouslyUserInputManager();

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(PiouslyGameBase).Assembly), "Resources"));

            dependencies.CacheAs(Storage);

            dependencies.CacheAs(this);
            dependencies.Cache(LocalConfig);

            AddFont(Resources, @"Fonts/InkFree-Bold");
            AddFont(Resources, @"Fonts/InkFree");
            AddFont(Resources, @"Fonts/InkFree-Italic");
            AddFont(Resources, @"Fonts/InkFree-BoldItalic");

            dependencies.Cache(KeyBindingStore = new KeyBindingStore(Storage));
            dependencies.Cache(new PiouslyColour());

            MenuCursorContainer = new MenuCursorContainer { RelativeSizeAxes = Axes.Both };

            PiouslyKeyBindingContainer globalBindings;

            MenuCursorContainer.Child = globalBindings = new PiouslyKeyBindingContainer(this)
            {
                RelativeSizeAxes = Axes.Both,
                //TO BE IMPLEMENTED
                //Child = content = new PiouslyTooltipContainer(MenuCursorContainer.Cursor) { RelativeSizeAxes = Axes.Both }
            };

            base.Content.Add(CreateScalingContainer().WithChild(MenuCursorContainer));

            KeyBindingStore.Register(globalBindings);
            dependencies.Cache(globalBindings);
        }

        protected virtual Container CreateScalingContainer() => new DrawSizePreservingFillContainer();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            fpsDisplayVisible = LocalConfig.GetBindable<bool>(PiouslySetting.ShowFpsDisplay);
            fpsDisplayVisible.ValueChanged += visible => { FrameStatistics.Value = visible.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None; };
            fpsDisplayVisible.TriggerChange();

            FrameStatistics.ValueChanged += e => fpsDisplayVisible.Value = e.NewValue != FrameStatisticsMode.None;

            base.Content.Add(CreateScalingContainer());
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            LocalConfig?.Dispose();
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
            (Storage as PiouslyStorage)?.Migrate(Host.GetStorage(path));
        }
    }
}
