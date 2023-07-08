using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Handlers
{
    public class BotMessageHandler : IBotMessageHandler
    {
        private readonly IBotTextHandler _textHandler;
        private readonly IBotPhotoHandler _photoHandler;
        private readonly IBotCommandPoolManager _commandPool;
        
        public BotMessageHandler(IBotTextHandler textHandler, IBotPhotoHandler photoHandler, IBotCommandPoolManager commandPool)
        {
            _textHandler = textHandler;
            _photoHandler = photoHandler;
            _commandPool = commandPool;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            if (message.Type == MessageType.Text && message.Text!.IsCommand())
            {
                _commandPool.Clear();
            }
            else if (_commandPool.IsActive())
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
    }
}
