using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using Piously.Game.Configuration;
using Piously.Game.IO;

namespace Piously.Game
{
    //This class does not load UI, just everything else
    public class PiouslyGameBase : osu.Framework.Game
    {
        protected Storage Storage { get; set; }

        protected PiouslyConfigManager LocalConfig;

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
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            fpsDisplayVisible = LocalConfig.GetBindable<bool>(PiouslySetting.ShowFpsDisplay);
            fpsDisplayVisible.ValueChanged += visible => { FrameStatistics.Value = visible.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None; };
            fpsDisplayVisible.TriggerChange();

            FrameStatistics.ValueChanged += e => fpsDisplayVisible.Value = e.NewValue != FrameStatisticsMode.None;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            LocalConfig?.Dispose();
        }

        public void Migrate(string path)
        {
            (Storage as PiouslyStorage)?.Migrate(Host.GetStorage(path));
        }
    }
}
