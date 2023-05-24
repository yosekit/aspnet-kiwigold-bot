namespace KiwigoldBot.Interfaces
{
    public interface IDbContext
    {
        public IEnumerable<T> Query<T>(string sql, object? param = null);
        public T QuerySingle<T>(string sql, object? param = null);
        public int Execute(string sql, object? param = null);
    }
}
