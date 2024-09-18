using Services.Workforce.Crosscutting.Dtos.User;

namespace Services.Workforce.Application.Services.UserAppServices
{
    public interface IUserAppService
    {
        public Task<UserResponse> AuthenticateUser(AuthenticateUserRequest authenticateUserRequest);
        public Task<UserResponse> CreateOrUpdateUser(CreateOrUpdateUserRequest createOrUpdateUserRequest);
    }
}
