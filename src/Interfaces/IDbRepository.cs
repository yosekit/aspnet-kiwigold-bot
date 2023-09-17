namespace KiwigoldBot.Interfaces
{
    public interface IDbRepository
    {
        public string? Table { get; set; }

        public IDbRepository FromTable(string name);

        public IEnumerable<string> GetAll();
        public bool Add(string data);
        public bool Update(string data, string updated);
        public bool Remove(string data);
    }
}
