using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork.Data.Core;

namespace Services.NetCore.Infraestructure.Data.UnitOfWork
{
    public class DataContext : DbContext, IQueryableUnitOfWork
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {

        }

        public void AddDynamicDbSet(ModelBuilder modelBuilder)
        {
            var entityTypes = Assembly.Load("Services.Workforce.Domain").GetTypes()
                  .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(BaseEntity)) && type.Namespace.StartsWith("Services.Workforce.Domain.Aggregates"));

            foreach (var entityType in entityTypes)
            {
                var entity = modelBuilder.Entity(entityType);

                if (entityType.Name.EndsWith("_Transactions"))
                {
                    entity.HasKey("UId");
                    entity.Property<int>("UId").ValueGeneratedOnAdd();
                }
                else
                {
                    entity.HasKey("Id");
                    entity.Property<int>("Id").ValueGeneratedOnAdd();
                    entity.Ignore("UId");
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var clrProperty = property.PropertyInfo;

                    // Skip properties with [NotMapped] attribute 
                    if (clrProperty != null && Attribute.IsDefined(clrProperty, typeof(NotMappedAttribute)))
                    {
                        continue;
                    }

                    if (clrProperty != null)
                    {
                        // Handle [StringLength] attribute 
                        var stringLengthAttribute = clrProperty.GetCustomAttribute<StringLengthAttribute>();
                        if (stringLengthAttribute != null)
                        {
                            property.SetMaxLength(stringLengthAttribute.MaximumLength);
                        }

                        // Handle [MaxLength] attribute 
                        var maxLengthAttribute = clrProperty.GetCustomAttribute<MaxLengthAttribute>();
                        if (maxLengthAttribute != null)
                        {
                            property.SetMaxLength(maxLengthAttribute.Length);
                        }

                        // Handle [Unicode] attribute (by default set to false if not found) 
                        var unicodeAttribute = clrProperty.GetCustomAttribute<UnicodeAttribute>();
                        if (unicodeAttribute != null)
                        {
                            property.SetIsUnicode(unicodeAttribute.IsUnicode);
                        }
                        else if (property.ClrType == typeof(string))
                        {
                            property.SetIsUnicode(false);
                        }

                        // Default string length 
                        if (property.ClrType == typeof(string) && !property.GetMaxLength().HasValue)
                        {
                            property.SetMaxLength(255);
                        }
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("EfCore_dbo");

            AddDynamicDbSet(modelBuilder);
        }

        void IQueryableUnitOfWork.ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
        {
            //if it is not attached, attach original and set current values
            Entry(original).CurrentValues.SetValues(current);
        }

        void IQueryableUnitOfWork.Attach<TEntity>(TEntity item)
        {
            //attach and set as unchanged
            Entry(item).State = EntityState.Unchanged;
        }

        void IUnitOfWork.Commit()
        {
            base.SaveChanges();
        }


        async Task IUnitOfWork.CommitAsync()
        {
            await base.SaveChangesAsync();
        }

        void IUnitOfWork.CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);
        }

        async void IUnitOfWork.CommitAndRefreshChangesAsync()
        {
            bool saveFailed;

            do
            {
                try
                {
                    await base.SaveChangesAsync();

                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);
        }

        DbSet<TEntity> IQueryableUnitOfWork.CreateSet<TEntity>()
        {
            return Set<TEntity>();
        }

        void IQueryableUnitOfWork.SetModified<TEntity>(TEntity item)
        {
            throw new NotImplementedException();
        }


        private void AplicarInformacionTransaccion(EntityEntry item, string nombrePropiedad, object valorPropiedad)
        {
            if (item != null && item.Entity != null)
            {
                PropertyInfo propInfoEntity = item.Entity.GetType().GetProperty(nombrePropiedad);
                if (propInfoEntity != null)
                {
                    propInfoEntity.SetValue(item.Entity, valorPropiedad, null);
                }
            }
        }

        private IEnumerable<EntityEntry> GetChangedDbEntityEntries()
        {
            return ChangeTracker.Entries().Where(
                e =>
                (e.Entity is Entity || e.Entity is BaseEntity) &&
                (e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted));
        }
        private void ApplyTransactionInfo(TransactionInfo transaction, EntityEntry entry)
        {
            ((BaseEntity)entry.Entity).TransactionDate = DateTime.Now;
            ((BaseEntity)entry.Entity).TransactionDescription = entry.State.ToString();
            ((BaseEntity)entry.Entity).ModifiedBy = transaction.ModifiedBy;

            if (!((BaseEntity)entry.Entity).CreationDate.HasValue)
            {
                ((BaseEntity)entry.Entity).CreationDate = DateTime.Now;
            }


            AplicarInformacionTransaccion(entry, "TransactionType", transaction.TransactionType);
            AplicarInformacionTransaccion(entry, "TransactionUId", transaction.TransactionUId);
            AplicarInformacionTransaccion(entry, "TransactionDateUtc", transaction.TransactionDateUtc);
        }

        private string GetTransactionTableName(string tname)
        {
            if (tname.Contains("_Transactions"))
            {
                return tname;
            }

            string result = string.Format("{0}_Transactions", tname);

            return result;
        }

        private EntityMapping CreateTableMapping(Type type, string tname)
        {
            return new EntityMapping { EntityType = type, TableName = tname, TransactionTableName = GetTransactionTableName(tname) };
        }

        private Type GetDomainEntityType(EntityEntry entry)
        {
            Type type = entry.Entity.GetType();
            if (type.FullName != null)
            {
                if (type.FullName.Contains("ShippingCenter.Domain.Aggregates") || type.FullName.Contains("T4"))
                {
                    return type;
                }
                if (type.BaseType != null)
                {
                    return type.BaseType;
                }
            }

            return null;
        }

        private EntityMapping GetEntityMappingConfiguration(List<EntityMapping> tableMapping, EntityEntry entry)
        {
            var type = GetDomainEntityType(entry);

            var name = entry.Metadata.GetTableName();
            var schema = entry.Metadata.GetSchema();

            string nameTable = string.Format("{0}.{1}", schema, name);

            EntityMapping entityMapping = tableMapping.FirstOrDefault(m => m.EntityType == type);
            if (entityMapping == null)
            {
                entityMapping = CreateTableMapping(type, nameTable);
                tableMapping.Add(entityMapping);
            }
            return entityMapping;
        }

        private List<string> GetPropertiesEntity(EntityEntry entry, PropertyValues originalValues)
        {
            List<string> propertyNames = new();
            var entity = entry.Entity;
            var entityType = entity.GetType();

            var properties = entry.OriginalValues.Properties;

            foreach (var prop in properties)
            {
                if (entityType.GetProperty(prop.Name) == null)
                    continue;
                var pp = entityType.GetProperty(prop.Name);
                if (pp.GetValue(entity) == null)
                    continue;

                propertyNames.Add(prop.Name);
            }

            return propertyNames;
        }

        private void TryGeTransactionInfo(string property, TransactionInfo transaction, out object value)
        {
            switch (property)
            {
                case "TransactionUId":
                    value = transaction.TransactionUId;
                    break;

                case "TransactionType":
                    value = transaction.TransactionType;
                    break;

                case "TransactionDate":
                    value = transaction.TransactionDate;
                    break;

                case "TransactionDateUtc":
                    value = transaction.TransactionDateUtc;
                    break;

                case "ModifiedBy":
                    value = transaction.ModifiedBy;
                    break;

                case "RowVersion":
                    value = transaction.RowVersion;
                    break;

                default:
                    value = null;
                    break;
            }
        }


        private object GetEntityPropertyValue(EntityEntry entry, string prop, TransactionInfo transaction)
        {
            object value;
            TryGeTransactionInfo(prop, transaction, out value);
            if (value != null)
            {
                return value;
            }

            if (entry.State == EntityState.Deleted || entry.State == EntityState.Detached)
            {
                return prop == "TransactionDescription"
                           ? EntityState.Deleted.ToString()
                           : entry.Property(prop).OriginalValue;
            }
            var ret = entry.Property(prop).CurrentValue;

            return ret;
        }

        private void CreateTransactionInsertStatement(EntityMapping entityMapping, EntityEntry entry,
                                                     TransactionInfo transaction, out string sqlInsert, out object[] objects)
        {
            var insert = new StringBuilder();
            var fields = new StringBuilder();
            var paramNames = new StringBuilder();
            var values = new List<object>();

            insert.AppendLine(string.Format("Insert Into {0} ", entityMapping.TransactionTableName));

            int index = 0;

            IEnumerable<string> propertyNames = entry.State == EntityState.Deleted
                                                    ? GetPropertiesEntity(entry, entry.OriginalValues)
                                                    : GetPropertiesEntity(entry, entry.CurrentValues); ;

            foreach (string property in propertyNames)
            {
                if (property == "UId") continue;

                string prop = property;
                if (prop != "RowVersion")
                {
                    if (fields.Length == 0)
                    {
                        fields.Append(string.Format(" ({0}", prop));
                        paramNames.Append(string.Format(" values ({0}{1}{2}", "{", index, "}"));
                    }
                    else
                    {
                        fields.Append(string.Format(", {0}", prop));
                        paramNames.Append(string.Format(", {0}{1}{2}", "{", index, "}"));
                    }

                    values.Add(GetEntityPropertyValue(entry, prop, transaction));
                    index++;
                }
            }

            fields.Append(string.Format(") "));
            paramNames.Append(string.Format(") "));

            insert.AppendLine(fields.ToString());
            insert.AppendLine(paramNames.ToString());

            sqlInsert = insert.ToString();
            objects = values.ToArray();
        }

        private SqlCommandInfo GetSqlCommandInfo(TransactionInfo transaction, EntityEntry entry, EntityMapping entityMapping)
        {
            if (entityMapping.TableName.Contains("_Transacciones"))
            {
                return null;
            }

            string sqlInsert;
            object[] param;
            CreateTransactionInsertStatement(entityMapping, entry, transaction, out sqlInsert, out param);

            var sqlCommandInfo = new SqlCommandInfo(sqlInsert, param);
            return sqlCommandInfo;
        }


        public async Task CommitAsync(TransactionInfo transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (string.IsNullOrWhiteSpace(transaction.TransactionType)) throw new ArgumentException(nameof(transaction.TransactionType));
            if (string.IsNullOrWhiteSpace(transaction.ModifiedBy)) throw new ArgumentException(nameof(transaction.ModifiedBy));
            transaction.TransactionDate = DateTime.Now;

            try
            {
                base.Database.OpenConnection();
                // Resetenado el detalla de las transacciones.
                transaction.TransactionDetail = new List<TransactionDetail>();

                using (var scope = TransactionScopeFactory.GetTransactionScope())
                {
                    var changedEntities = new List<ModifiedEntityEntry>();
                    var tableMapping = new List<EntityMapping>();
                    var sqlCommandInfos = new List<SqlCommandInfo>();

                    IEnumerable<EntityEntry> changedDbEntityEntries = GetChangedDbEntityEntries();

                    //Actualiza la FechaTransaccion de cada entidad agregada, modificada o eliminada con la fecha del servidor
                    foreach (EntityEntry entry in changedDbEntityEntries)
                    {
                        ApplyTransactionInfo(transaction, entry);

                        if (transaction.GenerateTransactions)
                        {
                            // Get the deleted records info first
                            if (entry.State == EntityState.Deleted)
                            {
                                EntityMapping entityMapping = GetEntityMappingConfiguration(tableMapping, entry);
                                SqlCommandInfo sqlCommandInfo = GetSqlCommandInfo(transaction, entry, entityMapping);
                                if (sqlCommandInfo != null) sqlCommandInfos.Add(sqlCommandInfo);

                                transaction.AddDetail(entityMapping.TableName, entry.State.ToString(), transaction.TransactionType);
                            }
                            else
                            {
                                changedEntities.Add(new ModifiedEntityEntry(entry, entry.State.ToString()));
                            }
                        }
                    }

                    await base.SaveChangesAsync();

                    if (transaction.GenerateTransactions)
                    {
                        // Get the Added and Mdified records after changes, that way we will be able to get the generated .
                        foreach (ModifiedEntityEntry entry in changedEntities)
                        {
                            EntityMapping entityMapping = GetEntityMappingConfiguration(tableMapping, entry.EntityEntry);
                            SqlCommandInfo sqlCommandInfo = GetSqlCommandInfo(transaction, entry.EntityEntry, entityMapping);
                            if (sqlCommandInfo != null) sqlCommandInfos.Add(sqlCommandInfo);

                            transaction.AddDetail(entityMapping.TableName, entry.State, transaction.TransactionType);
                        }

                        // Adding Audit Detail Transaction CommandInfo.
                        //sqlCommandInfos.AddRange(GetAuditRecords(transaction));

                        // Insert Transaction and audit records.
                        foreach (SqlCommandInfo sqlCommandInfo in sqlCommandInfos)
                        {
                            await Database.ExecuteSqlRawAsync(sqlCommandInfo.Sql, sqlCommandInfo.Parameters);
                        }
                    }

                    scope.Complete();
                }
            }
            finally
            {
                base.Database.CloseConnection();
            }
        }
    }

}