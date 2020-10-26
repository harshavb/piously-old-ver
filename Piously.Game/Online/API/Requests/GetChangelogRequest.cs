using Piously.Game.Online.API.Requests.Responses;

namespace Piously.Game.Online.API.Requests
{
    public class GetChangelogRequest : APIRequest<APIChangelogIndex>
    {
        protected override string Target => @"changelog";
    }
}
