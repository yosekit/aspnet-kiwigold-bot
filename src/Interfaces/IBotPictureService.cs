using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotPictureService
    {
        public Task SavePictureAsync(string fileId, CancellationToken cancellationToken);
        public Task SendPictureAsync(string fileId, CancellationToken cancellationToken);
        public Task SendPictureFromUrlAsync(string url, CancellationToken cancellationToken);
    }
}
