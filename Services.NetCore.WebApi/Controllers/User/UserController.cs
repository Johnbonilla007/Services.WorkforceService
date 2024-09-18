using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Workforce.Application.Services.UserAppServices;
using Services.Workforce.Crosscutting.Dtos.User;

namespace Services.Workforce.WebApi.Controllers.User
{
    [ApiController]
    [Route("api/v2/security/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost, Route("create-or-update")]
        public async Task<IActionResult> CreateOrUpdateUser(CreateOrUpdateUserRequest authenticateUserRequest)
        {
            var response = await _userAppService.CreateOrUpdateUser(authenticateUserRequest);

            return Ok(response);
        }

        [HttpPost, Route("authenticate")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserRequest authenticateUserRequest)
        {
            var response = await _userAppService.AuthenticateUser(authenticateUserRequest);

            return Ok(response);
        }
    }
}
