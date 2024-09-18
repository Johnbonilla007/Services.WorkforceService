using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork.Data.Core;

namespace Services.NetCore.Infraestructure.Core
{
    public interface IGenericRepository<T> : IDisposable
       where T : IQueryableUnitOfWork

    {
        IUnitOfWork UnitOfWork { get; }

        void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void Add<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity;
        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
        void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        Task<TEntity> AddAndGetIdAsync<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity;
        Task RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
        Task RemoveRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity;
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        IQueryable<TEntity> GetAllWithoutFilters<TEntity>() where TEntity : BaseEntity;
        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(List<string> includes) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes, Expression<Func<TEntity, object>> includeFilter = null) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(List<string> includes, Expression<Func<TEntity, object>> includeFilter = null) where TEntity : BaseEntity;
        Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task AddAsync<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity;
        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool? asNoTracking = null) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity;
        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes, bool? asNoTracking = null) where TEntity : BaseEntity;
        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool? asNoTracking = null) where TEntity : BaseEntity;
        void Modify<TEntity>(TEntity item) where TEntity : BaseEntity;
        void UpdateAsync<TEntity>(TEntity item) where TEntity : BaseEntity;
        Task<IEnumerable<T>> ExecuteSqlCommandAsync<T>(SqlCommand command, object parameters = null);
        Task<IEnumerable<T>> ExecuteSqlCommandAsync<T>(SqlCommand command, object parameters, string externalConectionString);
    }
}