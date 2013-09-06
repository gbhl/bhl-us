using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using MOBOT.BHL.API.BHLApiDAL;
using MOBOT.BHL.API.BHLApiDataObjects2;

namespace BHLApi3Web
{
    static public class Helper
    {
        static private int _applicationID = 5;  // represents "BHL API v3" in the MOBOTAdmin.Application table

        // Enum values map to entries in the MOBOTAdmin.RequestType table
        public enum API3RequestType
        {
            GetTitleMetadata = 10001,
            GetTitleSinceUntil = 10002,
            GetResolve = 10003
        }

        /// <summary>
        /// Log the specified API request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestType"></param>
        static public void LogRequest(HttpRequestMessage request, API3RequestType requestType)
        {
            try
            {
                string clientIP = GetClientIp(request);
                string apiKey = GetUserName(request);
                int? userID = GetUserID(apiKey);

                // Get the details of the request
                string details = request.RequestUri.PathAndQuery;

#if !DEBUG
                new MOBOT.BHL.Web.Utilities.RequestLog().SaveRequestLog(_applicationID, 
                    clientIP, userID, (int)requestType, details);
#endif
            }
            catch
            {
                // Do nothing, allow logging failures to fail silently
            }
        }

        /// <summary>
        /// Get the Client IP address from a WebAPI request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static private string GetClientIp(HttpRequestMessage request)
        {
            HttpContextWrapper httpContext = (HttpContextWrapper)(request.Properties["MS_HttpContext"]);
            return httpContext.Request.UserHostAddress;
        }

        /// <summary>
        /// Get the username (api key) from an authenticated WebAPI request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        static private string GetUserName(HttpRequestMessage request)
        {
            GenericPrincipal userPrincipal = (GenericPrincipal)(request.Properties["MS_UserPrincipal"]);
            return userPrincipal.Identity.Name;
        }

        /// <summary>
        /// Get the database ID of the authenticated user account
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static int? GetUserID(string key)
        {
            int? userID = null;
            ApiKey apiKey = new Api2DAL().ApiKeySelectByKey(null, null, new Guid(key));

            if (apiKey != null)
            {
                if (apiKey.IsActive == 1) userID = apiKey.ApiKeyID;
            }

            return userID;
        }
    }
}