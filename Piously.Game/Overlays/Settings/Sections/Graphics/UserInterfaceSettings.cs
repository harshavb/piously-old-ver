using osu.Framework.Allocation;
using osu.Framework.Graphics;
using Piously.Game.Configuration;
using Piously.Game.Graphics.UserInterface;

namespace Piously.Game.Overlays.Settings.Sections.Graphics
{
    public class UserInterfaceSettings : SettingsSubsection
    {
        protected override string Header => "User Interface";

        [BackgroundDependencyLoader]
        private void load(PiouslyConfigManager config)
        {
            Children = new Drawable[]
            {
                //TO BE IMPLEMENTED
                /*new SettingsCheckbox
                {
                    LabelText = "Rotate cursor when dragging",
                    Current = config.GetBindable<bool>(PiouslySetting.CursorRotation)
                },*/
                new SettingsCheckbox
                {
                    LabelText = "Parallax",
                    Current = config.GetBindable<bool>(PiouslySetting.MenuParallax)
                },
            };
        }

        private class TimeSlider : PiouslySliderBar<float>
        {
            public override string TooltipText => Current.Value.ToString("N0") + "ms";
        }
    }
}
