using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Quorra.Interfaces
{
    public interface IHelpService
    {
        Task HandleHelpAsync(Message message);
    }
}
