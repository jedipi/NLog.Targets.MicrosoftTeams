using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NLog.Config;
using NLog.Layouts;

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
        public Layout WebhookUrl { get; set; }

        /// <summary>
        /// Name of the Accplication<br/>
        /// Will be displayed as Title in the default card layout
        /// </summary>
        [RequiredParameter]
        public Layout ApplicationName { get; set; }

        /// <summary>
        /// The machine name of the computer
        /// </summary>
        [RequiredParameter]
        public Layout MachineName { get; set; }

        /// <summary>
        /// CardTitle
        /// </summary>
        [RequiredParameter]
        public Layout CardTitle { get; set; }

        // <summary>
        /// Construction
        /// </summary>        
        public MicrosoftTeamsTarget()
        {
            IncludeEventProperties = true; // Include LogEvent Properties by default
            MachineName = "${machinename}";
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

            var applicationName = RenderLogEvent(this.ApplicationName, logEvent);
            var machineName = RenderLogEvent(this.MachineName, logEvent);

            facts.Add("Application", applicationName);
            facts.Add("Machine", machineName);
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
            var webHookUrl = RenderLogEvent(this.WebhookUrl, logEvent);

            var client = new MicrosoftTeamsClient(webHookUrl);
            await client.CreateAndSendMessage(title, logMessage, facts).ConfigureAwait(false);
        }       
    }
}
