using KiwigoldBot.Data;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Settings;
using System.Data.Common;
using System.Data.SQLite;
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
            IConfiguration configuration, string section = "TelegramBot")
        {
            var settings = configuration.GetSection(section).Get<BotSettings>()!;

            return services.AddSingleton(settings);
        }

        public static IServiceCollection AddDbConnectionManager(this IServiceCollection services,
            ConnectionStringSettings connectionString)
        {
            DbProviderFactories.RegisterFactory(
                 connectionString.Provider.InvariantName, 
                 connectionString.Provider.TypeAssemblyName);

            return services.AddSingleton<IDbConnectionManager>(new DbConnectionManager(
                DbProviderFactories.GetFactory(connectionString.Provider.InvariantName),
                connectionString.ConnectionString));
        }
    }
}
