﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcThrottle
{
    /// <summary>
    /// Rate limits policy
    /// </summary>
    [Serializable]
    public class ThrottlePolicy
    {
        /// <summary>
        /// Enables IP throttling
        /// </summary>
        public bool IpThrottling { get; set; }
        public List<string> IpWhitelist { get; set; }
        public Dictionary<string, RateLimits> IpRules { get; set; }

        /// <summary>
        /// Enables User-Agent throttling
        /// </summary>
        public bool UserAgentThrottling { get; set; }
        public List<string> UserAgentWhitelist { get; set; }
        public Dictionary<string, RateLimits> UserAgentRules { get; set; }

        /// <summary>
        /// Enables Cient Key throttling
        /// </summary>
        public bool ClientThrottling { get; set; }
        public List<string> ClientWhitelist { get; set; }
        public Dictionary<string, RateLimits> ClientRules { get; set; }

        /// <summary>
        /// Enables routes throttling
        /// </summary>
        public bool EndpointThrottling { get; set; }
        public List<string> EndpointWhitelist { get; set; }
        public Dictionary<string, RateLimits> EndpointRules { get; set; }
        public EndpointThrottlingType EndpointType { get; set; }

        /// <summary>
        /// All requests including the rejected ones will stack in this order: week, day, hour, min, sec
        /// </summary>
        public bool StackBlockedRequests { get; set; }

        internal Dictionary<RateLimitPeriod, long> Rates { get; set; }

        /// <summary>
        /// Configure default request limits per second, minute, hour or day
        /// </summary>
        public ThrottlePolicy(long? perSecond, long? perMinute = null, long? perHour = null, long? perDay = null, long? perWeek = null)
        {
            EndpointType = EndpointThrottlingType.AbsolutePath;
            Rates = new Dictionary<RateLimitPeriod, long>();
            if (perSecond.HasValue) Rates.Add(RateLimitPeriod.Second, perSecond.Value);
            if (perMinute.HasValue) Rates.Add(RateLimitPeriod.Minute, perMinute.Value);
            if (perHour.HasValue) Rates.Add(RateLimitPeriod.Hour, perHour.Value);
            if (perDay.HasValue) Rates.Add(RateLimitPeriod.Day, perDay.Value);
            if (perWeek.HasValue) Rates.Add(RateLimitPeriod.Week, perWeek.Value);
        }

    }

    public enum EndpointThrottlingType
    {
        AbsolutePath = 1,
        PathAndQuery,
        ControllerAndAction,
        Controller
    }
}
