using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Mapping;
using Services.Workforce.Crosscutting.Dtos.Address;
using Services.Workforce.Domain.Aggregates.AddressAgg;

namespace Services.Workforce.Infraestructure.Mapping.User
{
    public class UserProfile : GenericProfileBase<BaseEntity, ResponseBase>
    {
        public UserProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }
    }
}
