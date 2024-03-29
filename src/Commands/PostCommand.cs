﻿using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Commands
{
    public class PostCommand : IBotCommand
    {
        private readonly IBotMessenger _messenger;

        public PostCommand(IBotMessenger messenger)
        {
            _messenger = messenger;
        }

        public async Task ExecuteAsync(string[]? args, BotCommandContext? context, CancellationToken cancellationToken)
        {
            await _messenger.SendTextAsync("Enter the message to post.", cancellationToken: cancellationToken);

        }

        public Task ExecuteNextAsync(string text, BotCommandContext? context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
