using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NLog.Config;

namespace NLog.Targets.MicrosoftTeams
{
    /// <summary>
    /// NLog Looging Target for MS Teams Incoming Webhook
    /// </summary>    
    [Target("MicrosoftTeams")]
    public class MicrosoftTeamsTarget :  AsyncTaskTarget
    {
        /// <summary>
        /// Ms Teams Incoming Webhook URL as string
        /// </summary>
        [RequiredParameter]
        public string WebhookUrl { get; set; }

        /// <summary>
        /// Name of the Accplication<br/>
        /// Will be displayed as Title in the default card layout
        /// </summary>
        [RequiredParameter]
        public string ApplicationName { get; set; }

        /// <summary>
        /// CardTitle
        /// </summary>
        [RequiredParameter]
        public string CardTitle { get; set; }

        // <summary>
        /// Construction
        /// </summary>        
        public MicrosoftTeamsTarget()
        {
            IncludeEventProperties = true; // Include LogEvent Properties by default            
        }

        /// <summary>
        /// <see cref="AsyncTaskTarget.WriteAsyncTask(LogEventInfo, CancellationToken)"/>
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {

            var facts = new Dictionary<string, string>();

            facts.Add("Application", ApplicationName);
            facts.Add("Machine", Environment.MachineName);
            facts.Add("Level", logEvent.Level.ToString());
            facts.Add("Logger", logEvent.LoggerName);

            // Add exception fields if exception occurred
            var exception = logEvent.Exception;
            if (exception != null)
            {
                facts.Add("Exception Type", exception.GetType().Name);
                facts.Add("Exception Message", exception.Message);
                facts.Add("Stacktrace", exception.StackTrace?.ToString());
            }

            var title = RenderLogEvent(this.CardTitle, logEvent);
            string logMessage = RenderLogEvent(this.Layout, logEvent);

            var client = new MicrosoftTeamsClient(WebhookUrl);
            await client.CreateAndSendMessage(title, logMessage, facts);
        }

        
        
    }
}
