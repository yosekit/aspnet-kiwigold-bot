using System.Data.Common;

namespace KiwigoldBot.Interfaces
{
    public interface IDbConnectionBuilder
    {
        public DbConnection CreateConnection();
    }
}
