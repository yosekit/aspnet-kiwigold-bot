﻿namespace KiwigoldBot.Models
{
    public record Picture
    {
        public int Id { get; set; }
        public string FileId { get; set; }

        public override string ToString() => FileId ?? string.Empty;
    }
}
