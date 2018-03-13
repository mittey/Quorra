using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quorra.Data;
using Quorra.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quorra.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBotService _botService;

        public UserService(ApplicationDbContext context, IBotService botService)
        {
            _context = context;
            _botService = botService;
        }

        public async Task HandleUserAsync(Message message)
        {
            if (!await UserExistsAsync(message.Chat.Username))
            {
                await AddUserAsync(message);
            }
            else if (await UserExistsAsync(message.Chat.Username) && message.Text == "/start")
            {
                await TellWelcomeBackAsync(message);
            }
        }

        private async Task AddUserAsync(Message message)
        {
            var user = new Models.User()
            {
                Username = message.Chat.Username
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        private async Task TellWelcomeBackAsync(Message message)
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
