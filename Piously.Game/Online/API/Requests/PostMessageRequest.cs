using System.Net.Http;
using osu.Framework.IO.Network;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class PostMessageRequest : APIRequest<Message>
    {
        private readonly Message message;

        public PostMessageRequest(Message message)
        {
            this.message = message;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();

            req.Method = HttpMethod.Post;
            req.AddParameter(@"is_action", message.IsAction.ToString().ToLowerInvariant());
            req.AddParameter(@"message", message.Content);

            return req;
        }

        protected override string Target => $@"chat/channels/{message.ChannelId}/messages";
    }
}
