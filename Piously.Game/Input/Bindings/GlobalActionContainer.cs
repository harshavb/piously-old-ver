using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;

namespace Piously.Game.Input.Bindings
{
    public class GlobalActionContainer : DatabasedKeyBindingContainer<GlobalAction>, IHandleGlobalKeyboardInput
    {
        private readonly Drawable handler;

        public GlobalActionContainer(PiouslyGameBase game)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<GlobalAction>)
                handler = game;
        }

        public override IEnumerable<KeyBinding> DefaultKeyBindings => GlobalKeyBindings.Concat(InGameKeyBindings);

        public IEnumerable<KeyBinding> GlobalKeyBindings => new[]
        {
            new KeyBinding(InputKey.F8, GlobalAction.ToggleChat),
            new KeyBinding(InputKey.F9, GlobalAction.ToggleSocial),

            new KeyBinding(new[] { InputKey.Control, InputKey.Alt, InputKey.R }, GlobalAction.ResetInputSettings),
            new KeyBinding(new[] { InputKey.Control, InputKey.T }, GlobalAction.ToggleToolbar),
            new KeyBinding(new[] { InputKey.Control, InputKey.O }, GlobalAction.ToggleSettings),
            
            new KeyBinding(InputKey.Escape, GlobalAction.Back),
            new KeyBinding(InputKey.Space, GlobalAction.Select),
            new KeyBinding(InputKey.Enter, GlobalAction.Select),
            new KeyBinding(InputKey.KeypadEnter, GlobalAction.Select),

            new KeyBinding(new[] { InputKey.Alt, InputKey.Home }, GlobalAction.Home),
        };

        public IEnumerable<KeyBinding> InGameKeyBindings => new[]
        {
            new KeyBinding(InputKey.Escape, GlobalAction.ToggleExit),
        };

        protected override IEnumerable<Drawable> KeyBindingInputQueue =>
            handler == null ? base.KeyBindingInputQueue : base.KeyBindingInputQueue.Prepend(handler);
    }

    public enum GlobalAction
    {
        [Description("Toggle chat overlay")]
        ToggleChat,

        [Description("Toggle social overlay")]
        ToggleSocial,

        [Description("Reset input settings")]
        ResetInputSettings,

        [Description("Toggle toolbar")]
        ToggleToolbar,

        [Description("Toggle settings")]
        ToggleSettings,

        [Description("Back")]
        Back,

        [Description("Select")]
        Select,

        [Description("Home")]
        Home,

        // In-Game Keybindings
        [Description("Toggle exit")]
        ToggleExit,
    }
}
