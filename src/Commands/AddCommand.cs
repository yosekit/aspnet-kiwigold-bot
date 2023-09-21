using KiwigoldBot.Data;
using KiwigoldBot.Helpers;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Commands
{
    public class AddCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;
        private readonly IDbRepository _repository;

        public AddCommand(IBotMessenger messenger, IDbRepository repository)
        {
            _messenger = messenger;
            _repository = repository;
        }

        public async Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken)
        {
            // check args not empty and first arg is table
            if (!args.IsNullOrEmpty() && DbTables.IsTable(args[0]))
            {
                // check args has data
                if (args.Length == 2)
                {
                    string message = AddDataToTable(args[0], args[1]);

                    await _messenger.SendTextAsync(message, cancellationToken: cancellationToken);
                }
                else
                {
                    SaveTable(context, args[0]);

                    await _messenger.SendTextAsync("Enter the data...", cancellationToken: cancellationToken);
                }

                return;
            }

            // request for table
            var inlineKeyboard = BotReplyMarkup
                .InlineKeyboard()
                .WithCallbackData<AddCommand>(DbTables.AsArray());

            await _messenger.SendTextAsync("Enter the table:", inlineKeyboard, cancellationToken: cancellationToken);
        }

        public async Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken)
        {
            string? table = GetTable(context);

            // text is table from callback
            if (table == null)
            {
                SaveTable(context, text);

                await _messenger.SendTextAsync("Enter the data...", cancellationToken: cancellationToken);
            }
            // text is data from message
            else
            {
                string message = AddDataToTable(table, text);

                await _messenger.SendTextAsync(message, cancellationToken: cancellationToken);
            }
        }

        private string? GetTable(BotCommandContext? context)
        {
            var ctx = context as AddCommandContext;

            return ctx!.Table;
        }

        private void SaveTable(BotCommandContext? context, string table)
        {
            var ctx = context as AddCommandContext;

            ctx!.Table = table;
        }

        private string AddDataToTable(string table, string data)
        {
            string upperTable = table.ToUpperFirst();

            bool res = _repository.FromTable(upperTable).Add(data);

            return res ? 
                $"The data successfully added to {upperTable}." : 
                $"The data could not be added to {upperTable}.";
        }
    }

    public class AddCommandContext : BotCommandContext
    {
        public string Table { get; set; }
    }
}
