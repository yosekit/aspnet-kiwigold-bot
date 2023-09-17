using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Data;

namespace KiwigoldBot.Commands
{
    public class AddAuthorCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;
        private readonly IBotCommandPoolManager _pool;
        private readonly IDbRepository _repository;

        public AddAuthorCommand(
            IBotMessenger messenger,
            IBotCommandPoolManager pool,
            IDbRepository repository)
        {
            _messenger = messenger;
            _pool = pool;
            _repository = repository;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            // выполнить стартовый метод
            await SendRequestAsync(message, cancellationToken);

            // добавить остальные методы в пул
            _pool.Add(AddAuthorAsync);
        }

        private async Task SendRequestAsync(Message message, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("Enter the author name (Any characters).", cancellationToken: cancellationToken);
        }

        private Task AddAuthorAsync(Message message, CancellationToken cancellationToken)
        {
            string author = message.Text!;

            _repository.FromTable(DbTables.Author).Add(author);

            return Task.CompletedTask;
        }
    }
}
