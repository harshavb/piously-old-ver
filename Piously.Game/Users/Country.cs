using System;
using Newtonsoft.Json;

namespace Piously.Game.Users
{
    public class Country : IEquatable<Country>
    {
        /// <summary>
        /// The name of this country.
        /// </summary>
        [JsonProperty(@"name")]
        public string FullName;

        /// <summary>
        /// Two-letter flag acronym (ISO 3166 standard)
        /// </summary>
        [JsonProperty(@"code")]
        public string FlagName;

        public bool Equals(Country other) => FlagName == other?.FlagName;
    }
}
