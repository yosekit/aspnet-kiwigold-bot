using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandPoolManager
    {
        public bool IsActive();
        public IBotCommandPoolManager Add(Func<Message, CancellationToken, Task> action);
        public IBotCommandPoolManager Add(IEnumerable<Func<Message, CancellationToken, Task>> actions);
        public void Clear();
        public Task ExecuteLastAsync(Message message, CancellationToken cancellationToken);
    }
}
