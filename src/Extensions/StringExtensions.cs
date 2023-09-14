namespace KiwigoldBot.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateLong(this string origin, int length = 32) =>
            origin[..Math.Min(origin.Length, length)] ?? origin;

    }
}