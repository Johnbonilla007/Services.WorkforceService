using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.Workforce.Crosscutting.Core;

namespace Services.Workforce.Crosscutting.Dtos.Provider
{
    public class ProviderResponse : ResponseBase
    {
        public ProviderDto Provider { get; set; }
        public List<ProviderDto> Providers { get; set; }
    }
    public class ProviderDto : BaseDto
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string Location { get; set; }
        public decimal Rating { get; set; }
        public bool IsVerified { get; set; }
    }
}
