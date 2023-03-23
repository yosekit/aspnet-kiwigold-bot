using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using System.Diagnostics;

namespace KiwigoldBot.Services
{
	public class BotUpdateHandler
	{
		private readonly ITelegramBotClient _botClient;

        public BotUpdateHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

		private async Task OnUnknown(Update update, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		private async Task OnMessage(Message message, CancellationToken cancellationToken)
		{
			string messageText = message.Text ?? string.Empty;

			Debug.WriteLine($"\nReceived a '{messageText}' message in chat {message.Chat.Id}.\n");

			await _botClient.SendTextMessageAsync(
				chatId: message.Chat.Id, 
				text: "Hello", 
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

		public async Task HandlePollingErrorAsync(Exception exception, CancellationToken cancellationToken)
		{
			var errorMessage = exception switch
			{
				ApiRequestException apiRequestException
					=> $"Telegram API Error: code '{apiRequestException.ErrorCode}' message '{apiRequestException.Message}'",
				_ => exception.ToString()
			};

			Console.WriteLine($"\nException: {errorMessage}\n");
			return;
		}
	}
}
