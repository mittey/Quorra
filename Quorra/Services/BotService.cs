﻿using Microsoft.Extensions.Options;
using Quorra.Interfaces;
using Quorra.Models.Configs;
using Telegram.Bot;

namespace Quorra.Services
{
    public class BotService : IBotService
    {
        private static IOptions<BotConfiguration> _options;

        public BotService(IOptions<BotConfiguration> options)
        {
            _options = options;

            TelegramBotClient = new TelegramBotClient(_options.Value.APIKey);
        }

        public TelegramBotClient TelegramBotClient { get; }
    }
}
