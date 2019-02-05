using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
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
            string numRows = context.Request.QueryString["rows"] as string;
            string pageNum = context.Request.QueryString["page"] as string;
            string sortColumn = context.Request.QueryString["sidx"] as string;
            string sortOrder = context.Request.QueryString["sord"] as string;

            int verifyInt;
            // Make sure roleId, numRows, and pageNum are valid integer values
            roleId = String.IsNullOrEmpty(roleId) ? "0" : (!Int32.TryParse(roleId, out verifyInt) ? "0" : roleId);
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "TitleName" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            CustomGenericList<Item> searchResult = ItemSelectByInstitutionAndRole(institutionCode,
                Convert.ToInt32(roleId), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

            string xmlResponse = GetItemXmlResponse(searchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows));

            context.Response.ContentType = "text/xml";
            context.Response.Write(xmlResponse);
        }

        /// <summary>
        /// Call the BHLImport web service to get the list of IA items that match the specified criteria.
        /// </summary>
        /// <param name="itemStatusId"></param>
        /// <param name="numRows"></param>
        /// <param name="pageNume"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private CustomGenericList<Item> ItemSelectByInstitutionAndRole(string institutionCode,
            int roleID, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            CustomGenericList<Item> items = new CustomGenericList<Item>();
            BHLProvider service = null;

            try
            {
                service = new BHLProvider();
                items = service.ItemSelectByInstitutionAndRole(institutionCode, roleID, numRows, pageNum, sortColumn, sortOrder);
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetItemXmlResponse(CustomGenericList<Item> searchResult, int pageNum, int numRows)
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
                    response.Append("<cell> <![CDATA[<a title=\"Info\" target=\"_blank\" href=\"/ItemEdit.aspx/?id=" + searchResult[x].ItemID.ToString() + "\">" + searchResult[x].ItemID.ToString() + "</a>]]> </cell>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" target=\"_blank\" href=\"https://www.archive.org/details/" + searchResult[x].BarCode + "\">" + searchResult[x].BarCode + "</a>]]> </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].TitleName) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Volume) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Year) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].AuthorListString) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].CopyrightStatus) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].Rights) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].LicenseUrl) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(searchResult[x].DueDiligence) + " </cell>");
                    response.Append("<cell> " + SecurityElement.Escape(((DateTime)searchResult[x].CreationDate).ToString("yyyy/MM/dd HH:mm:ss")) + " </cell>");
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