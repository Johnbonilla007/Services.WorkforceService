using Services.NetCore.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.User
{
    public class UserRequest
    {
    }

    public class AuthenticateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateOrUpdateUserRequest : RequestBase
    {
        public UserDto User { get; set; }
    }
}
