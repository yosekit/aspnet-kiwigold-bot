using Telegram.Bot.Types;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Callbacks;
using KiwigoldBot.Helpers;

namespace KiwigoldBot.Commands
{
    public class ShowCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public ShowCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            var inlineKeyboard = BotReplyMarkup
                .InlineKeyboard()
                .WithCallbackData<ShowCallback>(new()
                {
                    ["Picture"] = ShowCallbackData.Picture.ToString(),
                    ["Author"] = ShowCallbackData.Author.ToString(),
                    ["Title"] = ShowCallbackData.Title.ToString(),
                    ["Hashtag"] = ShowCallbackData.Hashtag.ToString(),
                });

            await _messenger.SendTextAsync("Choose needed data to show", inlineKeyboard, cancellationToken: cancellationToken);
        }
    }
}
