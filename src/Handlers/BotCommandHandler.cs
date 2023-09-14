using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCommandHandler : IBotCommandHandler
    {
        private readonly IBotCommandResolver _commands;
        private readonly IBotCommandPoolManager _commandPool;

        public BotCommandHandler(IBotCommandResolver commands, IBotCommandPoolManager commandPool)
        {
            _commands = commands;
            _commandPool = commandPool;
        }

        public bool IsCommandActive => _commandPool.IsActive;

        public async Task ExecuteActiveCommandAsync(Message message, CancellationToken cancellationToken)
        {
            await _commandPool.ExecuteLastAsync(message, cancellationToken);
        }

        public async Task ExecuteNewCommandAsync(Message message, CancellationToken cancellationToken)
        {
            _commandPool.Clear();

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
