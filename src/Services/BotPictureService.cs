using KiwigoldBot.Callbacks;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Helpers;
using KiwigoldBot.Data;
using KiwigoldBot.Extensions;

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

        public async Task SavePictureAsync(string fileId, CancellationToken cancellationToken)
        {
            _repository.Table = DbTables.Picture;

            bool added = _repository.Add(fileId);

            if (added)
            {
                var pictures = _repository.GetAll();

                var callbackData = pictures.TakeLast(5).ToDictionary(x => x.TruncateLong(5));

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

        public async Task SendPictureAsync(string fileId, CancellationToken cancellationToken)
        {
            _repository.Table = DbTables.Picture;

            var pictures = _repository.GetAll();

            if (pictures.Contains(fileId))
            {
                await _messenger.SendPhotoAsync(fileId, cancellationToken: cancellationToken);
            }
            else
            {
                await _messenger.SendTextAsync("Picture could not be retrieved...", cancellationToken: cancellationToken);
            }
        }

        public async Task SendPictureFromUrlAsync(string url, CancellationToken cancellationToken)
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
