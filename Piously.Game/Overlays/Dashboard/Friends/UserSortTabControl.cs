using System.ComponentModel;

namespace Piously.Game.Overlays.Dashboard.Friends
{
    public class UserSortTabControl : OverlaySortTabControl<UserSortCriteria>
    {
    }

    public enum UserSortCriteria
    {
        [Description(@"Recently Active")]
        LastVisit,
        Rank,
        Username
    }
}
