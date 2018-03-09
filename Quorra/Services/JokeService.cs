using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quorra.Interfaces;
using Quorra.Models;

namespace Quorra.Services
{
    public class JokeService : IJokeService
    {
        public async Task<Joke> GetJokeAsync()
        {
            Joke result;

            var urlToJoke = "https://08ad1pao69.execute-api.us-east-1.amazonaws.com/dev/random_joke";

            using (var client = new HttpClient())
            {
                var msg = await client.GetAsync(urlToJoke);

                var jsonDataResponse = await msg.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<Joke>(jsonDataResponse);
            }

            return result;
        }
    }
}
