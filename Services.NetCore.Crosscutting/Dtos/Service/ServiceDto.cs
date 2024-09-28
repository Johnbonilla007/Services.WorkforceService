
using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.Workforce.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.Service
{
    public class ServiceResponse : ResponseBase
    {
        public ServiceDto Service { get; set; }
        public List<ServiceDto> Services { get; set; }
    }
    public class ServiceDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
