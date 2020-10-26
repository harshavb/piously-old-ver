using System;
using osu.Framework.Configuration;

namespace Piously.Game.Configuration
{
    public class InMemoryConfigManager<TLookup> : ConfigManager<TLookup>
        where TLookup : struct, Enum
    {
        public InMemoryConfigManager()
        {
            InitialiseDefaults();
        }

        protected override void PerformLoad()
        {
        }

        protected override bool PerformSave() => true;
    }
}
