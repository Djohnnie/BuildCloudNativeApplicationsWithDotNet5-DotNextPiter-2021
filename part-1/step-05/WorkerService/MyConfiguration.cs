using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WorkerService
{
    public interface IMyConfiguration
    {
        string WebApiServiceUri { get; }
        string GrpcServiceUri { get; }
    }

    public class MyConfiguration : IMyConfiguration
    {
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

            WebApiServiceUri = webApiServiceUri;
            GrpcServiceUri = grpcApiServiceUri;
        }
    }
}