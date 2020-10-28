using System;
using Newtonsoft.Json;
using osu.Framework.Bindables;

namespace Piously.Game.Users
{
    public class User
    {
        [JsonProperty(@"id")]
        public long Id = 1;

        [JsonProperty(@"join_date")]
        public DateTimeOffset JoinDate;

        [JsonProperty(@"username")]
        public string Username;

        [JsonProperty(@"previous_usernames")]
        public string[] PreviousUsernames;

        [JsonProperty(@"country")]
        public Country Country;

        public readonly Bindable<UserStatus> Status = new Bindable<UserStatus>();

        public readonly Bindable<UserActivity> Activity = new Bindable<UserActivity>();

        [JsonProperty(@"profile_color")]
        public string Color;

        [JsonProperty(@"avatar_url")]
        public string AvatarUrl;

        [JsonProperty(@"cover_url")]
        public string CoverUrl
        {
            get => Cover?.Url;
            set => Cover = new UserCover { Url = value };
        }

        [JsonProperty(@"cover")]
        public UserCover Cover;

        public class UserCover
        {
            [JsonProperty(@"custom_url")]
            public string CustomUrl;

            [JsonProperty(@"url")]
            public string Url;

            [JsonProperty(@"id")]
            public int? Id;
        }

        [JsonProperty(@"is_admin")]
        public bool IsAdmin;

        [JsonProperty(@"competitive_rating")]
        public int? CompetitiveRating;

        [JsonProperty(@"is_bot")]
        public bool IsBot;

        [JsonProperty(@"is_active")]
        public bool Active;

        [JsonProperty(@"is_online")]
        public bool IsOnline;

        [JsonProperty(@"pm_friends_only")]
        public bool PMFriendsOnly;

        [JsonProperty(@"interests")]
        public string Interests;

        [JsonProperty(@"occupation")]
        public string Occupation;

        [JsonProperty(@"title")]
        public string Title;

        [JsonProperty(@"location")]
        public string Location;

        [JsonProperty(@"last_visit")]
        public DateTimeOffset? LastVisit;

        [JsonProperty(@"twitter")]
        public string Twitter;

        [JsonProperty(@"skype")]
        public string Skype;

        [JsonProperty(@"discord")]
        public string Discord;

        [JsonProperty(@"website")]
        public string Website;

        [JsonProperty(@"profile_order")]
        public string[] ProfileOrder;

        private UserStatistics statistics;

        [JsonProperty(@"statistics")]
        public UserStatistics Statistics
        {
            get => statistics ??= new UserStatistics();
            set
            {
                statistics = value;
            }
        }

        public override string ToString() => Username;

        /// <summary>
        /// A user instance for displaying locally created system messages.
        /// </summary>
        public static readonly User SYSTEM_USER = new User
        {
            Username = "system",
            Id = 0
        };
    }
}
