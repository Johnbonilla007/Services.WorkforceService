using Services.Workforce.Crosscutting.Dtos.Provider;

namespace Services.Workforce.Application.Services.ProviderAppServices
{
    public interface IProviderAppService
    {
        Task<ProviderResponse> CreateOrUpdateProviderAsync(CreateOrUpdateProviderRequest createOrUpdateProviderRequest);
        Task<ProviderResponse> GetProvidersAsync(string searchValue);
        Task<ProviderResponse> DeleteProviderAsync(DeleteProviderRequest deleteProviderRequest);
    }
}
