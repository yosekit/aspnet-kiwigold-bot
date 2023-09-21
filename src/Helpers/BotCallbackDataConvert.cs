using KiwigoldBot.Commands;

namespace KiwigoldBot.Helpers
{
    public class BotCallbackDataConvert
    {
        private const char Separator = ':';

        public static string ToString(Type commandType, string callbackData)
        {
            return $"{commandType.Name}{Separator}{callbackData}";
        }

        public static (Type, string) ToTypeAndData(string @string)
        {
            var splited = @string.Split(Separator);
            return (GetCommandType(splited[0]), splited[1]);
        }

        private static Type GetCommandType(string typeName)
        {
            // using StartCommand class because it's basic command 
            string @namespace = typeof(StartCommand).Namespace!;

            return TypeHelper.GetTypes(@namespace).First(t => t.Name == typeName);
        }
    }
}
