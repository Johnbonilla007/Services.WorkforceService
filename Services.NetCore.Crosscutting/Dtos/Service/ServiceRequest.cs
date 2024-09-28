

using Services.NetCore.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.Service
{
    public class CreateOrUpdateServiceRequest : RequestBase
    {
        public ServiceDto Service { get; set; }

    }
    public class ServiceRequest : RequestBase
    {
        public int Id { get; set; }
    }

    public class DeleteServiceRequest : RequestBase
    {
        public int Id { get; set; }
    }
}
