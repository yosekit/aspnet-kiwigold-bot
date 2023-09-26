using Dapper;

using KiwigoldBot.Interfaces;
using System.Data.Common;

namespace KiwigoldBot.Data
{
    public class DapperContext : IDbContext
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public DapperContext(DapperSettings settings)
        {
            _providerFactory = settings.Provider;
            _connectionString = settings.ConnectionString;
        }

        public IEnumerable<string> Query(string sql, object? param = null)
        {
            IEnumerable<string> rows;
            using (var conn = CreateConnection())
            {
                conn.Open();
                rows = conn.Query<string>(sql, param);
            }
            
            return rows;
        }

        public string QuerySingle(string sql, object? param = null)
        {
            string row;
            using (var conn = CreateConnection())
            {
                conn.Open();
                row = conn.QuerySingle<string>(sql, param);
            }

            return row;
        }

        public int Execute(string sql, object? param = null)
        {
            int rowsAffected;
            using (var conn = CreateConnection())
            {
                conn.Open();
                rowsAffected = conn.Execute(sql, param);
            }

            return rowsAffected;
        }

        private DbConnection CreateConnection()
        {
            var conn = _providerFactory.CreateConnection();

            if (conn != null)
            {
                conn.ConnectionString = _connectionString;
                return conn;
            }

            // TODO: set right exception
            throw new Exception("DbConnection is null");
        }
    }
}
