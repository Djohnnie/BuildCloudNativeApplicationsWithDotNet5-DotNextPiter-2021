using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Threading.Tasks;

namespace WorkerService
{
    public interface IWebApiClient
    {
        Task<string> GetStatus();
    }

    public class WebApiClient : IWebApiClient
    {
        private readonly IMyConfiguration _configuration;

        public WebApiClient(IMyConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetStatus()
        {
            var restClient = new RestClient(_configuration.WebApiServiceUri);
            var restRequest = new RestRequest("status", Method.GET);
            var response = await restClient.ExecuteAsync<string>(restRequest);
            return response.Content;
        }
    }
}