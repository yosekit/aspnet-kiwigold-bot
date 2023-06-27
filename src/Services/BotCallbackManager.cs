using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotCallbackManager : IBotCallbackManager
    {
        private readonly IBotCallbackContext _context;
        private readonly IEnumerable<IBotCallback> _callbacks;

        public BotCallbackManager(IBotCallbackContext callbackContext, IEnumerable<IBotCallback> callbacks)
        {
            _context = callbackContext;
            _callbacks = callbacks;
        }

        public async Task RequestAsync(string data, Message message, CancellationToken cancellationToken)
        {
            var activeType = _context.GetActive();
            var active = _callbacks.FirstOrDefault(x => x.GetType() == activeType);

            if (active != null)
            {
                await active.InvokeAsync(data, message, cancellationToken);
            }
        }
    }
}
