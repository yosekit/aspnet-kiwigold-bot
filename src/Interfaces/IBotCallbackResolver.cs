namespace KiwigoldBot.Interfaces
{
    public interface IBotCallbackResolver
    {
        public IBotCallback? Get(Type type);
    }
}
