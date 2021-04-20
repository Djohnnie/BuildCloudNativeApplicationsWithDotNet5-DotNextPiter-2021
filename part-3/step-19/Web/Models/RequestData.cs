using System.Collections.Generic;

namespace Web.Models
{
    public class RequestData
    {
        public RequestData()
        {
            Entries = new List<RequestEntry>();
        }

        public int TotalRequests { get; set; }
        public List<RequestEntry> Entries { get; set; }
    }

    public class RequestEntry
    {
        public string MachineName { get; set; }
        public int Occurences { get; set; }
        public decimal Percentage { get; set; }
    }
}