using System.Collections.Generic;
using Piously.Game.Users;

namespace Piously.Game.Online.API.Requests
{
    public class GetFriendsRequest : APIRequest<List<User>>
    {
        protected override string Target => @"friends";
    }
}
