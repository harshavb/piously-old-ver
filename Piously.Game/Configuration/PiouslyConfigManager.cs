using System;
using System.Diagnostics;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Configuration.Tracking;
using osu.Framework.Extensions;
using osu.Framework.Platform;
using osu.Framework.Testing;
using Piously.Game.Input;
using Piously.Game.Input.Bindings;
using Piously.Game.Overlays;
//using Piously.Game.Screens.Select;
//using Piously.Game.Screens.Select.Filter;

namespace Piously.Game.Configuration
{
    public class PiouslyConfigManager : IniConfigManager<PiouslySetting>
    {
        protected override void InitialiseDefaults()
        {
            //Update
            Set(PiouslySetting.Version, string.Empty);

            Set(PiouslySetting.Scaling, ScalingMode.Off);

            Set(PiouslySetting.ScalingSizeX, 0.8f, 0.2f, 1f);
            Set(PiouslySetting.ScalingSizeY, 0.8f, 0.2f, 1f);

            Set(PiouslySetting.ScalingPositionX, 0.5f, 0f, 1f);
            Set(PiouslySetting.ScalingPositionY, 0.5f, 0f, 1f);

            Set(PiouslySetting.UIScale, 1f, 0.8f, 1.6f, 0.01f);

            Set(PiouslySetting.UIHoldActivationDelay, 200f, 0f, 500f, 50f);

            //Audio
            Set(PiouslySetting.MenuVoice, true);
            Set(PiouslySetting.MenuMusic, true);

            //Input
            Set(PiouslySetting.MenuCursorSize, 1.0f, 0.5f, 2f, 0.01f);
            Set(PiouslySetting.ConfineMouseMode, PiouslyConfineMouseMode.DuringGameplay);

            //Graphics
            Set(PiouslySetting.ShowFpsDisplay, false);

            Set(PiouslySetting.CursorRotation, true);

            Set(PiouslySetting.MenuParallax, true);
        }

        public PiouslyConfigManager(Storage storage)
            : base(storage)
        {
            Migrate();
        }

        public void Migrate()
        {
            // arrives as 2020.123.0
            var rawVersion = Get<string>(PiouslySetting.Version);

            if (rawVersion.Length < 6)
                return;
        }

        public override TrackedSettings CreateTrackedSettings()
        {
            // these need to be assigned in normal game startup scenarios.
            Debug.Assert(LookupKeyBindings != null);

            return new TrackedSettings
            {
                new TrackedSetting<ScalingMode>(PiouslySetting.Scaling, m => new SettingDescription(m, "scaling", m.GetDescription())),
            };
        }

        public Func<GlobalAction, string> LookupKeyBindings { get; set; }
    }

    public enum PiouslySetting
    {
        Version,
        MenuMusic,
        MenuVoice,
        MenuParallax,
        ShowFpsDisplay,
        Scaling,
        ScalingPositionX,
        ScalingPositionY,
        ScalingSizeX,
        ScalingSizeY,
        UIScale,
        CursorRotation,
        UIHoldActivationDelay,
        ConfineMouseMode,
        MenuCursorSize,
    }
}
