using Microsoft.AspNetCore.Mvc;
using Services.Workforce.Application.Services.AddressAppServices;
using Services.Workforce.Crosscutting.Dtos.Address;
using System.Threading.Tasks;

namespace Services.Workforce.WebApi.Controllers.Address
{
    [ApiController, Route("api/v2/user/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressAppService _addressAppService;
        public AddressController(IAddressAppService addressAppService)
        {
            _addressAppService = addressAppService;
        }

        [HttpGet, Route("all-addresses-by-user")]
        public async Task<IActionResult> GetAllAddressesByUserAsync([FromQuery] int userId)
        {
            var response = await _addressAppService.GetAllAddresseseByUserAsync(userId);

            return Ok(response);
        }

        [HttpPost, Route("create-or-update-address")]
        public async Task<IActionResult> CreateOrUpdateAddressAsync(CreateOrUpdateAddressRequest createOrUpdateAddressRequest)
        {
            var response = await _addressAppService.CreateOrUpdateAddressAsync(createOrUpdateAddressRequest);

            return Ok(response);
        }

        [HttpDelete, Route("delete-address")]
        public async Task<IActionResult> DeleteProviderAsync(DeleteAddressRequest deleteAddressRequest)
        {
            var response = await _addressAppService.RemoveAddressAsync(deleteAddressRequest);

            return Ok(response);
        }

        [HttpPost, Route("set-as-default-address")]
        public async Task<IActionResult> SetAsDefaultAddressAsync(CreateOrUpdateAddressRequest createOrUpdateAddressRequest)
        {
            var response = await _addressAppService.SetAsDefaultAddressAsync(createOrUpdateAddressRequest);

            return Ok(response);
        }
    }
}
