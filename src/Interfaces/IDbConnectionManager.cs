using System.Data.Common;

namespace KiwigoldBot.Interfaces
{
    public interface IDbConnectionManager
    {
        public DbConnection GetConnection();
    }
}
