using Services.NetCore.Crosscutting.Core;
using Services.Workforce.Crosscutting.Dtos.ServiceProvider;

namespace Services.Workforce.Crosscutting.Dtos.Provider
{
    public class CreateOrUpdateProviderRequest : RequestBase
    {
        public ProviderDto Provider { get; set; }
    }

    public class DeleteProviderRequest : RequestBase
    {
        public int Id { get; set; }
    }

    public class UpdateServicesByProviderRequest : RequestBase
    {
        public List<ServiceProviderDto> ServicesByProvider { get; set; }
    }
}
