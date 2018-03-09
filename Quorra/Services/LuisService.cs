using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quorra.Interfaces;
using Quorra.Models;

namespace Quorra.Services
{
    public class LuisService : ILuisService
    {
        private readonly IOptions<LuisConfiguration> _options;

        public LuisService(IOptions<LuisConfiguration> options)
        {
            _options = options;
        }

        public async Task<RootObjectLuis> GetLuisDataAsync(string query)
        {
            RootObjectLuis result;

            //Get the LUIS app's configuration
            var appId = _options.Value.AppId;
            var subscriptionKey = _options.Value.SubscriptionKey;

            //Convert a string to its escaped representation.
            query = Uri.EscapeDataString(query);

            //Construct the string
            var urlToLuis = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{appId}?subscription-key={subscriptionKey}&verbose=true&timezoneOffset=180&q={query}";

            using (var client = new HttpClient())
            {
                var msg = await client.GetAsync(urlToLuis);

                var jsonDataResponse = await msg.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<RootObjectLuis>(jsonDataResponse);
            }

            return result;
        }
    }
}
