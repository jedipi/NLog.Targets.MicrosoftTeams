using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsMessageBodyTitle: MicrosoftTeamsMessageBody
	{
		[JsonPropertyName("size")]
		public string Size { get; set; } = "Medium";

		[JsonPropertyName("weight")]
		public string Weight { get; set; } = "Bolder";

		[JsonPropertyName("color")]
		public string Color { get; set; }

		[JsonPropertyName("wrap")]
		public bool wrap { get; set; } = true;

		[JsonPropertyName("text")]
		public string Text { get; set; }
	}
}
