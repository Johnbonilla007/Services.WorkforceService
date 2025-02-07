using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.Workforce.Domain.Aggregates.AddressAgg
{
    [Table(nameof(Address_Transactions), Schema = SchemaTypes.UserProfile)]
    public class Address_Transactions : BaseEntity
    {
        [StringLength(100)]
        public string HouseNumber { get; set; }

        [Required, StringLength(200)]
        public string Reference { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Coordinates { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; }

        [Required, StringLength(200)]
        public string Alias { get; set; }
    }
}
