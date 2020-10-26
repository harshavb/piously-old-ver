using osuTK.Graphics;
using Piously.Game.Graphics;

namespace Piously.Game.Users
{
    public abstract class UserActivity
    {
        public abstract string Status { get; }
        public virtual Color4 GetAppropriateColour(PiouslyColor colors) => colors.GreenDarker;
    }

    public class Ranked : UserActivity
    {
        public override string Status => "Playing Ranked";
    }

    public class Casual : UserActivity
    {
        public override string Status => "Playing Casual";
    }

    public class Solo : UserActivity
    {
        public override string Status => "Playing solo";
    }

    public class Idle : UserActivity
    {
        public override string Status => "Idle";
    }
}
