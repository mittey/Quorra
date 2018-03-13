using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Quorra.Interfaces
{
    public interface IUserService
    {
        Task HandleUserAsync(Message message);
    }
}
