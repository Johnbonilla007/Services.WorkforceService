using AutoMapper;
using Services.NetCore.Application.Core;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.Workforce.Crosscutting.Dtos.Provider;
using Services.Workforce.Domain.Aggregates.ProviderAgg;

namespace Services.Workforce.Application.Services.ProviderAppServices
{
    public class ProviderAppService : BaseAppService, IProviderAppService
    {
        private readonly IMapper _mapper;
        public ProviderAppService(IGenericRepository<IGenericDataContext> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<ProviderResponse> CreateOrUpdateProviderAsync(CreateOrUpdateProviderRequest createOrUpdateProviderRequest)
        {
            ThrowIf.Argument.IsNull(nameof(createOrUpdateProviderRequest), nameof(createOrUpdateProviderRequest));
            ThrowIf.Argument.IsNull(nameof(createOrUpdateProviderRequest.Provider), nameof(createOrUpdateProviderRequest.Provider));

            var provider = await _repository.GetSingleAsync<Provider>(x => x.Id == createOrUpdateProviderRequest.Provider.Id);
            TransactionInfo transactionInfo;
            if (provider == null)
            {
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateProviderRequest.RequestUserInfo, Transactions.CreateProvider);
                provider = _mapper.Map<Provider>(createOrUpdateProviderRequest.Provider);
                await _repository.AddAsync(provider);
            }
            else
            {
                provider.Location = createOrUpdateProviderRequest.Provider.Location;
                provider.ServiceId = createOrUpdateProviderRequest.Provider.ServiceId;
                provider.UserId = createOrUpdateProviderRequest.Provider.UserId;
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateProviderRequest.RequestUserInfo, Transactions.UpdateProvider);
            }

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new ProviderResponse { Success = true };
        }

        public async Task<ProviderResponse> DeleteProviderAsync(DeleteProviderRequest deleteProviderRequest)
        {
            ThrowIf.Argument.IsNull(nameof(deleteProviderRequest), nameof(deleteProviderRequest));

            var provider = await _repository.GetSingleAsync<Provider>(x => x.Id == deleteProviderRequest.Id);

            if (provider == null) return new ProviderResponse { Success = false, ValidationErrorMessage = Settings.providerDoesntExist };
            var transactionInfo = TransactionInfoFactory.CrearTransactionInfo(deleteProviderRequest.RequestUserInfo, Transactions.DeleteProvider);

            await _repository.RemoveAsync(provider);

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new ProviderResponse { Success = true };
        }

        public async Task<ProviderResponse> GetProvidersAsync(string searchValue)
        {
            IEnumerable<Provider> providers;

            if (!string.IsNullOrEmpty(searchValue))
            {
                providers = await _repository.GetFilteredAsync<Provider>(x => x.Location.Contains(searchValue));
            }
            else
            {
                providers = await _repository.GetAllAsync<Provider>();
            }

            var providersDto = _mapper.Map<List<ProviderDto>>(providers);

            return new ProviderResponse { Providers = providersDto, Success = true };
        }
    }
}
