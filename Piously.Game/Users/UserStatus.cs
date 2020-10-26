using osuTK.Graphics;
using Piously.Game.Graphics;

namespace Piously.Game.Users
{
    public abstract class UserStatus
    {
        public abstract string Message { get; }
        public abstract Color4 GetAppropriateColor(PiouslyColor colors);
    }

    public class UserStatusOnline : UserStatus
    {
        public override string Message => @"Online";
        public override Color4 GetAppropriateColor(PiouslyColor colors) => colors.GreenLight;
    }

    public abstract class UserStatusBusy : UserStatusOnline
    {
        public override Color4 GetAppropriateColor(PiouslyColor colors) => colors.YellowDark;
    }
    
    public class UserStatusOffline : UserStatus
    {
        public override string Message => @"Offline";
        public override Color4 GetAppropriateColor(PiouslyColor colors) => Color4.Black;
    }

    public class UserStatusDoNotDisturb : UserStatus
    {
        public override string Message => @"Do not disturb";
        public override Color4 GetAppropriateColor(PiouslyColor colors) => colors.RedDark;
    }
}
