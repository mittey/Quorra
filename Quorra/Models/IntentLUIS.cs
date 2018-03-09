using Newtonsoft.Json;

namespace Quorra.Models
{
    public class IntentLuis
    {
        [JsonProperty(PropertyName = "intent")]
        public string Intent { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}
