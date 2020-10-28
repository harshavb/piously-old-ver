using osu.Framework.Configuration;
using osu.Framework.Platform;
using osu.Framework.Testing;

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
    }
}
