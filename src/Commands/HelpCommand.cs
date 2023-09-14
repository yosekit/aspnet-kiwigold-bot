using KiwigoldBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KiwigoldBot.Commands
{
    public class HelpCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public HelpCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            string text = """
                This is a help message.

                Bot commands:
                /start - start bot
                /help - help message
                """;

            await _messenger.SendTextAsync(text, cancellationToken: cancellationToken);
        }
    }
}
