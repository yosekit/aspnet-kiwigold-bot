namespace KiwigoldBot.Models
{
    public record Hashtag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name ?? string.Empty;
    }
}
