using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotCallbackQueryHandler : IBotCallbackQueryHandler
    {
        private readonly IBotCallbackStateManager _stateManager;
        private readonly IBotCallbackResolver _callbacks;

        public BotCallbackQueryHandler(IBotCallbackStateManager stateManager, IBotCallbackResolver callbacks)
        {
            _stateManager = stateManager;
            _callbacks = callbacks;

        }

        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            // TODO: check callbackQuery

            await RequestAsync(callbackQuery.Data, callbackQuery.Message, cancellationToken);
        }

        private async Task RequestAsync(string callbackData, Message message, CancellationToken cancellationToken)
        {
            var activeType = _stateManager.GetActive();
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

            _stateManager.RemoveActive();
        }

    }
}
