using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotChannelPostHandler
    {
        public Task HandleChannelPostAsync(Message message, CancellationToken cancellationToken);
    }
}
