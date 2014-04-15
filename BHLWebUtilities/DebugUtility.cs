using System;
using System.Web;

namespace MOBOT.BHL.Web.Utilities
{
    public class DebugUtility
    {
        private string _debugValue = string.Empty;

        /// <summary>
        /// Class for checking the debugging status of the site and for providing detailed error messages.
        /// </summary>
        /// <param name="debugValue">The value portion of the "directive=[value]" querystring.  If the querystring value matches the string supplied here, the site is in debug mode.</param>
        public DebugUtility(string debugValue)
        {
            _debugValue = debugValue;
        }

        /// <summary>
        /// This method will check for "directive=debug" in the querystring to indicate debugging mode.  If we're
        /// in debugging mode, the error message and stack trace will be written to the page and the response will be ended.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="exception"></param>
        public void WriteErrorInfo(HttpResponse response, HttpRequest request, Exception exception)
        {
            if (IsDebugMode(response, request))
            {
                response.Write("<b>Message:</b> " + exception.Message + "<br /><br />");
                response.Write("<b>Stack Trace:</b> " + exception.StackTrace.Replace("\n", "<br />"));
                response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public string GetErrorInfo(HttpRequest request, Exception exception)
        {
            if (IsDebugMode(request))
                return "<b>Message:</b> " + exception.Message + "\n\n<b>Stack Trace:</b> " + exception.StackTrace;
            else
                return "";
        }

        public bool IsDebugMode(HttpRequest request)
        {
            return IsDebugMode(null, request);
        }

        public bool IsDebugMode(HttpResponse response, HttpRequest request)
        {
            return IsValueSet(response, request, "directive", _debugValue, "IsDebugMode", "debugmode.asmx");
        }

        private bool IsValueSet(HttpResponse response, HttpRequest request, string requestKey, string expectedValue, string cookieKey, string webServiceNameSuffix)
        {
            bool isValueSet = false;
            //first check to see if debug mode is set in a cookie
            isValueSet = (request.Cookies[cookieKey] != null && request.Cookies[cookieKey].Value == "true");

            //next look for "directive=debug" in the query string if it is a web page or a name ending with "debugmode.asmx" if it's a web service call
            if (!isValueSet)
            {
                if ((request.QueryString[requestKey] != null && request.QueryString[requestKey].Trim().ToLower() == expectedValue) ||
                    (webServiceNameSuffix != null && request.RawUrl.ToLower().IndexOf(webServiceNameSuffix) >= 0))
                {
                    isValueSet = true;
                    if (response != null)
                        response.Cookies.Add(new HttpCookie(cookieKey, "true"));
                }
            }

            //check to see if we need to turn off debug mode
            if (request.QueryString[requestKey] != null && request.QueryString[requestKey].Trim().ToLower() != expectedValue)
            {
                isValueSet = false;
                if (request.Cookies[cookieKey] != null && response != null)
                    response.Cookies[cookieKey].Value = "false";
            }

            return isValueSet;
        }
    }
}
