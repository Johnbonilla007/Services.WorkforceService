using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.Workforce.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.User
{
    public class UserResponse : ResponseBase
    {
        public UserDto User { get; set; }
    }
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
