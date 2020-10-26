using System.ComponentModel;

namespace Piously.Game.Configuration
{
    public enum ScalingMode
    {
        Off,
        Everything,

        [Description("Excluding overlays")]
        ExcludeOverlays,
        Gameplay,
    }
}
