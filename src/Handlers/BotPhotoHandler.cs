using KiwigoldBot.Interfaces;
using Telegram.Bot.Types;

namespace KiwigoldBot.Handlers
{
    public class BotPhotoHandler : IBotPhotoHandler
    {
        private readonly IBotPictureService _pictureService;

        public BotPhotoHandler(IBotPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public async Task HandlePhotoAsync(Message message, CancellationToken cancellationToken)
        {
            string fileId = message.Photo!.Last().FileId;

            await _pictureService.SavePictureAsync(fileId, message, cancellationToken);
        }
    }
}
