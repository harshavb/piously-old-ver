using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using Piously.Game.Configuration;

namespace Piously.Game.Overlays.Settings.Sections.Graphics
{
    public class RendererSettings : SettingsSubsection
    {
        protected override string Header => "Renderer";

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager config, PiouslyConfigManager piouslyConfig)
        {
            // NOTE: Compatability mode omitted
            Children = new Drawable[]
            {
                new SettingsEnumDropdown<FrameSync>
                {
                    LabelText = "Frame limiter",
                    Current = config.GetBindable<FrameSync>(FrameworkSetting.FrameSync)
                },
                new SettingsEnumDropdown<ExecutionMode>
                {
                    LabelText = "Threading mode",
                    Current = config.GetBindable<ExecutionMode>(FrameworkSetting.ExecutionMode)
                },
                new SettingsCheckbox
                {
                    LabelText = "Show FPS",
                    Current = piouslyConfig.GetBindable<bool>(PiouslySetting.ShowFpsDisplay)
                },
            };
        }
    }
}
