using osu.Framework.Configuration;
using osu.Framework.Platform;
using osu.Framework.Testing;
using Piously.Game.Input;
using Piously.Game.Overlays;

namespace Piously.Game.Configuration
{
    [ExcludeFromDynamicCompile]
    public class PiouslyConfigManager : IniConfigManager<PiouslySetting>
    {
        protected override void InitialiseDefaults()
        {
            Set(PiouslySetting.MenuCursorSize, 1.0f, 0.5f, 2f, 0.01f);
            Set(PiouslySetting.GameplayCursorSize, 1.0f, 0.1f, 2f, 0.01f);
            Set(PiouslySetting.Version, string.Empty);
            Set(PiouslySetting.ChatDisplayHeight, ChatOverlay.DEFAULT_HEIGHT, 0.2f, 1f);

            // Online settings
            Set(PiouslySetting.Username, string.Empty);
            Set(PiouslySetting.Token, string.Empty);

            Set(PiouslySetting.SavePassword, false).ValueChanged += enabled =>
            {
                if (enabled.NewValue) Set(PiouslySetting.SaveUsername, true);
            };

            Set(PiouslySetting.SaveUsername, true).ValueChanged += enabled =>
            {
                if (!enabled.NewValue) Set(PiouslySetting.SavePassword, false);
            };

            Set(PiouslySetting.ExternalLinkWarning, true);

            // Audio
            Set(PiouslySetting.VolumeInactive, 0.25, 0, 1, 0.01);
            Set(PiouslySetting.MenuVoice, true);
            Set(PiouslySetting.MenuMusic, true);

            // Input
            Set(PiouslySetting.MenuCursorSize, 1.0f, 0.5f, 2f, 0.01f);
            Set(PiouslySetting.GameplayCursorSize, 1.0f, 0.1f, 2f, 0.01f);
            Set(PiouslySetting.AutoCursorSize, false);

            Set(PiouslySetting.ConfineMouseMode, PiouslyConfineMouseMode.DuringGameplay);

            // Graphics
            Set(PiouslySetting.ShowFpsDisplay, false);

            Set(PiouslySetting.CursorRotation, true);

            Set(PiouslySetting.MenuParallax, true);

            // Update
            Set(PiouslySetting.ReleaseStream, ReleaseStream.Piously);

            Set(PiouslySetting.Version, string.Empty);

            Set(PiouslySetting.Scaling, ScalingMode.Off);

            Set(PiouslySetting.ScalingSizeX, 0.8f, 0.2f, 1f);
            Set(PiouslySetting.ScalingSizeY, 0.8f, 0.2f, 1f);

            Set(PiouslySetting.ScalingPositionX, 0.5f, 0f, 1f);
            Set(PiouslySetting.ScalingPositionY, 0.5f, 0f, 1f);

            Set(PiouslySetting.UIScale, 1f, 0.8f, 1.6f, 0.01f);

            Set(PiouslySetting.UIHoldActivationDelay, 200f, 0f, 500f, 50f);

            Set(PiouslySetting.IntroSequence, IntroSequence.Triangles);
        }

        public PiouslyConfigManager(Storage storage)
            : base(storage)
        {
            Migrate();
        }

        public void Migrate()
        {
            var rawVersion = Get<string>(PiouslySetting.Version);
        }

    }
    public enum PiouslySetting
    {
        Token,
        MenuCursorSize,
        GameplayCursorSize,
        AutoCursorSize,
        Version,
        Username,
        SaveUsername,
        SavePassword,
        CursorRotation,
        ShowFpsDisplay,
        MenuBackgroundSource,
        IntroSequence,
        UIScale,
        Scaling,
        ScalingSizeX,
        ScalingSizeY,
        ScalingPositionX,
        ScalingPositionY,
        ChatDisplayHeight,
        ExternalLinkWarning,
        MenuParallax,
        UIHoldActivationDelay,
        VolumeInactive,
        MenuVoice,
        MenuMusic,
        ConfineMouseMode,
        ReleaseStream,
    }
}
