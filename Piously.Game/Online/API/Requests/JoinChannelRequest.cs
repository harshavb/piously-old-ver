using System.Net.Http;
using osu.Framework.IO.Network;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class JoinChannelRequest : APIRequest
    {
        private readonly Channel channel;

        public JoinChannelRequest(Channel channel)
        {
            this.channel = channel;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Put;
            return req;
        }

        protected override string Target => $@"chat/channels/{channel.Id}/users/{User.Id}";
    }
}
