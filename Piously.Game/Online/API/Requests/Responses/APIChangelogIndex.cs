using System.Collections.Generic;
using Newtonsoft.Json;

namespace Piously.Game.Online.API.Requests.Responses
{
    public class APIChangelogIndex
    {
        [JsonProperty]
        public List<APIChangelogBuild> Builds;

        [JsonProperty]
        public List<APIUpdateStream> Streams;
    }
}
