using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageCard
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "message";

        [JsonPropertyName("attachments")]
        public IList<MicrosoftTeamsMessageAttachment> Attachments { get; set; }
    }
}
