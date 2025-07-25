using Countersoft.Foundation.Commons.Extensions;
using MOBOT.BHL.Web.Utilities;
using MvcThrottle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MOBOT.BHL.Web2
{
    // Details at https://github.com/stefanprodan/MvcThrottle
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, string rateLimitConfigFile, string rateLimitWhitelistConfigFile)
        {
            if (File.Exists(rateLimitConfigFile))
            {
                try
                {
                    // Read the rate limiting rules from the config files and separate them into Policy, IP Address, Endpoint, and User Agent rules
                    List<string> rateLimitConfigs = File.ReadAllLines(rateLimitConfigFile).ToList<string>();
                    RateLimitConfig policyConfig = null;
                    List<RateLimitConfig> ipConfig = new List<RateLimitConfig>();
                    List<RateLimitConfig> endpointConfig = new List<RateLimitConfig>();
                    List<RateLimitConfig> userAgentConfig = new List<RateLimitConfig>();

                    List<string> ipWhitelistConfig = new List<string>();
                    List<string> endpointWhitelistConfig = new List<string>();
                    List<string> userAgentWhitelistConfig = new List<string>();

                    foreach (string rateLimitConfig in rateLimitConfigs)
                    {
                        // Skip any comments in the config file
                        if (!rateLimitConfig.StartsWith("#"))
                        {
                            if (rateLimitConfig.StartsWith("Policy")) policyConfig = GetRateLimitConfig(rateLimitConfig);
                            if (rateLimitConfig.StartsWith("Ip")) ipConfig.Add(GetRateLimitConfig(rateLimitConfig));
                            if (rateLimitConfig.StartsWith("Endpoint")) endpointConfig.Add(GetRateLimitConfig(rateLimitConfig));
                            if (rateLimitConfig.StartsWith("UserAgent")) userAgentConfig.Add(GetRateLimitConfig(rateLimitConfig));
                        }
                    }

                    if (File.Exists(rateLimitWhitelistConfigFile))
                    {
                        List<string> whitelistConfigs = File.ReadAllLines(rateLimitWhitelistConfigFile).ToList<string>();

                        foreach (string whitelistConfig in whitelistConfigs)
                        {
                            if (!whitelistConfig.StartsWith("#"))
                            {
                                if (whitelistConfig.StartsWith("Ip")) ipWhitelistConfig.Add(GetWhitelistConfig(whitelistConfig));
                                if (whitelistConfig.StartsWith("Endpoint")) endpointWhitelistConfig.Add(GetWhitelistConfig(whitelistConfig));
                                if (whitelistConfig.StartsWith("UserAgent")) userAgentWhitelistConfig.Add(GetWhitelistConfig(whitelistConfig));
                            }
                        }
                    }

                    // The global policy rate limits are required
                    if (policyConfig == null) throw new Exception("No Rate Limit policy defined");

                    // Initiate the Rate Limiting policy object
                    /* EXAMPLE RULES
                    policy.IpRules = new Dictionary<string, RateLimits>
                    {
                        { "192.168.1.1", new RateLimits { PerSecond = 2 } },
                        { "192.168.2.0/24", new RateLimits { PerMinute = 30, PerHour = 1800, PerDay = 43200 } },
                        { "192.168.0.0-192.168.0.255", new RateLimits { PerSecond = 1 } },
                    };
                    policy.EndpointRules = new Dictionary<string, RateLimits>
                    {
                        { "Books/", new RateLimits { PerSecond = 5, PerMinute = 30, PerHour = 1800, PerDay = 43200 } },
                        { "Books/Index", new RateLimits { PerSecond = 5, PerMinute = 30, PerHour = 1800, PerDay = 43200 } }
                    };
                    policy.UserAgentRules = new Dictionary<string, RateLimits>
                    {
                        {"ClaudeBot/1.0;+claudebot@anthropic.com", new RateLimits { PerSecond = 1 }},
                    }
                    */
                    ThrottlePolicy policy = new ThrottlePolicy(perSecond: policyConfig.PerSecond, 
                        perMinute: policyConfig.PerMinute, perHour: policyConfig.PerHour, perDay: policyConfig.PerDay)
                    {
                        IpThrottling = (ipConfig.Count > 0),
                        IpWhitelist = ipWhitelistConfig,
                        IpRules = GetRateLimitRules(ipConfig),
                        EndpointThrottling = true,
                        EndpointType = EndpointThrottlingType.AbsolutePath,
                        EndpointWhitelist = endpointWhitelistConfig,
                        EndpointRules = GetRateLimitRules(endpointConfig),
                        UserAgentThrottling = true,
                        UserAgentWhitelist = userAgentWhitelistConfig,
                        UserAgentRules = GetRateLimitRules(userAgentConfig),
                    };

                    // Set up the rate limit filter
                    var throttleFilter = new ThrottlingFilter
                    {
                        Policy = policy,
                        Repository = new CacheRepository(),
                        Logger = new MVCServices.MvcThrottleLogger()
                    };

                    filters.Add(throttleFilter);
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex, "FilterConfig.RegisterGlobalFilters");
                }
            }
            else
            {
                ExceptionUtility.LogException(new Exception("Missing ratelimit.config file"), "FilterConfig.RegisterGlobalFilters");
            }
        }

        static RateLimitConfig GetRateLimitConfig(string rateLimitConfig)
        {
            string[] configs = rateLimitConfig.Split('|');
            if (configs.Length == 6)
            {
                RateLimitConfig rlConfig = new RateLimitConfig
                {
                    Label = configs[1],
                };
                long x;
                if (Int64.TryParse(configs[2], out x)) rlConfig.PerSecond = x;
                if (Int64.TryParse(configs[3], out x)) rlConfig.PerMinute = x;
                if (Int64.TryParse(configs[4], out x)) rlConfig.PerHour = x;
                if (Int64.TryParse(configs[5], out x)) rlConfig.PerDay = x;
                
                return rlConfig;
            }
            else
            {
                throw new Exception(string.Format("Invalid ratelimit.config entry: {0}", rateLimitConfig));
            }
        }

        static Dictionary<string, RateLimits> GetRateLimitRules(List<RateLimitConfig> configs)
        {
            Dictionary<string, RateLimits> rules = (configs.Count > 0) ? new Dictionary<string, RateLimits>() : null;
            foreach (RateLimitConfig config in configs)
            {
                Tuple<string, RateLimits> rl = GetRateLimitRule(config);
                rules.Add(rl.Item1, rl.Item2);
            }
            return rules;
        }

        static Tuple<string, RateLimits> GetRateLimitRule(RateLimitConfig rateLimitConfig)
        {
            RateLimits rl = new RateLimits();
            if (rateLimitConfig.PerSecond != null) rl.PerSecond = (long)rateLimitConfig.PerSecond;
            if (rateLimitConfig.PerMinute != null) rl.PerMinute = (long)rateLimitConfig.PerMinute;
            if (rateLimitConfig.PerHour != null) rl.PerHour = (long)rateLimitConfig.PerHour;
            if (rateLimitConfig.PerDay != null) rl.PerDay = (long)rateLimitConfig.PerDay;
            Tuple<string, RateLimits> rule = new Tuple<string, RateLimits>(rateLimitConfig.Label, rl);
            return rule;
        }

        static string GetWhitelistConfig(string whitelistConfig)
        {
            string[] configs = whitelistConfig.Split('|');
            if (configs.Length == 2)
            {
                string whiteListValue = configs[1];
                return whiteListValue;
            }
            else
            {
                throw new Exception(string.Format("Invalid ratelimitwhitelist.config entry: {0}", whitelistConfig));
            }
        }
    }

    class RateLimitConfig
    {
        public string Label { get; set; }
        public long? PerSecond { get; set; }
        public long? PerMinute { get; set; }
        public long? PerHour { get; set; }
        public long? PerDay { get; set; }

        public RateLimitConfig()
        {
            Label = string.Empty;
            PerSecond = null;
            PerMinute = null;
            PerHour = null;
            PerDay = null;
        }
    }
}