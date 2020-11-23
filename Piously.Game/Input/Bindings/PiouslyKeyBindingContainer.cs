using System.Collections.Generic;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;

namespace Piously.Game.Input.Bindings
{
    public class PiouslyKeyBindingContainer : KeyBindingContainer<GlobalAction>, IHandleGlobalKeyboardInput
    {
        public PiouslyKeyBindingContainer(KeyCombinationMatchingMode keyCombinationMatchingMode = KeyCombinationMatchingMode.Any, SimultaneousBindingMode simultaneousBindingMode = SimultaneousBindingMode.All)
            : base(simultaneousBindingMode, keyCombinationMatchingMode)
        {
        }

        public override IEnumerable<KeyBinding> DefaultKeyBindings => GlobalKeyBindings;
        public static IEnumerable<KeyBinding> GlobalKeyBindings => new[]
        {
            new KeyBinding(InputKey.A, GlobalAction.TestAction1),
            new KeyBinding(new[] { InputKey.Control, InputKey.F }, GlobalAction.TestAction2)
        };
    }

    public enum GlobalAction
    {
        TestAction1,
        TestAction2,
    }
}
