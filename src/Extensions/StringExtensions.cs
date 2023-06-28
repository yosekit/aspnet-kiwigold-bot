namespace KiwigoldBot.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateLong(this string origin, int length = 32) =>
            origin[..Math.Min(origin.Length, length)] ?? origin;


        public static bool IsCommand(this string origin)
        {
            // TODO: change handling command name

            return origin.StartsWith("/");
        }

        public static bool IsLink(this string origin)
        {
            return Uri.TryCreate(origin, UriKind.Absolute, out _);
        }
    }
}