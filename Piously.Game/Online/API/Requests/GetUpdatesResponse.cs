using System.Collections.Generic;
using Newtonsoft.Json;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class GetUpdatesResponse
    {
        [JsonProperty]
        public List<Channel> Presence;

        [JsonProperty]
        public List<Message> Messages;
    }
}
