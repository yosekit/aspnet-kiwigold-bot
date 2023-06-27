using KiwigoldBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KiwigoldBot.Commands
{
    public class StartCommand : IBotCommand
    {
        private readonly ITelegramBotClient _client;

        public StartCommand(ITelegramBotClient client)
        {
            _client = client;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            string text = "Hello, I'm TestBot.";

            await _client.SendTextMessageAsync(message.Chat.Id, text,
                cancellationToken: cancellationToken);
        }
    }
}
