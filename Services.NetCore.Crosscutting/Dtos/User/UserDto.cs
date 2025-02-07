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
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "Customer";
        public string Genre { get; set; }
        public string Location { get; set; }
    }
}
