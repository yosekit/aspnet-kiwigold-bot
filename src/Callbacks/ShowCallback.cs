using KiwigoldBot.Interfaces;
using Telegram.Bot.Types;

namespace KiwigoldBot.Callbacks
{
    public class ShowCallback : IBotCallback
    {
        private readonly IBotPictureService _pictureService;
        private readonly IBotAuthorService _authorService;
        private readonly IBotTitleService _titleService;
        private readonly IBotHashtagService _hashtagService;

        public ShowCallback(
            IBotPictureService pictureService,
            IBotAuthorService authorService,
            IBotTitleService titleService,
            IBotHashtagService hashtagService
            )
        {
            _pictureService = pictureService;
            _authorService = authorService;
            _titleService = titleService;
            _hashtagService = hashtagService;
        }

        public async Task InvokeAsync(string data, Message message, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<ShowCallbackData>(data, out var parsedData))
            {
                // TODO: log

                return;
            }

            var method = parsedData switch
            {
                ShowCallbackData.Picture => _pictureService.ShowAllPicturesAsync(message, cancellationToken),
                ShowCallbackData.Author => _authorService.ShowAllAuthorsAsync(message, cancellationToken),
                ShowCallbackData.Title => _titleService.ShowAllTitlesAsync(message, cancellationToken),
                ShowCallbackData.Hashtag => _hashtagService.ShowAllHashtagAsync(message, cancellationToken),
            };

            await method;
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
