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
                new SettingsCheckbox
                {
                    LabelText = "Show FPS",
                    Current = piouslyConfig.GetBindable<bool>(PiouslySetting.ShowFpsDisplay)
                },
            };
        }
    }
}
