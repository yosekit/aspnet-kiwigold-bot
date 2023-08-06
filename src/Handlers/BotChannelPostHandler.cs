using KiwigoldBot.Interfaces;
using KiwigoldBot.Settings;
using Telegram.Bot.Types;

namespace KiwigoldBot.Handlers
{
    public class BotChannelPostHandler : IBotChannelPostHandler
    {
        private readonly BotSettings _settings;

        public BotChannelPostHandler(BotSettings settings)
        {
            _settings = settings;
        }

        public Task HandleChannelPostAsync(Message message, CancellationToken cancellationToken)
        {
            MemorizeChannelChat(message);

            return Task.CompletedTask;
        }

        private void MemorizeChannelChat(Message message)
        {
            if(_settings.ChannelChat == null)
            {
                var channelChat = message.Chat;

                if(channelChat != null) 
                    _settings.ChannelChat = channelChat;
            }
        }
    }
}
