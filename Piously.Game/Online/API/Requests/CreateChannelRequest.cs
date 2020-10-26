using System.Linq;
using System.Net.Http;
using osu.Framework.IO.Network;
using Piously.Game.Online.API.Requests.Responses;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class CreateChannelRequest : APIRequest<APIChatChannel>
    {
        private readonly Channel channel;

        public CreateChannelRequest(Channel channel)
        {
            this.channel = channel;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            req.Method = HttpMethod.Post;

            req.AddParameter("type", $"{ChannelType.PM}");
            req.AddParameter("target_id", $"{channel.Users.First().Id}");

            return req;
        }

        protected override string Target => @"chat/channels";
    }
}
