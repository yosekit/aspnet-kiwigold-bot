using KiwigoldBot.Models;
using Telegram.Bot.Types;

namespace KiwigoldBot.Interfaces
{
    public interface IBotAuthorService
    {
        public Task ShowAllAuthorsAsync(Message message, CancellationToken cancellationToken);
        public Task SaveAuthorAsync(string author, Message message, CancellationToken cancellationToken);
    }
}
