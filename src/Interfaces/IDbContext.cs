namespace KiwigoldBot.Interfaces
{
    public interface IDbContext
    {
        public IEnumerable<string> Query(string sql, object? param = null);
        public string QuerySingle(string sql, object? param = null);
        public int Execute(string sql, object? param = null);
    }
}
