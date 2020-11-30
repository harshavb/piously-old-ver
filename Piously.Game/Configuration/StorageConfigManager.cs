using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace Piously.Game.Configuration
{
    public class StorageConfigManager : IniConfigManager<StorageConfig>
    {
        protected override string Filename => "storage.ini";

        public StorageConfigManager(Storage storage)
            : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            Set(StorageConfig.FullPath, string.Empty);
        }
    }

    public enum StorageConfig
    {
        FullPath,
    }
}
