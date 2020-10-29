using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Win32;
using Piously.Desktop.Overlays;
using osu.Framework.Platform;
using Piously.Game;
using Piously.Desktop.Updater;
using osu.Framework;
using osu.Framework.Logging;
using osu.Framework.Screens;
using Piously.Game.Screens.Menu;
using Piously.Game.Updater;

namespace Piously.Desktop
{
    internal class PiouslyGameDesktop : PiouslyGame
    {
        private readonly bool noVersionOverlay;
        private VersionManager versionManager;

        public PiouslyGameDesktop(string[] args = null)
            : base(args)
        {
            noVersionOverlay = args?.Any(a => a == "--no-version-overlay") ?? false;
        }

        public override Storage GetStorageForStableInstall()
        {
            try
            {
                if (Host is DesktopGameHost desktopHost)
                {
                    string stablePath = getStableInstallPath();
                    if (!string.IsNullOrEmpty(stablePath))
                        return new DesktopStorage(stablePath, desktopHost);
                }
            }
            catch (Exception)
            {
                Logger.Log("Could not find a stable install", LoggingTarget.Runtime, LogLevel.Important);
            }

            return null;
        }

        private string getStableInstallPath()
        {
            static bool checkExists(string p) => Directory.Exists(Path.Combine(p, "Songs"));

            string stableInstallPath;

            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey("Piously"))
                    stableInstallPath = key?.OpenSubKey(@"shell\open\command")?.GetValue(string.Empty).ToString()?.Split('"')[1].Replace("Piously.exe", "");

                if (checkExists(stableInstallPath))
                    return stableInstallPath;
            }
            catch
            {
            }

            stableInstallPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"osu!");
            if (checkExists(stableInstallPath))
                return stableInstallPath;

            stableInstallPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".osu");
            if (checkExists(stableInstallPath))
                return stableInstallPath;

            return null;
        }

        protected override UpdateManager CreateUpdateManager()
        {
            switch (RuntimeInfo.OS)
            {
                case RuntimeInfo.Platform.Windows:
                    return new SquirrelUpdateManager();

                default:
                    return new SimpleUpdateManager();
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            if (!noVersionOverlay)
                LoadComponentAsync(versionManager = new VersionManager { Depth = int.MinValue }, Add);

            LoadComponentAsync(new DiscordRichPresence(), Add);
        }

        protected override void ScreenChanged(IScreen lastScreen, IScreen newScreen)
        {
            base.ScreenChanged(lastScreen, newScreen);

            switch (newScreen)
            {
                case IntroScreen _:
                case MainMenu _:
                    versionManager?.Show();
                    break;

                default:
                    versionManager?.Hide();
                    break;
            }
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType(), "Piously.ico");

            switch (host.Window)
            {
                // Legacy osuTK DesktopGameWindow
                case DesktopGameWindow desktopGameWindow:
                    desktopGameWindow.CursorState |= CursorState.Hidden;
                    desktopGameWindow.SetIconFromStream(iconStream);
                    desktopGameWindow.Title = Name;
                    desktopGameWindow.FileDrop += (_, e) => fileDrop(e.FileNames);
                    break;

                // SDL2 DesktopWindow
                case DesktopWindow desktopWindow:
                    desktopWindow.CursorState.Value |= CursorState.Hidden;
                    desktopWindow.SetIconFromStream(iconStream);
                    desktopWindow.Title = Name;
                    desktopWindow.DragDrop += f => fileDrop(new[] { f });
                    break;
            }
        }

        private void fileDrop(string[] filePaths)
        {
            var firstExtension = Path.GetExtension(filePaths.First());

            if (filePaths.Any(f => Path.GetExtension(f) != firstExtension)) return;

            Task.Factory.StartNew(() => Import(filePaths), TaskCreationOptions.LongRunning);
        }
    }
}
