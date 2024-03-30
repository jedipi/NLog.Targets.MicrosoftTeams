[![NuGet版本(NLog.Targets.MicrosoftTeams)](https://img.shields.io/nuget/v/NLog.Targets.MicrosoftTeams.svg?style=flat)](https://www.nuget.org/packages/NLog.Targets.MicrosoftTeams)
[![构建状态](https://dev.azure.com/jedipi/NLog.Targets.MicrosoftTeams/_apis/build/status/jedipi.NLog.Targets.MicrosoftTeams?branchName=master)](https://dev.azure.com/jedipi/NLog.Targets.MicrosoftTeams/_build/latest?definitionId=1&branchName=master)
[![.NET](https://github.com/jedipi/NLog.Targets.MicrosoftTeams/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/jedipi/NLog.Targets.MicrosoftTeams/actions/workflows/dotnet.yml)
![GitHub](https://img.shields.io/github/license/jedipi/NLog.Targets.MicrosoftTeams)
[![NuGet下载](https://img.shields.io/nuget/dt/NLog.Targets.MicrosoftTeams)](https://www.nuget.org/packages/NLog.Targets.MicrosoftTeams)
[![点击计数](http://hits.dwyl.com/jedipi/NLogTargetsMicrosoftTeams.svg)](https://github.com/jedipi/NLog.Targets.MicrosoftTeams)
##### 构建历史
[![构建历史](https://buildstats.info/github/chart/jedipi/NLog.Targets.MicrosoftTeams?branch=master)](https://github.com/jedipi/NLog.Targets.MicrosoftTeams/actions?query=branch%3Amaster)
##### Stargazers & Forkers
[![Stargazers名册 @jedipi/NLog.Targets.MicrosoftTeams](https://reporoster.com/stars/jedipi/NLog.Targets.MicrosoftTeams)](https://github.com/jedipi/NLog.Targets.MicrosoftTeams/stargazers)
[![Forkers名册 @jedipi/NLog.Targets.MicrosoftTeams](https://reporoster.com/forks/jedipi/NLog.Targets.MicrosoftTeams)](https://github.com/jedipi/NLog.Targets.MicrosoftTeams/network/members)


# NLog.Targets.MicrosoftTeams 
![](image/nlog-teams.png)
<br>
一个NLog目标，通过O365 Webhook连接器将日志写入Microsoft Teams频道。

- 支持自定义ApplicationName布局
- 支持自定义Teams消息卡片标题
- 支持.Net Framework、.Net Core、.Net 5/6/7/8和.Net Standard
- 可以在NLog.conf、app.config或appsetting.json中配置你的Webhook URL


关于Teams中的webhhoks的更多信息请阅读:
- https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/what-are-webhooks-and-connectors
- https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/add-incoming-webhook

# 输出
![示例NLog.Targets.MicrosoftTeams输出](image/output.png)

# 入门
### 安装 

将NLog.Targets.MicrosoftTeams包从nuget添加到你的项目中。

```cmd
PM> Install-Package NLog.Targets.MicrosoftTeams


<br>

### Usage
```xml
<!-- 示例 app.config -->
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
  <appSettings>
    <add key="Logging.TeamsUrl" value="你的Teams频道Webhook" />
  </appSettings>
</configuration>
```



NLog.config
```xml
NLog.config
<!-- 从app.config获取Webhook URL的示例 -->
<!-- 将日志写入Microsoft Teams -->
<target xsi:type="MicrosoftTeams, NLog.Targets.MicrosoftTeams" 
         name="msTeams" 
         WebhookUrl="${appsetting:name=Logging.TeamsUrl}"          
         ApplicationName="你的应用名称"
         CardTitle="标题 - ${level:uppercase=true}: ${date} - [${logger}]"
         layout="[${level:uppercase=true}] ${logger} - ${message} ${all-event-properties}"
    />
```

```xml
<!-- 从appsetting.json获取Webhook URL的示例 -->
<!-- 将日志写入Microsoft Teams -->
<target xsi:type="MicrosoftTeams, NLog.Targets.MicrosoftTeams" 
         name="msTeams" 
         WebhookUrl="${configsetting:name=Logging.TeamsUrl}"          
         ApplicationName="你的应用名称"
         CardTitle="标题 - ${level:uppercase=true}: ${date} - [${logger}]"
         layout="[${level:uppercase=true}] ${logger} - ${message} ${all-event-properties}"
    />
```

```xml

<!-- 在nlog.conf内设置Webhook URL的示例 -->
<!-- 将日志写入Microsoft Teams -->
<target xsi:type="MicrosoftTeams, NLog.Targets.MicrosoftTeams" 
         name="msTeams" 
         WebhookUrl="你的Teams Webhook URL在这里"          
         ApplicationName="你的应用名称"
         CardTitle="标题 - ${level:uppercase=true}: ${date} - [${logger}]"
         layout="[${level:uppercase=true}] ${logger} - ${message} ${all-event-properties}"
    />

```


# Support
如果你从我创建的任何内容中获得了价值，就请我喝一杯吧。

[![请我喝一杯](https://www.lifeofanarchitect.com/wp-content/uploads/2017/12/Ko-Fi-Image-Buy-Me-a-Beer.png)](https://www.paypal.com/donate/?hosted_button_id=WW82TCHX3P6EG)





