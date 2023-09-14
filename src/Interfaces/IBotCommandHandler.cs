using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandHandler
    {
        public bool IsCommandActive { get; }
        public Task ExecuteActiveCommandAsync(Message message, CancellationToken cancellationToken);
        public Task ExecuteNewCommandAsync(Message message, CancellationToken cancellationToken);
    }
}
