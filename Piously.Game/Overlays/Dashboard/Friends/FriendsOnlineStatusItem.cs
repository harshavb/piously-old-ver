using System;
using Piously.Game.Graphics;
using osuTK.Graphics;

namespace Piously.Game.Overlays.Dashboard.Friends
{
    public class FriendsOnlineStatusItem : OverlayStreamItem<FriendStream>
    {
        public FriendsOnlineStatusItem(FriendStream value)
            : base(value)
        {
        }

        protected override string MainText => Value.Status.ToString();

        protected override string AdditionalText => Value.Count.ToString();

        protected override Color4 GetBarColour(PiouslyColor colors)
        {
            switch (Value.Status)
            {
                case OnlineStatus.All:
                    return Color4.White;

                case OnlineStatus.Online:
                    return colors.GreenLight;

                case OnlineStatus.Offline:
                    return Color4.Black;

                default:
                    throw new ArgumentException($@"{Value.Status} status does not provide a colour in {nameof(GetBarColour)}.");
            }
        }
    }
}
