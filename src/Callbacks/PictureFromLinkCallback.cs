using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Callbacks
{
    public class PictureFromLinkCallback : IBotCallback
    {
        private readonly IBotMessenger _messenger;
        private readonly IBotPictureService _pictureService;

        public PictureFromLinkCallback(IBotMessenger messenger, IBotPictureService pictureService)
        {
            _messenger = messenger;
            _pictureService = pictureService;
        }

        public async Task InvokeAsync(string data, Message message, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<PictureFromLinkCallbackData>(data, out var parsedData))
            {
                // TODO: log

                return;
            }

            switch (parsedData)
            {
                case PictureFromLinkCallbackData.Save:
                    {
                        string fileId = message.Photo!.Last().FileId;

                        await _pictureService.SavePictureAsync(fileId, cancellationToken);
                        break;
                    }

                case PictureFromLinkCallbackData.Delete:
                    {
                        await _messenger.DeleteLastMessageAsync(cancellationToken: cancellationToken);
                        break;
                    }
            }
        }
    }

    public enum PictureFromLinkCallbackData
    {
        Delete = 0,
        Save = 1,
    }
}
