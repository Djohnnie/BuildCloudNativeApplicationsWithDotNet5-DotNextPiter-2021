# Project Tye and custom configuration

[Previous step](step-03.md) - [Next step](step-05.md)

[Link to example code inside this repository](part-1/step-04/)

Add a custom configuration manager class to your Worker project:

[MyConfiguration.cs](part-1/step-04/WorkerService/MyConfiguration.cs)

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WorkerService
{
    public interface IMyConfiguration
    {
        string WebApiServiceUri { get; }
    }

    public class MyConfiguration : IMyConfiguration
    {
        public string WebApiServiceUri { get; }

        public MyConfiguration(
            IConfiguration configuration,
            ILogger<MyConfiguration> logger)
        {
            var webApiServiceUri = $"{configuration.GetServiceUri("webapi")}";

            if (string.IsNullOrEmpty(webApiServiceUri))
            {
                webApiServiceUri = configuration.GetValue<string>(nameof(WebApiServiceUri));
            }

            logger.LogInformation($"WebApiServiceUri configuration value loaded: \"{webApiServiceUri}\"");

            WebApiServiceUri = webApiServiceUri;
        }
    }
}
```

Add an application configuration property to appsettings.json:

[appsettings.json](part-1/step-04/WorkerService/appsettings.json)

```json
{
  "WebApiServiceUri": "http://localhost:5000",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

You can now run the application services from command-line, and with project Tye!

[Previous step](step-03.md) - [Next step](step-05.md)