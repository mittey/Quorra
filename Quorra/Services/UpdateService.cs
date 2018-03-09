using System.Threading.Tasks;
using Quorra.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quorra.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;

        public UpdateService(IBotService botService)
        {
            _botService = botService;
        }

        public Task<Message> Receive(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate)
            {
                return null;
            }

            return Task.FromResult(update.Message);
        }
    }
}
