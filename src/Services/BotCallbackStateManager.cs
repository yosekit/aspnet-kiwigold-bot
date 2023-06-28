using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotCallbackStateManager : IBotCallbackStateManager
    {
        private Type? _state;

        public void SetActive<TCallback>() where TCallback : IBotCallback =>
            _state = typeof(TCallback);

        public Type? GetActive() => _state;

        public void RemoveActive() => _state = null;
    }
}
