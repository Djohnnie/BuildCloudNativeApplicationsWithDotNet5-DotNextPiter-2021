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
        public async Task<string> GetStatus()
        {
            var restClient = new RestClient("http://localhost:59201");
            var restRequest = new RestRequest("status", Method.GET);
            var response = await restClient.ExecuteAsync<string>(restRequest);
            return response.Content;
        }
    }
}