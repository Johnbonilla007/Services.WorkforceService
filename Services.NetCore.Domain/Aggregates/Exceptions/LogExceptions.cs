using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.NetCore.Domain.Aggregates.Exceptions
{
    [Table(nameof(LogExceptions), Schema = SchemaTypes.ExceptionHandler)]
    public class LogExceptions : BaseEntity
    {
        public string Message { get; set; }
        public string ExceptionError { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }

        public virtual ICollection<RequestParameter> RequestParameters { get; set; }
    }
}
