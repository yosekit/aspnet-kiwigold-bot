using KiwigoldBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KiwigoldBot.Commands
{
    public class StartCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public StartCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            string text = "Hello, I'm TestBot.";

            await _messenger.SendTextAsync(text, cancellationToken: cancellationToken);
        }
    }
}
