using Telegram.Bot;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Commands;
using KiwigoldBot.Settings;

namespace KiwigoldBot.Services
{
    public class BotCommandService : IBotCommandService
    {
        private readonly Dictionary<string, BotCommandBase> _commands = new();

        public BotCommandService(ITelegramBotClient client, BotSettings settings) 
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == typeof(BotCommandBase).Namespace);

            foreach (var command in settings.Commands)
            {
                string commandName = command.Command;

                var obj = (BotCommandBase)Activator.CreateInstance(
                    assemblies.First(t => t.Name.ToLower().StartsWith(commandName.Trim('/'))), 
                    client)!;

                _commands.Add(commandName, obj);
            }
        }

        public BotCommandBase? Get(string name) => _commands.GetValueOrDefault(name);
    }
}
