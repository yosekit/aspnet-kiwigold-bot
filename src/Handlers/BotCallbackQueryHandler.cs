using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCallbackQueryHandler : IBotCallbackQueryHandler
    {
        private readonly IBotCallbackManager _callbackManager;

        public BotCallbackQueryHandler(IBotCallbackManager callbackManager)
        {
            _callbackManager = callbackManager;
        }

        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            await _callbackManager.RequestAsync(callbackQuery.Data, callbackQuery.Message, cancellationToken);
        }
    }

}
