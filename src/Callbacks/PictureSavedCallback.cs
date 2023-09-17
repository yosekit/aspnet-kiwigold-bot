using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Callbacks
{
    public class PictureSavedCallback : IBotCallback
    {
        private readonly IBotPictureService _pictureService;

        public PictureSavedCallback(IBotPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public async Task InvokeAsync(string data, Message message, CancellationToken cancellationToken)
        {
            await _pictureService.SendPictureAsync(fileId: data, cancellationToken);
        }
    }
}
