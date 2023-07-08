using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Callbacks;

namespace KiwigoldBot.Commands
{
    public class ShowCommand : IBotCommand
    {
        private readonly ITelegramBotClient _client;
        private readonly IBotCallbackStateManager _callbackState;

        public ShowCommand(ITelegramBotClient client, IBotCallbackStateManager callbackState)
        {
            _client = client;
            _callbackState = callbackState;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            string text = "Choose needed data to show";

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Picture", ShowCallbackData.Picture.ToString()),
                InlineKeyboardButton.WithCallbackData("Author", ShowCallbackData.Author.ToString()),
                InlineKeyboardButton.WithCallbackData("Title", ShowCallbackData.Title.ToString()),
                InlineKeyboardButton.WithCallbackData("Hashtag", ShowCallbackData.Hashtag.ToString()),
            });

            _callbackState.SetActive<ShowCallback>();

            await _client.SendTextMessageAsync(message.Chat.Id, text,
                replyMarkup: inlineKeyboard, cancellationToken: cancellationToken);
        }
    }
}
