using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Workforce.Application.Services.ProviderAppServices;
using Services.Workforce.Crosscutting.Dtos.Provider;

namespace Services.Workforce.WebApi.Controllers.Provider
{
    [ApiController, Route("api/v2/commons/providers")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderAppService _providerAppService;
        public ProviderController(IProviderAppService providerAppService)
        {
            _providerAppService = providerAppService;
        }

        [HttpGet, Route("all-providers")]
        public async Task<IActionResult> GetAllProvidersAsync([FromQuery] string searchValue)
        {
            var response = await _providerAppService.GetProvidersAsync(searchValue);

            return Ok(response);
        }


        [HttpGet, Route("servies-by-userId")]
        public async Task<IActionResult> GetServicesByUserId([FromQuery] int userId)
        {
            var response = await _providerAppService.GetServicesByUserId(userId);

            return Ok(response);
        }

        [HttpPost, Route("create-or-update-provider")]
        public async Task<IActionResult> CreateOrUpdateProviderAsync(CreateOrUpdateProviderRequest createOrUpdateProviderRequest)
        {
            var response = await _providerAppService.CreateOrUpdateProviderAsync(createOrUpdateProviderRequest);

            return Ok(response);
        }

        [HttpPost, Route("update-services-by-provider")]
        public async Task<IActionResult> UpdateServicesByProvider(UpdateServicesByProviderRequest updateServicesByProviderRequest)
        {
            var response = await _providerAppService.UpdateServicesByProvider(updateServicesByProviderRequest);

            return Ok(response);
        }

        [HttpDelete, Route("delete-provider")]
        public async Task<IActionResult> DeleteProviderAsync(DeleteProviderRequest deleteProviderRequest)
        {
            var response = await _providerAppService.DeleteProviderAsync(deleteProviderRequest);

            return Ok(response);
        }
    }
}
