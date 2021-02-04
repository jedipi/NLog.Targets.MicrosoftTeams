using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NLog.Targets.MicrosoftTeams
{
    public class MicrosoftTeamsClient
    {
        private readonly Uri _uri;
        private readonly HttpClient _client = new HttpClient();

        public MicrosoftTeamsClient(string url)
        {
            _uri = new Uri(url);
        }

        /// <summary>
        /// Create card message and send to Teams
        /// </summary>
        /// <param name="title">Card title</param>
        /// <param name="logMessage">Log message</param>
        /// <param name="facts"></param>
        /// <returns></returns>
        public async Task CreateAndSendMessage(string title, string logMessage, Dictionary<string, string> facts)
        {
            var message = CreateMessageCard(title, logMessage, facts);
            var json = JsonConvert.SerializeObject(message);

            var response = await SendMessage(json);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Rest Call Failed - {response.ReasonPhrase}");
            }
        }

        /// <summary>
        /// posts the message to the url
        /// </summary>
        /// <param name="logMessage"></param>
        private async Task<HttpResponseMessage> SendMessage(string logMessage)
        {
            var messageContent = new StringContent(logMessage);
            messageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (var httpClient = new HttpClient())
            {
                var targetUrl = _uri;
                return await httpClient.PostAsync(targetUrl, messageContent);
            }
        }

        /// <summary>
        /// Creates the Message string with card title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="logMessage"></param>
        /// <param name="facts"></param>
        /// <returns></returns>
        private MicrosoftTeamsMessageCard CreateMessageCard(string title, string logMessage, Dictionary<string, string> facts)
        {
            var level = facts.FirstOrDefault(x => x.Key.StartsWith("Level"));

            var request = new MicrosoftTeamsMessageCard
            {
                Title = title,
                Text = logMessage,
                Color = AttachementColor.GetAttachmentColor(level.Value),
                Sections = new[]
                {
                    new MicrosoftTeamsMessageSection
                    {
                        Title = "Properties",
                        Facts = facts.Where(x => !x.Key.StartsWith("Exception")).Select(x => new MicrosoftTeamsMessageFact{ Name = x.Key, Value = x.Value}).ToArray()
                    },
                    new MicrosoftTeamsMessageSection
                    {
                        Title = "Exception",
                        Facts = facts.Where(x => x.Key.StartsWith("Exception")).Select(x => new MicrosoftTeamsMessageFact{ Name = x.Key, Value = x.Value}).ToArray()
                    }
                }
            };

            return request;
        }

    }
}
