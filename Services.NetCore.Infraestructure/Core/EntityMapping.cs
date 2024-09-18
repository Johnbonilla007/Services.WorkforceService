using System;

namespace Services.NetCore.Infraestructure.Core
{
    public class EntityMapping
    {
        public Type EntityType { get; set; }
        public string TableName { get; set; }
        public string TransactionTableName { get; set; }
    }
}
