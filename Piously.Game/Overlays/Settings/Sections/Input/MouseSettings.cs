using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Input.Handlers.Mouse;
using osu.Framework.Platform;
using Piously.Game.Configuration;
using Piously.Game.Graphics.UserInterface;
using Piously.Game.Input;

namespace Piously.Game.Overlays.Settings.Sections.Input
{
    public class MouseSettings : SettingsSubsection
    {
        private MouseHandler mouseHandler;

        protected override string Header => "Mouse";

        private Bindable<double> handlerSensitivity;

        private Bindable<double> localSensitivity;

        private Bindable<WindowMode> windowMode;
        private SettingsEnumDropdown<PiouslyConfineMouseMode> confineMouseModeSetting;
        private Bindable<bool> relativeMode;

        [BackgroundDependencyLoader]
        private void load(GameHost host, PiouslyConfigManager piouslyConfig, FrameworkConfigManager config)
        {
            // I'm pretty sure this isn't how one should get the mouseHandler (use [Cached] for the host instead) but idk how to do that so :P
            mouseHandler = host.AvailableInputHandlers.OfType<MouseHandler>().FirstOrDefault();

            // use local bindable to avoid changing enabled state of game host's bindable.
            handlerSensitivity = host.AvailableInputHandlers.OfType<MouseHandler>().FirstOrDefault().Sensitivity.GetBoundCopy();
            localSensitivity = handlerSensitivity.GetUnboundCopy();

            relativeMode = mouseHandler.UseRelativeMode.GetBoundCopy();
            windowMode = config.GetBindable<WindowMode>(FrameworkSetting.WindowMode);

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "High precision mouse",
                    Current = relativeMode
                },
                new SensitivitySetting
                {
                    LabelText = "Cursor sensitivity",
                    Current = localSensitivity
                },
                confineMouseModeSetting = new SettingsEnumDropdown<PiouslyConfineMouseMode>
                {
                    LabelText = "Confine mouse cursor to window",
                    Current = piouslyConfig.GetBindable<PiouslyConfineMouseMode>(PiouslySetting.ConfineMouseMode)
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            relativeMode.BindValueChanged(relative => localSensitivity.Disabled = !relative.NewValue, true);

            handlerSensitivity.BindValueChanged(val =>
            {
                var disabled = localSensitivity.Disabled;

                localSensitivity.Disabled = false;
                localSensitivity.Value = val.NewValue;
                localSensitivity.Disabled = disabled;
            }, true);

            localSensitivity.BindValueChanged(val => handlerSensitivity.Value = val.NewValue);

            windowMode.BindValueChanged(mode =>
            {
                var isFullscreen = mode.NewValue == WindowMode.Fullscreen;

                if (isFullscreen)
                {
                    confineMouseModeSetting.Current.Disabled = true;
                }
                else
                {
                    confineMouseModeSetting.Current.Disabled = false;
                }
            }, true);
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
            public override string TooltipText => Current.Disabled ? "enable high precision mouse to adjust sensitivity" : $"{base.TooltipText}x";
        }
    }
}
