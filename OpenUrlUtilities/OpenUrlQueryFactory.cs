using System;
using System.Collections.Generic;
using System.Text;

namespace MOBOT.OpenUrl.Utilities
{
    static public class OpenUrlQueryFactory
    {
        static public IOpenUrlQuery CreateOpenUrlQuery(String queryString)
        {
            IOpenUrlQuery query = null;
            bool version10 = false;

            // If "url_ver=z39.88-2004" exists in the querystring, then this is an OpenUrl 1.0
            // request.  Otherwise, assume it is an OpenUrl 0.1 request.
            String[] qsKeyValuePairs = queryString.ToLower().Split('&');
            foreach (String qsKeyValuePair in qsKeyValuePairs)
            {
                String[] keyValue = qsKeyValuePair.Split('=');
                if (keyValue.Length == 2)
                {
                    if ((keyValue[0] == "url_ver") && (keyValue[1] == "z39.88-2004"))
                    {
                        version10 = true;
                    }
                }
            }

            // Instantiate the appropriate type
            if (version10)
            {
                query = new OpenUrlQueryv10();
            }
            else
            {
                query = new OpenUrlQueryv01();
            }

            query.SetQuery(queryString);

            return query;
        }
    }
}
