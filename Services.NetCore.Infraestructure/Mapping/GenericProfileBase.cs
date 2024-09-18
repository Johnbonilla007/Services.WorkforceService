using AutoMapper;

namespace Services.NetCore.Infraestructure.Mapping
{
    public abstract class GenericProfileBase<BaseEntity, ResponseBase> : Profile
    {
        protected GenericProfileBase()
        {
            CreateMap<BaseEntity, ResponseBase>();
        }
    }
}
