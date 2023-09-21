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
            return _context.Query(ReplaceTable(SqlQueries.SelectAll));
        }

        public bool Add(string data)
        {
            return _context.Execute(ReplaceTable(SqlQueries.Insert), new { Data = data }) > 0;
        }

        public bool Update(string data, string updated)
        {
            return _context.Execute(ReplaceTable(SqlQueries.Update), new { Data = data, Updated = updated }) > 0;
        }

        public bool Remove(string data)
        {
            return _context.Execute(ReplaceTable(SqlQueries.Delete), new { Data = data }) > 0;
        }

        private string ReplaceTable(string query) => query.Replace("@Table", Table);
    }

    internal static class SqlQueries
    {
        public static string SelectAll { get; } = "SELECT Data FROM @Table";
        public static string Insert { get; } = "INSERT INTO @Table (Data) VALUES (@Data)";
        public static string Update { get; } = "UPDATE @Table SET Data=@Updated WHERE Data=@Data";
        public static string Delete { get; } = "DELETE FROM @Table WHERE Data=@Data";
    } 
}
