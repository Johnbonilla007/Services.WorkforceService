using Microsoft.AspNetCore.Mvc;
using Services.Workforce.Application.Services.ServiceAppServices;
using Services.Workforce.Crosscutting.Dtos.Service;
using System.Threading.Tasks;

namespace Services.Workforce.WebApi.Controllers.Service
{
    [ApiController, Route("api/v2/commons/providers")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceAppService _serviceAppServices;

        public ServiceController(IServiceAppService serviceAppServices)
        {
            _serviceAppServices = serviceAppServices;
        }

        [HttpGet, Route("all-services")]
        public async Task<IActionResult> GetAllServicesAsync([FromQuery] string searchValue)
        {
            var response = await _serviceAppServices.GetServiceAsync(searchValue);
            return Ok(response);
        }

        [HttpPost, Route("create-or-upodate-service")]
        public async Task<IActionResult> CreateOrUpdateServiceAsync(CreateOrUpdateServiceRequest createOrUpdateServiceRequest)
        {
            var response = await _serviceAppServices.CreateOrUpdateServiceAsync(createOrUpdateServiceRequest);
            return Ok(response);
        }

        [HttpDelete, Route("delete-service")]
        public async Task<IActionResult> DeleteServiceAsync(DeleteServiceRequest deleteServiceRequest)
        {
            var response = await _serviceAppServices.DeleteServiceAsync(deleteServiceRequest);
            return Ok(response);
        }

    }
}
