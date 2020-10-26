using System.Net.Http;
using osu.Framework.IO.Network;
using Piously.Game.Online.Chat;
using Piously.Game.Users;

namespace Piously.Game.Online.API.Requests
{
    public class CreateNewPrivateMessageRequest : APIRequest<CreateNewPrivateMessageResponse>
    {
        private readonly User user;
        private readonly Message message;

        public CreateNewPrivateMessageRequest(User user, Message message)
        {
            this.user = user;
            this.message = message;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;
            req.AddParameter(@"target_id", user.Id.ToString());
            req.AddParameter(@"message", message.Content);
            req.AddParameter(@"is_action", message.IsAction.ToString().ToLowerInvariant());
            return req;
        }

        protected override string Target => @"chat/new";
    }
}
