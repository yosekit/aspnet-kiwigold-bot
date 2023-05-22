using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace KiwigoldBot.Services
{
	public class BotWebhookHosted : IHostedService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly BotSettings _botSettings;

        public BotWebhookHosted(IServiceProvider serviceProvider, BotSettings botSettings)
        {
			_serviceProvider = serviceProvider;
			_botSettings = botSettings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
		{
			var botClient = _serviceProvider.GetRequiredService<ITelegramBotClient>();

			string webhookAddress = _botSettings.HostAddress + _botSettings.Route;
 
			await botClient.SetWebhookAsync(
				url: webhookAddress,
				allowedUpdates: Array.Empty<UpdateType>(),
				dropPendingUpdates: true,
				cancellationToken: cancellationToken);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			var botClient = _serviceProvider.GetRequiredService<ITelegramBotClient>();

			await botClient.DeleteWebhookAsync(
                dropPendingUpdates: true, 
				cancellationToken: cancellationToken);
		}
	}
}
