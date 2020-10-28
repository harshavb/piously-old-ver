using osu.Framework.Input.Bindings;
using Piously.Game.Input.Bindings;
using Piously.Game.Overlays;

namespace Piously.Game.Screens.Menu
{
    public class ExitConfirmOverlay : HoldToConfirmOverlay, IKeyBindingHandler<GlobalAction>
    {
        protected override bool AllowMultipleFires => true;

        public void Abort() => AbortConfirm();

        public bool OnPressed(GlobalAction action)
        {
            if (action == GlobalAction.Back)
            {
                BeginConfirm();
                return true;
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
            if (action == GlobalAction.Back)
            {
                if (!Fired)
                    AbortConfirm();
            }
        }
    }
}
