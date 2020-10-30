using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Configuration.Tracking;
using osu.Framework.Graphics;
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
        private void load(/*TextureStore store, AudioManager audio,*/)
        {
            Resources.AddStore(new DllResourceStore(@"Piously.dll"));

            AddFont(Resources, @"Resources/Fonts/InkFree-Bold");
            AddFont(Resources, @"Resources/Fonts/InkFree");
            AddFont(Resources, @"Resources/Fonts/InkFree-Italic");
            AddFont(Resources, @"Resources/Fonts/InkFree-BoldItalic");
        }
    }
}
