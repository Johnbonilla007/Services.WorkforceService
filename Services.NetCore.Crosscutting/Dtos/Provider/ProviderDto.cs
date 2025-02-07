using System.ComponentModel.DataAnnotations;
using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.Workforce.Crosscutting.Core;
using Services.Workforce.Crosscutting.Dtos.ServiceProvider;

namespace Services.Workforce.Crosscutting.Dtos.Provider
{
    public class ProviderResponse : ResponseBase
    {
        public ProviderDto Provider { get; set; }
        public List<ProviderDto> Providers { get; set; }
        public List<ServiceProviderDto> ServicesByProvider { get; set; }
    }
    public class ProviderDto : BaseDto
    {
        public int UserId { get; set; }
        public bool IsVerified { get; set; }
        public string IdentificationNumber { get; set; }
        public string VerificationDocumentFrontPath { get; set; }
        public string VerificationDocumentBackPath { get; set; }
        public string VerificationStatus { get; set; }
        public DateTime? VerificationDate { get; set; }
        public string VerifiedBy { get; set; }
        public List<ServiceProviderDto> ServicesByProvider { get; set; }
    }
}
