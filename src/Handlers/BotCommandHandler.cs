using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCommandHandler : IBotCommandHandler
    {
        private readonly IBotCommandResolver _commands;

        public BotCommandHandler(IBotCommandResolver commands)
        {
            _commands = commands;
        }

        public async Task HandleCommandAsync(Message message, CancellationToken cancellationToken)
        {
            string name = message.Text!;

            var command = _commands.Get(name);
            if (command == null) 
            {
                // TODO: log

                return;
            }

            await command.ExecuteAsync(message, cancellationToken);
        }
    }
}
