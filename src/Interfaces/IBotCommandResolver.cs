namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandResolver
    {
        public IBotCommand? GetCommand(string name);
        public IBotCommand? GetCommand(Type type);
    }
}
