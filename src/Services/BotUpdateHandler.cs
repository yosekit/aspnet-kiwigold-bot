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

        public BotUpdateHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
			
        }

		private Task OnUnknown(Update update, CancellationToken cancellationToken)
		{
            // TODO: log

            return Task.CompletedTask;
		}

		private async Task OnMessage(Message message, CancellationToken cancellationToken)
		{
			string messageText = message.Text ?? string.Empty;

            // TODO: log
            Debug.WriteLine($"\nReceived a '{messageText}' message in chat {message.Chat.Id}.\n");

			await _botClient.SendTextMessageAsync(
				chatId: message.Chat.Id, 
				text: messageText, 
				cancellationToken: cancellationToken);
		}

		public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
		{
			var handler = update.Type switch
			{
				UpdateType.Message => OnMessage(update.Message!, cancellationToken),
				_ => OnUnknown(update, cancellationToken)
			};

			await handler;
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
	}
}
