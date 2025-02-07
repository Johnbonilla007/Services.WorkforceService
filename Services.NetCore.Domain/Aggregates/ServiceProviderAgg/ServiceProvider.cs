using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Services.NetCore.Domain.Core;
using Services.NetCore.Crosscutting.Resources;
using Services.Workforce.Domain.Aggregates.ProviderAgg;
using Services.Workforce.Domain.Aggregates.ServiceAgg;

namespace Services.Workforce.Domain.Aggregates.ServiceProviderAgg
{
    [Table(nameof(ServiceProvider), Schema = SchemaTypes.Business)]
    public class ServiceProvider : BaseEntity
    {
        [ForeignKey(nameof(ProviderId))]
        public int ProviderId { get; set; }

        [Required, Column(TypeName = standardDecimal)]
        public decimal Rating { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public int ServiceId { get; set; }

        [Required, Column(TypeName = standardDecimal)]
        public decimal Price { get; set; }
        [Required, StringLength(shortVarcharLength)]
        public string Location { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual Service Service { get; set; }
    }
}
