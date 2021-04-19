using Grpc.Net.Client;
using System.Threading.Tasks;
using GrpcService;

namespace WorkerService
{
    public interface IGrpcClient
    {
        Task<string> GetStatus();
    }

    public class GrpcClient : IGrpcClient
    {
        private readonly IMyConfiguration _configuration;

        public GrpcClient(IMyConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetStatus()
        {
            var grpcChannel = GrpcChannel.ForAddress(_configuration.GrpcServiceUri);
            var grpcClient = new Status.StatusClient(grpcChannel);
            var response = await grpcClient.GetStatusAsync(new StatusRequest());
            return response.Message;
        }
    }
}