using Telegram.Bot.Types;

namespace KiwigoldBot.Settings
{
    public class BotSettings
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string HostAddress { get; set; }
        public string Route { get; set; }
        public IEnumerable<BotCommand> Commands { get; set; }

        public Chat BotChat{ get; set; }
        public Chat ChannelChat { get; set; }
        public Message LastMessage { get; set; }
    }
}
