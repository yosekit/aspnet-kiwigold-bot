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

        public IEnumerable<string> Query(string sql, object? param = null)
        {
            using var conn = _connectionManager.GetConnection();

            return conn.Query<string>(sql, param);
        }

        public string QuerySingle(string sql, object? param = null)
        {
            using var conn = _connectionManager.GetConnection();

            return conn.QuerySingle<string>(sql, param);
        }

        public int Execute(string sql, object? param = null)
        {
            using var conn = _connectionManager.GetConnection();

            return conn.Execute(sql, param);
        }
    }
}
