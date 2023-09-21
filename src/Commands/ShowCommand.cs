using KiwigoldBot.Interfaces;
using KiwigoldBot.Data;
using KiwigoldBot.Helpers;
using KiwigoldBot.Extensions;
using System.Text;

namespace KiwigoldBot.Commands
{
    public class ShowCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;
        private readonly IDbRepository _repository;

        public ShowCommand(IBotMessenger messenger, IDbRepository repository)
        {
            _messenger = messenger;
            _repository = repository;
        }

        public async Task ExecuteAsync(string[]? args, BotCommandContext? _, CancellationToken cancellationToken)
        {
            // check args not empty and first arg is table
            if(!args.IsNullOrEmpty() && DbTables.IsTable(args[0]))
            {
                string message = GetRowsFromTable(args[0]);

                await _messenger.SendTextAsync(message, cancellationToken: cancellationToken);

                return;
            }

            var inlineKeyboard = BotReplyMarkup
                .InlineKeyboard()
                .WithCallbackData<ShowCommand>(DbTables.AsArray());

            await _messenger.SendTextAsync("Enter the table:", inlineKeyboard, cancellationToken: cancellationToken);
        }

        public async Task ExecuteNextAsync(string text, BotCommandContext? _, CancellationToken cancellationToken)
        {
            // text is table from callback

            string message = GetRowsFromTable(text);

            await _messenger.SendTextAsync(message, cancellationToken: cancellationToken);
        }

        private string GetRowsFromTable(string table)
        {
            // get rows from table
            var rows = _repository.FromTable(table).GetAll();

            // build message
            var builder = new StringBuilder($"Here is a saved data from {table}");
            foreach (string row in rows) builder.Append($"\n{row}");

            return builder.ToString();
        }
    }
}
