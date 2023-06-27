using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotUpdateHandler
    {
        public Task HandlePollingErrorAsync(Exception exception, CancellationToken cancellationToken);
        public Task HandleUpdateAsync(Update update, CancellationToken cancellationToken);
    }
}
