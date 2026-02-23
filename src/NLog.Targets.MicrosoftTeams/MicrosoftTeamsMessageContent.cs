using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsMessageContent
	{
		[JsonPropertyName("$schema")]
		public string Schema { get; set; } = "http://adaptivecards.io/schemas/adaptive-card.json";

		[JsonPropertyName("type")]
		public string Type { get; set; } = "AdaptiveCard";

		[JsonPropertyName("version")]
		public string Version { get; set; } = "1.5";

		[JsonPropertyName("msteams")]
		public MicrosoftTeamsConfig MSteams { get; set; }

		[JsonPropertyName("body")]
		public IList<MicrosoftTeamsMessageBody> Body { get; set; }
	}
}
