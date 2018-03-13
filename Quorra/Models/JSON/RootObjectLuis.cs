using System.Collections.Generic;
using Newtonsoft.Json;

namespace Quorra.Models.JSON
{
    public class RootObjectLuis
    {
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }

        [JsonProperty(PropertyName = "topScoringIntent")]
        public TopScoringIntentLuis TopScoringIntent { get; set; }

        [JsonProperty(PropertyName = "intents")]
        public List<IntentLuis> Intents { get; set; }

        [JsonProperty(PropertyName = "entities")]
        public List<EntityLuis> Entities { get; set; }
    }
}
