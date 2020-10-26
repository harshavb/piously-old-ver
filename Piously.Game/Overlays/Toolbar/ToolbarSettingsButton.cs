using osu.Framework.Allocation;
using Piously.Game.Input.Bindings;

namespace Piously.Game.Overlays.Toolbar
{
    public class ToolbarSettingsButton : ToolbarOverlayToggleButton
    {
        public ToolbarSettingsButton()
        {
            Width *= 1.4f;
            Hotkey = GlobalAction.ToggleSettings;
        }

        [BackgroundDependencyLoader(true)]
        private void load(SettingsOverlay settings)
        {
            StateContainer = settings;
        }
    }
}
