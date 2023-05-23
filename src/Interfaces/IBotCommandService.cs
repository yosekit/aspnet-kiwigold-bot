using KiwigoldBot.Commands;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandService
    {
        public BotCommandBase? Get(string name);
    }
}
