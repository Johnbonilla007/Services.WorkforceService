using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.NetCore.Application.Produce;
using Services.NetCore.Crosscutting.Dtos.Produce;

namespace Services.NetCore.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/test1")]
    public class TestController : ControllerBase
    {
        private readonly IProduceAppService _produceAppService;
        public TestController(IProduceAppService produceAppService)
        {
            if (produceAppService == null) throw new ArgumentException(nameof(produceAppService));

            _produceAppService = produceAppService;

        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProducts()
        {
            List<ProduceDto> response = await _produceAppService.GetProducts(new ProduceRequest());

            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveProduct(ProduceRequest request)
        {
            ProduceDto response = await _produceAppService.SaveProduce(request);

            return Ok(response);
        }


        [HttpDelete]
        [Route("")]
        public IActionResult DeleProduct()
        {
            var response = _produceAppService.GetProducts(new ProduceRequest());

            return Ok(response);
        }

    }
}