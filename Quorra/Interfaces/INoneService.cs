using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Quorra.Interfaces
{
    public interface INoneService
    {
        Task HandleNoneAsync(Message message);
    }
}
