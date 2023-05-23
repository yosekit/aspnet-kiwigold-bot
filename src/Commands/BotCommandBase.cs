using Telegram.Bot;
using Telegram.Bot.Types;

namespace KiwigoldBot.Commands
{
    public abstract class BotCommandBase
    {
        protected readonly ITelegramBotClient _client;

        public BotCommandBase(ITelegramBotClient client)
        {
            _client = client;
        }

        public abstract Task ExecuteAsync(Message message, CancellationToken cancellationToken);
    }
}
