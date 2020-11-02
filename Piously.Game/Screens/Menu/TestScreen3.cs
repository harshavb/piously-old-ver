using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using Piously.Game.Input.Bindings;
using osuTK.Graphics;

namespace Piously.Game.Screens.Menu
{
    public class TestScreen3 : Screen
    {

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new PiouslyLogo());
        }
    }
}
