using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Handlers
{
    public class BotLinkHandler : IBotLinkHandler
    {
        private readonly IBotPictureService _pictureService;

        public BotLinkHandler(IBotPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public async Task HandleLinkAsync(Message message, CancellationToken cancellationToken)
        {
            string link = message.Text!;

            // TODO: add validation

            await _pictureService.SendPictureFromUrlAsync(link, cancellationToken);
        }
    }
}
