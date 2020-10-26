using osu.Framework.IO.Network;

namespace Piously.Game.Online.API
{
    public class PiouslyWebRequest : WebRequest
    {
        public PiouslyWebRequest(string uri)
            : base(uri)
        {
        }

        public PiouslyWebRequest()
        {
        }

        protected override string UserAgent => "Piously";
    }
}
