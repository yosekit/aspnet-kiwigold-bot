using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Helpers;

namespace KiwigoldBot.Handlers
{
    public class BotCallbackQueryHandler : IBotCallbackQueryHandler
    {
        private readonly IBotCommandHandler _commandHandler;

        public BotCallbackQueryHandler(IBotCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            // TODO: check callbackQuery on Data is null
            string data = callbackQuery.Data!;

            (Type commandType, string callbackData) = BotCallbackDataConvert.ToTypeAndData(data);

            await _commandHandler.ExecuteCallbackCommandAsync(commandType, callbackData, cancellationToken);
        }
    }
}
