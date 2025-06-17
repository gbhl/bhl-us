using MvcThrottle;
using System.Diagnostics;

namespace MOBOT.BHL.Web2.MVCServices
{
    public class MvcThrottleLogger : IThrottleLogger
    {
        public void Log(ThrottleLogEntry entry)
        {
            // Output details of rate limiting to a log
            /*
            Debug.WriteLine("{0} Request {1} from {2} has been blocked, quota {3}/{4} exceeded by {5}",
                entry.LogDate, entry.RequestId, entry.ClientIp, entry.RateLimit, entry.RateLimitPeriod, entry.TotalRequests);
            */
        }
    }
}