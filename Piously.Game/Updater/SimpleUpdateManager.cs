using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Platform;
using Piously.Game.Online.API;

namespace Piously.Game.Updater
{
    class SimpleUpdateManager : UpdateManager
    {
        private string version;

        [Resolved]
        private GameHost host { get; set; }

        [BackgroundDependencyLoader]
        private void load(PiouslyGameBase game)
        {
            version = game.Version;
        }

        protected override async Task<bool> PerformUpdateCheck()
        {
            try
            {
                var releases = new PiouslyJsonWebRequest<GitHubRelease>("https://api.github.com/repos/harshavb/Piously/releases/latest");

                await releases.PerformAsync();

                var latest = releases.ResponseObject;

                if (latest.TagName != version)
                {
                    host.OpenUrlExternally(getBestUrl(latest));
                    return true;
                }
            }
            catch
            {
                // we shouldn't crash on a web failure. or any failure for the matter.
                return true;
            }

            return false;
        }

        private string getBestUrl(GitHubRelease release)
        {
            GitHubAsset bestAsset = null;
            switch (RuntimeInfo.OS)
            {
                case RuntimeInfo.Platform.Windows:
                    bestAsset = release.Assets?.Find(f => f.Name.EndsWith(".exe", StringComparison.Ordinal));
                    break;

                case RuntimeInfo.Platform.MacOsx:
                    bestAsset = release.Assets?.Find(f => f.Name.EndsWith(".app.zip", StringComparison.Ordinal));
                    break;

                case RuntimeInfo.Platform.Linux:
                    bestAsset = release.Assets?.Find(f => f.Name.EndsWith(".AppImage", StringComparison.Ordinal));
                    break;
            }

            return bestAsset?.BrowserDownloadUrl ?? release.HtmlUrl;
        }

        public class GitHubRelease
        {
            [JsonProperty("html_url")]
            public string HtmlUrl { get; set; }

            [JsonProperty("tag_name")]
            public string TagName { get; set; }

            [JsonProperty("assets")]
            public List<GitHubAsset> Assets { get; set; }
        }

        public class GitHubAsset
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }
        }
    }
}
