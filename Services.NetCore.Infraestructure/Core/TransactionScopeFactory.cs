using System.Transactions;

namespace Services.NetCore.Infraestructure.Core
{
    public static class TransactionScopeFactory
    {
        public static TransactionScope GetTransactionScope()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
