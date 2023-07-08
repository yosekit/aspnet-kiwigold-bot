using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class AddAuthorCommand : IBotCommand
    {
        private readonly ITelegramBotClient _client;
        private readonly IBotCommandPoolManager _pool;
        private readonly IBotAuthorService _service;

        public AddAuthorCommand(ITelegramBotClient client, IBotCommandPoolManager pool, IBotAuthorService service)
        {
            _client = client;
            _pool = pool;
            _service = service;
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
            string text = "Enter the author name (Any characters).";

            await _client.SendTextMessageAsync(message.Chat.Id, text,
                cancellationToken: cancellationToken);
        }

        private async Task AddAuthorAsync(Message message, CancellationToken cancellationToken)
        {
            string author = message.Text!;

            await _service.SaveAuthorAsync(author, message, cancellationToken);
        }
    }
}
