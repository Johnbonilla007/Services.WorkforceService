namespace Services.NetCore.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void CommitAndRefreshChanges();
        void CommitAndRefreshChangesAsync();
        Task CommitAsync(TransactionInfo transactionInfo);
    }
}