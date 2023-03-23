namespace KiwigoldBot
{
	public class BotSettings
	{
		public static readonly string JsonName = "TelegramBot";

		public string Username { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public string HostAddress { get; set; } = string.Empty;
		public string Route { get; set; } = string.Empty;
    }
}
