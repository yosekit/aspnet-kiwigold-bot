using System.Data;
using System.Data.Common;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Data
{
    public class DbConnectionManager : IDbConnectionManager
    {
        private readonly DbProviderFactory _providerFactory;
        private readonly string _connectionString;

        public DbConnectionManager(DbProviderFactory providerFactory, string connectionString)
        {
            _providerFactory = providerFactory;
            _connectionString = connectionString;
        }

        public DbConnection GetConnection()
        {
            var conn = _providerFactory.CreateConnection();

            if (conn != null || conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                return conn;
            }
            else throw new Exception("DB Connection is not established");
        }
    }
}
