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

        public IEnumerable<T> GetAll<T>()
        {
            return _context.Query<T>(SqlHelper.SelectAll(GetTableName<T>()));
        }

        public T? Get<T>(int id)
        {
            try
            {
                return _context.QuerySingle<T>(SqlHelper.Select(GetTableName<T>()), new { Id = id });
            }
            catch (Exception)
            {
                return default;
            }
        }

        public bool Remove<T>(T entity)
        {
            return _context.Execute(SqlHelper.Delete(GetTableName<T>()), entity) > 0;
        }

        public bool Add<T>(T entity)
        {
            return _context.Execute(SqlHelper.Insert(GetTableName<T>(), entity!), entity) > 0;
        }

        public bool Update<T>(T entity) 
        {
            return _context.Execute(SqlHelper.Update(GetTableName<T>(), entity!), entity) > 0;
        }

        private string GetTableName<T>() => typeof(T).Name;
    }

    internal static class SqlHelper
    {
        private const string SqlSelectAll = "SELECT * FROM @Table";
        private const string SqlSelect = "SELECT * FROM @Table WHERE id = @Id";
        private const string SqlInsert = "INSERT INTO @Table (@columns) VALUES (@values)";
        private const string SqlDelete = "DELETE FROM @Table WHERE id = @Id";
        private const string SqlUpdate = "UPDATE @Table SET @params WHERE id = @Id";

        public static string SelectAll(string table) => ReplaceTable(SqlSelectAll, table);
        public static string Select(string table) => ReplaceTable(SqlSelect, table);
        public static string Delete(string table) => ReplaceTable(SqlDelete, table);
        public static string Insert(string table, object param)
        {
            var columns = param.GetType().GetProperties().Where(x => x.Name != "Id").Select(x => x.Name);

            return ReplaceTable(SqlInsert, table)
                .Replace("@columns", string.Join(",", columns))
                .Replace("@values", string.Join(",", columns.Select(x => string.Concat("@", x))));
        }

        public static string Update(string table, object param) => ReplaceTable(SqlUpdate, table)
            .Replace("@params", string.Join(",", param.GetType().GetProperties().Where(x => x.Name != "Id")
                .Select(x => string.Concat(x.Name, "=", string.Concat("@", x.Name)))));

        private static string ReplaceTable(string sql, string name) => sql.Replace("@Table", name);
    }
}
