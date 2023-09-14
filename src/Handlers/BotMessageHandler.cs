using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions;
using KiwigoldBot.Settings;

namespace KiwigoldBot.Handlers
{
    public class BotMessageHandler : IBotMessageHandler
    {
        private readonly BotSettings _settings;

        private readonly IBotTextHandler _textHandler;
        private readonly IBotPhotoHandler _photoHandler;
        
        public BotMessageHandler(
            BotSettings settings, 
            IBotTextHandler textHandler, 
            IBotPhotoHandler photoHandler)
        {
            _settings = settings;

            _textHandler = textHandler;
            _photoHandler = photoHandler;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            MemorizeBotChat(message);
            MemorizeLastMessage(message);

            var handler = message.Type switch
            {
                MessageType.Text => _textHandler.HandleTextAsync(message, cancellationToken),
                MessageType.Photo => _photoHandler.HandlePhotoAsync(message, cancellationToken),
                _ => OnDiscard(message, cancellationToken),
            };

            await handler;
        }

        private Task OnDiscard(Message message, CancellationToken cancellationToken)
        {
            // TODO: log

            return Task.CompletedTask;
        }

        private void MemorizeBotChat(Message message)
        {
            if (_settings.BotChat == null)
            {
                var botChat = message.Chat;

                if (botChat != null)
                    _settings.BotChat = botChat;
            }
        }

        private void MemorizeLastMessage(Message message) 
        {
            _settings.LastMessage = message;
        }
    }
}
