using osu.Framework.Allocation;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Overlays;

namespace Piously.Game.Screens.Menu
{
    public class MainMenu : Screen
    {
        MenuLogo menuLogo;

        [BackgroundDependencyLoader(true)]
        private void load(SettingsOverlay settings)
        {
            AddInternal(menuLogo = new MenuLogo());

            menuLogo.menuButtons.OnSettings = () => settings?.ToggleVisibility();
        }
    }
}
