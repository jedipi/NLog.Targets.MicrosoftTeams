using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsMessageAttachment
	{
		[JsonPropertyName("contentType")]
		public string ContentType { get; set; } = "application/vnd.microsoft.card.adaptive";

		[JsonPropertyName("content")]
		public MicrosoftTeamsMessageContent Content { get; set; }
	}
}
