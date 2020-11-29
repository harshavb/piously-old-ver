using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace Piously.Game
{
    //This class does not load UI, just everything else
    public class PiouslyGameBase : osu.Framework.Game
    {
        public PiouslyGameBase()
        {
            Name = @"Piously";
        }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(PiouslyGameBase).Assembly), "Resources"));

            dependencies.CacheAs(this);

            AddFont(Resources, @"Fonts/InkFree-Bold");
            AddFont(Resources, @"Fonts/InkFree");
            AddFont(Resources, @"Fonts/InkFree-Italic");
            AddFont(Resources, @"Fonts/InkFree-BoldItalic");
        }
    }
}
