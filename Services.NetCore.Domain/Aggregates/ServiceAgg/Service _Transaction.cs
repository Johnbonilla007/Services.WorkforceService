
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Workforce.Domain.Aggregates.ServiceAgg
{
    [Table(nameof(Service_Transactions), Schema = SchemaTypes.Commons)]
    public class Service_Transactions : BaseEntity
    {
        [Required, StringLength(longVarcharLength)]
        public string Name { get; set; }

        [Required, StringLength(longVarcharLength)]
        public string Description { get; set; }

        [Required, StringLength(shortVarcharLength)]
        public string IconName { get; set; }

    }
}
