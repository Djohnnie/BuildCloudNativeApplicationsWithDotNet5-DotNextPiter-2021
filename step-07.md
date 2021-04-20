# Putting everything together with custom configuration and logging

[Previous step](step-06.md) - [Next step](step-08.md)

[Link to example code inside this repository](part-1/step-07/)

Thanks to .NET 5, many concepts like Dependency Injection, Logging and Configuration are extensible.

By default, ILogger<T> logs to the console window, but thanks to the extensible nature of ILogger and the ILoggingBuilder, eveyone can extend this like they want.

SeriLog is a nice framework for logging that supports ILogger and can extend it.

Modify the WorkerService project and add some SeriLog NuGet packages:

[WorkerService.csproj](part-1/step-07/WorkerService/WorkerService.csproj)

```xml
<Project Sdk="Microsoft.NET.Sdk.Worker">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <UserSecretsId>dotnet-WorkerService-F47A1FEB-1F5D-426D-BF50-F7977E113B0D</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Grpc.Net.Client" Version="2.36.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="5.0.0" />
    <PackageReference Include="Microsoft.Tye.Extensions.Configuration" Version="0.6.0-alpha.21070.5" />
    <PackageReference Include="RestSharp" Version="106.11.7" />
    <PackageReference Include="Serilog" Version="2.10.0" />
    <PackageReference Include="Serilog.Extensions.Logging" Version="3.0.1" />
    <PackageReference Include="Serilog.Sinks.Console" Version="3.1.1" />
    <PackageReference Include="Serilog.Sinks.File" Version="4.1.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\GrpcCommon\GrpcCommon.csproj" />
  </ItemGroup>
  
</Project>
```

Update Program.cs to configure the SeriLog logging framework and add environment variables to the configuration space:

[Program.cs](part-1/step-07/WorkerService/Program.cs)

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddEnvironmentVariables();
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .Enrich.With()
                        .WriteTo.Console()
                        .WriteTo.File("logging.txt")
                        .CreateLogger();

                    loggingBuilder.AddSerilog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IMyConfiguration, MyConfiguration>();
                    services.AddSingleton<IWebApiClient, WebApiClient>();
                    services.AddSingleton<IGrpcClient, GrpcClient>();
                    services.AddHostedService<Worker>();
                });
    }
}
```

Update the MyConfiguration class to have an additional configuration value "LoggingPrefix":

[MyConfiguration.cs](part-1/step-07/WorkerService/MyConfiguration.cs)

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WorkerService
{
    public interface IMyConfiguration
    {
        string LoggingPrefix { get; }
        string WebApiServiceUri { get; }
        string GrpcServiceUri { get; }
    }

    public class MyConfiguration : IMyConfiguration
    {
        public string LoggingPrefix { get; }
        public string WebApiServiceUri { get; }
        public string GrpcServiceUri { get; }

        public MyConfiguration(
            IConfiguration configuration,
            ILogger<MyConfiguration> logger)
        {
            var webApiServiceUri = $"{configuration.GetServiceUri("webapi")}";
            var grpcApiServiceUri = $"{configuration.GetServiceUri("grpc")}";

            if (string.IsNullOrEmpty(webApiServiceUri))
            {
                webApiServiceUri = configuration.GetValue<string>(nameof(WebApiServiceUri));
            }

            if (string.IsNullOrEmpty(grpcApiServiceUri))
            {
                grpcApiServiceUri = configuration.GetValue<string>(nameof(GrpcServiceUri));
            }

            logger.LogInformation($"WebApiServiceUri configuration value loaded: \"{webApiServiceUri}\"");
            logger.LogInformation($"GrpcServiceUri configuration value loaded: \"{grpcApiServiceUri}\"");

            LoggingPrefix = configuration.GetValue<string>(nameof(LoggingPrefix));
            WebApiServiceUri = webApiServiceUri;
            GrpcServiceUri = grpcApiServiceUri;
        }
    }
}
```

Update the Worker so that it uses the additional configuration value in its logging output:

[Worker.cs](part-1/step-07/WorkerService/Worker.cs)

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IWebApiClient _webApiClient;
        private readonly IGrpcClient _grpcClient;
        private readonly IMyConfiguration _myConfiguration;
        private readonly ILogger<Worker> _logger;

        public Worker(
            IWebApiClient webApiClient,
            IGrpcClient grpcClient,
            IMyConfiguration myConfiguration,
            ILogger<Worker> logger)
        {
            _webApiClient = webApiClient;
            _grpcClient = grpcClient;
            _myConfiguration = myConfiguration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var webApiStatus = await _webApiClient.GetStatus();
                _logger.LogInformation($"{_myConfiguration.LoggingPrefix}WebApi: {webApiStatus}");

                var grpcApiStatus = await _grpcClient.GetStatus();
                _logger.LogInformation($"{_myConfiguration.LoggingPrefix}gRPC: {grpcApiStatus}");

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
```

Add the additional configuration value to the appsettings.json

[appsettings.json](part-1/step-07/WorkerService/appsettings.json)

```json
{
  "LoggingPrefix": "PREFIX-FROM-APPSETTINGS-",
  "WebApiServiceUri": "http://localhost:5000",
  "GrpcServiceUri": "https://localhost:5001",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

Add the additional configuration value as an environment variable to the Project Tye configuration:

[tye.yaml](part-1/step-07/tye.yaml)

```yaml
name: cloudnative-webapi
services:
- name: webapi
  project: WebApi/WebApi.csproj
- name: grpc
  project: GrpcService/GrpcService.csproj
- name: worker-service
  project: WorkerService/WorkerService.csproj
  env:
    - name: LoggingPrefix
      value: "PREFIX-FROM-ENVIRONMENT-"
```

If you run the application from the command-line, you will notice that the output contains the configuration value from appsettings.json.
If you run the application using Project Tye, you will notice that the output contains the configuration value from tye.yaml, because it overrides the default configuration value from appsettings.json.

[Previous step](step-06.md) - [Next step](step-08.md)