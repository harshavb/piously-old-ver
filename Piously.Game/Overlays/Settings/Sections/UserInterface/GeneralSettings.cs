using osu.Framework.Allocation;
using osu.Framework.Graphics;
using Piously.Game.Configuration;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings.Sections.UserInterface
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "General";

        [BackgroundDependencyLoader]
        private void load(PiouslyConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Rotate cursor when dragging",
                    Current = config.GetBindable<bool>(PiouslySetting.CursorRotation)
                },
                new SettingsSlider<float, SizeSlider>
                {
                    LabelText = "Menu cursor size",
                    Current = config.GetBindable<float>(PiouslySetting.MenuCursorSize),
                    KeyboardStep = 0.01f
                },
                new SettingsCheckbox
                {
                    LabelText = "Parallax",
                    Current = config.GetBindable<bool>(PiouslySetting.MenuParallax)
                },
                new SettingsSlider<float, TimeSlider>
                {
                    LabelText = "Hold-to-confirm activation time",
                    Current = config.GetBindable<float>(PiouslySetting.UIHoldActivationDelay),
                    KeyboardStep = 50
                },
            };
        }

        private class TimeSlider : PiouslySliderBar<float>
        {
            public override string TooltipText => Current.Value.ToString("N0") + "ms";
        }
    }
}
