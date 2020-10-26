using System.Collections.Generic;
using Piously.Game.Online.Chat;

namespace Piously.Game.Online.API.Requests
{
    public class ListChannelsRequest : APIRequest<List<Channel>>
    {
        protected override string Target => @"chat/channels";
    }
}
