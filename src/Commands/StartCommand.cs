using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KiwigoldBot.Commands
{
    public class StartCommand : BotCommandBase
    {
        public StartCommand(ITelegramBotClient client) : base(client)
        {
        }

        public override async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            string text = "Hello, I'm TestBot.";

            await _client.SendTextMessageAsync(message.Chat.Id, text,
                cancellationToken: cancellationToken);
        }
    }
}
