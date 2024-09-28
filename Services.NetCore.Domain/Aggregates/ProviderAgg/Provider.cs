using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.Workforce.Domain.Aggregates.ProviderAgg
{
    [Table(nameof(Provider), Schema = SchemaTypes.Commons)]
    public class Provider : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required, StringLength(shortVarcharLength)]
        public string Location { get; set; }

        [Required, Column(TypeName = standardDecimal)]
        public decimal Rating { get; set; }

        [Required]
        public bool IsVerified { get; set; }
    }
}
