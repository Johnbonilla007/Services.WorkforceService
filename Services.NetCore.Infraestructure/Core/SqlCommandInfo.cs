using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Services.NetCore.Infraestructure.Core
{
    public class SqlCommandInfo
    {
        public SqlCommandInfo(string sql, object[] parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        public string Sql { get; set; }
        public object[] Parameters { get; set; }

        private SqlCommandInfo()
        {
        }

        public string NameOrConnectionString { get; private set; }
        public string SqlQuery { get; private set; }
        public string StoredProcedureName { get; private set; }
        public string SqlLogs { get; private set; }

        public IEnumerable<SqlParameter> SqlParameters { get; private set; }

        public Dictionary<string, object> Parametros
        {
            get
            {
                return _parameters ?? (_parameters = new Dictionary<string, object>());
            }

            private set
            {
                _parameters = value;
            }
        }

        private Dictionary<string, object> _parameters;

        public int CommandTimeout { get; private set; } = 30;

        internal void SetSqlLogs(string sqlLogs)
        {
            SqlLogs = sqlLogs;
        }

        /// <summary>
        /// Get the parameters to be used to execute a Transact-SQL statement.
        /// </summary>
        /// <returns></returns>
        public object[] GetParameters()
        {
            return (from qry in Parametros select qry.Value).ToArray();
        }

        public class Builder
        {
            private readonly SqlCommandInfo _sqlCommandInfo;

            public Builder()
            {
                _sqlCommandInfo = new SqlCommandInfo();
            }

            public Builder WithNameOrConnectionString(string nameOrConnectionString)
            {
                _sqlCommandInfo.NameOrConnectionString = nameOrConnectionString;
                return this;
            }

            public Builder WithSqlParameters(IEnumerable<SqlParameter> parameters)
            {
                _sqlCommandInfo.SqlParameters = parameters;
                return this;
            }

            /// <summary>
            /// Sets the Transact-SQL statement, table name or stored procedure to execute at the
            /// data source.
            /// </summary>
            /// <param name="sqlQuery">The Transact-SQL statement to execute.</param>
            /// <returns>The <see cref="SqlCommandInfo"/> Builder.</returns>
            public Builder WithSqlQuery(string sqlQuery)
            {
                _sqlCommandInfo.SqlQuery = sqlQuery;
                return this;
            }

            /// <summary>
            /// Sets the stored procedure to execute at the data source.
            /// </summary>
            /// <param name="storedProcedureName">The stored procedure to execute.</param>
            /// <returns>The <see cref="SqlCommandInfo"/> Builder.</returns>
            public Builder WithStoredProcedureName(string storedProcedureName)
            {
                _sqlCommandInfo.StoredProcedureName = storedProcedureName;
                return this;
            }

            /// <summary>
            /// Represents a collection of parameters associated with a <see cref="SqlCommandInfo"/>.
            /// </summary>
            /// <param name="parameters">The parameters</param>
            /// <returns>The <see cref="SqlCommandInfo"/> Builder.</returns>
            public Builder WithParameters(Dictionary<string, object> parameters)
            {
                _sqlCommandInfo.Parametros = parameters;
                return this;
            }

            /// <summary>
            /// Sets the timeout value, in seconds, for all context operations. The default value is
            /// 0, where 0 indicates that the default value of the underlying provider will be used.
            /// </summary>
            /// <param name="commandTimeout">The timeout value, in seconds, for all context operations.</param>
            /// <returns>The <see cref="SqlCommandInfo"/> Builder.</returns>
            public Builder WithCommandTimeout(int commandTimeout)
            {
                int timeout = commandTimeout < 0 ? 30 : commandTimeout;

                _sqlCommandInfo.CommandTimeout = timeout;

                return this;
            }

            public SqlCommandInfo Build()
            {
                return _sqlCommandInfo;
            }
        }
    }
}
