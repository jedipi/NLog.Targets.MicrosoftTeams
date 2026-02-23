using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageBodyFacts : MicrosoftTeamsMessageBody
    {
        [JsonPropertyName("facts")]
        public IList<MicrosoftTeamsMessageFact> Facts { get; set; }

        public MicrosoftTeamsMessageBodyFacts()
        {
            Type = "FactSet";
        }
    }
}
