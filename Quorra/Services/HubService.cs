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

        public HubService(ApplicationDbContext context, IBotService botService)
        {
            _context = context;
            _botService = botService;
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
