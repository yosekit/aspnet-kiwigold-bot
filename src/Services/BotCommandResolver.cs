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

        public IBotCommand? GetCommand(string name) => 
            _commands.FirstOrDefault(x => name == x.GetName());

        public IBotCommand? GetCommand(Type type) =>
            _commands.FirstOrDefault(x => type == x.GetType());
    }
}
