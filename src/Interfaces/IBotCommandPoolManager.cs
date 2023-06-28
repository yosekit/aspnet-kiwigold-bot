using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandPoolManager
    {
        public bool IsActive { get; }

        public IBotCommandPoolManager Add(Func<Message, CancellationToken, Task> action);
        public Task ExecuteLastAsync(Message message, CancellationToken cancellationToken);
    }
}
