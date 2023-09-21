using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Data;
using KiwigoldBot.Helpers;

namespace KiwigoldBot.Commands
{
    public class DeleteCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public DeleteCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken)
        {
            var inlineKeyboard = BotReplyMarkup
                .InlineKeyboard()
                .WithCallbackData<DeleteCommand>(DbTables.AsArray());

            await _messenger.SendTextAsync("Choose needed data to delete", inlineKeyboard, cancellationToken: cancellationToken);
        }

        public Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
