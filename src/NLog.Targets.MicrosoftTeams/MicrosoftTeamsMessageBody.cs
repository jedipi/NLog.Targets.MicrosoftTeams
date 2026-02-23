using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
	internal abstract class MicrosoftTeamsMessageBody
	{
		[JsonPropertyName("type")]
		public string Type { get; set; } = "TextBlock";

	}
}
