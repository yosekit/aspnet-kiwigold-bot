using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotCallbackContext : IBotCallbackContext
    {
        public Type? Active { get; set; }
    }
}
