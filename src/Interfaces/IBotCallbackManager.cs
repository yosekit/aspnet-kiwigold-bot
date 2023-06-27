using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCallbackManager
    {
        public Task RequestAsync(string data, Message message, CancellationToken cancellationToken);
    }
}
