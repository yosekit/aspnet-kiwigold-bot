using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotCommandResolver : IBotCommandResolver
    {
        private readonly IEnumerable<IBotCommand> _commands;

        public BotCommandResolver(IEnumerable<IBotCommand> commands)
        {
            _commands = commands;
        }

        public IBotCommand? Get(string name)
        {
            return _commands.FirstOrDefault(x =>
                name == string.Concat("/", x.GetType().Name.Replace("Command", "").ToLower()));
        }
    }
}
