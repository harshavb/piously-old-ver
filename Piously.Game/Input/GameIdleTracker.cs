using osu.Framework.Input;

namespace Piously.Game.Input
{
    public class GameIdleTracker : IdleTracker
    {
        private InputManager inputManager;

        public GameIdleTracker(int time)
            : base(time)
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            inputManager = GetContainingInputManager();
        }

        protected override bool AllowIdle => inputManager.FocusedDrawable == null;
    }
}
