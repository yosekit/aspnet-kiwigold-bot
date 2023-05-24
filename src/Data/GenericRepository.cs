﻿using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Data
{
    public class GenericRepository : IRepository
    {
        private readonly IDbContext _context;

        public GenericRepository(IDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<T> GetAll<T>() => _context.Query<T>(SqlHelper.SelectAll(GetTableName<T>()));
        public T Get<T>(int id) => _context.QuerySingle<T>(SqlHelper.Select(GetTableName<T>()), new { Id = id });
        public void Remove<T>(T entity) => _context.Execute(SqlHelper.Delete(GetTableName<T>()), entity);
        public void Add<T>(T entity) => _context.Execute(SqlHelper.Insert(GetTableName<T>(), entity!), entity);
        public void Update<T>(T entity) => _context.Execute(SqlHelper.Update(GetTableName<T>(), entity!), entity);

        private string GetTableName<T>() => typeof(T).Name;
    }

    internal static class SqlHelper
    {
        private const string SqlSelectAll = "SELECT * FROM @Table";
        private const string SqlSelect = "SELECT * FROM @Table WHERE id = @Id";
        private const string SqlInsert = "INSERT INTO @Table VALUES (@params)";
        private const string SqlDelete = "DELETE FROM @Table WHERE id = @Id";
        private const string SqlUpdate = "UPDATE @Table SET @params WHERE id = @Id";

        public static string SelectAll(string table) => ReplaceTable(SqlSelectAll, table);
        public static string Select(string table) => ReplaceTable(SqlSelect, table);
        public static string Delete(string table) => ReplaceTable(SqlDelete, table);
        public static string Insert(string table, object param) => ReplaceTable(SqlInsert, table)
            .Replace("@params", string.Join(",", param.GetType().GetProperties()
                .Where(x => x.Name != "Id")
                .Select(x => x.GetValue(param, null)!.ToString())));
        public static string Update(string table, object param) => ReplaceTable(SqlUpdate, table)
            .Replace("@params", string.Join(",", param.GetType().GetProperties()
                .Where(x => x.Name != "Id")
                .Select(x => string.Concat(x.Name, " = ", x.GetValue(param, null)!.ToString()))));

        private static string ReplaceTable(string sql, string name) => sql.Replace("@Table", name);
    }
}
