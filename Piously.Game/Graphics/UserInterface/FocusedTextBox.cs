using osuTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using Piously.Game.Input.Bindings;
using osuTK.Input;
using osu.Framework.Input.Bindings;

namespace Piously.Game.Graphics.UserInterface
{
    public class FocusedTextBox : PiouslyTextBox, IKeyBindingHandler<GlobalAction>
    {
        private bool focus;

        private bool allowImmediateFocus => host?.OnScreenKeyboardOverlapsGameWindow != true;

        public void TakeFocus()
        {
            if (allowImmediateFocus) GetContainingInputManager().ChangeFocus(this);
        }

        public bool HoldFocus
        {
            get => allowImmediateFocus && focus;
            set
            {
                focus = value;
                if (!focus && HasFocus)
                    base.KillFocus();
            }
        }

        [Resolved]
        private GameHost host { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            BackgroundUnfocused = new Color4(10, 10, 10, 255);
            BackgroundFocused = new Color4(10, 10, 10, 255);
        }

        // We may not be focused yet, but we need to handle keyboard input to be able to request focus
        public override bool HandleNonPositionalInput => HoldFocus || base.HandleNonPositionalInput;

        protected override void OnFocus(FocusEvent e)
        {
            base.OnFocus(e);
            BorderThickness = 0;
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!HasFocus) return false;

            if (e.Key == Key.Escape)
                return false; // disable the framework-level handling of escape key for conformity (we use GlobalAction.Back).

            return base.OnKeyDown(e);
        }

        public bool OnPressed(GlobalAction action)
        {
            if (!HasFocus) return false;

            if (action == GlobalAction.Back)
            {
                if (Text.Length > 0)
                {
                    Text = string.Empty;
                    return true;
                }
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }

        public override bool RequestsFocus => HoldFocus;
    }
}
