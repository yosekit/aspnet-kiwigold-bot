namespace KiwigoldBot.Data
{
    public static class DbTables
    {
        public static string Picture { get; } = "Picture";
        public static string Author { get; } = "Author";
        public static string Title { get; } = "Title";
        public static string Hashtag { get; } = "Hashtag";

        public static string[] AsArray() => new[] { Picture, Author, Title, Hashtag };
        public static bool IsTable(string @string)
            => AsArray().Any(x => x.ToLower() == @string.ToLower());
    }
}
