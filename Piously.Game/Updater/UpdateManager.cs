using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using Piously.Game.Configuration;
using System.Threading.Tasks;

namespace Piously.Game.Updater
{
    public class UpdateManager : CompositeDrawable
    {
        /// <summary>
        /// Whether this UpdateManager should be or is capable of checking for updates.
        /// </summary>
        public bool CanCheckForUpdate => game.IsDeployedBuild &&
                                            // only implementations will actually check for updates.
                                            GetType() != typeof(UpdateManager);

        [Resolved]
        private PiouslyConfigManager config { get; set; }
        
        [Resolved]
        private PiouslyGameBase game { get; set; }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Schedule(() => Task.Run(CheckForUpdateAsync));

            var version = game.Version;

            // debug / local compilations will reset to a non-release string.
            // can be useful to check when an install has transitioned between release and otherwise (see OsuConfigManager's migrations).
            config.Set(PiouslySetting.Version, version);
        }

        private readonly object updateTaskLock = new object();

        private Task<bool> updateCheckTask;

        public async Task<bool> CheckForUpdateAsync()
        {
            if (!CanCheckForUpdate)
                return false;

            Task<bool> waitTask;

            lock (updateTaskLock)
                waitTask = (updateCheckTask ??= PerformUpdateCheck());

            bool hasUpdates = await waitTask;

            lock (updateTaskLock)
                updateCheckTask = null;

            return hasUpdates;
        }

        /// <summary>
        /// Performs an asynchronous check for application updates.
        /// </summary>
        /// <returns>Whether any update is waiting. May return true if an error occured (there is potentially an update available).</returns>
        protected virtual Task<bool> PerformUpdateCheck() => Task.FromResult(false);
    }
}
