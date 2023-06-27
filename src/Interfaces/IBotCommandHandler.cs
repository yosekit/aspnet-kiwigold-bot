using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandHandler
    {
        public Task HandleCommandAsync(Message message, CancellationToken cancellationToken);
    }
}
