using osu.Framework.Allocation;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;

namespace Piously.Game.Screens.Menu
{
    public class MainMenu : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(new MenuLogo());
        }
    }
}
