using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotCommandState : IBotCommandState
    {
        private Type? _active = null;

        public bool IsActive => _active != null;

        public BotCommandContext? Context { get; private set; }

        public Type? GetActive() => _active;

        public void SetActive(Type type)
        {
            _active = type;
            Context = TryCreateContext(type);
        }
        public void SetActive<T>() where T : IBotCommand => SetActive(typeof(T));

        public void ClearActive()
        {
            _active = null;
            Context = null;
        }

        private static BotCommandContext? TryCreateContext(Type type)
        {
            string contextName = $"{type.FullName}Context";

            Type? contextType = Type.GetType(contextName);

            return contextType == null ? null : (BotCommandContext?)Activator.CreateInstance(contextType);
        }
    }
}
