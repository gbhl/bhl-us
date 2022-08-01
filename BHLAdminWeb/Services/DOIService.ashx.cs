using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
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
            string response = string.Empty;

            // Clean up inputs
            string userId = context.Request.QueryString["uid"] as string;
            string doiStatusId = context.Request.QueryString["sid"] as string;
            string typeId = context.Request.QueryString["tid"] as string;
            string entityIdStr = context.Request.QueryString["eid"] as string;
            DateTime startDate;
            DateTime endDate;
            if (!DateTime.TryParse(context.Request.QueryString["sdate"] as string, out startDate)) startDate = new DateTime(1980, 1, 1);
            if (!DateTime.TryParse(context.Request.QueryString["edate"] as string, out endDate)) endDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());
            bool download = (context.Request.QueryString["dl"] as string) == "1";
            string numRows = context.Request.QueryString["rows"] as string;
            string pageNum = context.Request.QueryString["page"] as string;
            string sortColumn = context.Request.QueryString["sidx"] as string;
            string sortOrder = context.Request.QueryString["sord"] as string;

            int verifyInt;
            int? entityId = null;
            // Make sure itemStatusId, entityIdStr, numRows, and pageNum are valid integer values
            doiStatusId = String.IsNullOrEmpty(doiStatusId) ? "0" : (!Int32.TryParse(doiStatusId, out verifyInt) ? "0" : doiStatusId);
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);
            if (Int32.TryParse(entityIdStr, out verifyInt)) entityId = verifyInt;

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "LastModifiedDate" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "desc" : sortOrder;

            List<DOI> searchResult = this.DOISelectStatusReport(Convert.ToInt32(userId),
                Convert.ToInt32(doiStatusId), Convert.ToInt32(typeId), entityId,
                startDate, endDate, Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, 
                sortOrder);

            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();

            if (download)
            {
                context.Response.Buffer = true;
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=DOIStatus.csv");

                byte[] csvBytes = GetDownloadString(searchResult);
                context.Response.Write(Encoding.UTF8.GetString(csvBytes, 0, csvBytes.Length));
                context.Response.Flush();

                context.Response.End();
            }
            else
            {
                context.Response.ContentType = "text/xml";
                context.Response.Write(GetXmlResponse(searchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows)));
            }
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
        private List<DOI> DOISelectStatusReport(
            int userId, int doiStatusId, int doiEntityTypeId, int? entityID, DateTime startDate, 
            DateTime endDate, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            List<DOI> items = new List<DOI>();

            try
            {
                BHLProvider service = new BHLProvider();
                items = service.DOISelectStatusReport(userId, doiStatusId, doiEntityTypeId, entityID,
                    startDate, endDate, numRows, pageNum, sortColumn, sortOrder);
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
                                entityUrl = string.Format("/TitleEdit.aspx?id={0}", searchResult[x].EntityID.ToString());
                                break;
                        }
                        case "Item":
                            {
                                entityUrl = string.Format("/ItemEdit.aspx?id={0}", searchResult[x].EntityID.ToString());
                                break;
                            }
                        case "Page":
                            {
                                entityUrl = string.Format("/PageEdit.aspx?id={0}", searchResult[x].EntityID.ToString());
                                break;
                            }
                        case "Segment":
                            {
                                entityUrl = string.Format("/SegmentEdit.aspx?id={0}", searchResult[x].EntityID.ToString());
                                break;
                            }
                        default:
                            {
                                entityUrl = string.Empty;
                                break;
                            }
                    }

                    string containerUrl = string.Empty;
                    if (searchResult[x].ContainerTitleID != null)
                    {
                        containerUrl = string.Format("<![CDATA[<a title=\"Container Info\" rel=\"noopener noreferrer\" href=\"/TitleEdit.aspx?id={0}\">{0}</a>]]>", searchResult[x].ContainerTitleID.ToString());
                    }

                    response.Append("<row id='" + searchResult[x].DOIID.ToString() + "'>");
                    response.Append("<cell> " + HttpUtility.HtmlEncode(searchResult[x].CreationUserName) + " </cell>");
                    response.Append("<cell> " + HttpUtility.HtmlEncode(searchResult[x].DOIStatusName) + " </cell>");
                    response.Append("<cell> " + searchResult[x].Action + " </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"Entity Info\" rel=\"noopener noreferrer\" href=\"" + entityUrl + "\">" + entityID + "</a>]]> </cell>");
                    response.Append("<cell> " + HttpUtility.HtmlEncode(searchResult[x].EntityDetail) + " </cell>");
                    response.Append("<cell> " + containerUrl + " </cell>");
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

        /// <summary>
        /// Write the search results into CSV
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        private byte[] GetDownloadString(List<DOI> searchResult)
        {
            var data = new List<dynamic>();
            foreach (DOI doi in searchResult)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Queued By", doi.CreationUserName);
                record.Add("Status", doi.DOIStatusName);
                record.Add("Action", doi.Action);
                record.Add("Entity Type", doi.DOIEntityTypeName);
                record.Add("Entity ID", doi.EntityID.ToString());
                record.Add("Entity Detail", doi.EntityDetail);
                record.Add("Container Title ID", doi.ContainerTitleID);
                //record.Add("DOI Batch ID", doi.DOIBatchID);
                record.Add("DOI", doi.DOIName);
                record.Add("Message", doi.StatusMessage);
                record.Add("Queued", doi.CreationDate.ToString());
                record.Add("Last Update", doi.LastModifiedDate.ToString());
                data.Add(record);
            }

            return new CSV().FormatCSVData(data);
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