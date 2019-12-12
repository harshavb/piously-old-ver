using System.Collections.Generic;

using osu.Framework.Input.Bindings;

namespace Piously.Game.Input
{
    public class PiouslyKeyBindingContainer : KeyBindingContainer<InputAction> // Specify the struct (enum) that PiouslyKeyBindingContainer will update
    {
        public override IEnumerable<KeyBinding> DefaultKeyBindings => new[] // Assings the following keys to the following InputActions
        {
            new KeyBinding(new[] { InputKey.A }, InputAction.Left),
            new KeyBinding(new[] { InputKey.W }, InputAction.Jump),
            new KeyBinding(new[] { InputKey.D }, InputAction.Right)
        };

        public PiouslyKeyBindingContainer(KeyCombinationMatchingMode keyCombinationMatchingMode = KeyCombinationMatchingMode.Any, SimultaneousBindingMode simultaneousBindingMode = SimultaneousBindingMode.All) : base(simultaneousBindingMode, keyCombinationMatchingMode) // Allows multiple keys to be pressed at once
        {

        }
    }
}
