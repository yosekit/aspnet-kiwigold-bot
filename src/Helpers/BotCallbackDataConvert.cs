namespace KiwigoldBot.Helpers
{
    public class BotCallbackDataConvert
    {
        private const char Separator = ':';

        public static string ToString(Type callbackType, string callbackData)
        {
            return $"{callbackType.Name}{Separator}{callbackData}";
        }

        public static (Type, string) ToTypeAndData(string @string)
        {
            var splited = @string.Split(Separator);
            return (TypeHelper.ByName(splited[0]), splited[1]);
        }
    }
}
