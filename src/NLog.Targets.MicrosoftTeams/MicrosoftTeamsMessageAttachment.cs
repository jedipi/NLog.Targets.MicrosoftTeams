using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsMessageAttachment
	{
		[JsonProperty("contentType")]
		public string ContentType { get; set; } = "application/vnd.microsoft.card.adaptive";

		[JsonProperty("content")]
		public MicrosoftTeamsMessageContent Content { get; set; }
	}
}
