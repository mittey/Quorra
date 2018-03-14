using Newtonsoft.Json;

namespace Quorra.Models.JSON
{
    public class EntityLuis
    {
        [JsonProperty(PropertyName = "entity")]
        public string Entity { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "startIndex")]
        public int StartIndex { get; set; }

        [JsonProperty(PropertyName = "endIndex")]
        public int EndIndex { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}
