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
        private readonly IDistributedCache _distributedCache;

        public StatusManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> GetMachineName()
        {
            await Task.Delay(1000);

            var cachedMachineName = await _distributedCache.GetStringAsync("machine-name");

            if (string.IsNullOrEmpty(cachedMachineName))
            {
                cachedMachineName = $"{DateTime.UtcNow:HH:mm:ss}-{Environment.MachineName}";

                await _distributedCache.SetStringAsync("machine-name", cachedMachineName, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });
            }

            return cachedMachineName;
        }
    }
}