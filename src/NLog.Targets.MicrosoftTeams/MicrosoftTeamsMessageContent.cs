using Newtonsoft.Json;
using System.Collections.Generic;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageContent
    {
        [JsonProperty("$schema")]
        public string Schema { get; set; } = "http://adaptivecards.io/schemas/adaptive-card.json";

        [JsonProperty("type")]
        public string Type { get; set; } = "AdaptiveCard";

        [JsonProperty("version")]
        public string Version { get; set; } = "1.5";

        [JsonProperty("msteams")]
        public MicrosoftTeamsConfig MSteams { get; set; }

        [JsonProperty("body")]
		public IList<MicrosoftTeamsMessageBody> Body { get; set; }
	}
}
