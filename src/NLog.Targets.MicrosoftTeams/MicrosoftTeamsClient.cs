using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NLog.Targets.MicrosoftTeams
{
    public class MicrosoftTeamsClient
    {
        private readonly Uri _uri;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new MicrosoftTeamsMessageBodyConverter() }
        };

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
        public async Task CreateAndSendMessage(string title, string logMessage, string level, Dictionary<string, string> facts)
        {
            var message = CreateMessageCard(title, logMessage, level, facts);
            var json = JsonSerializer.Serialize(message, _jsonOptions);

            NLog.Common.InternalLogger.Log(LogLevel.Info, json);
            
            var response = await SendMessage(json).ConfigureAwait(false);
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
                return await httpClient.PostAsync(targetUrl, messageContent).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Creates the Message string with card title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="logMessage"></param>
        /// <param name="level"></param>
        /// <param name="facts"></param>
        /// <returns></returns>
        private MicrosoftTeamsMessageCard CreateMessageCard(string title, string logMessage, string level, Dictionary<string, string> facts)
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
