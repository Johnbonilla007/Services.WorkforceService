using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;
using Services.Workforce.Domain.Aggregates.UserAgg;

namespace Services.Workforce.Domain.Aggregates.AddressAgg
{
    [Table(nameof(Address), Schema = SchemaTypes.UserProfile)]
    public class Address : BaseEntity
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

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        [Required, StringLength(200)]
        public string Alias { get; set; }

        public bool IsDefault { get; set; }

        public virtual User User { get; set; }
    }
}
