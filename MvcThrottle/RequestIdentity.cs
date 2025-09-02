﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcThrottle
{
    /// <summary>
    /// Stores the client ip, key and endpoint
    /// </summary>
    [Serializable]
    public class RequestIdentity
    {
        public string ClientIp { get; set; }
        public string ClientKey { get; set; }
        public string Endpoint { get; set; }
        public string UserAgent { get; set; }

        public override string ToString()
        {
            return string.Format("{0}_{1}_{2}_{3}", ClientIp, ClientKey, Endpoint, UserAgent);
        }
    }
}
