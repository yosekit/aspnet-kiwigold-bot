using KiwigoldBot.Settings;

namespace KiwigoldBot.Extensions.Di
{
    public static class ConfigurationExtensions
    {
        public static ConnectionStringSettings? ConnectionString(this IConfigurationRoot configuration,
            string name, string section = "ConnectionStrings")
        {
            var connectionStrings = configuration.GetSection(section).Get<ConnectionStringSettings[]>();

            if (connectionStrings != null) 
                return connectionStrings.FirstOrDefault(x => x.Name == name);

            return null;
        }
    }
}
