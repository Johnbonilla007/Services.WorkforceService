using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
