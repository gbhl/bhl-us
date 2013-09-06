using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MOBOT.BHL.API.BHLApi;

namespace BHLApi3Web
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var requestAuthToken = GetRequestAuthToken(request);
            //string clientIP = GetClientIp(request);
            //if (ValidAuthorization(requestAuthToken, clientIP, request.RequestUri))
            if (ValidAuthorization(requestAuthToken))
                {
                var user = new GenericPrincipal(new GenericIdentity(requestAuthToken), null);
                request.Properties["MS_UserPrincipal"] = user;
                //request.SetUserPrincipal(user);
                return base.SendAsync(request, cancellationToken);
            }

            /*
            ** This will make the whole API protected by the API token.
            ** To only protect parts of the API then mark controllers/methods
            ** with the Authorize attribute and always return this:
            **
            ** return base.SendAsync(request, cancellationToken);
            */
            return Task<HttpResponseMessage>.Factory.StartNew(
                () => new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("Authorization failed")
                    });
        }

        /// <summary>
        /// Validate the authorizatation token (Api Key)
        /// </summary>
        /// <param name="requestAuthToken"></param>
        /// <param name="clientIP"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        private static bool ValidAuthorization(string requestAuthToken)
        {
            Api2 api2 = new Api2(5);    // 5 = "BHL API v3"
            bool authorized = api2.ValidateApiUser(requestAuthToken);
            return authorized;
        }

        private static string GetRequestAuthToken(HttpRequestMessage request)
        {
            IEnumerable<string> requestAuthTokens = new[] { "" };
            if (!request.Headers.TryGetValues("ApiKey", out requestAuthTokens))
            {
                // No auth token found in the HTTP headers, so check the querystring instead
                IEnumerable<KeyValuePair<string, string>> apiKeys = from apikey 
                                                                    in request.GetQueryNameValuePairs()
                                                                    where apikey.Key.ToLower() == "apikey" 
                                                                    select apikey;

                if (apiKeys.Count() != 0) 
                    requestAuthTokens = new[] { apiKeys.First().Value };
                else 
                    requestAuthTokens = new[] { "No API token found" };
            }
            return requestAuthTokens.First<string>();
        }

        /*
        private string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else
            {
                return string.Empty;
            }
        }
         */
    }
}
