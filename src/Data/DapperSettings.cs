using System.Data.Common;

using KiwigoldBot.Settings;

namespace KiwigoldBot.Data
{
    public class DapperSettings
    {
        public DbProviderFactory Provider { get; set; }
        public string ConnectionString { get; set; }
    }

    public class DapperSettingsBuilder
    {
        private readonly DapperSettings _settings = new();

        private readonly ProviderSettingsResolver _providers;

        public DapperSettingsBuilder(ProviderSettingsResolver providers)
        {
            _providers = providers;
        }

        public DapperSettingsBuilder UseSqliteServer(string connectionString)
        {
            var provider = _providers.GetSettings("SQLite");

            DbProviderFactories.RegisterFactory(provider.InvariantName, provider.FactoryTypeAssemblyName);

            _settings.Provider = DbProviderFactories.GetFactory(provider.InvariantName);
            _settings.ConnectionString = connectionString;

            return this;
        }

        public DapperSettings Build() => _settings;
    }
}
