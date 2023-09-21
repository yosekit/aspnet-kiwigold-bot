using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class HelpCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public HelpCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken)
        {
            string message = """
                This is a help message.

                Bot commands:
                /start - start bot
                /help - help message
                """;

            await _messenger.SendTextAsync(message, cancellationToken: cancellationToken);
        }

        public Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
