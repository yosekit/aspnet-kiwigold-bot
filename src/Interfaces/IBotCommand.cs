namespace KiwigoldBot.Interfaces
{
    public interface IBotCommand
    {
        public Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken);
        public Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken);

        sealed public string GetName() => $"/{this.GetType().Name.Replace("Command", "").ToLower()}";
    }
}
