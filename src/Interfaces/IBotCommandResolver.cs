namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandResolver
    {
        // e.g. "/start" => StartCommand
        public IBotCommand? Get(string name);
    }
}
