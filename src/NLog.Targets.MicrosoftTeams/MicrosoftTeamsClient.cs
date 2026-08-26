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
        public async Task CreateAndSendMessage(string title, string level, Dictionary<string, string> facts)
        {
            var message = CreateMessageCard(title, level, facts);
            var jsonContent = JsonConvert.SerializeObject(message);

            NLog.Common.InternalLogger.Debug("MicrosoftTeamsTarget - JSON:{0}{1}", Environment.NewLine, jsonContent);
            
            var response = await SendMessage(jsonContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode(); // Throws when not success http statuscode
        }

        /// <summary>
        /// posts the message to the url
        /// </summary>
        private async Task<HttpResponseMessage> SendMessage(string jsonContent)
        {
            var messageContent = new StringContent(jsonContent);
            messageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (var httpClient = new HttpClient())
            {
                var targetUrl = _uri;
                return await httpClient.PostAsync(targetUrl, messageContent).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Creates the Message string with card title
        /// </summary>
        private MicrosoftTeamsMessageCard CreateMessageCard(string title, string level, Dictionary<string, string> facts)
        {
            var request = new MicrosoftTeamsMessageCard
            {
                Attachments = new[]
                {
                    new MicrosoftTeamsMessageAttachment()
                    {
                        Content = new MicrosoftTeamsMessageContent()
                        {
                            MSteams = new MicrosoftTeamsConfig(),

                            Body = new MicrosoftTeamsMessageBody[]
                            {
                                new MicrosoftTeamsMessageBodyTitle
                                {
                                    Text = title,
                                    Color = AttachementColor.GetAttachmentColor(level)
								},
                                new MicrosoftTeamsMessageBodyFacts
                                {
                                    Facts = facts.Where(x => !x.Key.StartsWith("Exception")).Select(x => new MicrosoftTeamsMessageFact{ Name = x.Key, Value = x.Value}).ToArray()
                                },
                                 new MicrosoftTeamsMessageBodyFacts
                                {
                                    Facts = facts.Where(x => x.Key.StartsWith("Exception")).Select(x => new MicrosoftTeamsMessageFact{ Name = x.Key, Value = x.Value}).ToArray()
                                }
                          }
                        }
                    }
                }

            };

            return request;
        }

    }
}
