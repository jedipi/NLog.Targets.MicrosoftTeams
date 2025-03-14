﻿using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageFact: MicrosoftTeamsMessageBody
    {
        [JsonProperty("title")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
