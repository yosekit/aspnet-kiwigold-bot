using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotTextHandler
    {
        public Task HandleTextAsync(Message message, CancellationToken cancellationToken);
    }

}
