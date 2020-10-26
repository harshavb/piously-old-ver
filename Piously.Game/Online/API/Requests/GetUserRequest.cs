using Piously.Game.Users;

namespace Piously.Game.Online.API.Requests
{
    public class GetUserRequest : APIRequest<User>
    {
        private readonly long? userId;

        public GetUserRequest(long? userId = null)
        {
            this.userId = userId;
        }

        protected override string Target => userId.HasValue ? $@"users/{userId}" : $@"me";
    }
}
