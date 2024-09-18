using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.NetCore.Domain.Aggregates;
using Services.NetCore.Domain.Core;

namespace Services.NetCore.Infraestructure.Mapping.Commons
{
    public class CommonsProfile : GenericProfileBase<BaseEntity, ResponseBase>
    {
        public CommonsProfile()
        {
            CreateMap<Product, ProduceDto>();
            CreateMap<ProduceDto, Product>();
        }
    }
}
