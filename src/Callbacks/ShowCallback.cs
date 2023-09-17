using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Data;
using KiwigoldBot.Extensions;

namespace KiwigoldBot.Callbacks
{
    public class ShowCallback : IBotCallback
    {
        private readonly IBotMessenger _messenger;
        private readonly IDbRepository _repository;

        public ShowCallback(IBotMessenger messenger, IDbRepository repository)
        {
            _messenger = messenger;
            _repository = repository;
        }

        public async Task InvokeAsync(string data, Message message, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<ShowCallbackData>(data, out var parsedData))
            {
                // TODO: log

                return;
            }

            var models = parsedData switch
            {
                ShowCallbackData.Picture => _repository.FromTable(DbTables.Picture).GetAll(),
                ShowCallbackData.Author  => _repository.FromTable(DbTables.Author).GetAll(),
                ShowCallbackData.Title   => _repository.FromTable(DbTables.Title).GetAll(),
                ShowCallbackData.Hashtag => _repository.FromTable(DbTables.Hashtag).GetAll(),
                _ => throw new NotImplementedException(), 
            };

            if (models.IsAny())
            {
                string text = $"Here are the saved data:\n\n{string.Join("\n", models)}";

                await _messenger.SendTextAsync(text, cancellationToken: cancellationToken);
            }
            else
            {
                await _messenger.SendTextAsync("You haven't saved data...", cancellationToken: cancellationToken);
            }
        }
    }

    public enum ShowCallbackData
    {
        Picture,
        Author,
        Title,
        Hashtag
    }
}
