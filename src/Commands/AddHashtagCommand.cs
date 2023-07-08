using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class AddHashtagCommand : IBotCommand
    {
        private readonly ITelegramBotClient _client;
        private readonly IBotCommandPoolManager _pool;
        private readonly IBotHashtagService _service;

        public AddHashtagCommand(ITelegramBotClient client, IBotCommandPoolManager pool, IBotHashtagService service)
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
            _pool.Add(AddHashtagAsync);
        }

        private async Task SendRequestAsync(Message message, CancellationToken cancellationToken)
        {
            string text = "Enter the hashtag (as snake_case).";

            await _client.SendTextMessageAsync(message.Chat.Id, text,
                cancellationToken: cancellationToken);
        }

        private async Task AddHashtagAsync(Message message, CancellationToken cancellationToken)
        {
            string hashtag = message.Text!;

            await _service.SaveHashtagAsync(hashtag, message, cancellationToken);
        }
    }
}
