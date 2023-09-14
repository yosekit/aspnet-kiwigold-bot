using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotTextHandler : IBotTextHandler
    {
        private readonly IBotCommandHandler _commandHandler;
        private readonly IBotLinkHandler _linkHandler;

        public BotTextHandler(
            IBotCommandHandler commandHandler,
            IBotLinkHandler linkHandler)
        {
            _commandHandler = commandHandler;
            _linkHandler = linkHandler;
        }

        public async Task HandleTextAsync(Message message, CancellationToken cancellationToken)
        {
            string text = message.Text!;

            if (text.IsCommand())
            {
                await _commandHandler.ExecuteNewCommandAsync(message, cancellationToken);
            }
            else if (_commandHandler.IsCommandActive)
            {
                await _commandHandler.ExecuteActiveCommandAsync(message, cancellationToken);
            }
            else if (text.IsLink())
            {
                await _linkHandler.HandleLinkAsync(message, cancellationToken);
            }
            else return;
        }

    }

    public static class StringExtensions
    {
        public static bool IsCommand(this string origin)
        {
            // TODO: improve handling logic

            return origin.StartsWith("/");
        }

        public static bool IsLink(this string origin)
        {
            return Uri.TryCreate(origin, UriKind.Absolute, out _);
        }
    }
}
