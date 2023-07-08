using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

using KiwigoldBot.Callbacks;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Services
{
    public class BotPictureService : IBotPictureService
    {
        private readonly ITelegramBotClient _client;
        private readonly IDbRepository _repository;
        private readonly IBotCallbackStateManager _callbackStateManager;

        public BotPictureService(
            ITelegramBotClient client,
            IDbRepository repository,
            IBotCallbackStateManager callbackStateManager)
        {
            _client = client;
            _repository = repository;
            _callbackStateManager = callbackStateManager;
        }

        public async Task ShowAllPicturesAsync(Message message, CancellationToken cancellationToken)
        {
            var pictures = _repository.GetAll<Picture>();

            if (pictures.IsAny())
            {
                var mediaGroup = pictures.Take(5).Select(x => new InputMediaPhoto(x.FileId));

                await _client.SendMediaGroupAsync(message.Chat.Id, mediaGroup, cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "You haven't saved any pictures...",
                    cancellationToken: cancellationToken);
            }
        }

        public async Task SavePictureAsync(string fileId, Message message, CancellationToken cancellationToken)
        {
            bool added = _repository.Add(new Picture { FileId = fileId });

            if (added)
            {
                var pictures = _repository.GetAll<Picture>();

                var inlineKeyboardButtons = pictures.TakeLast(5)
                    .Select(x => InlineKeyboardButton.WithCallbackData(x.Id.ToString()));

                var inlineKeyboard = new InlineKeyboardMarkup(inlineKeyboardButtons);

                _callbackStateManager.SetActive<PictureSavedCallback>();

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

            _callbackStateManager.SetActive<PictureFromLinkCallback>();

            await _client.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new(link),
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
    }
}
