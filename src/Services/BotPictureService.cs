using Telegram.Bot.Types;

using KiwigoldBot.Callbacks;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;
using KiwigoldBot.Helpers;

namespace KiwigoldBot.Services
{
    public class BotPictureService : IBotPictureService
    {
        private readonly IBotMessenger _messenger;
        private readonly IDbRepository _repository;

        public BotPictureService(IBotMessenger messenger, IDbRepository repository)
        {
            _messenger = messenger;
            _repository = repository;
        }

        public async Task SavePictureAsync(string fileId, Message message, CancellationToken cancellationToken)
        {
            bool added = _repository.Add(new Picture { FileId = fileId });

            if (added)
            {
                var pictures = _repository.GetAll<Picture>();
                var callbackData = pictures.TakeLast(5).Select(x => x.Id.ToString()).ToDictionary(x => x);

                var inlineKeyboard = BotReplyMarkup
                    .InlineKeyboard()
                    .WithCallbackData<PictureSavedCallback>(callbackData);

                await _messenger.SendTextAsync("Picture was added!", inlineKeyboard, cancellationToken: cancellationToken);
            }
            else
            {
                await _messenger.SendTextAsync("Picture was not added...", cancellationToken: cancellationToken);
            }
        }

        public async Task SendPictureAsync(int pictureId, Message message, CancellationToken cancellationToken)
        {
            var picture = _repository.Get<Picture>(pictureId);

            if (picture != null)
            {
                await _messenger.SendPhotoAsync(picture.FileId, cancellationToken: cancellationToken);
            }
            else
            {
                await _messenger.SendTextAsync("Picture could not be retrieved...", cancellationToken: cancellationToken);
            }
        }

        public async Task SendPictureFromLinkAsync(string url, Message message, CancellationToken cancellationToken)
        {
            var inlineKeyboard = BotReplyMarkup
                .InlineKeyboard()
                .WithCallbackData<PictureFromLinkCallback>(new()
                {
                    ["Save"] = PictureFromLinkCallbackData.Save.ToString(),
                    ["Delete"] = PictureFromLinkCallbackData.Delete.ToString(),
                });

            await _messenger.SendPhotoAsync(url, inlineKeyboard, cancellationToken: cancellationToken);
        }
    }
}
