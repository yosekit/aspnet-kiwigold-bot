using Dapper;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Data
{
    public class DapperContext : IDbContext
    {
        private readonly IDbConnectionManager _connectionManager;

        public DapperContext(IDbConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public IEnumerable<T> Query<T>(string sql, object? param = null)
        {
            using var conn = _connectionManager.GetConnection();

            return conn.Query<T>(sql, param);
        }

        public T QuerySingle<T>(string sql, object? param = null)
        {
            using var conn = _connectionManager.GetConnection();

            return conn.QuerySingle<T>(sql, param);
        }

        public int Execute(string sql, object? param = null)
        {
            using var conn = _connectionManager.GetConnection();

            return conn.Execute(sql, param);
        }
    }
}
