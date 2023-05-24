namespace KiwigoldBot.Settings
{
    public class ConnectionStringSettings
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public ProviderSettings Provider { get; set; }
    }

    public class ProviderSettings
    {
        public string InvariantName { get; set; }
        public string TypeAssemblyName { get; set; }
    }
}
