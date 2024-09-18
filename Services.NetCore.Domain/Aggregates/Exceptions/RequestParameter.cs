using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.NetCore.Domain.Aggregates.Exceptions
{
    [Table(nameof(RequestParameter), Schema = SchemaTypes.ExceptionHandler)]
    public class RequestParameter : BaseEntity
    {
        public string Property { get; set; }
        public string Value { get; set; }

        public virtual LogExceptions LogException { get; set; }
    }
}
