using KiwigoldBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KiwigoldBot.Commands
{
    public class HelpCommand : IBotCommand
    {
        private readonly ITelegramBotClient _client;

        public HelpCommand(ITelegramBotClient client)
        {
            _client = client;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            string text = """
                This is a help message.

                Bot commands:
                /start - start bot
                /help - help message
                """;

            await _client.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: cancellationToken);
        }
    }
}
