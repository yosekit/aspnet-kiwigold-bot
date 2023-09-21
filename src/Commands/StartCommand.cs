using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class StartCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public StartCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("Hello, I'm TestBot.", cancellationToken: cancellationToken);
        }

        public Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
