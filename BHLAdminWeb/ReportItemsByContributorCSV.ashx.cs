using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MOBOT.BHL.Server;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Web.Utilities;
using CustomDataAccess;

namespace MOBOT.BHL.AdminWeb
{
    /// <summary>
    /// Summary description for ReportItemsByContributorCSV
    /// </summary>
    public class ReportItemsByContributorCSV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string institutionCode = context.Request.QueryString["inst"] as string;

            BHLProvider provider = new BHLProvider();
            Institution institution = provider.InstitutionSelectAuto(institutionCode);
            CustomGenericList<Item> items = provider.ItemSelectByInstitution(institutionCode, 1000000, "Date");

            this.WriteHttpHeaders(context, "text/csv", "ItemsByContributor" + institutionCode + DateTime.Now.ToString("yyyyMMdd") + ".csv");

            // Write file header
            StringBuilder csvString = new StringBuilder();
            csvString.AppendLine("\"Contributor\",\"Item ID\",\"IA Identifier\",\"Title\",\"Volume\",\"Year\",\"Authors\",\"Date Added\"");
            context.Response.Write(csvString.ToString());
            context.Response.Flush();

            foreach (Item item in items)
            {
                // Write record
                csvString.Remove(0, csvString.Length);
                csvString.Append("\"" + institution.InstitutionName + "\",");
                csvString.Append("\"" + item.ItemID.ToString() + "\",");
                csvString.Append("\"" + item.BarCode + "\",");
                csvString.Append("\"" + (item.TitleName ?? string.Empty).Replace('"', '\'') + "\",");
                csvString.Append("\"" + (item.Volume ?? string.Empty).Replace('"', '\'') + "\",");
                csvString.Append("\"" + (item.Year ?? string.Empty).Replace('"', '\'') + "\",");
                csvString.Append("\"" + item.AuthorListString + "\",");
                csvString.AppendLine("\"" + item.CreationDate + "\"");

                context.Response.Write(csvString.ToString());
                context.Response.Flush();
            }

            context.Response.End();
        }

        /// <summary>
        /// Write the HTTP header information for the download
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        public void WriteHttpHeaders(HttpContext context, string contentType, string fileName)
        {
            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ClearHeaders();
            context.Response.Buffer = true;
            context.Response.ContentType = contentType;
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
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