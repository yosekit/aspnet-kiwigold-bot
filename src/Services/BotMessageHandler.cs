using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Services
{
    public class BotMessageHandler
	{
        private readonly ITelegramBotClient _client;
        private readonly IBotCommandService _commands;

        public BotMessageHandler(ITelegramBotClient client, IBotCommandService commandService)
        {
            _client = client;
            _commands = commandService;
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
            // string phtotoId = message.Photo.Last().FileId;

            string messageText = message.Text!;

            var command = _commands.Get(messageText);

            if (command is not null)
            {
                await command.ExecuteAsync(message, cancellationToken);
            } 
            else
            {
                await _client.SendTextMessageAsync(message.Chat.Id, messageText, cancellationToken: cancellationToken);
            }

        }

        private Task OnDiscard(Message message, CancellationToken cancellationToken)
        {
            // TODO: log

            return Task.CompletedTask;
        }
    }
}
