using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;
using KiwigoldBot.Models;

namespace KiwigoldBot.Services
{
    public class BotMessageHandler
	{
        private readonly ITelegramBotClient _client;
        private readonly IBotCommandService _commands;
        private readonly IRepository _repository;

        public BotMessageHandler(
            ITelegramBotClient client, 
            IBotCommandService commandService,
            IRepository repository)
        {
            _client = client;
            _commands = commandService;
            _repository = repository;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
		{
            var handler = message.Type switch
            {
                MessageType.Text => OnText(message, cancellationToken),
                _ => OnDiscard(message, cancellationToken),
            };

            await handler;
        }

        private async Task OnText(Message message, CancellationToken cancellationToken)
        {
            string messageText = message.Text!;

            var command = _commands.Get(messageText);

            if (command is not null)
            {
                await command.ExecuteAsync(message, cancellationToken);
            } 
            else
            {
                // Example
                object result = _repository.Get<Picture>(1);

                await _client.SendTextMessageAsync(message.Chat.Id, result.ToString() ?? "null", 
                    cancellationToken: cancellationToken);
            }

        }

        private Task OnDiscard(Message message, CancellationToken cancellationToken)
        {
            // TODO: log

            return Task.CompletedTask;
        }
    }
}
