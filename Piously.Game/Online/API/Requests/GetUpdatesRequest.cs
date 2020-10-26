using JetBrains.Annotations;
using osu.Framework.IO.Network;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class GetUpdatesRequest : APIRequest<GetUpdatesResponse>
    {
        private readonly long since;
        private readonly Channel channel;

        public GetUpdatesRequest(long sinceId, [CanBeNull] Channel channel = null)
        {
            this.channel = channel;
            since = sinceId;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            if (channel != null) req.AddParameter(@"channel", channel.Id.ToString());
            req.AddParameter(@"since", since.ToString());

            return req;
        }

        protected override string Target => @"chat/updates";
    }
}
