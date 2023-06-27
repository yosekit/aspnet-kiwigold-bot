using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotPictureService
    {
        public Task SavePictureAsync(string fileId, Message message, CancellationToken cancellationToken);
        public Task SendPictureAsync(int pictureId, Message message, CancellationToken cancellationToken);
        public Task SendPictureFromLinkAsync(string link, Message message, CancellationToken cancellationToken);
    }
}
