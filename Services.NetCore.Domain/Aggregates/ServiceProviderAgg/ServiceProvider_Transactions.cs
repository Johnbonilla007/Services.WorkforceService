using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.Workforce.Domain.Aggregates.ServiceProviderAgg
{
    [Table(nameof(ServiceProvider_Transactions), Schema = SchemaTypes.Business)]
    public class ServiceProvider_Transactions : BaseEntity
    {
        public int ProviderId { get; set; }

        [Required, Column(TypeName = standardDecimal)]
        public decimal Rating { get; set; }
        public int ServiceId { get; set; }

        [Required, Column(TypeName = standardDecimal)]
        public decimal Price { get; set; }

        [Required, StringLength(shortVarcharLength)]
        public string Location { get; set; }
    }
}
