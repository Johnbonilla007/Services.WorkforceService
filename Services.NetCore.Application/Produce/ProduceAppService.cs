using AutoMapper;
using Services.NetCore.Application.Core;
using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.NetCore.Domain.Aggregates;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;

namespace Services.NetCore.Application.Produce
{
    public class ProduceAppService : BaseAppService, IProduceAppService
    {
        private readonly IMapper _mapper;

        public ProduceAppService(IGenericRepository<IGenericDataContext> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public async Task<List<ProduceDto>> GetProducts(ProduceRequest request)
        {
            var products = await _repository.GetAllAsync<Product>();

            return _mapper.Map<List<ProduceDto>>(products);
        }

        public async Task<ProduceDto> SaveProduce(ProduceRequest request)
        {
            Product product = new Product
            {
                PurchasePrice = request.PurchasePrice,
                Quantity = request.Quantity,
                DatePurchase = request.DatePurchase
            };

            await _repository.AddAsync(product);
            await _repository.UnitOfWork.CommitAsync();

            return new ProduceDto { Message = "Success" };
        }
    }
}