using osu.Framework.Allocation;
using Piously.Game.Input.Bindings;

namespace Piously.Game.Overlays.Toolbar
{
    public class ToolbarHomeButton : ToolbarButton
    {
        public ToolbarHomeButton()
        {
            Width *= 1.4f;
            Hotkey = GlobalAction.Home;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            TooltipMain = "home";
            TooltipSub = "return to the main menu";
            SetIcon("Icons/Hexacons/home");
        }
    }
}
