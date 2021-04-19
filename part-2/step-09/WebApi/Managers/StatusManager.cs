using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace WebApi.Managers
{
    public interface IStatusManager
    {
        Task<string> GetMachineName();
    }

    public class StatusManager : IStatusManager
    {
        public async Task<string> GetMachineName()
        {
            await Task.Delay(1000);
            return Environment.MachineName;
        }
    }
}