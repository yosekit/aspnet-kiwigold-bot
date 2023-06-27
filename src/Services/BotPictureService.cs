using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

using KiwigoldBot.Callbacks;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;

namespace KiwigoldBot.Services
{
    public class BotPictureService : IBotPictureService
    {
        private readonly ITelegramBotClient _client;
        private readonly IDbRepository _repository;
        private readonly IBotCallbackContext _callbackContext;

        public BotPictureService(
            ITelegramBotClient client,
            IDbRepository repository,
            IBotCallbackContext callbackContext)
        {
            _client = client;
            _repository = repository;
            _callbackContext = callbackContext;
        }


        public async Task SavePictureAsync(string fileId, Message message, CancellationToken cancellationToken)
        {
            if (_repository.Add(new Picture { FileId = fileId }))
            {
                var pictures = _repository.GetAll<Picture>();

                var inlineKeyboardButtons = pictures.TakeLast(5)
                    .Select(x => InlineKeyboardButton.WithCallbackData(x.Id.ToString()));

                var inlineKeyboard = new InlineKeyboardMarkup(inlineKeyboardButtons);

                _callbackContext.Active = typeof(PictureSavedCallback);

                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Picture was added!",
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Picture was not added...",
                cancellationToken: cancellationToken);
            }
        }

        public async Task SendPictureAsync(int pictureId, Message message, CancellationToken cancellationToken)
        {
            var picture = _repository.Get<Picture>(pictureId);
            long chatId = message!.Chat.Id;

            if (picture != null)
            {
                await _client.SendPhotoAsync(chatId, picture.FileId,
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(chatId, "Picture could not be retrieved...",
                    cancellationToken: cancellationToken);
            }
        }

        public async Task SendPictureFromLinkAsync(string link, Message message, CancellationToken cancellationToken)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithCallbackData("Save", PictureFromLinkCallbackData.Save.ToString()),
                    InlineKeyboardButton.WithCallbackData("Delete", PictureFromLinkCallbackData.Delete.ToString()),
                });

            _callbackContext.Active = typeof(PictureFromLinkCallback);

            await _client.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new(link),
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }

        
    }
}
