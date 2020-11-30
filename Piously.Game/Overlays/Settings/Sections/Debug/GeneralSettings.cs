using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;

namespace Piously.Game.Overlays.Settings.Sections.Debug
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "General";

        [BackgroundDependencyLoader]
        private void load(FrameworkDebugConfigManager config, FrameworkConfigManager frameworkConfig)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Show log overlay",
                    Current = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay)
                },
                /*new SettingsCheckbox
                {
                    LabelText = "Bypass front-to-back render pass",
                    Current = config.GetBindable<bool>(DebugSetting.BypassFrontToBackPass)
                }*/
            };
        }
    }
}
