using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageFact: MicrosoftTeamsMessageBody
    {
        [JsonPropertyName("title")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
