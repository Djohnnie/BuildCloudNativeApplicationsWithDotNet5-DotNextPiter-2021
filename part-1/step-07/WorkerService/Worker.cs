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