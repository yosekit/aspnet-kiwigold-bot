using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotTitleService
    {
        public Task SaveTitleAsync(string title, Message message, CancellationToken cancellationToken);
        public Task ShowAllTitlesAsync(Message message, CancellationToken cancellationToken);
    }
}
