namespace KiwigoldBot.Settings
{
    public class ProviderSettings
    {
        public string Name { get; set; }
        public string InvariantName { get; set; }
        public string FactoryTypeAssemblyName { get; set; }
    }
     
    public class ProviderSettingsResolver
    {
        private readonly IEnumerable<ProviderSettings> _providers;

        public ProviderSettingsResolver(IEnumerable<ProviderSettings> providers)
        {
            _providers = providers;
        }

        public ProviderSettings GetSettings(string name) =>
            _providers.First(provider => provider.Name == name);
    }
}
