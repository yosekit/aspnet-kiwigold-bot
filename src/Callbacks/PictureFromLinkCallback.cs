using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Callbacks
{
    public class PictureFromLinkCallback : IBotCallback
    {
        private readonly ITelegramBotClient _client;
        private readonly IBotPictureService _pictureService;

        public PictureFromLinkCallback(ITelegramBotClient client, IBotPictureService pictureService)
        {
            _client = client;
            _pictureService = pictureService;

        }

        public async Task InvokeAsync(string data, Message message, CancellationToken cancellationToken)
        {
            if(!Enum.TryParse<PictureFromLinkCallbackData>(data, out var parsedData))
            {
                // TODO: log

                return;
            }

            switch (parsedData)
            {
                case PictureFromLinkCallbackData.Save:
                    {
                        string fileId = message.Photo!.Last().FileId;

                        await _pictureService.SavePictureAsync(fileId, message, cancellationToken);
                        break;
                    }

                case PictureFromLinkCallbackData.Delete:
                    {
                        // delete last message
                        await _client.DeleteMessageAsync(message.Chat.Id, message.MessageId, cancellationToken);
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
