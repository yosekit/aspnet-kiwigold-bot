using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCommandHandler : IBotCommandHandler
    {
        private readonly Dictionary<string, IBotCommand> _commands;

        public BotCommandHandler(IEnumerable<IBotCommand> commands)
        {
            _commands = MapCommands(commands);
        }

        public async Task HandleCommandAsync(Message message, CancellationToken cancellationToken)
        {
            var command = Get(message.Text!);

            if (command == null) return;

            await command.ExecuteAsync(message, cancellationToken);
        }

        private static Dictionary<string, IBotCommand> MapCommands(IEnumerable<IBotCommand> commands) =>
            commands.ToDictionary(x => string.Concat("/", x.GetType().Name.Replace("Command", "").ToLower()));

        // e.g. /help => HelpCommand
        private IBotCommand? Get(string name) => _commands.GetValueOrDefault(name);
    }
}
