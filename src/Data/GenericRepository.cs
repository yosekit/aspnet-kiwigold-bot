using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Data
{
    public class GenericRepository : IDbRepository
    {
        private readonly IDbContext _context;

        public GenericRepository(IDbContext dbContext)
        {
            _context = dbContext;
        }

        public string? Table { get; set; }

        public IDbRepository FromTable(string name)
        {
            Table = name;

            return this;
        }

        public IEnumerable<string> GetAll()
        {
            return _context.Query(SqlQueries.SelectAll, new { Table });
        }

        public bool Add(string data)
        {
            return _context.Execute(SqlQueries.Insert, new { Table, Data = data }) > 0;
        }

        public bool Update(string data, string updated)
        {
            return _context.Execute(SqlQueries.Update, new { Table, Data = data, Updated = updated }) > 0;
        }

        public bool Remove(string data)
        {
            return _context.Execute(SqlQueries.Delete, new { Table, Data = data }) > 0;
        }
    }

    internal static class SqlQueries
    {
        public static string SelectAll { get; } = "SELECT * FROM @Table";
        public static string Insert { get; } = "INSERT INTO @Table (Data) VALUES (@Data)";
        public static string Update { get; } = "UPDATE @Table SET Data=@Updated WHERE Data=@Data";
        public static string Delete { get; } = "DELETE FROM @Table WHERE Data=@Data";
    }
}
