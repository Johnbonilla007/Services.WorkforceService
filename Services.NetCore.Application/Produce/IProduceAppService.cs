using Services.NetCore.Crosscutting.Dtos.Produce;

namespace Services.NetCore.Application.Produce
{
    public interface IProduceAppService : IDisposable
    {
        Task<ProduceDto> SaveProduce(ProduceRequest request);
        Task<List<ProduceDto>> GetProducts(ProduceRequest request);
    }
}