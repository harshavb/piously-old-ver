namespace Piously.Game.Overlays.Dashboard.Friends
{
    public class FriendStream
    {
        public OnlineStatus Status { get; }

        public int Count { get; }

        public FriendStream(OnlineStatus status, int count)
        {
            Status = status;
            Count = count;
        }
    }
}
