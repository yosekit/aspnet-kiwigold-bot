using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class AddHashtagCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;
        private readonly IBotCommandPoolManager _pool;

        public AddHashtagCommand(IBotMessenger messenger, IBotCommandPoolManager pool)
        {
            _messenger = messenger;
            _pool = pool;
        }


        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            // выполнить стартовый метод
            await SendRequestAsync(message, cancellationToken);

            // добавить остальные методы в пул
            _pool.Add(AddHashtagAsync);
        }

        private async Task SendRequestAsync(Message message, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("Enter the hashtag (as snake_case).", cancellationToken: cancellationToken);
        }

        private async Task AddHashtagAsync(Message message, CancellationToken cancellationToken)
        {
            string hashtag = message.Text!;

            /*await _service.SaveHashtagAsync(hashtag, message, cancellationToken);*/
        }
    }
}
