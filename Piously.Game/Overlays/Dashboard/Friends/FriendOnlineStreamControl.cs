using System.Collections.Generic;
using System.Linq;
using Piously.Game.Users;

namespace Piously.Game.Overlays.Dashboard.Friends
{
    public class FriendOnlineStreamControl : OverlayStreamControl<FriendStream>
    {
        protected override OverlayStreamItem<FriendStream> CreateStreamItem(FriendStream value) => new FriendsOnlineStatusItem(value);

        public void Populate(List<User> users)
        {
            Clear();

            var userCount = users.Count;
            var onlineUsersCount = users.Count(user => user.IsOnline);

            AddItem(new FriendStream(OnlineStatus.All, userCount));
            AddItem(new FriendStream(OnlineStatus.Online, onlineUsersCount));
            AddItem(new FriendStream(OnlineStatus.Offline, userCount - onlineUsersCount));

            Current.Value = Items.FirstOrDefault();
        }
    }
}
