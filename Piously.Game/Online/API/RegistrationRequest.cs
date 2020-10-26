using Newtonsoft.Json;

namespace Piously.Game.Online.API
{
    public class RegistrationRequest : PiouslyWebRequest
    {
        internal string Username;
        internal string Email;
        internal string Password;

        protected override void PrePerform()
        {
            AddParameter("user[username]", Username);
            AddParameter("user[user_email]", Email);
            AddParameter("user[password]", Password);

            base.PrePerform();
        }

        public class RegistrationRequestErrors
        {
            public UserErrors User;

            public class UserErrors
            {
                [JsonProperty("username")]
                public string[] Username;

                [JsonProperty("user_email")]
                public string[] Email;

                [JsonProperty("password")]
                public string[] Password;
            }
        }
    }
}
