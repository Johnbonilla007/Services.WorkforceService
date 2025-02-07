using AutoMapper;
using Services.NetCore.Application.Core;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.Workforce.Crosscutting.Dtos.Address;
using Services.Workforce.Domain.Aggregates.AddressAgg;

namespace Services.Workforce.Application.Services.AddressAppServices
{
    public class AddressAppService : BaseAppService, IAddressAppService
    {
        private readonly IMapper _mapper;
        public AddressAppService(IGenericRepository<IGenericDataContext> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<AddressResponse> CreateOrUpdateAddressAsync(CreateOrUpdateAddressRequest createOrUpdateAddressRequest)
        {
            ThrowIf.Argument.IsNull(nameof(createOrUpdateAddressRequest), nameof(createOrUpdateAddressRequest));
            ThrowIf.Argument.IsNull(nameof(createOrUpdateAddressRequest.Address), nameof(createOrUpdateAddressRequest.Address));

            var address = await _repository.GetSingleAsync<Address>(x => x.Id == createOrUpdateAddressRequest.Address.Id);
            TransactionInfo transactionInfo;

            createOrUpdateAddressRequest.RequestUserInfo = new NetCore.Crosscutting.Core.UserInfoDto
            {
                ModifiedBy = "jdbonilla",
                CreatedBy = "jdbonilla"
            };

            if (address == null)
            {
                address = _mapper.Map<Address>(createOrUpdateAddressRequest.Address);
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateAddressRequest.RequestUserInfo, Transactions.CreateAddress);
                await _repository.AddAsync(address);
            }
            else
            {
                address.Alias = createOrUpdateAddressRequest.Address.Alias;
                address.HouseNumber = createOrUpdateAddressRequest.Address.HouseNumber;
                address.Name = createOrUpdateAddressRequest.Address.Name;
                address.Reference = createOrUpdateAddressRequest.Address.Reference;
                address.PhoneNumber = createOrUpdateAddressRequest.Address.PhoneNumber;
                address.Coordinates = createOrUpdateAddressRequest.Address.Coordinates;
                address.IsDefault = createOrUpdateAddressRequest.Address.IsDefault;
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateAddressRequest.RequestUserInfo, Transactions.UpdateAddress);
            }
            try
            {
                await _repository.UnitOfWork.CommitAsync(transactionInfo);
            }
            catch (Exception exception)
            {

                throw;
            }

            return new AddressResponse { Success = true };
        }

        public async Task<AddressResponse> GetAllAddresseseByUserAsync(int userId)
        {
            ThrowIf.Argument.IsZeroOrNegative(userId, nameof(userId));

            var addresses = await _repository.GetFilteredAsync<Address>(x => x.UserId == userId);

            var addressesDto = _mapper.Map<List<AddressDto>>(addresses);

            return new AddressResponse { Addresses = addressesDto };
        }

        public async Task<AddressResponse> RemoveAddressAsync(DeleteAddressRequest deleteAddressRequest)
        {
            ThrowIf.Argument.IsNull(nameof(deleteAddressRequest), nameof(deleteAddressRequest));
            ThrowIf.Argument.IsZeroOrNegative(deleteAddressRequest.Id, nameof(deleteAddressRequest.Id));

            var address = await _repository.GetSingleAsync<Address>(x => x.Id == deleteAddressRequest.Id);
            if (address == null) return new AddressResponse { ValidationErrorMessage = Settings.addressUserDoesntExist, Success = false };

            var transactionInfo = TransactionInfoFactory.CrearTransactionInfo(deleteAddressRequest.RequestUserInfo, Transactions.DeleteAddress);

            await _repository.RemoveAsync(address);

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new AddressResponse { Success = true };
        }

        public async Task<AddressResponse> SetAsDefaultAddressAsync(CreateOrUpdateAddressRequest createOrUpdateAddressRequest)
        {
            ThrowIf.Argument.IsNull(nameof(createOrUpdateAddressRequest), nameof(createOrUpdateAddressRequest));
            ThrowIf.Argument.IsNull(nameof(createOrUpdateAddressRequest.Address), nameof(createOrUpdateAddressRequest.Address));

            var address = await _repository.GetSingleAsync<Address>(x => x.Id == createOrUpdateAddressRequest.Address.Id);

            if (address == null) return new AddressResponse { ValidationErrorMessage = Settings.addressUserDoesntExist, Success = false };

            address.IsDefault = true;
            var otherAddresses = await _repository.GetFilteredAsync<Address>(x => x.UserId == createOrUpdateAddressRequest.Address.UserId && x.IsDefault && x.Id != address.Id);

            if (otherAddresses.Any())
            {
                foreach (var item in otherAddresses)
                {
                    item.IsDefault = false;
                }
            }

            var transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateAddressRequest.RequestUserInfo, Transactions.SetAsDefaultAddress);

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new AddressResponse { Success = true };
        }
    }
}
