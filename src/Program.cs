using KiwigoldBot;
using KiwigoldBot.Services;
using KiwigoldBot.Controllers;
using KiwigoldBot.Extensions.Di;

var builder = WebApplication.CreateBuilder(args);

// settings
builder.Services.AddBotSettings(builder.Configuration);

// http client
builder.Services.AddBotClient();

// services
builder.Services.AddSingleton<BotUpdateHandler>();

// hosted service
builder.Services.AddHostedService<BotWebhookHosted>();

// built-in services
builder.Services
	.AddControllers()
	.AddNewtonsoftJson();


var app = builder.Build();

app.MapBotWebhook<BotController>(
	app.Services.GetRequiredService<BotSettings>().Route);

app.MapControllers();

app.Run();
