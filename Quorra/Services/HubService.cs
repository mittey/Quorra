using System.Threading.Tasks;
using Quorra.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quorra.Services
{
    public class HubService : IHubService
    {
        private readonly ILuisService _luisService;
        private readonly IJokeService _jokeService;
        private readonly IHelpService _helpService;
        private readonly INoneService _noneService;
        private readonly IUserService _userService;

        public HubService(ILuisService luisService,
            IJokeService jokeService,
            IHelpService helpService,
            INoneService noneService,
            IUserService userService)
        {
            _luisService = luisService;
            _jokeService = jokeService;
            _helpService = helpService;
            _noneService = noneService;
            _userService = userService;
        }

        public async Task HandleMessageAsync(Message message)
        {
            if (message.Type == MessageType.TextMessage)
            {
                await HandleTextMessageAsync(message);
            }

            if (message.Type == MessageType.VoiceMessage)
            {
                await HandleVoiceMessageAsync(message);
            }
        }

        private async Task HandleTextMessageAsync(Message message)
        {
            await _userService.HandleUserAsync(message);

            var luisData = await _luisService.GetAllDataAsync(message.Text);

            if (luisData.TopScoringIntent.Intent == "Help.Show")
            {
                await _helpService.HandleHelpAsync(message);
            }

            if (luisData.TopScoringIntent.Intent == "Joke.Show")
            {
                await _jokeService.HandleJokeAsync(message);
            }

            if (luisData.TopScoringIntent.Intent == "None")
            {
                await _noneService.HandleNoneAsync(message);
            }
        }

        private Task HandleVoiceMessageAsync(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}
