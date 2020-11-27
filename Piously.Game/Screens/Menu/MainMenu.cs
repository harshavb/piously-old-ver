using osu.Framework.Allocation;
using osu.Framework.Screens;
using Piously.Game.Graphics.Containers;
using Piously.Game.Graphics.Overlays;

namespace Piously.Game.Screens.Menu
{
    public class MainMenu : Screen
    {
        [BackgroundDependencyLoader]
        private void load(MenuLogo menuLogo, SettingsOverlay settings)
        {
            AddInternal(menuLogo);

            menuLogo.menuButtons.OnSettings = () => settings?.ToggleVisibility();
        }
    }
}
