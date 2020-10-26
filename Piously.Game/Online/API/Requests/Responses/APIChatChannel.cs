using System.Collections.Generic;
using Newtonsoft.Json;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests.Responses
{
    public class APIChatChannel
    {
        [JsonProperty(@"channel_id")]
        public int? ChannelID { get; set; }

        [JsonProperty(@"recent_messages")]
        public List<Message> RecentMessages { get; set; }
    }
}
