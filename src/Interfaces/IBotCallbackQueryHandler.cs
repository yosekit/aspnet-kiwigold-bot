using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCallbackQueryHandler
    {
        public Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken);
    }
}
