using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;
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
                ShowCallbackData.Picture => _repository.GetAll<Picture>().Select(x => x.ToString()),
                ShowCallbackData.Author  => _repository.GetAll<Author>() .Select(x => x.ToString()),
                ShowCallbackData.Title   => _repository.GetAll<Title>()  .Select(x => x.ToString()),
                ShowCallbackData.Hashtag => _repository.GetAll<Hashtag>().Select(x => x.ToString()),
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
