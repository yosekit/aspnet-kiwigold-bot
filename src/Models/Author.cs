namespace KiwigoldBot.Models
{
    public record Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name ?? string.Empty;
    }
}
