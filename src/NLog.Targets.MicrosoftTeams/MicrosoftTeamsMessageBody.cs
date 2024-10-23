using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
	internal abstract class MicrosoftTeamsMessageBody
	{
		[JsonProperty("type")]
		public string Type { get; set; } = "TextBlock";

	}
}
