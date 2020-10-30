using System.Threading.Tasks;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osu.Framework.Screens;
using Piously.Game.Configuration;
using Piously.Game.Overlays.Settings.Sections.Maintenance;
using Piously.Game.Updater;

namespace Piously.Game.Overlays.Settings.Sections.General
{
    public class UpdateSettings : SettingsSubsection
    {
        [Resolved(CanBeNull = true)]
        private UpdateManager updateManager { get; set; }

        protected override string Header => "Updates";

        private SettingsButton checkForUpdatesButton;

        [BackgroundDependencyLoader(true)]
        private void load(Storage storage, PiouslyConfigManager config, PiouslyGame game)
        {
            Add(new SettingsEnumDropdown<ReleaseStream>
            {
                LabelText = "Release stream",
                Current = config.GetBindable<ReleaseStream>(PiouslySetting.ReleaseStream),
            });

            if (updateManager?.CanCheckForUpdate == true)
            {
                Add(checkForUpdatesButton = new SettingsButton
                {
                    Text = "Check for updates",
                    Action = () =>
                    {
                        checkForUpdatesButton.Enabled.Value = false;
                        Task.Run(updateManager.CheckForUpdateAsync).ContinueWith(t => Schedule(() =>
                        {
                            checkForUpdatesButton.Enabled.Value = true;
                        }));
                    }
                });
            }

            if (RuntimeInfo.IsDesktop)
            {
                Add(new SettingsButton
                {
                    Text = "Open Piously folder",
                    Action = storage.OpenInNativeExplorer,
                });

                Add(new SettingsButton
                {
                    Text = "Change folder location...",
                    Action = () => game?.PerformFromScreen(menu => menu.Push(new MigrationSelectScreen()))
                });
            }
        }
    }
}
