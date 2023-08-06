using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Helpers;

namespace KiwigoldBot.Handlers
{
    public class BotCallbackQueryHandler : IBotCallbackQueryHandler
    {
        private readonly IBotCallbackResolver _callbacks;

        public BotCallbackQueryHandler(IBotCallbackResolver callbacks)
        {
            _callbacks = callbacks;
        }

        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            // TODO: check callbackQuery on Data is null

            (Type callbackType, string callbackData) = BotCallbackDataConvert.ToTypeAndData(callbackQuery.Data!);

            var callback = _callbacks.Get(callbackType);

            if (callback == null)
            {
                // TODO: log

                return;
            }

            await callback.InvokeAsync(callbackData, callbackQuery.Message!, cancellationToken);
        }
    }
}
