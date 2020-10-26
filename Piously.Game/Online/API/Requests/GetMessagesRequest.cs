using System.Collections.Generic;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class GetMessagesRequest : APIRequest<List<Message>>
    {
        private readonly Channel channel;

        public GetMessagesRequest(Channel channel)
        {
            this.channel = channel;
        }

        protected override string Target => $@"chat/channels/{channel.Id}/messages";
    }
}
