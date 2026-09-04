using System.Collections.Specialized;

namespace MOBOT.BHL.Web.Utilities
{
    public class DebugUtility
    {
        private string _debugValue = string.Empty;

        /// <summary>
        /// Class for checking the debugging status of the site
        /// </summary>
        /// <param name="debugValue">The value portion of the "directive=[value]" querystring.  If the querystring value matches the string supplied here, the site is in debug mode.</param>
        public DebugUtility(string debugValue)
        {
            _debugValue = debugValue;
        }

        public bool IsDebugMode(NameValueCollection querystring)
        {
            return IsValueSet(querystring, "directive", _debugValue, "IsDebugMode");
        }

        private bool IsValueSet(NameValueCollection querystring, string requestKey, string expectedValue, string cookieKey)
        {
            bool isValueSet = false;

            //Look for "directive" value in the query string
            if (querystring[requestKey] != null)
            {
                isValueSet = (querystring[requestKey].Trim().ToLower() == expectedValue);
            }

            return isValueSet;
        }
    }
}
