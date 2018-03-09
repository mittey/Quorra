using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Quorra.Interfaces
{
    public interface IHubService
    {
        Task HandleMessageAsync(Message message);
    }
}
