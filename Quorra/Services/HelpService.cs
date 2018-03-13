using System.Threading.Tasks;
using Quorra.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace Quorra.Services
{
    public class HelpService : IHelpService
    {
        private readonly IBotService _botService;

        public HelpService(IBotService botService)
        {
            _botService = botService;
        }

        public async Task HandleHelpAsync(Message message)
        {
            await ShowHelpCategoriesAsync(message);
        }

        private async Task ShowHelpCategoriesAsync(Message message)
        {
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("Office"),
                        new KeyboardButton("Teams")
                    }
                }
            };

            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "Categories", replyMarkup: rkm);
        }

        private async Task ShowTextByCategoryAsync(Message message, string category)
        {
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, category);
        }
    }
}
