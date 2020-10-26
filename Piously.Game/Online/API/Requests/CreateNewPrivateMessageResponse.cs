using Newtonsoft.Json;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class CreateNewPrivateMessageResponse
    {
        [JsonProperty("new_channel_id")]
        public int ChannelID;

        public Message Message;
    }
}
