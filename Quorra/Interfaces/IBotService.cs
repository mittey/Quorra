using System.Threading.Tasks;
using Telegram.Bot;

namespace Quorra.Interfaces
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}
