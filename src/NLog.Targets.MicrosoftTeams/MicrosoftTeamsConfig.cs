
using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsConfig
	{
		[JsonPropertyName("width")]
		public string Width { get; set; } = "Full";
	}
}
