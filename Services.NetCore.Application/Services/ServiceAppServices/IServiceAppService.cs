using Services.Workforce.Crosscutting.Dtos.Service;

namespace Services.Workforce.Application.Services.ServiceAppServices
{
    public interface IServiceAppService
    {
        Task<ServiceResponse> CreateOrUpdateServiceAsync(CreateOrUpdateServiceRequest createOrUpdateServiceRequest);
        Task<ServiceResponse> GetServiceAsync(string searchValue);
        Task<ServiceResponse> DeleteServiceAsync(DeleteServiceRequest serviceRequest);
    }
}
