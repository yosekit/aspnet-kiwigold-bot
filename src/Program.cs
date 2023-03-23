using Telegram.Bot;

using KiwigoldBot;
using KiwigoldBot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(
	builder.Configuration.GetSection(BotSettings.JsonName).Get<BotSettings>()!);

builder.Services
	.AddHttpClient("telegram_bot_client")
	.AddTypedClient<ITelegramBotClient>((client, sp) =>
	{
		var settings = sp.GetRequiredService<BotSettings>();
		var options = new TelegramBotClientOptions(settings.Token);
		return new TelegramBotClient(options, client);
	});

builder.Services.AddScoped<BotUpdateHandler>();

builder.Services.AddHostedService<BotWebhookHosted>();

builder.Services
	.AddControllers()
	.AddNewtonsoftJson();

var app = builder.Build();

app.MapControllers();

app.Run();
