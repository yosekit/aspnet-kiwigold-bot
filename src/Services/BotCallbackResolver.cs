using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotCallbackResolver : IBotCallbackResolver
    {
        private readonly IEnumerable<IBotCallback> _callbacks;

        public BotCallbackResolver(IEnumerable<IBotCallback> callbacks)
        {
            _callbacks = callbacks;
        }

        public IBotCallback? Get(Type type)
        {
            return _callbacks.FirstOrDefault(x => x.GetType() == type);
        }
    }
}
