using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.NetCore.Crosscutting.Dtos.Produce;

namespace Services.NetCore.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/test1")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProducts()
        {
            List<ProduceDto> response = new();

            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveProduct(ProduceRequest request)
        {
            ProduceDto response = new();

            return Ok(response);
        }


        [HttpDelete]
        [Route("")]
        public IActionResult DeleProduct()
        {
            var response = "Hola";

            return Ok(response);
        }

    }
}