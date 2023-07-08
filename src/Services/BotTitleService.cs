using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Services
{
    public class BotTitleService : IBotTitleService
    {
        private readonly ITelegramBotClient _client;
        private readonly IDbRepository _repository;

        public BotTitleService(ITelegramBotClient client, IDbRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task SaveTitleAsync(string title, Message message, CancellationToken cancellationToken)
        {
            bool added = _repository.Add(new Title { Name = title });

            if (added)
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Title was added!",
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Title was not added...",
                cancellationToken: cancellationToken);
            }
        }

        public async Task ShowAllTitlesAsync(Message message, CancellationToken cancellationToken)
        {
            var titles = _repository.GetAll<Title>();

            if (titles.IsAny())
            {
                string text = string.Join("\n", titles.Select(x => x.Name));

                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Here are the saved titles:\n\n{text}",
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "You haven't saved any titles...",
                    cancellationToken: cancellationToken);
            }
        }
    }
}
