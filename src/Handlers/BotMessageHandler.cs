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
        private readonly IBotCommandPoolManager _commandPool;

        private readonly IBotTextHandler _textHandler;
        private readonly IBotPhotoHandler _photoHandler;
        
        public BotMessageHandler(
            BotSettings settings, 
            IBotCommandPoolManager commandPool,
            IBotTextHandler textHandler, 
            IBotPhotoHandler photoHandler)
        {
            _settings = settings;
            _commandPool = commandPool;

            _textHandler = textHandler;
            _photoHandler = photoHandler;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            MemorizeBotChat(message);
            MemorizeLastMessage(message);

            if (CommandPoolIsActive(message))
            {
                await _commandPool.ExecuteLastAsync(message, cancellationToken);

                return;
            }

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

        private bool CommandPoolIsActive(Message message)
        {
            if (message.Type == MessageType.Text && message.Text!.IsCommand())
            {
                _commandPool.Clear();
            }
            else if (_commandPool.IsActive())
            {
                return true;
            }

            return false;
        }
    }
}
