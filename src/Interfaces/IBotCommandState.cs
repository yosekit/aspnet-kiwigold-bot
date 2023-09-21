namespace KiwigoldBot.Interfaces
{
    public interface IBotCommandState
    {
        public BotCommandContext? Context { get; }
        public bool IsActive { get; }

        public Type? GetActive();
        public void SetActive(Type type);
        public void SetActive<T>() where T : IBotCommand;
        public void ClearActive();
    }

    public abstract class BotCommandContext
    {
    }
}
