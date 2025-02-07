using Services.Workforce.Crosscutting.Dtos.Address;

namespace Services.Workforce.Application.Services.AddressAppServices
{
    public interface IAddressAppService
    {
        Task<AddressResponse> CreateOrUpdateAddressAsync(CreateOrUpdateAddressRequest createOrUpdateAddressRequest);
        Task<AddressResponse> SetAsDefaultAddressAsync(CreateOrUpdateAddressRequest createOrUpdateAddressRequest);
        Task<AddressResponse> RemoveAddressAsync(DeleteAddressRequest deleteAddressRequest);
        Task<AddressResponse> GetAllAddresseseByUserAsync(int userId);
    }
}
