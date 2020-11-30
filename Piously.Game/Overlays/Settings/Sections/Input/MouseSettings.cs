using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using Piously.Game.Configuration;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Input;

namespace Piously.Game.Overlays.Settings.Sections.Input
{
    public class MouseSettings : SettingsSubsection
    {
        protected override string Header => "Mouse";

        private readonly BindableBool rawInputToggle = new BindableBool();
        private Bindable<double> sensitivityBindable = new BindableDouble();
        private Bindable<string> ignoredInputHandler;

        [BackgroundDependencyLoader]
        private void load(PiouslyConfigManager piouslyConfig, FrameworkConfigManager config)
        {
            var configSensitivity = config.GetBindable<double>(FrameworkSetting.CursorSensitivity);

            // use local bindable to avoid changing enabled state of game host's bindable.
            sensitivityBindable = configSensitivity.GetUnboundCopy();
            configSensitivity.BindValueChanged(val => sensitivityBindable.Value = val.NewValue);
            sensitivityBindable.BindValueChanged(val => configSensitivity.Value = val.NewValue);

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Raw input",
                    Current = rawInputToggle
                },
                new SensitivitySetting
                {
                    LabelText = "Cursor sensitivity",
                    Current = sensitivityBindable
                },
                new SettingsCheckbox
                {
                    LabelText = "Map absolute input to window",
                    Current = config.GetBindable<bool>(FrameworkSetting.MapAbsoluteInputToWindow)
                },
                new SettingsEnumDropdown<PiouslyConfineMouseMode>
                {
                    LabelText = "Confine mouse cursor to window",
                    Current = piouslyConfig.GetBindable<PiouslyConfineMouseMode>(PiouslySetting.ConfineMouseMode)
                },
            };

            if (RuntimeInfo.OS != RuntimeInfo.Platform.Windows)
            {
                rawInputToggle.Disabled = true;
                sensitivityBindable.Disabled = true;
            }
            else
            {
                rawInputToggle.ValueChanged += enabled =>
                {
                    // this is temporary until we support per-handler settings.
                    const string raw_mouse_handler = @"OsuTKRawMouseHandler";
                    const string standard_mouse_handler = @"OsuTKMouseHandler";

                    ignoredInputHandler.Value = enabled.NewValue ? standard_mouse_handler : raw_mouse_handler;
                };

                ignoredInputHandler = config.GetBindable<string>(FrameworkSetting.IgnoredInputHandlers);
                ignoredInputHandler.ValueChanged += handler =>
                {
                    bool raw = !handler.NewValue.Contains("Raw");
                    rawInputToggle.Value = raw;
                    sensitivityBindable.Disabled = !raw;
                };

                ignoredInputHandler.TriggerChange();
            }
        }

        private class SensitivitySetting : SettingsSlider<double, SensitivitySlider>
        {
            public SensitivitySetting()
            {
                KeyboardStep = 0.01f;
                TransferValueOnCommit = true;
            }
        }

        private class SensitivitySlider : PiouslySliderBar<double>
        {
            public override string TooltipText => Current.Disabled ? "enable raw input to adjust sensitivity" : $"{base.TooltipText}x";
        }
    }
}
