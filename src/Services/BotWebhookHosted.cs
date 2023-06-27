using KiwigoldBot.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;
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
			var client = _serviceProvider.GetRequiredService<ITelegramBotClient>();

            await SetCommandsAsync(client, cancellationToken);

            await SetWebhookAsync(client, cancellationToken);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			var botClient = _serviceProvider.GetRequiredService<ITelegramBotClient>();

			await botClient.DeleteWebhookAsync(
                dropPendingUpdates: true, 
				cancellationToken: cancellationToken);
		}

        private async Task SetCommandsAsync(ITelegramBotClient client, CancellationToken cancellationToken)
        {
            var commands = _botSettings.Commands;

            await client.SetMyCommandsAsync(commands, cancellationToken: cancellationToken);
        }

        private async Task SetWebhookAsync(ITelegramBotClient client, CancellationToken cancellationToken)
        {
            string webhookAddress = _botSettings.HostAddress + _botSettings.Route;

            await client.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                dropPendingUpdates: true,
                cancellationToken: cancellationToken);
        }
    }
}
