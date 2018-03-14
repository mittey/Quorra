using System.Threading.Tasks;
using Quorra.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quorra.Services
{
    public class NoneService : INoneService
    {
        private readonly IBotService _botService;

        public NoneService(IBotService botService)
        {
            _botService = botService;
        }

        public async Task HandleNoneAsync(Message message)
        {
            await TellNoneAsync(message);
        }

        private async Task TellNoneAsync(Message message)
        {
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "Sorry, I don't understand you...");
            await _botService.TelegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(1000);
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, "yet");
        }
    }
}
