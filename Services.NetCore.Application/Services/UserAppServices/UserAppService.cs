using AutoMapper;
using Services.NetCore.Application.Core;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.Workforce.Application.Core;
using Services.Workforce.Crosscutting.Dtos.User;
using Services.Workforce.Domain.Aggregates.UserAgg;

namespace Services.Workforce.Application.Services.UserAppServices
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IMapper _mapper;
        public UserAppService(IGenericRepository<IGenericDataContext> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<UserResponse> AuthenticateUser(AuthenticateUserRequest authenticateUserRequest)
        {
            string username = authenticateUserRequest.UserName;
            string password = Encrypt.GetSHA256(authenticateUserRequest.Password);

            var user = await _repository.GetSingleAsync<User>(x => x.UserName == username);

            if (user == null)
            {
                return new UserResponse { ValidationErrorMessage = "Usuario Incorrecto" };
            }

            if (user.Password != password)
            {
                return new UserResponse { ValidationErrorMessage = "Cantraseña Incorrecta" };
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Password = string.Empty;
            userDto.Id = user.Id;

            return new UserResponse
            {
                Success = true,
                User = userDto,
            };
        }

        public async Task<UserResponse> CreateOrUpdateUser(CreateOrUpdateUserRequest createOrUpdateUserRequest)
        {
            ThrowIf.Argument.IsNull(createOrUpdateUserRequest, nameof(createOrUpdateUserRequest));
            var userDto = createOrUpdateUserRequest.User;
            if (createOrUpdateUserRequest.User.Id == 0)
            {
                var userExistence = await _repository.GetSingleAsync<User>(x => x.PhoneNumber == userDto.PhoneNumber || x.UserName == userDto.UserName);
                if (userExistence != null) return new UserResponse { ValidationErrorMessage = Settings.userAlreadyExist };
            }


            var user = await _repository.GetSingleAsync<User>(x => x.Id == createOrUpdateUserRequest.User.Id);
            string password = Encrypt.GetSHA256(createOrUpdateUserRequest.User.Password);
            TransactionInfo transactionInfo;

            if (user == null)
            {
                user = _mapper.Map<User>(createOrUpdateUserRequest.User);
                user.Password = password;
                createOrUpdateUserRequest.RequestUserInfo.CreatedBy = user.UserName;
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateUserRequest.RequestUserInfo, Settings.AddUser);
                await _repository.AddAsync(user);
            }
            else
            {
                user.Password = password;
                user.FullName = createOrUpdateUserRequest.User.FullName;
                user.UserName = createOrUpdateUserRequest.User.UserName;
                user.Email = createOrUpdateUserRequest.User.Email;
                user.PhoneNumber = createOrUpdateUserRequest.User.PhoneNumber;
                user.Genre = createOrUpdateUserRequest.User.Genre;
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateUserRequest.RequestUserInfo, Settings.UpdateUser);
            }

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return await AuthenticateUser(new AuthenticateUserRequest { UserName = user.UserName, Password = createOrUpdateUserRequest.User.Password });
        }
    }
}
