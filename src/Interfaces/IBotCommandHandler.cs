namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandHandler
    {
        public bool IsCommandActive { get; }

        public Task ExecuteNewCommandAsync(string commandName, string[]? commandArgs, CancellationToken cancellationToken);
        public Task ExecuteActiveCommandAsync(string text, CancellationToken cancellationToken);
        public Task ExecuteCallbackCommandAsync(Type commandType, string callbackData, CancellationToken cancellationToken);
    }
}
