using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quorra.Data;
using Quorra.Interfaces;
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
        private readonly IHelpService _helpService;

        public HubService(ApplicationDbContext context,
            IBotService botService,
            ILuisService luisService,
            IJokeService jokeService,
            IHelpService helpService)
        {
            _context = context;
            _botService = botService;
            _luisService = luisService;
            _jokeService = jokeService;
            _helpService = helpService;
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
                await IntroduceAsync(message);
            }
            else if (await UserExistsAsync(message.Chat.Username) && message.Text == "/start")
            {
                await WelcomeBackAsync(message);
            }

            var luisData = await _luisService.GetAllDataAsync(message.Text);

            if (luisData.TopScoringIntent.Intent == "Help.Show")
            {
                await _helpService.HandleHelpAsync(message);
            }

            if (luisData.TopScoringIntent.Intent == "Joke.Show")
            {
                await TellJokeAsync(message);
            }

            if (luisData.TopScoringIntent.Intent == "None")
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
            await Task.Delay(1000);
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "yet");
        }

        private async Task RememberUserAsync(Message message)
        {
            var text = $"Hi, {message.Chat.Username}! It looks like this is our first conversation. Nice to meet you!";
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, text);

            var user = new Models.User()
            {
                Username = message.Chat.Username
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        private async Task IntroduceAsync(Message message)
        {
            var text = "My name is Quora. I'm a smart assistant at Sopra Steria Saint Petersburg.";
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, text);
        }

        private async Task WelcomeBackAsync(Message message)
        {
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Welcome back, {message.Chat.Username}");

            await _botService.TelegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(1000);

            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "How can I help you?");
        }

        private async Task<bool> UserExistsAsync(string usernameFromTelegram)
        {
            return await _context.Users.AnyAsync(m => m.Username == usernameFromTelegram);
        }
    }
}
