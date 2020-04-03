using MOBOT.BHL.API.BHLApi;
using MOBOT.BHL.API.BHLApiDataObjects;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace MOBOT.BHL.Web2.Services
{
    /// <summary>
    /// Summary description for NameServiceJsonXml
    /// </summary>
    [WebService(Namespace = "http://mobot.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class NameServiceJsonXml : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            String operation = context.Request.QueryString["op"];
            String startRow = context.Request.QueryString["startRow"];
            String batchSize = context.Request.QueryString["batchSize"];
            String nameBankID = context.Request.QueryString["nameBankID"];
            String name = context.Request.QueryString["name"];
            String startDate = context.Request.QueryString["startDate"];
            String endDate = context.Request.QueryString["endDate"];
            String callback = context.Request.QueryString["callback"];
            String format = context.Request.QueryString["format"];

            OutputType outputType = OutputType.Xml;
            if (format == "json") outputType = OutputType.Json;

            try
            {
                if (String.Compare(operation, "NameCount", true) == 0)
                {
                    ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
                    serviceResponse.Result = this.NameCount(startDate, endDate);
                    response = serviceResponse.Serialize(outputType);
                }
                else if (String.Compare(operation, "NameList", true) == 0)
                {
                    ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
                    serviceResponse.Result = this.NameList(startRow, batchSize, startDate, endDate);
                    response = serviceResponse.Serialize(outputType);
                }
                else if (String.Compare(operation, "NameGetDetail", true) == 0)
                {
                    ServiceResponse<Name> serviceResponse = new ServiceResponse<Name>();
                    serviceResponse.Result = this.NameGetDetail(nameBankID);
                    response = serviceResponse.Serialize(outputType);
                }
                else if (String.Compare(operation, "NameSearch", true) == 0)
                {
                    ServiceResponse<List<Name>> serviceResponse = new ServiceResponse<List<Name>>();
                    serviceResponse.Result = this.NameSearch(name);
                    response = serviceResponse.Serialize(outputType);
                }
            }
            catch (Exception ex)
            {
                ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
                serviceResponse.Status = "error";
                serviceResponse.ErrorMessage = ex.Message;
                serviceResponse.Result = ex.Message;
                response = serviceResponse.Serialize(outputType);

                context.Response.Status = "500 Internal Server Error";
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "Internal Server Error";
            }

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

            //context.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            context.Response.Write(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private int NameCount(string startDate, string endDate)
        {
            NameService nameService = new NameService();
            if ((startDate != null) || (endDate != null))
                return nameService.NameCountBetweenDates(startDate, endDate);
            else
                return nameService.NameCount();
        }

        private List<Name> NameList(string startRow, string batchSize, string startDate, string endDate)
        {
            NameService nameService = new NameService();
            if ((startDate != null) || (endDate != null))
                return nameService.NameListBetweenDates(startRow, batchSize, startDate, endDate);
            else
                return nameService.NameList(startRow, batchSize);
        }

        private Name NameGetDetail(string nameBankID)
        {
            NameService nameService = new NameService();
            return nameService.NameGetDetail(nameBankID);
        }

        private List<Name> NameSearch(string name)
        {
            NameService nameService = new NameService();
            return nameService.NameSearch(name);
        }
    }
}
