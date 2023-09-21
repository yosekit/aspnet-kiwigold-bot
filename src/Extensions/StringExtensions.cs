namespace KiwigoldBot.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateLong(this string origin, int length = 32) =>
            origin[..Math.Min(origin.Length, length)] ?? origin;

        public static string ToUpperFirst(this string origin) =>
            char.IsUpper(origin[0]) ? origin : string.Concat(origin[0].ToString().ToUpper(), origin.AsSpan(1));
    }
}