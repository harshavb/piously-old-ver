using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Platform;
using Piously.Game.Input.Bindings;
using osu.Framework.Input.Bindings;

namespace Piously.Game.Input
{
    public class KeyBindingStore
    {
        public event Action KeyBindingChanged;

        protected List<PiouslyKeyBinding> keyBindings = new List<PiouslyKeyBinding>();

        protected readonly Storage Storage;

        public KeyBindingStore(Storage storage = null)
        {
            Storage = storage;
        }

        public void Register(PiouslyKeyBindingContainer manager) => insertDefaults(manager.DefaultKeyBindings);

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
                    Update(new PiouslyKeyBinding(insertable.KeyCombination, (GlobalAction)insertable.Action));
                }
            }
        }

        /// <summary>
        /// Retrieve <see cref="DatabasedKeyBinding"/>s.
        /// </summary>
        /// <returns></returns>
        public List<PiouslyKeyBinding> Query() => keyBindings;

        public void Update(PiouslyKeyBinding keyBinding)
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
            keyBindings.Sort();
            //WRITE TO FILE
            KeyBindingChanged?.Invoke();
        }
    }
}
