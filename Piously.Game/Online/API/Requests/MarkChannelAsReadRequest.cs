using System.Net.Http;
using osu.Framework.IO.Network;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    class MarkChannelAsReadRequest : APIRequest
    {
        private readonly Channel channel;
        private readonly Message message;

        public MarkChannelAsReadRequest(Channel channel, Message message)
        {
            this.channel = channel;
            this.message = message;
        }

        protected override string Target => $"chat/channels/{channel.Id}/mark-as-read/{message.Id}";

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Put;
            return req;
        }
    }
}
