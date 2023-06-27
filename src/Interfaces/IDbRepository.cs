namespace KiwigoldBot.Interfaces
{
    public interface IDbRepository
    {
        public IEnumerable<T> GetAll<T>();
        public T? Get<T>(int id);
        public bool Remove<T>(T entity);
        public bool Add<T>(T entity);
        public bool Update<T>(T entity);
    }
}
