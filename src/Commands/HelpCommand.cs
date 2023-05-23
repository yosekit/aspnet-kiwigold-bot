using Telegram.Bot;
using Telegram.Bot.Types;

namespace KiwigoldBot.Commands
{
    public class HelpCommand : BotCommandBase
    {
        public HelpCommand(ITelegramBotClient client) : base(client)
        {
        }

        public override async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
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
