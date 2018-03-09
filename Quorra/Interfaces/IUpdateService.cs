using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace Quorra.Interfaces
{
    public interface IUpdateService
    {
        Task EchoAsync(Update update);
    }
}
