using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Services
{
    public class BotAuthorService : IBotAuthorService
    {
        private readonly ITelegramBotClient _client;
        private readonly IDbRepository _repository;

        public BotAuthorService(ITelegramBotClient client, IDbRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task ShowAllAuthorsAsync(Message message, CancellationToken cancellationToken)
        {
            var authors = _repository.GetAll<Author>();

            if(authors.IsAny())
            {
                string text = string.Join("\n", authors.Select(x => x.Name));

                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Here are the saved authors:\n\n{text}",
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "You haven't saved any authors...",
                    cancellationToken: cancellationToken);
            }
        }

        public async Task SaveAuthorAsync(string author, Message message, CancellationToken cancellationToken)
        {
            bool added = _repository.Add(new Author { Name = author });

            if (added)
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Author was added!",
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Author was not added...",
                cancellationToken: cancellationToken);
            }
        }
    }
}
