using Piously.Game.Overlays;

namespace Piously.Game.Screens
{
    public class StartupScreen : PiouslyScreen
    {
        public override bool AllowBackButton => false;

        public override bool HideOverlaysOnEnter => true;

        public override bool CursorVisible => false;

        public override bool AllowRateAdjustments => false;

        protected override OverlayActivation InitialOverlayActivationMode => OverlayActivation.Disabled;
    }
}
