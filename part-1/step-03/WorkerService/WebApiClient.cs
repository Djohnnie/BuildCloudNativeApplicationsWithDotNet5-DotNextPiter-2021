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
        private readonly IConfiguration _configuration;

        public WebApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetStatus()
        {
            var restClient = new RestClient(_configuration.GetServiceUri("webapi"));
            var restRequest = new RestRequest("status", Method.GET);
            var response = await restClient.ExecuteAsync<string>(restRequest);
            return response.Content;
        }
    }
}