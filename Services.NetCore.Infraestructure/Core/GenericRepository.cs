using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork.Data.Core;
using Z.EntityFramework.Plus;

namespace Services.NetCore.Infraestructure.Core
{
    public class GenericRepository<T> : IGenericRepository<T>
          where T : IQueryableUnitOfWork
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public GenericRepository(IQueryableUnitOfWork UnitOfWork, IConfiguration configuration)
        {
            _unitOfWork = UnitOfWork;
            _configuration = configuration;
        }
        private DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity
        {
            DbSet<TEntity> set = _unitOfWork.CreateSet<TEntity>();

            return set;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    entity.CreationDate = DateTime.Now;

                    GetSet<TEntity>().Add(entity);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public void Add<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    entity.ModifiedBy = transactionInfo.ModifiedBy;
                    entity.TransactionType = transactionInfo.TransactionType;
                    entity.CreationDate = transactionInfo.CreationDate;
                    entity.IsActive = true;
                    entity.TransactionDateUtc = transactionInfo.TransactionDateUtc;
                    entity.TransactionDate = DateTime.Now;
                    entity.RowVersion = Array.Empty<byte>();
                    entity.TransactionUId = transactionInfo.TransactionUId;
                    entity.TransactionDescription = Transactions.Insert;

                    GetSet<TEntity>().Add(entity);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    entity.CreationDate = DateTime.Now;

                    await GetSet<TEntity>().AddAsync(entity);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task AddAsync<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    entity.ModifiedBy = transactionInfo.ModifiedBy;
                    entity.TransactionType = transactionInfo.TransactionType;
                    entity.CreationDate = DateTime.Now;
                    entity.IsActive = true;
                    entity.TransactionDateUtc = transactionInfo.TransactionDateUtc;
                    entity.TransactionDate = DateTime.Now;
                    entity.RowVersion = Array.Empty<byte>();
                    entity.TransactionUId = transactionInfo.TransactionUId;
                    entity.TransactionDescription = Transactions.Insert;

                    await GetSet<TEntity>().AddAsync(entity);

                    await _unitOfWork.CommitAsync(transactionInfo);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }


        public async Task<TEntity> AddAndGetIdAsync<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    var addedEntity = GetSet<TEntity>().Add(entity).Entity;

                    await UnitOfWork.CommitAsync(transactionInfo);

                    return addedEntity;
                }

                return null;

            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }


        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            try
            {
                if (entities != null)
                {
                    GetSet<TEntity>().AddRange(entities);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            try
            {
                if (entities != null)
                {
                    await GetSet<TEntity>().AddRangeAsync(entities);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            try
            {
                return GetSet<TEntity>().ToList();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            try
            {
                return await GetSet<TEntity>().ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (includes != null && includes.Any())
                {
                    items = includes.Aggregate(items, (current, include) =>
                    {
                        return current.Include(include);
                    });
                }

                return await items.Where(predicate).ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(List<string> includes) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (includes != null && includes.Any())
                {
                    items = includes.Aggregate(items, (current, include) =>
                    {
                        return current.Include(include);
                    });
                }

                return await items.ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public IQueryable<TEntity> GetAllWithoutFilters<TEntity>() where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                return items;
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(List<string> includes, Expression<Func<TEntity, object>> includeFilter = null) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (includes != null && includes.Any())
                {
                    items = includes.Aggregate(items, (current, include) =>
                    {
                        return current.Include(include);
                    });
                }

                if (includeFilter != null)
                {
                    items = items.Include(includeFilter);
                }

                return await items.ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes, Expression<Func<TEntity, object>> includeFilter = null) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (includes != null && includes.Any())
                {
                    items = includes.Aggregate(items, (current, include) =>
                    {
                        return current.Include(include);
                    });
                }

                if (includeFilter != null)
                {
                    items = items.Include(includeFilter);
                }

                return await items.Where(predicate).ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            try
            {
                return GetSet<TEntity>().Where(predicate).ToList();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool? asNoTracking = null) where TEntity : BaseEntity
        {
            try
            {
                if (asNoTracking.HasValue && asNoTracking.Value)
                {
                    return await GetSet<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
                }

                return await GetSet<TEntity>().Where(predicate).ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                return items.FirstOrDefault(predicate);
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes, bool? asNoTracking = null) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (includes != null && includes.Any())
                {
                    items = includes.Aggregate(items, (current, include) =>
                    {
                        return current.Include(include);
                    });
                }

                if (asNoTracking.HasValue && asNoTracking.Value)
                {
                    return await items.AsNoTracking().FirstOrDefaultAsync(predicate);
                }

                return await items.FirstOrDefaultAsync(predicate);
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool? asNoTracking = null) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (asNoTracking.HasValue && asNoTracking.Value)
                {
                    return await items.AsNoTracking().FirstOrDefaultAsync(predicate);
                }

                return await items.FirstOrDefaultAsync(predicate);
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }


        public void Modify<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            try
            {
                if (item != null)
                    _unitOfWork.SetModified(item);
                else
                {
                    //LoggerFactory.CreateLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    //attach item if not exist
                    _unitOfWork.Attach(entity);

                    //set as "removed"
                    GetSet<TEntity>().Remove(entity);
                }
                else
                {
                    //LoggerFactory.CreateLog().LogInfo("Cannot remove null entity.", typeof(TEntity).ToString());
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            try
            {
                if (predicate != null)
                {
                    //set as "removed"
                    await GetSet<TEntity>().Where(predicate).ExecuteDeleteAsync();
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    //attach item if not exist
                    _unitOfWork.Attach(entity);

                    //set as "removed"
                    await Task.Run(() => GetSet<TEntity>().Remove(entity));
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            try
            {
                if (entities != null)
                {
                    await Task.Run(() => GetSet<TEntity>().RemoveRange(entities));
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task RemoveRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            try
            {
                if (predicate != null)
                {
                    await GetSet<TEntity>().Where(predicate).ExecuteDeleteAsync();
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    entity.TransactionDate = DateTime.Now;
                    entity.TransactionDescription = Transactions.Update;

                    GetSet<TEntity>().Update(entity);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async void UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                if (entity != null)
                {
                    entity.TransactionDate = DateTime.Now;
                    entity.TransactionDescription = Transactions.Update;

                    await GetSet<TEntity>().UpdateAsync(x => x.Id == entity.Id);
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity
        {
            try
            {
                IQueryable<TEntity> items = GetSet<TEntity>();

                if (includes != null && includes.Any())
                {
                    items = includes.Aggregate(items, (current, include) =>
                    {
                        return current.Include(include);
                    });
                }

                return await items.Where(predicate).ToListAsync();
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }

        public async Task<IEnumerable<T>> ExecuteSqlCommandAsync<T>(SqlCommand command, object parameters)
        {
            try
            {
                if (command.Connection == null)
                {
                    var conectecion = new SqlConnection(_configuration.GetConnectionString("ImpExpCenterDB"));
                    command.Connection = conectecion;
                }

                if (command.Connection.State == ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync();
                }

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = command.Connection;
                    cmd.CommandText = command.CommandText;
                    cmd.CommandType = command.CommandType;
                    cmd.CommandTimeout = command.CommandTimeout;

                    if (parameters != null)
                    {
                        var props = parameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                        foreach (var prop in props)
                        {
                            var parameter = new SqlParameter($"@{prop.Name}", prop.GetValue(parameters));
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var result = new List<T>();

                        while (await reader.ReadAsync())
                        {
                            var obj = Activator.CreateInstance<T>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var property = typeof(T).GetProperty(reader.GetName(i), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                                if (property != null && !reader.IsDBNull(i))
                                {
                                    var value = reader.GetValue(i);
                                    property.SetValue(obj, value);
                                }
                            }

                            result.Add(obj);
                        }

                        return result;
                    }
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
        public async Task<IEnumerable<T>> ExecuteSqlCommandAsync<T>(SqlCommand command, object parameters, string externalConectionString)
        {
            try
            {
                if (command.Connection == null)
                {
                    command.Connection = new SqlConnection(externalConectionString);
                }

                if (command.Connection.State == ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync();
                }

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = command.Connection;
                    cmd.CommandText = command.CommandText;
                    cmd.CommandType = command.CommandType;
                    cmd.CommandTimeout = command.CommandTimeout;

                    if (parameters != null)
                    {
                        var props = parameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                        foreach (var prop in props)
                        {
                            var parameter = new SqlParameter($"@{prop.Name}", prop.GetValue(parameters));
                            cmd.Parameters.Add(parameter);
                        }
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var result = new List<T>();

                        while (await reader.ReadAsync())
                        {
                            var obj = Activator.CreateInstance<T>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var property = typeof(T).GetProperty(reader.GetName(i), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                                if (property != null && !reader.IsDBNull(i))
                                {
                                    var value = reader.GetValue(i);
                                    property.SetValue(obj, value);
                                }
                            }

                            result.Add(obj);
                        }

                        return result;
                    }
                }
            }
            catch (RepositoryException exception)
            {
                throw new RepositoryException(exception.Message, exception.InnerException);
            }
        }
    }
}