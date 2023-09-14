using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotLinkHandler
    {
        public Task HandleLinkAsync(Message message, CancellationToken cancellationToken);
    }
}
