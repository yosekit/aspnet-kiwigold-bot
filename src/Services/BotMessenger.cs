using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Settings;

namespace KiwigoldBot.Services
{
    public class BotMessenger : IBotMessenger
    {
        private readonly BotSettings _settings;
        private readonly ITelegramBotClient _client;

        public BotMessenger(BotSettings settings, ITelegramBotClient client)
        {
            _settings = settings;
            _client = client;
        }

        public async Task SendTextAsync(string text, IReplyMarkup? replyMarkup = null, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default)
        {
            await _client.SendTextMessageAsync(
                chatId: ChatId(targetChat), 
                text: text, 
                replyMarkup: replyMarkup, 
                cancellationToken: cancellationToken);
        }

        public async Task SendPhotoAsync(string photoIdOrUrl, IReplyMarkup? replyMarkup = null, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default)
        {
            await _client.SendPhotoAsync(
                chatId: ChatId(targetChat), 
                photo: photoIdOrUrl, 
                replyMarkup: replyMarkup,
                cancellationToken: cancellationToken);
        }

        public async Task SendPhotoGroupAsync(string[] photoIds, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default)
        {
            var mediaGroup = photoIds.Select(x => new InputMediaPhoto(x));

            await _client.SendMediaGroupAsync(
                chatId: ChatId(targetChat), 
                media: mediaGroup, 
                cancellationToken: cancellationToken);
        }

        public async Task DeleteMessageAsync(Message message, BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default)
        {
            await _client.DeleteMessageAsync(
                chatId: ChatId(targetChat),
                messageId: message.MessageId,
                cancellationToken: cancellationToken);
        }

        public async Task DeleteLastMessageAsync(BotTargetChat targetChat = BotTargetChat.Bot, CancellationToken cancellationToken = default)
        {
            await _client.DeleteMessageAsync(
                chatId: ChatId(targetChat),
                messageId: _settings.LastMessage.MessageId,
                cancellationToken: cancellationToken);
        }

        private long ChatId(BotTargetChat targetChat) => targetChat switch
        {
            BotTargetChat.Bot => _settings.BotChat.Id,
            BotTargetChat.Channel => _settings.ChannelChat.Id,
            _ => _settings.BotChat.Id
        };
    }
}
