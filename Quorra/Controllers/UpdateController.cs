using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quorra.Interfaces;
using Telegram.Bot.Types;

namespace Quorra.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        private readonly IUpdateService _updateService;
        private readonly IHubService _hubService;

        public UpdateController(IUpdateService updateService, IHubService hubService)
        {
            _updateService = updateService;
            _hubService = hubService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var message = await _updateService.Receive(update);
            await _hubService.HandleMessageAsync(message);

            return Ok();
        }
    }
}