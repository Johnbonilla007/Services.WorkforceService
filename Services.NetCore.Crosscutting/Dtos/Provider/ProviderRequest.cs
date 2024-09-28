using Services.NetCore.Crosscutting.Core;

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
}
