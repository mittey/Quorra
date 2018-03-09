using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quorra.Data;
using Quorra.Interfaces;
using Quorra.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quorra.Services
{
    public class HubService : IHubService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBotService _botService;
        private readonly ILuisService _luisService;
        private readonly IJokeService _jokeService;

        public HubService(ApplicationDbContext context, IBotService botService, ILuisService luisService, IJokeService jokeService)
        {
            _context = context;
            _botService = botService;
            _luisService = luisService;
            _jokeService = jokeService;
        }

        public async Task HandleMessageAsync(Message message)
        {
            if (message.Type == MessageType.TextMessage)
            {
                await HandleTextMessageAsync(message);
            }
        }

        private async Task HandleTextMessageAsync(Message message)
        {
            if (!await UserExistsAsync(message.Chat.Username))
            {
                await RememberUserAsync(message);

                //Send an answer to Telegram
                await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Hi, {message.Chat.Username}! It looks like this is our first conversation. Nice to meet you!");
            }

            var topScoringIntent = await _luisService.GetTopScoringIntentAsync(message.Text);

            if (topScoringIntent == "Joke.Show")
            {
                await TellJokeAsync(message);
            }

            if (topScoringIntent == "None")
            {
                await TellNoneAsync(message);
            }
        }

        private async Task TellJokeAsync(Message message)
        {
            var joke = await _jokeService.GetJokeAsync();

            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, joke.Setup);
            await _botService.TelegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(1500);
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, joke.Punchline);
        }

        private async Task TellNoneAsync(Message message)
        {
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "Sorry, I don't understand you...");
            await _botService.TelegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(1500);
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "yet");
        }

        private async Task RememberUserAsync(Message message)
        {
            var user = new Models.User()
            {
                Username = message.Chat.Username
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> UserExistsAsync(string usernameFromTelegram)
        {
            return await _context.Users.AnyAsync(m => m.Username == usernameFromTelegram);
        }
    }
}
