using Telegram.Bot.Types;
using Telegram.Bot;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Services
{
    public class BotHashtagService : IBotHashtagService

    {
        private readonly ITelegramBotClient _client;
        private readonly IDbRepository _repository;

        public BotHashtagService(ITelegramBotClient client, IDbRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task SaveHashtagAsync(string hashtag, Message message, CancellationToken cancellationToken)
        {
            bool added = _repository.Add(new Hashtag { Name = hashtag });

            if (added)
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Hashtag was added!",
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Hashtag was not added...",
                cancellationToken: cancellationToken);
            }
        }

        public async Task ShowAllHashtagAsync(Message message, CancellationToken cancellationToken)
        {
            var hashtags = _repository.GetAll<Hashtag>();

            if (hashtags.IsAny())
            {
                string text = string.Join("\n", hashtags.Select(x => x.Name));

                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Here are the saved hashtags:\n\n{text}",
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "You haven't saved any hashtags...",
                    cancellationToken: cancellationToken);
            }
        }
    }
}
