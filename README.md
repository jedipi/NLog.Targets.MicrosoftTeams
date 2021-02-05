
[![NuGet version (NLog.Targets.MicrosoftTeams)](https://img.shields.io/nuget/v/NLog.Targets.MicrosoftTeams.svg?style=flat)](https://www.nuget.org/packages/NLog.Targets.MicrosoftTeams)
[![Build Status](https://dev.azure.com/jedipi/NLog.Targets.MicrosoftTeams/_apis/build/status/jedipi.NLog.Targets.MicrosoftTeams?branchName=master)](https://dev.azure.com/jedipi/NLog.Targets.MicrosoftTeams/_build/latest?definitionId=1&branchName=master)

# NLog.Targets.MicrosoftTeams
A NLog target that write log to Microsoft Teams channel via O365 Webhook Connector.

- Support for custom ApplicationName layout
- Support custom Teams card message title
- Options to configure your Webhook URL in NLog.conf, app.config, or appsetting.json
- Support .Net Framework, .Net Core, and .Net Standard

For more Information about webhhoks in Teams read:
- https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/what-are-webhooks-and-connectors
- https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/add-incoming-webhook

# Installation 
[![NuGet downloads](https://img.shields.io/nuget/dt/NLog.Targets.MicrosoftTeams)](https://www.nuget.org/packages/NLog.Targets.MicrosoftTeams)

```cmd
PM> Install-Package NLog.Targets.MicrosoftTeams
```


# Output
![Example NLog.Targets.MicrosoftTeams output](image/output.png)

# Usage
```xml
<!-- Example app.config -->
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
  <appSettings>
    <add key="Logging.TeamsUrl" value="Your Teams Channel Webhook" />
  </appSettings>
</configuration>
```



NLog.config
```xml
<!-- This is an nlog.conf for app.config -->
<!-- write logs to Microsoft Teams -->
<target xsi:type="MicrosoftTeams" 
         name="msTeams" 
         WebhookUrl="${appsetting:name=Logging.TeamsUrl}"          
         ApplicationName="Your Application Name"
         CardTitle="Title - ${level:uppercase=true}: ${date} - [${logger}]"
         layout="[${level:uppercase=true}] ${logger} - ${message} ${all-event-properties}"
    />
```

```xml

<!-- This is an nlog.conf for appsetting.json -->
<!-- write logs to Microsoft Teams -->
<target xsi:type="MicrosoftTeams" 
         name="msTeams" 
         WebhookUrl="${configsetting:name=Logging.TeamsUrl}"          
         ApplicationName="Your Application Name"
         CardTitle="Title - ${level:uppercase=true}: ${date} - [${logger}]"
         layout="[${level:uppercase=true}] ${logger} - ${message} ${all-event-properties}"
    />
```

```xml

<!-- This is an nlog.conf for Xamarin. -->
<!-- WebhookURL can be specified inside the nlog.conf -->
<!-- write logs to Microsoft Teams -->
<target xsi:type="MicrosoftTeams" 
         name="msTeams" 
         WebhookUrl="Your Teams Webhook URL here"          
         ApplicationName="Your Application Name"
         CardTitle="Title - ${level:uppercase=true}: ${date} - [${logger}]"
         layout="[${level:uppercase=true}] ${logger} - ${message} ${all-event-properties}"
    />
```
