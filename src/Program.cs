using KiwigoldBot.Services;
using KiwigoldBot.Controllers;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions.Di;
using KiwigoldBot.Settings;
using KiwigoldBot.Data;

var builder = WebApplication.CreateBuilder(args);

// configuration settings
builder.Services.AddBotSettings(builder.Configuration);
builder.Services.AddProviderSettings(builder.Configuration);

// connection string
string connectionString = builder.Configuration.GetConnectionString("SqliteConnection")!;

// data services
builder.Services.AddDapperContext(settings => settings.UseSqliteServer(connectionString));
builder.Services.AddScoped<IDbRepository, GenericRepository>();

// http client
builder.Services.AddBotClient();

// services
builder.Services.AddBotCommands();
builder.Services.AddBotHandlers();

builder.Services.AddScoped<IBotMessenger, BotMessenger>();
builder.Services.AddScoped<IBotPictureService, BotPictureService>();

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
