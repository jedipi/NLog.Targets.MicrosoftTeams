using System.Collections.Generic;
using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageBodyFacts : MicrosoftTeamsMessageBody
    {
        [JsonProperty("facts")]
        public IList<MicrosoftTeamsMessageFact> Facts { get; set; }

        public MicrosoftTeamsMessageBodyFacts()
        {
            Type = "FactSet";
        }
    }
}
