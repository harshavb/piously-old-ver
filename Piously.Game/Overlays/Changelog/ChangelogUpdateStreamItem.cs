using Humanizer;
using Piously.Game.Graphics;
using Piously.Game.Online.API.Requests.Responses;
using osuTK.Graphics;
namespace Piously.Game.Overlays.Changelog
{
    class ChangelogUpdateStreamItem : OverlayStreamItem<APIUpdateStream>
    {
        public ChangelogUpdateStreamItem(APIUpdateStream stream)
            : base(stream)
        {
            if (stream.IsFeatured)
                Width *= 2;
        }

        protected override string MainText => Value.DisplayName;

        protected override string AdditionalText => Value.LatestBuild.DisplayVersion;

        protected override string InfoText => Value.LatestBuild.Users > 0 ? $"{"user".ToQuantity(Value.LatestBuild.Users, "N0")} online" : null;

        protected override Color4 GetBarColour(PiouslyColor colors) => Value.Colour;
    }
}
