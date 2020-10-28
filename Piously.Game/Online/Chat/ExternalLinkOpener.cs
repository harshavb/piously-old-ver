using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using Piously.Game.Configuration;
using Piously.Game.Overlays;
using Piously.Game.Overlays.Chat;

namespace Piously.Game.Online.Chat
{
    public class ExternalLinkOpener : Component
    {
        [Resolved]
        private GameHost host { get; set; }

        [Resolved(CanBeNull = true)]
        private DialogOverlay dialogOverlay { get; set; }

        private Bindable<bool> externalLinkWarning;

        [BackgroundDependencyLoader(true)]
        private void load(PiouslyConfigManager config)
        {
            externalLinkWarning = config.GetBindable<bool>(PiouslySetting.ExternalLinkWarning);
        }

        public void OpenUrlExternally(string url)
        {
            if (externalLinkWarning.Value)
                dialogOverlay.Push(new ExternalLinkDialog(url, () => host.OpenUrlExternally(url)));
            else
                host.OpenUrlExternally(url);
        }
    }
}
