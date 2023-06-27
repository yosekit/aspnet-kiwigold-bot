﻿using KiwigoldBot.Callbacks;
using KiwigoldBot.Commands;
using KiwigoldBot.Data;
using KiwigoldBot.Handlers;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Settings;
using System.Data.Common;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        public static IServiceCollection AddBotCommands(this IServiceCollection services)
        {
            return services
                .AddScoped<IBotCommand, HelpCommand>()
                .AddScoped<IBotCommand, StartCommand>();
        }

        public static IServiceCollection AddBotCallbacks(this IServiceCollection services)
        {
            return services
                .AddScoped<IBotCallback, PictureFromLinkCallback>()
                .AddScoped<IBotCallback, PictureSavedCallback>();
        }   

        public static IServiceCollection AddBotHandlers(this IServiceCollection services)
        {
            return services
                .AddScoped<IBotCommandHandler, BotCommandHandler>()
                .AddScoped<IBotMessageHandler, BotMessageHandler>()
                .AddScoped<IBotCallbackQueryHandler, BotCallbackQueryHandler>()
                .AddScoped<IBotUpdateHandler, BotUpdateHandler>();
        }
    }
}
