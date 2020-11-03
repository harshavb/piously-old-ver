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

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(PiouslyGameBase).Assembly), "Resources"));

            AddFont(Resources, @"Resources/Fonts/InkFree-Bold");
            AddFont(Resources, @"Resources/Fonts/InkFree");
            AddFont(Resources, @"Resources/Fonts/InkFree-Italic");
            AddFont(Resources, @"Resources/Fonts/InkFree-BoldItalic");
        }
    }
}
