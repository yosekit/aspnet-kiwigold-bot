using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotHashtagService
    {
        public Task SaveHashtagAsync(string hashtag, Message message, CancellationToken cancellationToken);
        public Task ShowAllHashtagAsync(Message message, CancellationToken cancellationToken);
    }
}
