
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.NetCore.Application.Core;
using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.Workforce.Crosscutting.Dtos.Provider;
using Services.Workforce.Domain.Aggregates.ProviderAgg;
using Services.Workforce.Domain.Aggregates.ServiceProviderAgg;
using Z.EntityFramework.Plus;

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

            var providerDto = createOrUpdateProviderRequest.Provider;
            var providerExistince = await _repository.GetSingleAsync<Provider>(x => x.IdentificationNumber.Trim() == providerDto.IdentificationNumber);

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
                if (int.TryParse(searchValue, out int userId))
                {
                    providers = await _repository.GetFilteredAsync<Provider>(x => x.UserId == userId, new List<string> { "ServicesByProvider" });
                }
                else
                {
                    providers = await _repository.GetFilteredAsync<Provider>(x => x.IdentificationNumber.Contains(searchValue), new List<string> { "ServicesByProvider" });
                }
            }
            else
            {
                providers = await _repository.GetAllAsync<Provider>();
            }

            var providersDto = _mapper.Map<List<ProviderDto>>(providers);

            return new ProviderResponse { Providers = providersDto, Success = true };
        }

        public async Task<ProviderResponse> GetServicesByUserId(int userId)
        {
            ThrowIf.Argument.IsZeroOrNegative(userId, nameof(userId));

            var providers = await _repository.GetAllWithoutFilters<Provider>()
                                              .Where(x => x.UserId == userId)
                                              .Include(x => x.ServicesByProvider)
                                              .ThenInclude(x => x.Service)
                                              .AsNoTrackingWithIdentityResolution()
                                              .ToListAsync();

            if (providers != null && providers.Any())
            {
                var providersDto = _mapper.Map<List<ProviderDto>>(providers);

                var servicesByProvidersDto = providersDto.SelectMany(x => x.ServicesByProvider).ToList();

                return new ProviderResponse { Success = true, ServicesByProvider = servicesByProvidersDto };
            }

            return new ProviderResponse { Success = false };
        }

        public async Task<ResponseBase> UpdateServicesByProvider(UpdateServicesByProviderRequest updateServicesByProviderRequest)
        {
            ThrowIf.Argument.IsNull(nameof(updateServicesByProviderRequest), nameof(updateServicesByProviderRequest));
            ThrowIf.Argument.IsNull(nameof(updateServicesByProviderRequest.ServicesByProvider), nameof(updateServicesByProviderRequest.ServicesByProvider));

            var servicesIds = updateServicesByProviderRequest.ServicesByProvider.Select(x => x.Id).ToList();

            var sericesByProvider = await _repository.GetFilteredAsync<ServiceProvider>(x => servicesIds.Contains(x.Id));
            if (sericesByProvider == null || !sericesByProvider.Any()) return new ResponseBase { ValidationErrorMessage = Settings.servicesByProviderDoesntExist };


            foreach (var item in sericesByProvider)
            {
                item.Location = updateServicesByProviderRequest.ServicesByProvider.FirstOrDefault(x => x.Id == item.Id)?.Location;
            }

            var transactionInfo = TransactionInfoFactory.CrearTransactionInfo(updateServicesByProviderRequest.RequestUserInfo, Transactions.UpdateServicesByProvider);

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new ResponseBase { Success = true };
        }
    }
}
