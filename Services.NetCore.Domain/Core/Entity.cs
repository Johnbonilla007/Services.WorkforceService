using System;

namespace Services.NetCore.Domain.Core
{
    public class Entity
    {
        public string TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
