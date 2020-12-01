using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using System.ComponentModel;

namespace Piously.Game.Input.Bindings
{
    public class PiouslyKeyBindingContainer : KeyBindingContainer<GlobalAction>, IHandleGlobalKeyboardInput
    {
        private readonly Drawable handler;

        public PiouslyKeyBindingContainer(PiouslyGameBase game, KeyCombinationMatchingMode keyCombinationMatchingMode = KeyCombinationMatchingMode.Any, SimultaneousBindingMode simultaneousBindingMode = SimultaneousBindingMode.Unique)
            : base(simultaneousBindingMode, keyCombinationMatchingMode)
        {
            if (game is IKeyBindingHandler<GlobalAction>)
                handler = game;
        }

        public override IEnumerable<KeyBinding> DefaultKeyBindings => GlobalKeyBindings;
        public static IEnumerable<PiouslyKeyBinding> GlobalKeyBindings => new[]
        {
            new PiouslyKeyBinding(InputKey.Space, GlobalAction.Select),
            new PiouslyKeyBinding(InputKey.Enter, GlobalAction.Select),
            new PiouslyKeyBinding(InputKey.KeypadEnter, GlobalAction.Select),

            new PiouslyKeyBinding(InputKey.Escape, GlobalAction.Back),
            new PiouslyKeyBinding(InputKey.ExtraMouseButton1, GlobalAction.Back),

            new PiouslyKeyBinding(new[] { InputKey.Control, InputKey.O }, GlobalAction.ToggleSettings),
        };

        protected override IEnumerable<Drawable> KeyBindingInputQueue =>
            handler == null ? base.KeyBindingInputQueue : base.KeyBindingInputQueue.Prepend(handler);
    }

    public enum GlobalAction
    {
        [Description("Toggle settings")]
        ToggleSettings,

        [Description("Select")]
        Select,

        [Description("Back")]
        Back,
    }
}
