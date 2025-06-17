using MvcThrottle;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2
{
    // Details at https://github.com/stefanprodan/MvcThrottle
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var throttleFilter = new ThrottlingFilter
            {
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 60, perHour: 3600, perDay: 86400)
                {
                    IpThrottling = true,
                    /*
                    IpRules = new Dictionary<string, RateLimits>
                    {
                        { "192.168.1.1", new RateLimits { PerSecond = 2 } },
                        { "192.168.2.0/24", new RateLimits { PerMinute = 30, PerHour = 30*60, PerDay = 30*60*24 } }
                    },
                    */
                    EndpointThrottling = true,
                    EndpointType = EndpointThrottlingType.ControllerAndAction,
                    /*
                    EndpointRules = new Dictionary<string, RateLimits>
                    {
                        { "Search/", new RateLimits { PerSecond = 1, PerMinute = 1, PerHour = 60 } },
                        { "Search/Index", new RateLimits { PerSecond = 1, PerMinute = 1, PerHour = 60 } }
                    },
                    */
                    /*
                    UserAgentThrottling = true,
		            UserAgentWhitelist = new List<string>
		            {
			            "Googlebot",
			            "Bingbot",
			            "YandexBot",
			            "DuckDuckBot"
		            },
                    UserAgentRules = new Dictionary<string, RateLimits>
                    {
                        {"YisouSpider", new RateLimits { PerSecond = 1 }},
                        {"AhrefsBot", new RateLimits { PerSecond = 1 } }
                    }
                    */
                },
                Repository = new CacheRepository(),
                Logger = new MVCServices.MvcThrottleLogger()
            };

            filters.Add(throttleFilter);
        }
    }
}