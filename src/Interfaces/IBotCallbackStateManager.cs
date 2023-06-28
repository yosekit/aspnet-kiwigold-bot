using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotCallbackStateManager
    {
        public void SetActive<TCallback>() where TCallback : IBotCallback;
        public Type? GetActive();
        public void RemoveActive();
    }
}
