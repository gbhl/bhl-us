using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MOBOT.BHL.API.BHLApi;
using CustomDataAccess;

namespace MOBOT.BHL.Web2.api
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class httpQuery : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;
            String operation = context.Request.QueryString["op"];

            if ((String.Compare(operation, "NameCount", true) == 0) ||
                (String.Compare(operation, "NameList", true) == 0) ||
                (String.Compare(operation, "NameGetDetail", true) == 0) ||
                (String.Compare(operation, "NameSearch", true) == 0))
            {
                // These operations are the previously existing Name Services.  Simply pass the HttpContext on
                // to the existing endpoint for these operations.  This service endpoint will become the 
                // preferred endpoint for these operations, but the existing endpoint will remain available
                // for the forseeable future, so we'll continue to use it.     MWL 2010/1/4
                MOBOT.BHL.Web2.Services.NameServiceJsonXml nameService = new MOBOT.BHL.Web2.Services.NameServiceJsonXml();
                nameService.ProcessRequest(context);
            }
            else
            {
                String callback = context.Request.QueryString["callback"];
                OutputType outputType = OutputType.Xml;
                String format = context.Request.QueryString["format"];
                if (format == "json") outputType = OutputType.Json;

                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                serviceResponse.Status = "error";
                serviceResponse.ErrorMessage = "Unknown operation: " + operation;
                serviceResponse.Result = "Unknown operation: " + operation;
                response = serviceResponse.Serialize(outputType);

                context.Response.Status = "500 Internal Server Error";
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "Internal Server Error";

                // Include any specified callback function in JSON responses
                if ((callback != null) && (callback != String.Empty) && outputType == OutputType.Json)
                {
                    response = callback + "(" + response + ");";
                }

                switch (outputType)
                {
                    case OutputType.Json:
                        context.Response.ContentType = "application/json";
                        break;
                    case OutputType.Xml:
                        context.Response.ContentType = "text/xml";
                        break;
                }
                context.Response.AppendHeader("Access-Control-Allow-Origin", "*"); 
                context.Response.Write(response);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
