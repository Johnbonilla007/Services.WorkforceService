using Services.Workforce.Crosscutting.Core;
using Services.Workforce.Crosscutting.Dtos.Service;
using System.ComponentModel;

namespace Services.Workforce.Crosscutting.Dtos.ServiceProvider
{
    public class ServiceProviderDto : BaseDto
    {
        public int ProviderId { get; set; }
        public decimal Rating { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }

        public ServiceDto Service { get; set; }
    }
}
