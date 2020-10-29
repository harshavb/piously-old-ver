using System;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Logging;
using Squirrel;
using LogLevel = Splat.LogLevel;

namespace Piously.Desktop.Updater
{
    public class SquirrelUpdateManager : Piously.Game.Updater.UpdateManager
    {
        private UpdateManager updateManager;

        public Task PrepareUpdateAsync() => UpdateManager.RestartAppWhenExited();

        private static readonly Logger logger = Logger.GetLogger("updater");

        /// <summary>
        /// Whether an update has been downloaded but not yet applied.
        /// </summary>
        private bool updatePending;

        [BackgroundDependencyLoader]
        private void load()
        {
            Splat.Locator.CurrentMutable.Register(() => new SquirrelLogger(), typeof(Splat.ILogger));
        }

        protected override async Task<bool> PerformUpdateCheck() => await checkForUpdateAsync();

        private async Task<bool> checkForUpdateAsync(bool useDeltaPatching = true)
        {
            // should we schedule a retry on completion of this check?
            bool scheduleRecheck = true;

            try
            {
                updateManager ??= await UpdateManager.GitHubUpdateManager(@"https://github.com/harshavb/Piously", @"Piously", null, null, true);

                var info = await updateManager.CheckForUpdate(!useDeltaPatching);

                try
                {
                    updatePending = true;
                }
                catch (Exception e)
                {
                    if (useDeltaPatching)
                    {
                        logger.Add(@"delta patching failed; will attempt full download!");

                        // could fail if deltas are unavailable for full update path (https://github.com/Squirrel/Squirrel.Windows/issues/959)
                        // try again without deltas.
                        await checkForUpdateAsync(false);
                        scheduleRecheck = false;
                    }
                    else
                    {
                        Logger.Error(e, @"update failed!");
                    }
                }
            }
            catch (Exception)
            {
                // we'll ignore this and retry later. can be triggered by no internet connection or thread abortion.
            }
            finally
            {
                if (scheduleRecheck)
                {
                    // check again in 30 minutes.
                    Scheduler.AddDelayed(async () => await checkForUpdateAsync(), 60000 * 30);
                }
            }

            return true;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            updateManager?.Dispose();
        }
        private class SquirrelLogger : Splat.ILogger, IDisposable
        {
            public LogLevel Level { get; set; } = LogLevel.Info;

            public void Write(string message, LogLevel logLevel)
            {
                if (logLevel < Level)
                    return;

                logger.Add(message);
            }

            public void Dispose()
            {
            }
        }
    }
}
