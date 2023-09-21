using System.Diagnostics;
using System.Text.RegularExpressions;

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
                (string command, string[]? args) = text.SplitCommandArgs();

                await _commandHandler.ExecuteNewCommandAsync(command, args, cancellationToken);
            }
            else if (_commandHandler.IsCommandActive)
            {
                await _commandHandler.ExecuteActiveCommandAsync(text, cancellationToken);
            }
            else if (text.IsLink())
            {
                await _linkHandler.HandleLinkAsync(message, cancellationToken);
            }
            else return;
        }

    }

    public static partial class StringExtensions
    {
        private const string CommandPattern = @"^\/\w+";

        public static bool IsCommand(this string origin)
        {
            return CommandRegex().IsMatch(origin);
        }

        public static bool IsLink(this string origin)
        {
            return Uri.TryCreate(origin, UriKind.Absolute, out _);
        }

        public static (string, string[]?) SplitCommandArgs(this string origin)
        {
            // TODO: Remove assert
            Debug.Assert(CommandRegex().IsMatch(origin));

            // TODO: Change args separation logic (now it split by spaces)
            string[] splitted = origin.Split(' ');

            string command = splitted[0];
            string[]? args = splitted.Length > 1 ? splitted[1..] : null;

            return (command, args);
        }

        [GeneratedRegex(CommandPattern)]
        private static partial Regex CommandRegex();
    }
}
