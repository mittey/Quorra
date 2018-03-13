using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quorra.Interfaces;
using Quorra.Models.JSON;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Quorra.Services
{
    public class JokeService : IJokeService
    {
        private readonly IBotService _botService;

        public JokeService(IBotService botService)
        {
            _botService = botService;
        }

        public async Task HandleJokeAsync(Message message)
        {
            await TellJokeAsync(message);
        }

        private async Task TellJokeAsync(Message message)
        {
            var joke = await GetJokeAsync();

            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, joke.Setup);
            await _botService.TelegramBotClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
            await Task.Delay(1000);
            await _botService.TelegramBotClient.SendTextMessageAsync(message.Chat.Id, joke.Punchline);
        }

        private async Task<Joke> GetJokeAsync()
        {
            Joke result = null;

            var urlToJoke = "https://08ad1pao69.execute-api.us-east-1.amazonaws.com/dev/random_joke";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(urlToJoke);

                var jsonDataResponse = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<Joke>(jsonDataResponse);
            }

            return result;
        }
    }
}
