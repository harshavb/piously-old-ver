using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Input;
using Piously.Game.Configuration;

namespace Piously.Game.Input
{
    /// <summary>
    /// Connects <see cref="PiouslySetting.ConfineMouseMode"/> with <see cref="FrameworkSetting.ConfineMouseMode"/>.
    /// If <see cref="PiouslyGame.LocalUserPlaying"/> is true, we should also confine the mouse cursor if it has been
    /// requested with <see cref="PiouslyConfineMouseMode.DuringGameplay"/>.
    /// </summary>
    public class ConfineMouseTracker : Component
    {
        private Bindable<ConfineMouseMode> frameworkConfineMode;
        private Bindable<PiouslyConfineMouseMode> PiouslyConfineMode;
        private IBindable<bool> localUserPlaying;

        [BackgroundDependencyLoader]
        private void load(PiouslyGame game, FrameworkConfigManager frameworkConfigManager, PiouslyConfigManager piouslyConfigManager)
        {
            frameworkConfineMode = frameworkConfigManager.GetBindable<ConfineMouseMode>(FrameworkSetting.ConfineMouseMode);
            PiouslyConfineMode = piouslyConfigManager.GetBindable<PiouslyConfineMouseMode>(PiouslySetting.ConfineMouseMode);
            localUserPlaying = game.LocalUserPlaying.GetBoundCopy();

            PiouslyConfineMode.ValueChanged += _ => updateConfineMode();
            localUserPlaying.BindValueChanged(_ => updateConfineMode(), true);
        }

        private void updateConfineMode()
        {
            // confine mode is unavailable on some platforms
            if (frameworkConfineMode.Disabled)
                return;

            switch (PiouslyConfineMode.Value)
            {
                case PiouslyConfineMouseMode.Never:
                    frameworkConfineMode.Value = ConfineMouseMode.Never;
                    break;

                case PiouslyConfineMouseMode.Fullscreen:
                    frameworkConfineMode.Value = ConfineMouseMode.Fullscreen;
                    break;

                case PiouslyConfineMouseMode.DuringGameplay:
                    frameworkConfineMode.Value = localUserPlaying.Value ? ConfineMouseMode.Always : ConfineMouseMode.Never;
                    break;

                case PiouslyConfineMouseMode.Always:
                    frameworkConfineMode.Value = ConfineMouseMode.Always;
                    break;
            }
        }
    }
}
