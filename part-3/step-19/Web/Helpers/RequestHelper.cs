using System.Collections.Generic;
using Web.Models;

namespace Web.Helpers
{
    public class RequestHelper
    {
        private readonly Dictionary<string, int> _responses = new();

        public void Register(string machineName)
        {
            if (!_responses.ContainsKey(machineName))
            {
                _responses.Add(machineName, 0);
            }

            _responses[machineName]++;
        }

        public RequestData GetData()
        {
            var data = new RequestData();

            foreach (var key in _responses.Keys)
            {
                data.TotalRequests += _responses[key];
            }

            foreach (var key in _responses.Keys)
            {
                data.Entries.Add(new RequestEntry
                {
                    MachineName = key,
                    Occurences = _responses[key],
                    Percentage = _responses[key] / (decimal)data.TotalRequests * 100.0M
                });
            }

            return data;
        }
    }
}