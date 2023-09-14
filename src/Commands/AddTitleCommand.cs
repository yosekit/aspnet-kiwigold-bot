﻿using Telegram.Bot;
using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class AddTitleCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;
        private readonly IBotCommandPoolManager _pool;

        public AddTitleCommand(IBotMessenger messenger, IBotCommandPoolManager pool)
        {
            _messenger = messenger;
            _pool = pool;
        }

        public async Task ExecuteAsync(Message message, CancellationToken cancellationToken)
        {
            // выполнить стартовый метод
            await SendRequestAsync(message, cancellationToken);

            // добавить остальные методы в пул
            _pool.Add(AddTitleAsync);
        }

        private async Task SendRequestAsync(Message message, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("Enter the title name (Any characters).", cancellationToken: cancellationToken);
        }

        private async Task AddTitleAsync(Message message, CancellationToken cancellationToken)
        {
            string title = message.Text!;

            /*await _service.SaveTitleAsync(title, message, cancellationToken);*/
        }
    }
}