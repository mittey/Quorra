using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Quorra.Interfaces
{
    public interface IUpdateService
    {
        Task<Message> Receive(Update update);
    }
}
