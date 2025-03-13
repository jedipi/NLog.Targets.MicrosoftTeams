
using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsConfig
	{
		[JsonProperty("width")]
		public string Width { get; set; } = "Full";
	}
}
