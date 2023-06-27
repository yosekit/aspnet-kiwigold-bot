using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCallback
    {
        public Task InvokeAsync(string data, Message message, CancellationToken cancellationToken);
    }
}
