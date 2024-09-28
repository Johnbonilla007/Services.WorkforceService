using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.NetCore.Domain.Core;
using Services.Workforce.Crosscutting.Dtos.Provider;
using Services.Workforce.Crosscutting.Dtos.User;
using Services.Workforce.Domain.Aggregates.ProviderAgg;
using Services.Workforce.Domain.Aggregates.UserAgg;

namespace Services.NetCore.Infraestructure.Mapping.Commons
{
    public class CommonsProfile : GenericProfileBase<BaseEntity, ResponseBase>
    {
        public CommonsProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<ProviderDto, Provider>();
            CreateMap<Provider, ProviderDto>();
        }
    }
}
