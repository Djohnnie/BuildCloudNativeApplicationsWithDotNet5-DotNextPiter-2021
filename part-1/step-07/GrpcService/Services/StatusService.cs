using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GrpcService
{
    public class StatusService : Status.StatusBase
    {
        private readonly ILogger<StatusService> _logger;

        public StatusService(ILogger<StatusService> logger)
        {
            _logger = logger;
        }

        public override async Task<StatusResponse> GetStatus(StatusRequest request, ServerCallContext context)
        {
            await Task.Delay(1000);

            return new StatusResponse
            {
                Message = Environment.MachineName
            };
        }
    }
}