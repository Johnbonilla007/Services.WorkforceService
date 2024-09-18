namespace Services.NetCore.Domain.Core
{
    public class TransactionInfo : BaseEntity
    {
        public TransactionInfo(string createdBy, string transactionType) : base(createdBy, transactionType)
        {
        }

        public bool GenerateTransactions { get; set; }

        public List<TransactionDetail> TransactionDetail { get; set; }

        public void AddDetail(string tableName, string crudOperation, string transactionType)
        {
            if (TransactionDetail.FirstOrDefault(t => t.TableName == tableName) == null)
            {
                TransactionDetail.Add(
                    new TransactionDetail
                    {
                        TransactionId = TransactionUId,
                        TableName = tableName,
                        CrudOperation = crudOperation,
                        TransactionType = transactionType
                    });
            }
        }
    }

    public class TransactionDetail
    {
        public Guid TransactionId { get; set; }
        public string TableName { get; set; }
        public string CrudOperation { get; set; }
        public string TransactionType { get; set; }
    }
}
