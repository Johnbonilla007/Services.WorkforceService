using AutoMapper;
using Azure.Core;
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

            return new UserResponse
            {
                Success = true,
                User = userDto,
            };
        }

        public async Task<UserResponse> CreateOrUpdateUser(CreateOrUpdateUserRequest createOrUpdateUserRequest)
        {
            ThrowIf.Argument.IsNull(createOrUpdateUserRequest, nameof(createOrUpdateUserRequest));

            var user = await _repository.GetSingleAsync<User>(x => x.Id == createOrUpdateUserRequest.Id);
            string password = Encrypt.GetSHA256(createOrUpdateUserRequest.Password);
            TransactionInfo transactionInfo;

            if (user == null)
            {
                user = new User
                {
                    Password = password,
                    FullName = createOrUpdateUserRequest.FullName,
                    UserName = createOrUpdateUserRequest.UserName
                };

                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateUserRequest.RequestUserInfo, Settings.AddUser);
                await _repository.AddAsync(user, transactionInfo);
            }
            else
            {
                user.Password = password;
                user.FullName = createOrUpdateUserRequest.FullName;
                user.UserName = createOrUpdateUserRequest.UserName;
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateUserRequest.RequestUserInfo, Settings.UpdateUser);
            }

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new UserResponse { Success = true };
        }
    }
}
