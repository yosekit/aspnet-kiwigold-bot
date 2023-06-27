using KiwigoldBot.Services;
using KiwigoldBot.Controllers;
using KiwigoldBot.Interfaces;
using KiwigoldBot.Extensions.Di;
using KiwigoldBot.Settings;
using KiwigoldBot.Data;

var builder = WebApplication.CreateBuilder(args);

// settings
builder.Services.AddBotSettings(builder.Configuration);

// data services
builder.Services.AddDbConnectionManager(builder.Configuration.ConnectionString("Sqlite")!);
builder.Services.AddSingleton<IDbContext, DapperContext>();
builder.Services.AddSingleton<IDbRepository, GenericRepository>();

// http client
builder.Services.AddBotClient();

// services
builder.Services.AddSingleton<IBotCommandService, BotCommandService>();
builder.Services.AddSingleton<BotMessageHandler>();
builder.Services.AddSingleton<BotUpdateHandler>();

// hosted service
builder.Services.AddHostedService<BotWebhookHosted>();

// built-in services
builder.Services
	.AddControllers()
	.AddNewtonsoftJson();


var app = builder.Build();

app.SetBotCommands();

app.MapBotWebhook<BotController>(
	app.Services.GetRequiredService<BotSettings>().Route);

app.MapControllers();

app.Run();
