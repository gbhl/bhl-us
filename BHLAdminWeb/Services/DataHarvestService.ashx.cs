using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;
using MOBOT.BHL.AdminWeb.BHLImportWebService;

namespace MOBOT.BHL.AdminWeb.Services
{
    /// <summary>
    /// Handler for requests coming from the /DataHarvestItemList.aspx page, in particular from the jqGrid object
    /// contained on that page.
    /// </summary>
    public class DataHarvestService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String response = String.Empty;

            // Clean up inputs
            String itemStatusId = context.Request.QueryString["id"] as String;
            String type = context.Request.QueryString["type"] as String;
            String numRows = context.Request.QueryString["rows"] as String;
            String pageNum = context.Request.QueryString["page"] as String;
            String sortColumn = context.Request.QueryString["sidx"] as String;
            String sortOrder = context.Request.QueryString["sord"] as String;

            int verifyInt;
            // Make sure itemStatusId, numRows, and pageNum are valid integer values
            itemStatusId = String.IsNullOrEmpty(itemStatusId) ? "0" : (!Int32.TryParse(itemStatusId, out verifyInt) ? "0" : itemStatusId);
            numRows = String.IsNullOrEmpty(numRows) ? "100" : (!Int32.TryParse(numRows, out verifyInt) ? "100" : numRows);
            pageNum = String.IsNullOrEmpty(pageNum) ? "1" : (!Int32.TryParse(pageNum, out verifyInt) ? "1" : pageNum);

            // Make sure sortColumn is a value column name
            sortColumn = String.IsNullOrEmpty(sortColumn) ? "IAIdentifier" : sortColumn;

            // Make sure sortOrder is "asc" or "desc"
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            sortOrder = (!(sortOrder.ToLower() == "asc") && !(sortOrder.ToLower() == "desc")) ? "asc" : sortOrder;

            string xmlResponse = string.Empty;
            switch (type)
            {
                case "bsstatus":
                    // Make sure sortColumn is a value column name
                    sortColumn = String.IsNullOrEmpty(sortColumn) ? "CreationDate" : sortColumn;

                    CustomGenericList<BioStorHarvestItem> biostorSearchResult = this.BioStorHarvestItemSelectByStatus(
                        Convert.ToInt32(itemStatusId), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

                    xmlResponse = GetBioStorHarvestItemXmlResponse(biostorSearchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows));

                    break;
                case "iastatus":
                default:
                    // Make sure sortColumn is a value column name
                    sortColumn = String.IsNullOrEmpty(sortColumn) ? "IAIdentifier" : sortColumn;

                    CustomGenericList<IAHarvestItem> iaSearchResult = this.IAHarvestItemSelectByStatus(
                        Convert.ToInt32(itemStatusId), Convert.ToInt32(numRows), Convert.ToInt32(pageNum), sortColumn, sortOrder);

                    xmlResponse = GetIAHarvestItemXmlResponse(iaSearchResult, Convert.ToInt32(pageNum), Convert.ToInt32(numRows));
                    break;
            }

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
        private CustomGenericList<IAHarvestItem> IAHarvestItemSelectByStatus(
            int itemStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            CustomGenericList<IAHarvestItem> items = new CustomGenericList<IAHarvestItem>();
            BHLImportWSSoapClient service = null;

            try
            {
                service = new BHLImportWSSoapClient();
                IAItem[] iaItems = service.IAItemSelectByStatus(itemStatusId, numRows, pageNum, sortColumn, sortOrder);

                foreach (IAItem iaItem in iaItems)
                {
                    IAHarvestItem item = new IAHarvestItem();
                    item.ItemId = iaItem.ItemID;
                    item.IAIdentifier = iaItem.IAIdentifier;
                    item.Sponsor = iaItem.Sponsor;
                    item.ScanningCenter = iaItem.ScanningCenter;
                    item.Volume = iaItem.Volume;
                    item.ScanDate = iaItem.ScanDate;
                    item.ExternalStatus = iaItem.ExternalStatus;
                    item.TotalItems = iaItem.TotalItems;
                    items.Add(item);
                }
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetIAHarvestItemXmlResponse(CustomGenericList<IAHarvestItem> searchResult, int pageNum, int numRows)
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
                    response.Append("<row id='" + searchResult[x].ItemId.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" target=\"_blank\" href=\"http://www.archive.org/details/" + searchResult[x].IAIdentifier + "\">" + searchResult[x].IAIdentifier + "</a>]]> </cell>");
                    response.Append("<cell> " + searchResult[x].Sponsor + " </cell>");
                    response.Append("<cell> " + searchResult[x].ScanningCenter + " </cell>");
                    response.Append("<cell> " + searchResult[x].Volume + " </cell>");
                    response.Append("<cell> " + searchResult[x].ScanDate + " </cell>");
                    response.Append("<cell> " + searchResult[x].ExternalStatus + " </cell>");
                    response.Append("</row>");
                }
                response.Append("</rows>");
            }

            return response.ToString();
        }

        private CustomGenericList<BioStorHarvestItem> BioStorHarvestItemSelectByStatus(
            int itemStatusId, int numRows, int pageNum, string sortColumn, string sortOrder)
        {
            CustomGenericList<BioStorHarvestItem> items = new CustomGenericList<BioStorHarvestItem>();
            BHLImportWSSoapClient service = null;

            try
            {
                service = new BHLImportWSSoapClient();
                BSItem[] bsItems = service.BSItemSelectByStatus(itemStatusId, numRows, pageNum, sortColumn, sortOrder);

                foreach (BSItem bsItem in bsItems)
                {
                    BioStorHarvestItem item = new BioStorHarvestItem();
                    item.ItemId = bsItem.ItemID;
                    item.BHLItemId = bsItem.BHLItemID;
                    item.Title = bsItem.Title;
                    item.Volume = bsItem.Volume;
                    item.TotalSegments = bsItem.TotalSegments;
                    item.CreationDate = bsItem.CreationDate;
                    item.TotalItems = bsItem.TotalItems;
                    items.Add(item);
                }
            }
            catch
            {
                // Do nothing
            }

            return items;
        }

        private string GetBioStorHarvestItemXmlResponse(CustomGenericList<BioStorHarvestItem> searchResult, int pageNum, int numRows)
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
                    response.Append("<row id='" + searchResult[x].ItemId.ToString() + "'>");
                    response.Append("<cell> <![CDATA[<a title=\"Info\" target=\"_blank\" href=\"/item/" + searchResult[x].BHLItemId + "\">" + searchResult[x].BHLItemId + "</a>]]> </cell>");
                    response.Append("<cell> " + searchResult[x].Title + " </cell>");
                    response.Append("<cell> " + searchResult[x].Volume + " </cell>");
                    response.Append("<cell> <![CDATA[<a href=\"#\" title=\"Segments\" onclick=\"window.open('BioStorSegmentsForItem.aspx?id=" + searchResult[x].ItemId.ToString() + "', 'Segments', 'resizeable=0,scrollbars=1,height=500,width=500,status=0,toolbar=0,menubar=0,location=0');\">" + searchResult[x].TotalSegments + "</a>]]> </cell>");
                    response.Append("<cell> " + searchResult[x].CreationDate + " </cell>");
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