using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ItemService : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            String qsItemID = context.Request.QueryString["itemID"] as String;
            String barCode = context.Request.QueryString["barcode"] as String;
            String qsTitleID = context.Request.QueryString["titleID"] as String;
            String marcBibId = context.Request.QueryString["marcBibId"] as String;

            String paginationStatusId = context.Request.QueryString["psid"] as String;
            String startDate = context.Request.QueryString["sdate"] as String;
            String endDate = context.Request.QueryString["edate"] as String;
            String numRows = context.Request.QueryString["rows"] as String;
            String pageNum = context.Request.QueryString["page"] as String;
            String sortColumn = context.Request.QueryString["sidx"] as String;
            String sortOrder = context.Request.QueryString["sord"] as String;

            int itemID;
            Int32.TryParse(qsItemID, out itemID);
            barCode = (barCode ?? "");
            int titleID;
            Int32.TryParse(qsTitleID, out titleID);
            marcBibId = (marcBibId ?? "");

            // Make sure paginationStatusId, startDate, endDate, numRows, and pageNum are valid values
            int verifyInt;
            paginationStatusId = String.IsNullOrEmpty(paginationStatusId) ? "0" : (!Int32.TryParse(paginationStatusId, out verifyInt) ? "0" : paginationStatusId);
            DateTime verifyDate;
            startDate = DateTime.TryParse(startDate, out verifyDate) ? startDate : "1/1/1980";
            endDate = DateTime.TryParse(endDate, out verifyDate) ? endDate : DateTime.Now.ToShortDateString();
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "IAIdentifier" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;


            switch (context.Request.QueryString["op"])
            {
                case "ItemSearch":
                    {
                        context.Response.ContentType = "application/json";
                        response = this.ItemSearch(itemID, barCode);
                        break;
                    }
                case "ItemSearchByTitle":
                    {
                        context.Response.ContentType = "application/json";
                        response = this.ItemSearchByTitle(titleID, marcBibId);
                        break;
                    }
                case "ItemPaginationReport":
                    {
                        // Make sure sortColumn is a value column name
                        sortColumn = String.IsNullOrEmpty(sortColumn) ? "ItemID" : sortColumn;

                        context.Response.ContentType = "text/xml";
                        response = this.ItemPaginationReport(Convert.ToInt32(paginationStatusId), Convert.ToDateTime(startDate),
                            Convert.ToDateTime(endDate), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);
                        break;
                    }
                default:
                    {
                        response = null;
                        break;
                    }

            }

            context.Response.Write(response);
        }

        private string ItemSearch(int itemId, String barCode)
        {
            try
            {
                Item item = null;
                if (itemId != 0)
                {
                    item = new BHLProvider().ItemSelectAuto(itemId);
                }
                else
                {
                    item = new BHLProvider().ItemSelectByBarCode(barCode);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(item);
            }
            catch
            {
                return null;
            }
        }

        private string ItemSearchByTitle(int titleId, String marcBibId)
        {
            try
            {
                List<Item> items = null;
                if (titleId != 0)
                {
                    items = new BHLProvider().ItemSelectByTitleId(titleId);
                }
                else
                {
                    items = new BHLProvider().ItemSelectByMarcBibId(marcBibId);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Serialize(items);
            }
            catch
            {
                return null;
            }
        }

        private string ItemPaginationReport(
            int paginationStatusId, DateTime startDate, DateTime endDate, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            try
            {
                StringBuilder response = new StringBuilder();

                List<Item> items = new BHLProvider().ItemSelectPaginationReport(paginationStatusId,
                    startDate, endDate, numRows, pageNum, sortColumn, sortOrder);

                if (items.Count > 0)
                {
                    int totalPages = (int)Math.Ceiling((double)items[0].TotalItems / (double)numRows);
                    int numRecords = items[0].TotalItems;

                    // <page> = page number
                    // <total> = total pages
                    // <records> = number of records
                    response.Append("<?xml version =\"1.0\" encoding=\"utf-8\"?>");
                    response.Append("<rows>");
                    response.Append("<page>" + pageNum + "</page>");
                    response.Append("<total>" + totalPages.ToString() + "</total>");
                    response.Append("<records>" + numRecords.ToString() + "</records>");

                    foreach (Item item in items)
                    {
                        response.Append("<row id='" + item.ItemID.ToString() + "'>");
                        response.Append("<cell> <![CDATA[<a title=\"Info\" href=\"itemedit.aspx?id=" + item.ItemID.ToString() + "\">" + item.ItemID.ToString() + "</a>]]> </cell>");
                        response.Append("<cell> " + item.BarCode + " </cell>");
                        response.Append("<cell> <![CDATA[" + item.InstitutionStrings[0] + "]]> </cell>");
                        response.Append("<cell> " + item.PaginationStatusName  + " </cell>");
                        response.Append("<cell> " + item.PaginationStatusDate.ToString() + " </cell>");
                        response.Append("<cell> " + item.PaginationUserName + " </cell>");
                        response.Append("<cell> " + item.NumberOfPages.ToString() + " </cell>");
                        response.Append("</row>");
                    }
                    response.Append("</rows>");
                }

                return response.ToString();
            }
            catch
            {
                return string.Empty;
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
