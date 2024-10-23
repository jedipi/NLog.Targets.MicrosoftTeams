using System.Collections.Generic;
using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageCard
    {
        [JsonProperty("type")]
        public string Type { get; } = "message";

        [JsonProperty("attachments")]
        public IList<MicrosoftTeamsMessageAttachment> Attachments { get; set; }
    }
}
