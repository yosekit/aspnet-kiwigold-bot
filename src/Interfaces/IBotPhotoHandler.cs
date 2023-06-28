using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotPhotoHandler
    {
        public Task HandlePhotoAsync(Message message, CancellationToken cancellationToken);
    }

}
