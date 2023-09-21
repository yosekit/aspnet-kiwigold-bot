using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCommandHandler : IBotCommandHandler
    {
        private readonly IBotCommandResolver _commands;
        private readonly IBotCommandState _commandState;

        public BotCommandHandler(IBotCommandResolver commands, IBotCommandState commandPool)
        {
            _commands = commands;
            _commandState = commandPool;
        }

        public bool IsCommandActive => _commandState.IsActive;

        public async Task ExecuteNewCommandAsync(string commandName, string[]? commandArgs, CancellationToken cancellationToken)
        {
            var command = _commands.GetCommand(commandName);
            if (command == null)
            {
                // TODO: log

                return;
            }

            _commandState.ClearActive();
            _commandState.SetActive(command.GetType());

            await command.ExecuteAsync(commandArgs, _commandState.Context, cancellationToken);
        }

        public async Task ExecuteNewCommandFromNextAsync(string commandName, string text, CancellationToken cancellationToken)
        {
            var command = _commands.GetCommand(commandName);
            if (command == null)
            {
                // TODO: log

                return;
            }

            _commandState.ClearActive();
            _commandState.SetActive(command.GetType());

            await command.ExecuteNextAsync(text, _commandState.Context, cancellationToken);
        }

        public async Task ExecuteActiveCommandAsync(string text, CancellationToken cancellationToken)
        {
            Type active = _commandState.GetActive()!;

            var command = _commands.GetCommand(active);
            if(command == null)
            {
                // TODO: log

                return;
            }

            await command.ExecuteNextAsync(text, _commandState.Context, cancellationToken);
        }

        public async Task ExecuteCallbackCommandAsync(Type commandType, string callbackData, CancellationToken cancellationToken)
        {
            if (commandType == _commandState.GetActive())
            {
                await this.ExecuteActiveCommandAsync(callbackData, cancellationToken);
            }
            else
            {
                var command = _commands.GetCommand(commandType);
                if (command == null)
                {
                    // TODO: log

                    return;
                }

                await this.ExecuteNewCommandFromNextAsync(command.GetName(), callbackData, cancellationToken);
            }
        }
    }
}
