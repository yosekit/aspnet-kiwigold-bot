using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCommand
    {
        public Task ExecuteAsync(Message message, CancellationToken cancellationToken);
    }
}
