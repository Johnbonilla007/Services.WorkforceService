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

        [Required, StringLength(200)]
        public string FullName { get; set; }

        [Required, StringLength(500)]
        public string Password { get; set; }
    }
}
