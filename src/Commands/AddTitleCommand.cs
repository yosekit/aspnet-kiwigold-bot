using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class AddTitleCommand : IBotCommand
    {
        private readonly ITelegramBotClient _client;
        private readonly IBotCommandPoolManager _pool;
        private readonly IBotTitleService _service;

        public AddTitleCommand(ITelegramBotClient client, IBotCommandPoolManager pool, IBotTitleService service)
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
            _pool.Add(AddTitleAsync);
        }

        private async Task SendRequestAsync(Message message, CancellationToken cancellationToken)
        {
            string text = "Enter the title name (Any characters).";

            await _client.SendTextMessageAsync(message.Chat.Id, text,
                cancellationToken: cancellationToken);
        }

        private async Task AddTitleAsync(Message message, CancellationToken cancellationToken)
        {
            string title = message.Text!;

            await _service.SaveTitleAsync(title, message, cancellationToken);
        }
    }
}
