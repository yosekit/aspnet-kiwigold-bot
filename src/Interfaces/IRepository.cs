namespace KiwigoldBot.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<T> GetAll<T>();
        public T Get<T>(int id);
        public void Remove<T>(T entity);
        public void Add<T>(T entity);
        public void Update<T>(T entity);
    }
}
