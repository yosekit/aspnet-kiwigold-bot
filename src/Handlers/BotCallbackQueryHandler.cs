using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCallbackQueryHandler : IBotCallbackQueryHandler
    {
        private readonly IBotCallbackStateManager _callbackState;
        private readonly IBotCallbackResolver _callbacks;

        public BotCallbackQueryHandler(IBotCallbackStateManager callbackState, IBotCallbackResolver callbacks)
        {
            _callbackState = callbackState;
            _callbacks = callbacks;
        }

        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            // TODO: check callbackQuery

            await RequestAsync(callbackQuery.Data, callbackQuery.Message, cancellationToken);
        }

        private async Task RequestAsync(string callbackData, Message message, CancellationToken cancellationToken)
        {
            var activeType = _callbackState.GetActive();
            if(activeType == null)
            {
                // TODO: log

                return;
            }

            var active = _callbacks.Get(activeType);
            if (active == null)
            {
                // TODO: log

                return;
            }

            await active.InvokeAsync(callbackData, message, cancellationToken);

            _callbackState.RemoveActive();
        }

    }
}
