namespace KiwigoldBot.Helpers
{
    public static class TypeHelper
    {
        public static IEnumerable<Type> GetTypes(string @namespace)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(a => a.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == @namespace);
        }
    }
}
