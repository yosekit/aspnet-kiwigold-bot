using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class TakeCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public TakeCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
