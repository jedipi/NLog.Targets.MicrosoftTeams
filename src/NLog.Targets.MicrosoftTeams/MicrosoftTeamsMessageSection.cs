using System.Collections.Generic;
using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageSection
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("facts")]
        public IList<MicrosoftTeamsMessageFact> Facts { get; set; }
    }
}
