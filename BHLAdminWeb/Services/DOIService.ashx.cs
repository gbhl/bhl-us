using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for DOIService
    /// </summary>
    public class DOIService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            String doiStatusId = context.Request.QueryString["id"] as String;
            String numRows = context.Request.QueryString["rows"] as String;
            String pageNum = context.Request.QueryString["page"] as String;
            String sortColumn = context.Request.QueryString["sidx"] as String;
            String sortOrder = context.Request.QueryString["sord"] as String;

            int verifyInt;
            // Make sure itemStatusId, numRows, and pageNum are valid integer values
            doiStatusId = String.IsNullOrEmpty(doiStatusId) ? "0" : (!Int32.TryParse(doiStatusId, out verifyInt) ? "0" : doiStatusId);
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "LastModifiedDate" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "desc" : sortOrder;

            List<DOI> searchResult = this.DOISelectByStatus(
                Convert.ToInt32(doiStatusId), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

            context.Response.ContentType = "text/xml";
            context.Response.Write(GetXmlResponse(searchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows)));
        }

        /// <summary>
        /// Get the list of DOIs that match the specified criteria.
        /// </summary>
        /// <param name="doiStatusId"></param>
        /// <param name="numRows"></param>
        /// <param name="pageNume"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private List<DOI> DOISelectByStatus(
            int doiStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            List<DOI> items = new List<DOI>();

            try
            {
                BHLProvider service = new BHLProvider();
                items = service.DOISelectByStatus(doiStatusId, numRows, pageNum, sortColumn, sortOrder);
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetXmlResponse(List<DOI> searchResult, int pageNum, int numRows)
        {
            StringBuilder response = new StringBuilder(); ;

            if (searchResult.Count > 0)
            {
                int totalPages = (int)Math.Ceiling((double)searchResult[0].TotalDOIs / (double)numRows);
                int numRecords = searchResult[0].TotalDOIs;

                // <page> = page number
                // <total> = total pages
                // <records> = number of records
                response.Append("<?xml version =\"1.0\" encoding=\"utf-8\"?>");
                response.Append("<rows>");
                response.Append("<page>" + pageNum + "</page>");
                response.Append("<total>" + totalPages.ToString() + "</total>");
                response.Append("<records>" + numRecords.ToString() + "</records>");

                for (int x = 0; x < searchResult.Count; x++)
                {
                    string entityID = searchResult[x].DOIEntityTypeName + " " + searchResult[x].EntityID.ToString();
                    string entityUrl = string.Empty;
                    switch (searchResult[x].DOIEntityTypeName)
                    {
                        case "Title":
                        {
                                entityUrl = string.Format(ConfigurationManager.AppSettings["BibPageUrl"], searchResult[x].EntityID.ToString());
                                break;
                        }
                        case "Item":
                            {
                                entityUrl = string.Format(ConfigurationManager.AppSettings["ItemPageUrl"], searchResult[x].EntityID.ToString());
                                break;
                            }
                        case "Page":
                            {
                                entityUrl = string.Format(ConfigurationManager.AppSettings["PagePageUrl"], searchResult[x].EntityID.ToString());
                                break;
                            }
                        case "Segment":
                            {
                                entityUrl = string.Format(ConfigurationManager.AppSettings["PartPageUrl"], searchResult[x].EntityID.ToString());
                                break;
                            }
                        default:
                            {
                                entityUrl = string.Empty;
                                break;
                            }
                    }

                    response.Append("<row id='" + searchResult[x].DOIID.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Entity Info\" rel=\"noopener noreferrer\" target=\"_blank\" href=\"" + entityUrl + "\">" + entityID + "</a>]]> </cell>");
                    response.Append("<cell> " + HttpUtility.HtmlEncode(searchResult[x].EntityDetail) + " </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"DOI Info\" target=\"_blank\" href=\"DOISubmissionDetail.aspx?id=" + searchResult[x].DOIBatchID + "&type=d\">" + searchResult[x].DOIBatchID + "</a>]]> </cell>");
                    response.Append("<cell> " + searchResult[x].DOIName + " </cell>");
                    if (string.IsNullOrEmpty(searchResult[x].StatusMessage))
                    {
                        response.Append("<cell>  </cell>");
                    }
                    else
                    {
                        response.Append("<cell> <![CDATA[<a title=\"DOI Info\" target=\"_blank\" href=\"DOISubmissionDetail.aspx?id=" + searchResult[x].DOIBatchID + "&type=s\">View Detail</a> " + searchResult[x].StatusMessage + "]]> </cell>");
                    }
                    response.Append("<cell> " + searchResult[x].CreationDate.ToString() + " </cell>");
                    response.Append("<cell> " + searchResult[x].LastModifiedDate.ToString() + " </cell>");
                    response.Append("</row>");
                }
                response.Append("</rows>");
            }

            return response.ToString();
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