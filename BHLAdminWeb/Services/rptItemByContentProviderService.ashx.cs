using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for rptItemByContentProviderService
    /// </summary>
    public class rptItemByContentProviderService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            string institutionCode = context.Request.QueryString["id"] as string;
            string roleId = context.Request.QueryString["role"] as string;
            string barcode = context.Request.QueryString["barcode"] as string;
            string titleID = context.Request.QueryString["titleid"] as string;
            string numRows = context.Request.QueryString["rows"] as string;
            string pageNum = context.Request.QueryString["page"] as string;
            string sortColumn = context.Request.QueryString["sidx"] as string;
            string sortOrder = context.Request.QueryString["sord"] as string;

            int verifyInt;
            // Make sure roleId, titleID, numRows, and pageNum are valid integer values
            roleId = string.IsNullOrEmpty(roleId) ? "0" : (!Int32.TryParse(roleId, out verifyInt) ? "0" : roleId);
            titleID = string.IsNullOrWhiteSpace(titleID) ? string.Empty : (!Int32.TryParse(titleID, out verifyInt) ? string.Empty : titleID);
            numRows = string.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = string.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "TitleName" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            List<Book> searchResult = ItemSelectByInstitutionAndRole(institutionCode,
                Convert.ToInt32(roleId), barcode, 
                (string.IsNullOrWhiteSpace(titleID) ? (int?)null : Convert.ToInt32(titleID)), 
                Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

            string xmlResponse = GetItemXmlResponse(searchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows));

            context.Response.ContentType = "text/xml";
            context.Response.Write(xmlResponse);
        }

        /// <summary>
        /// Call the BHLImport web service to get the list of IA items that match the specified criteria.
        /// </summary>
        /// <param name="institutionCode"></param>
        /// <param name="roleID"></param>
        /// <param name="barcode"></param>
        /// <param name="numRows"></param>
        /// <param name="pageNum"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private List<Book> ItemSelectByInstitutionAndRole(string institutionCode,
            int roleID, string barcode, int? titleID, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            List<Book> items = new List<Book>();
            BHLProvider service = null;

            try
            {
                service = new BHLProvider();
                items = service.BookSelectByInstitutionAndRole(institutionCode, roleID, barcode, titleID, numRows, pageNum, sortColumn, sortOrder);
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetItemXmlResponse(List<Book> searchResult, int pageNum, int numRows)
        {
            StringBuilder response = new StringBuilder(); ;

            if (searchResult.Count > 0)
            {
                int totalPages = (int)Math.Ceiling((double)searchResult[0].TotalItems / (double)numRows);
                int numRecords = searchResult[0].TotalItems;

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
                    response.Append("<row id='" + searchResult[x].ItemID.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" target=\"_blank\" href=\"/ItemEdit.aspx/?id=" + searchResult[x].BookID.ToString() + "\">" + searchResult[x].BookID.ToString() + "</a>]]> </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" rel=\"noopener noreferrer\" target=\"_blank\" href=\"https://www.archive.org/details/" + searchResult[x].BarCode + "\">" + searchResult[x].BarCode + "</a>]]> </cell>");
                    response.Append("<cell> " + searchResult[x].PrimaryTitleID.ToString() + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].TitleName) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Volume) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].StartYear) + " </cell>");
                    //response.Append("<cell> " + SecurityElement.Escape(searchResult[x].AuthorListString) + " </cell>");

                    response.Append("<cell> ");
                    for (int y = 0; y < searchResult[x].InstitutionStrings.Length; y++)
                    {
                        if (y > 0) response.Append("&lt;br/&gt;");
                        response.Append(SecurityElement.Escape(searchResult[x].InstitutionStrings[y]));
                    }
                    response.Append(" </cell>");
                    response.Append("<cell> ");
                    for (int y = 0; y < searchResult[x].RightsHolderStrings.Length; y++)
                    {
                        if (y > 0) response.Append("&lt;br/&gt;");
                        response.Append(SecurityElement.Escape(searchResult[x].RightsHolderStrings[y]));
                    }
                    response.Append(" </cell>");
                    response.Append("<cell> ");
                    for (int y = 0; y < searchResult[x].ScanningInstitutionStrings.Length; y++)
                    {
                        if (y > 0) response.Append("&lt;br/&gt;");
                        response.Append(SecurityElement.Escape(searchResult[x].ScanningInstitutionStrings[y]));
                    }
                    response.Append(" </cell>");

                    /*
                    response.Append("<cell>&lt;div&gt; Copyright Status: " + SecurityElement.Escape(searchResult[x].CopyrightStatus) + " &lt;br/&gt;" + 
                                    " Rights: " + SecurityElement.Escape(searchResult[x].Rights) + " &lt;br/&gt;" + 
                                    " License Type: " + SecurityElement.Escape(searchResult[x].LicenseUrl) + " &lt;br/&gt;" + 
                                    " Due Diligence: " + SecurityElement.Escape(searchResult[x].DueDiligence) + " &lt;/div&gt;</cell>");
                    */
                    response.Append("<cell> " + SecurityElement.Escape(((DateTime)searchResult[x].CreationDate).ToString("yyyy/MM/dd HH:mm:ss")) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(((DateTime)searchResult[x].LastModifiedDate).ToString("yyyy/MM/dd HH:mm:ss")) + " </cell>");
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