using System.Text;
using System.Diagnostics;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace KiwigoldBot.Services
{
    public class BotUpdateHandler
	{
		private readonly ITelegramBotClient _botClient;
		private readonly BotMessageHandler _messageHandler;

        public BotUpdateHandler(ITelegramBotClient botClient, BotMessageHandler messageHandler)
        {
            _botClient = botClient;
			_messageHandler = messageHandler;
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
