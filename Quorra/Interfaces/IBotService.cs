using Telegram.Bot;

namespace Quorra.Interfaces
{
    public interface IBotService
    {
        TelegramBotClient TelegramBotClient { get; }
    }
}
