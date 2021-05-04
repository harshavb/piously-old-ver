using System;
using System.Diagnostics;
using osu.Framework.Configuration;
using osu.Framework.Configuration.Tracking;
using osu.Framework.Extensions;
using osu.Framework.Platform;
using Piously.Game.Input;
using Piously.Game.Input.Bindings;

namespace Piously.Game.Configuration
{
    public class PiouslyConfigManager : IniConfigManager<PiouslySetting>
    {
        protected override void InitialiseDefaults()
        {
            //Update
            SetDefault(PiouslySetting.Version, string.Empty);

            SetDefault(PiouslySetting.Scaling, ScalingMode.Off);

            SetDefault(PiouslySetting.ScalingSizeX, 0.8f, 0.2f, 1f);
            SetDefault(PiouslySetting.ScalingSizeY, 0.8f, 0.2f, 1f);

            SetDefault(PiouslySetting.ScalingPositionX, 0.5f, 0f, 1f);
            SetDefault(PiouslySetting.ScalingPositionY, 0.5f, 0f, 1f);

            SetDefault(PiouslySetting.UIScale, 0.9f, 0.5f, 1f, 0.01f);

            SetDefault(PiouslySetting.UIHoldActivationDelay, 200f, 0f, 500f, 50f);

            //Audio
            SetDefault(PiouslySetting.MenuVoice, true);
            SetDefault(PiouslySetting.MenuMusic, true);

            //Input
            SetDefault(PiouslySetting.MenuCursorSize, 1.0f, 0.5f, 2f, 0.01f);
            SetDefault(PiouslySetting.ConfineMouseMode, PiouslyConfineMouseMode.DuringGameplay);

            //Graphics
            SetDefault(PiouslySetting.ShowFpsDisplay, false);

            SetDefault(PiouslySetting.CursorRotation, true);

            SetDefault(PiouslySetting.MenuParallax, true);
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
