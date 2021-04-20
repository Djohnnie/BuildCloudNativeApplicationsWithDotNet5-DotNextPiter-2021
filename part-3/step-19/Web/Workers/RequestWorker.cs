using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.Configuration;
using Web.Helpers;

namespace Web.Workers
{
    public class RequestWorker : BackgroundService
    {
        private readonly RequestHelper _requestHelper;
        private readonly IMyConfiguration _configuration;
        private readonly ILogger<RequestWorker> _logger;

        public RequestWorker(
            RequestHelper requestHelper,
            IMyConfiguration configuration,
            ILogger<RequestWorker> logger)
        {
            _requestHelper = requestHelper;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var client = new RestClient(_configuration.WebApiServiceUri);
                    var request = new RestRequest("status", Method.GET);
                    var response = await client.ExecuteAsync(request);

                    if (response.IsSuccessful)
                    {
                        _logger.LogInformation($"{_configuration.WebApiServiceUri} returned {response.Content}");
                        _requestHelper.Register(response.Content);
                    }

                    await Task.Delay(100, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
        }
    }
}