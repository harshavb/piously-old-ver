using System;
using Newtonsoft.Json;
using static Piously.Game.Users.User;

namespace Piously.Game.Users
{
    public class UserStatistics
    {
        [JsonProperty]
        public User User;

        [JsonProperty(@"level")]
        public LevelInfo Level;

        public struct LevelInfo
        {
            [JsonProperty(@"current")]
            public int Current;

            [JsonProperty(@"progress")]
            public int Progress;
        }
    }
}
