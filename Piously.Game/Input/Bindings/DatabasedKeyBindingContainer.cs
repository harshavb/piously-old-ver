using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input.Bindings;
using System.Linq;

namespace Piously.Game.Input.Bindings
{
    public class DatabasedKeyBindingContainer<T> : KeyBindingContainer<T>
        where T : struct
    {
        private KeyBindingStore store;

        private GlobalActionContainer keybinds;

        public override IEnumerable<KeyBinding> DefaultKeyBindings => keybinds.DefaultKeyBindings;

        public DatabasedKeyBindingContainer(SimultaneousBindingMode simultaneousMode = SimultaneousBindingMode.None, KeyCombinationMatchingMode matchingMode = KeyCombinationMatchingMode.Any)
            : base(simultaneousMode, matchingMode)
        {

        }

        [BackgroundDependencyLoader]
        private void load(KeyBindingStore keyBindings)
        {
            store = keyBindings;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            store.KeyBindingChanged += ReloadMappings;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (store != null)
                store.KeyBindingChanged -= ReloadMappings;
        }

        //TO BE IMPLEMENTED
        protected override void ReloadMappings()
        {
            //if (store != null) KeyBindings = store.Query().ToList();
            //else KeyBindings = DefaultKeyBindings;
        }
    }
}
