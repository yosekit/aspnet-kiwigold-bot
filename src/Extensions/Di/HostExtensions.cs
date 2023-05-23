using Telegram.Bot;

namespace KiwigoldBot.Extensions.Di
{
    public static class HostExtensions
    {
        public static IHost SetBotCommands(this IHost app)
        {
            var settings = app.Services.GetRequiredService<BotSettings>();
            var client = app.Services.GetRequiredService<ITelegramBotClient>();

            client.SetMyCommandsAsync(settings.Commands).Wait();

            return app;
        }
    }
}
