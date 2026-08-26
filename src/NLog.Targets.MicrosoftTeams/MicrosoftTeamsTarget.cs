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
        [Obsolete("Use HostName instead of MachineName.")]
        public Layout MachineName { get => HostName; set => HostName = value; }

        /// <summary>
        /// The machine name of the computer
        /// </summary>
        [RequiredParameter]
        public Layout HostName { get; set; }

        /// <summary>
        /// CardTitle
        /// </summary>
        [RequiredParameter]
        public Layout CardTitle { get; set; }

        /// <summary>
        /// Facts
        /// </summary>
        [ArrayParameter(typeof(TargetPropertyWithContext), "fact")]
        public virtual IList<TargetPropertyWithContext> Facts => ContextProperties;

        /// <summary>
        /// Construction
        /// </summary>        
        public MicrosoftTeamsTarget()
        {
            CardTitle = "${logger}";
            HostName = "${hostname}";
            Layout = "${message}";
            ApplicationName = "${appdomain:format=friendly}";
        }

        protected override void InitializeTarget()
        {
            if (this.Facts.Count == 0)
            {
                // Add default facts whene none provided
                this.Facts.Add(new TargetPropertyWithContext("Application", this.ApplicationName));
                this.Facts.Add(new TargetPropertyWithContext("Message", this.Layout));
                this.Facts.Add(new TargetPropertyWithContext("Level", "${level}"));
                this.Facts.Add(new TargetPropertyWithContext("Logger", "${logger}"));
                this.Facts.Add(new TargetPropertyWithContext("HostName", this.HostName));
                this.Facts.Add(new TargetPropertyWithContext("Exception Type", "${exception:format=type}"));
                this.Facts.Add(new TargetPropertyWithContext("Exception Message", "${exception:format=message}"));
                this.Facts.Add(new TargetPropertyWithContext("Exception Stacktrace", "${exception:format=stacktrace}"));
            }

            base.InitializeTarget();
        }

        /// <summary>
        /// <see cref="AsyncTaskTarget.WriteAsyncTask(LogEventInfo, CancellationToken)"/>
        /// </summary>
        /// <param name="logEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var facts = new Dictionary<string, string>(this.Facts.Count);
            foreach (var fact in this.Facts)
            {
                var factName = fact.Name;
                if (string.IsNullOrEmpty(factName))
                    continue;
                var factValue = RenderLogEvent(fact.Layout, logEvent);
                if (!fact.IncludeEmptyValue && string.IsNullOrEmpty(factValue))
                    continue;
                facts[factName] = factValue;
            }
            if (this.IncludeEventProperties && logEvent.HasProperties)
            {
                var excludeProperties = this.ExcludeProperties?.Count > 0 ? this.ExcludeProperties : null;
                foreach (var prop in logEvent.Properties)
                {
                    var propName = prop.Key?.ToString() ?? string.Empty;
                    if (string.IsNullOrEmpty(propName))
                        continue;
                    if (excludeProperties?.Contains(propName) == true)
                        continue;
                    facts[propName] = prop.Value?.ToString() ?? string.Empty;
                }
            }

            var level = logEvent.Level.ToString();
            var title = RenderLogEvent(this.CardTitle, logEvent);
            var webHookUrl = RenderLogEvent(this.WebhookUrl, logEvent);

            var client = new MicrosoftTeamsClient(webHookUrl);
            await client.CreateAndSendMessage(title, level, facts).ConfigureAwait(false);
        }       
    }
}
