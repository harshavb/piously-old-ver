﻿using System.Collections.Generic;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using System.ComponentModel;

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
            new KeyBinding(InputKey.Space, GlobalAction.Select),
            new KeyBinding(InputKey.Enter, GlobalAction.Select),
            new KeyBinding(InputKey.KeypadEnter, GlobalAction.Select),

            new KeyBinding(InputKey.Escape, GlobalAction.Back),
            new KeyBinding(InputKey.ExtraMouseButton1, GlobalAction.Back),

            new KeyBinding(new[] { InputKey.Control, InputKey.O }, GlobalAction.ToggleSettings),
        };
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
