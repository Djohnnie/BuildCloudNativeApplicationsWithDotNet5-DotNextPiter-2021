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