using Telegram.Bot;

namespace KiwigoldBot.Extensions.Di
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBotClient(this IServiceCollection services)
        {
            services
                .AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((client, sp) =>
                {
                    var settings = sp.GetRequiredService<BotSettings>();
                    var options = new TelegramBotClientOptions(settings.Token);
                    return new TelegramBotClient(options, client);
                });

            return services;
        }

        public static IServiceCollection AddBotSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration.GetSection(BotSettings.JsonName).Get<BotSettings>()!;

            return services.AddSingleton(settings);
        }
    }
}
