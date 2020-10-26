using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Input.Bindings;
using osu.Framework.Platform;
using Piously.Game.Database;
using Piously.Game.Input.Bindings;

namespace Piously.Game.Input
{
    public class KeyBindingStore : DatabaseBackedStore
    {
        public event Action KeyBindingChanged;

        public KeyBindingStore(DatabaseContextFactory contextFactory, Storage storage = null)
            : base(contextFactory, storage)
        {
        }

        public void Register(KeyBindingContainer manager) => insertDefaults(manager.DefaultKeyBindings);

        private void insertDefaults(IEnumerable<KeyBinding> defaults)
        {
            using (var usage = ContextFactory.GetForWrite())
            {
                // compare counts in database vs defaults
                foreach (var group in defaults.GroupBy(k => k.Action))
                {
                    int count = Query().Count(k => (int)k.Action == (int)group.Key);
                    int aimCount = group.Count();

                    if (aimCount <= count)
                        continue;

                    foreach (var insertable in group.Skip(count).Take(aimCount - count))
                    {
                        // insert any defaults which are missing.
                        usage.Context.DatabasedKeyBinding.Add(new DatabasedKeyBinding
                        {
                            KeyCombination = insertable.KeyCombination,
                            Action = insertable.Action,
                        });

                        // required to ensure stable insert order (https://github.com/dotnet/efcore/issues/11686)
                        usage.Context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve <see cref="DatabasedKeyBinding"/>s for a specified ruleset/variant content.
        /// </summary>
        /// <param name="rulesetId">The ruleset's internal ID.</param>
        /// <param name="variant">An optional variant.</param>
        /// <returns></returns>
        public List<DatabasedKeyBinding> Query() =>
            ContextFactory.Get().DatabasedKeyBinding.ToList();

        public void Update(KeyBinding keyBinding)
        {
            using (ContextFactory.GetForWrite())
            {
                var dbKeyBinding = (DatabasedKeyBinding)keyBinding;
                Refresh(ref dbKeyBinding);

                if (dbKeyBinding.KeyCombination.Equals(keyBinding.KeyCombination))
                    return;

                dbKeyBinding.KeyCombination = keyBinding.KeyCombination;
            }

            KeyBindingChanged?.Invoke();
        }
    }
}
