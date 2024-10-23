using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLog.Targets.MicrosoftTeams
{
	internal class MicrosoftTeamsMessageBodyTitle: MicrosoftTeamsMessageBody
	{
		[JsonProperty("size")]
		public string Size { get; set; } = "Medium";

		[JsonProperty("weight")]
		public string Weight { get; set; } = "Bolder";

		[JsonProperty("color")]
		public string Color { get; set; }

		[JsonProperty("wrap")]
		public bool wrap { get; set; } = true;

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
