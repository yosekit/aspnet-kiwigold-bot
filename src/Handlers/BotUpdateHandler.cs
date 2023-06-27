using System.Diagnostics;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotUpdateHandler : IBotUpdateHandler
    {
        private readonly IBotMessageHandler _messageHandler;
        private readonly IBotCallbackQueryHandler _callbackQueryHandler;

        public BotUpdateHandler(IBotMessageHandler messageHandler, IBotCallbackQueryHandler callbackQueryHandler)
        {
            _messageHandler = messageHandler;
            _callbackQueryHandler = callbackQueryHandler;
        }

        public Task HandlePollingErrorAsync(Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error: code '{apiRequestException.ErrorCode}' message '{apiRequestException.Message}'",
                _ => exception.ToString()
            };

            // TODO: log
            Debug.WriteLine($"\nException: {errorMessage}\n");

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => _messageHandler.HandleMessageAsync(update.Message!, cancellationToken),
                UpdateType.CallbackQuery => _callbackQueryHandler.HandleCallbackQueryAsync(update.CallbackQuery!, cancellationToken),
                _ => OnDiscard(update, cancellationToken)
            };

            await handler;
        }

        private Task OnDiscard(Update update, CancellationToken cancellationToken)
        {
            // TODO: log

            return Task.CompletedTask;
        }
    }

}
