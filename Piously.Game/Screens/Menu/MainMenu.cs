using osu.Framework.Allocation;
using osu.Framework.Screens;

namespace Piously.Game.Screens.Menu
{
    public class MainMenu : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new PiouslyLogo());
        }
    }
}
