using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.Workforce.Domain.Aggregates.UserAgg
{
    [Table(nameof(User_Transactions), Schema = SchemaTypes.Commons)]
    public class User_Transactions : BaseEntity
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(500)]
        public string Password { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; } = "Customer";
        public string Genre { get; set; }
    }
}
