using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;
using Services.Workforce.Domain.Aggregates.ServiceProviderAgg;

namespace Services.Workforce.Domain.Aggregates.ProviderAgg
{
    [Table(nameof(Provider), Schema = SchemaTypes.Commons)]
    public class Provider : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsVerified { get; set; }

        [Required, StringLength(50)]
        public string IdentificationNumber { get; set; }

        [StringLength(100)]
        public string VerificationDocumentFrontPath { get; set; }

        [StringLength(100)]
        public string VerificationDocumentBackPath { get; set; }

        [StringLength(50)]
        public string VerificationStatus { get; set; }

        public DateTime? VerificationDate { get; set; }

        [StringLength(100)]
        public string VerifiedBy { get; set; }

        public virtual ICollection<ServiceProvider> ServicesByProvider { get; set; }
    }
}
