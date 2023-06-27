using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Handlers
{
    public class BotMessageHandler : IBotMessageHandler
    {
        private readonly IBotCommandHandler _commandHandler;
        private readonly IBotPictureService _pictureService;

        public BotMessageHandler(
            IBotCommandHandler commandHandler,
            IBotPictureService pictureService)
        {
            _commandHandler = commandHandler;
            _pictureService = pictureService;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var handler = message.Type switch
            {
                MessageType.Text => OnText(message, cancellationToken),
                MessageType.Photo => OnPhoto(message, cancellationToken),
                _ => OnDiscard(message, cancellationToken),
            };

            await handler;
        }

        private async Task OnText(Message message, CancellationToken cancellationToken)
        {
            string text = message.Text!;

            if (text.IsCommand())
            {
                await _commandHandler.HandleCommandAsync(message, cancellationToken);
            }
            else if (text.IsLink())
            {
                await _pictureService.SendPictureFromLinkAsync(text, message, cancellationToken);
            }
        }

        private async Task OnPhoto(Message message, CancellationToken cancellationToken)
        {
            string fileId = message.Photo!.Last().FileId;

            await _pictureService.SavePictureAsync(fileId, message, cancellationToken);
        }

        private Task OnDiscard(Message message, CancellationToken cancellationToken)
        {
            // TODO: log

            return Task.CompletedTask;
        }
    }
}
