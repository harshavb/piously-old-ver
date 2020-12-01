using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Platform;
using Piously.Game.Input.Bindings;
using osu.Framework.Input.Bindings;
using osu.Framework.IO.Stores;

namespace Piously.Game.Input
{
    public class KeyBindingStore
    {
        public event Action KeyBindingChanged;

        protected List<KeyBinding> keyBindings = new List<KeyBinding>();

        protected readonly Storage Storage;

        public KeyBindingStore(Storage storage = null)
        {
            Storage = storage;
            insertDefaults(new PiouslyKeyBindingContainer().DefaultKeyBindings);
        }

        public void Register(KeyBindingContainer manager) => insertDefaults(manager.DefaultKeyBindings);

        /// <summary>
        /// Retrieve all user-defined key combinations (in a format that can be displayed) for a specific action.
        /// </summary>
        /// <param name="globalAction">The action to lookup.</param>
        /// <returns>A set of display strings for all the user's key configuration for the action.</returns>
        public IEnumerable<string> GetReadableKeyCombinationsFor(GlobalAction globalAction)
        {
            foreach (var action in Query().Where(b => (GlobalAction)b.Action == globalAction))
            {
                string str = action.KeyCombination.ReadableString();

                // even if found, the readable string may be empty for an unbound action.
                if (str.Length > 0)
                    yield return str;
            }
        }

        private void insertDefaults(IEnumerable<KeyBinding> defaults)
        {
            foreach (var group in defaults.GroupBy(k => k.Action))
            {
                int count = Query().Count(k => (int)k.Action == (int)group.Key);
                int aimCount = group.Count();

                if (aimCount <= count)
                    return;

                foreach(var insertable in group.Skip(count).Take(aimCount - count))
                {
                    keyBindings.Add(new KeyBinding(insertable.KeyCombination, insertable.Action));
                }
                //SOMEHOW store keyBindings to a file.
            }
        }

        /// <summary>
        /// Retrieve <see cref="DatabasedKeyBinding"/>s.
        /// </summary>
        /// <returns></returns>
        public List<KeyBinding> Query() => keyBindings;

        public void Update(KeyBinding keyBinding)
        {
            foreach(var group in keyBindings.GroupBy(k => k.Action))
            {
                if(Query().Count(k => k.Action == keyBinding.Action) > 0)
                {
                    if(!(Query().Count(k => k.KeyCombination.Equals(keyBinding.KeyCombination)) > 0))
                    {
                        int value = keyBindings.FindIndex(k => k.KeyCombination.Equals(keyBinding.KeyCombination));
                        keyBindings.RemoveAt(value);
                        keyBindings.Insert(value, keyBinding);
                    }
                }
            }
            KeyBindingChanged?.Invoke();
        }
    }
}
