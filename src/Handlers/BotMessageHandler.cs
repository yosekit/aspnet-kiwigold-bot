using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotMessageHandler : IBotMessageHandler
    {
        private readonly IBotTextHandler _textHandler;
        private readonly IBotPhotoHandler _photoHandler;
        private readonly IBotCommandPoolManager _commandPoolManager;
        
        public BotMessageHandler(IBotTextHandler textHandler, IBotPhotoHandler photoHandler, IBotCommandPoolManager commandPoolManager)
        {
            _textHandler = textHandler;
            _photoHandler = photoHandler;
            _commandPoolManager = commandPoolManager;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            if (_commandPoolManager.IsActive)
            {
                await _commandPoolManager.ExecuteLastAsync(message, cancellationToken);

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
    }
}
