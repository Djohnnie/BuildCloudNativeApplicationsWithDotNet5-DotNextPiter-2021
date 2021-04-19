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
        private readonly ILogger<Worker> _logger;

        public Worker(
            IWebApiClient webApiClient,
            IGrpcClient grpcClient,
            ILogger<Worker> logger)
        {
            _webApiClient = webApiClient;
            _grpcClient = grpcClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var webApiStatus = await _webApiClient.GetStatus();
                _logger.LogInformation($"WebApi: {webApiStatus}");

                var grpcApiStatus = await _grpcClient.GetStatus();
                _logger.LogInformation($"gRPC: {grpcApiStatus}");

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}