using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace KiwigoldBot.Handlers
{
    public class BotTextHandler : IBotTextHandler
    {
        private readonly IBotCommandHandler _commandHandler;
        private readonly IBotPictureService _pictureService;

        public BotTextHandler(IBotCommandHandler commandHandler, IBotPictureService pictureService)
        {
            _commandHandler = commandHandler;
            _pictureService = pictureService;
        }

        public async Task HandleTextAsync(Message message, CancellationToken cancellationToken)
        {
            string text = message.Text!;

            var handler = text.GetTextType() switch
            {
                TextType.Command => _commandHandler.HandleCommandAsync(message, cancellationToken),
                // TODO: add LinkHandler because there are different links
                TextType.Link => _pictureService.SendPictureFromLinkAsync(text, message, cancellationToken),
                TextType.PlainText => this.HandlePlainTextAsync(message, cancellationToken),
                _ => OnDiscard(message, cancellationToken),
            };

            await handler;
        }

        private Task HandlePlainTextAsync(Message message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Task OnDiscard(Message message, CancellationToken cancellationToken)
        {
            // TODO: log

            return Task.CompletedTask;
        }
    }

    public static class StringExtensions
    {
        public static TextType GetTextType(this string origin)
        {
            if (origin.IsCommand()) return TextType.Command;
            if (origin.IsLink()) return TextType.Link;
            return TextType.PlainText;
        }
    }


    public enum TextType
    {
        PlainText,
        Command,
        Link,
    }
}
