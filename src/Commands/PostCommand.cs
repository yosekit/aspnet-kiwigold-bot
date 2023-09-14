using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Settings;

namespace KiwigoldBot.Commands
{
    public class PostCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;
        private readonly IBotCommandPoolManager _pool;
        private readonly IBotPictureService _pictureService;

        public PostCommand(IBotMessenger messenger, IBotCommandPoolManager pool, IBotPictureService pictureService)
        {
            _messenger = messenger;
            _pool = pool;
            _pictureService = pictureService;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            // выполнить стартовый метод
            await SendRequestAsync(message, cancellationToken);

            // добавить остальные методы в пул
            _pool.Add(PostMessageAsync);
        }

        private async Task SendRequestAsync(Message message, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("Enter the message to post.", cancellationToken: cancellationToken);
        }

        private async Task PostMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("From /post success", targetChat: BotTargetChat.Channel, cancellationToken: cancellationToken);
        }
    }
}
