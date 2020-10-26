using System;
using Piously.Game.Users;

namespace Piously.Game.Online.Chat
{
    public class InfoMessage : LocalMessage
    {
        private static int infoID = -1;

        public InfoMessage(string message)
            : base(infoID--)
        {
            Timestamp = DateTimeOffset.Now;
            Content = message;

            Sender = User.SYSTEM_USER;
        }
    }
}
