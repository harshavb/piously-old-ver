using Piously.Game.Online.API.Requests.Responses;

namespace Piously.Game.Overlays.Changelog
{
    public class ChangelogUpdateStreamControl : OverlayStreamControl<APIUpdateStream>
    {
        protected override OverlayStreamItem<APIUpdateStream> CreateStreamItem(APIUpdateStream value) => new ChangelogUpdateStreamItem(value);
    }
}
