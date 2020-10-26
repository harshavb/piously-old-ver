using Piously.Game.Online.API.Requests.Responses;

namespace Piously.Game.Online.API.Requests
{
    public class GetChangelogBuildRequest : APIRequest<APIChangelogBuild>
    {
        private readonly string name;
        private readonly string version;

        public GetChangelogBuildRequest(string streamName, string buildVersion)
        {
            name = streamName;
            version = buildVersion;
        }

        protected override string Target => $@"changelog/{name}/{version}";
    }
}
