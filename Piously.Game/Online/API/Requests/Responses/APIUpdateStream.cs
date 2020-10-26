using System;
using Newtonsoft.Json;
using osu.Framework.Graphics.Colour;
using osuTK.Graphics;

namespace Piously.Game.Online.API.Requests.Responses
{
    public class APIUpdateStream : IEquatable<APIUpdateStream>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_featured")]
        public bool IsFeatured { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("latest_build")]
        public APIChangelogBuild LatestBuild { get; set; }

        public bool Equals(APIUpdateStream other) => Id == other?.Id;

        public ColourInfo Colour
        {
            get
            {
                switch (Name)
                {
                    case "idk":
                        return new Color4(102, 204, 255, 255);

                    case "these":
                        return new Color4(34, 153, 187, 255);

                    case "are":
                        return new Color4(255, 221, 85, 255);

                    case "some":
                        return new Color4(238, 170, 0, 255);

                    case PiouslyGameBase.CLIENT_STREAM_NAME:
                        return new Color4(237, 18, 33, 255);

                    case "words":
                        return new Color4(136, 102, 238, 255);

                    default:
                        return new Color4(0, 0, 0, 255);
                }
            }
        }
    }
}
