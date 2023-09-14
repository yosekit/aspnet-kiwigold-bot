using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KiwigoldBot.Interfaces
{
    public interface IBotMessenger
    {
        public Task SendTextAsync(string text, IReplyMarkup? replyMarkup = null, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default);
        public Task SendPhotoAsync(string photoIdOrUrl, IReplyMarkup? replyMarkup = null, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default);
        public Task SendPhotoGroupAsync(string[] photoIds, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default);
        public Task DeleteLastMessageAsync(BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default);
    }

    public enum BotTargetChat
    {
        Bot,
        Channel,
    }
}
