using AutoMapper;
using Services.NetCore.Application.Core;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.Workforce.Crosscutting.Dtos.Service;
using Services.Workforce.Domain.Aggregates.ServiceAgg;

namespace Services.Workforce.Application.Services.ServiceAppServices
{
    public class ServiceAppService : BaseAppService, IServiceAppService
    {
        private readonly IMapper _mapper;
        public ServiceAppService(IGenericRepository<IGenericDataContext> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateOrUpdateServiceAsync(CreateOrUpdateServiceRequest createOrUpdateServiceRequest)
        {
            ThrowIf.Argument.IsNull(nameof(CreateOrUpdateServiceRequest), nameof(CreateOrUpdateServiceRequest));
            ThrowIf.Argument.IsNull(nameof(CreateOrUpdateServiceRequest.Service), nameof(CreateOrUpdateServiceRequest.Service));

            var service = await _repository.GetSingleAsync<Service>(x => x.Id == createOrUpdateServiceRequest.Service.Id);
            TransactionInfo transactionInfo;

            if (service == null) {
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateServiceRequest.RequestUserInfo, Transactions.CreateService);
                service = _mapper.Map<Service>(createOrUpdateServiceRequest.Service);
                await _repository.AddAsync(service);
            }
            else
            {
                service.Name = createOrUpdateServiceRequest.Service.Name;
                service.Description = createOrUpdateServiceRequest.Service.Description;
                service.IconName = createOrUpdateServiceRequest.Service.IconName;
                transactionInfo = TransactionInfoFactory.CrearTransactionInfo(createOrUpdateServiceRequest.RequestUserInfo, Transactions.UpdateService);
            }

            await _repository.UnitOfWork.CommitAsync(transactionInfo);

            return new ServiceResponse { Success = true };
        }

        public async Task<ServiceResponse> DeleteServiceAsync(DeleteServiceRequest serviceRequest)
        {
            ThrowIf.Argument.IsNull(nameof(serviceRequest), nameof(serviceRequest));

            var service = await _repository.GetSingleAsync<Service>(x => x.Id == serviceRequest.Id);
            if (service == null) return new ServiceResponse { Success = false, ValidationErrorMessage = Settings.serviceDoesntExist };
            var transactionInfo = TransactionInfoFactory.CrearTransactionInfo(serviceRequest.RequestUserInfo, Transactions.DeleteService);


            await _repository.RemoveAsync(service);

            await _repository.UnitOfWork.CommitAsync(transactionInfo);
            return new ServiceResponse { Success = true };
        }

        public async Task<ServiceResponse> GetServiceAsync(string searchValue)
        {
            IEnumerable<Service> services;

            if (!string.IsNullOrEmpty(searchValue))
            {
                services = await _repository.GetFilteredAsync<Service>(x => x.Name.Contains(searchValue) || x.Description.Contains(searchValue));
            }
            else
            {
                services = await _repository.GetAllAsync<Service>();
            }

            var servicesDto = _mapper.Map<List<ServiceDto>>(services);
            return new ServiceResponse { Services = servicesDto, Success = true };
        }
    }
}
