using Telegram.Bot.Types.ReplyMarkups;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Helpers
{
    public static class BotReplyMarkup
    {
        public static BotReplyKeyboardBuilder ReplyKeyboard() => new();

        public static BotInlineKeyboardBuilder InlineKeyboard() => new();
    }

    public class BotReplyKeyboardBuilder
    {
        public ReplyKeyboardMarkup WithSimpleText(string button) =>
            new(new[] { new[] { new KeyboardButton(button) } });

        public ReplyKeyboardMarkup WithSimpleText(IEnumerable<string> buttonRow) =>
            new(new[] { buttonRow.Select(x => new KeyboardButton(x)) });

        public ReplyKeyboardMarkup WithSimpleText(IEnumerable<IEnumerable<string>> buttons) =>
            new(buttons.Select(buttonRow => buttonRow.Select(button => new KeyboardButton(button))));
    }

    public class BotInlineKeyboardBuilder
    {
        public InlineKeyboardMarkup WithCallbackData<T>(Dictionary<string, string> textAndCallbackDatas)
            where T : IBotCommand => 
            new(textAndCallbackDatas.Select(x =>
                InlineKeyboardButton.WithCallbackData(x.Key, BotCallbackDataConvert.ToString(typeof(T), x.Value))));

        public InlineKeyboardMarkup WithCallbackData<T>(IEnumerable<string> textAndCallbackDatas)
            where T : IBotCommand =>
            new(textAndCallbackDatas.Select(x => 
                InlineKeyboardButton.WithCallbackData(x, BotCallbackDataConvert.ToString(typeof(T), x))));
    }
}
