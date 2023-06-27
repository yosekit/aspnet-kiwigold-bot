using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotMessageHandler
    {
        public Task HandleMessageAsync(Message message, CancellationToken cancellationToken);
    }
}
