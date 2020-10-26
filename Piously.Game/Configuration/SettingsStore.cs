using System;
using System.Collections.Generic;
using System.Linq;
using Piously.Game.Database;

namespace Piously.Game.Configuration
{
    public class SettingsStore : DatabaseBackedStore
    {
        public event Action SettingChanged;

        public SettingsStore(DatabaseContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        /// <summary>
        /// Retrieve <see cref="DatabasedSetting"/>s.
        /// </summary>
        /// <returns></returns>
        public List<DatabasedSetting> Query() =>
            ContextFactory.Get().DatabasedSetting.ToList();

        public void Update(DatabasedSetting setting)
        {
            using (ContextFactory.GetForWrite())
            {
                var newValue = setting.Value;
                Refresh(ref setting);
                setting.Value = newValue;
            }

            SettingChanged?.Invoke();
        }

        public void Delete(DatabasedSetting setting)
        {
            using (var usage = ContextFactory.GetForWrite())
                usage.Context.Remove(setting);
        }
    }
}
