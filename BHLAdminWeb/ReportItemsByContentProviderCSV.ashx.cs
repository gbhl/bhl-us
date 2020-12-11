using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb
{
    /// <summary>
    /// Summary description for ReportItemsByContentProviderCSV
    /// </summary>
    public class ReportItemsByContentProviderCSV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string institutionCode = context.Request.QueryString["id"] as string;
            string institutionRoleID = context.Request.QueryString["role"] as string;
            string barcode = context.Request.QueryString["barcode"] as string;

            int roleId;
            // Make sure roleId is a valid integer value
            if (Int32.TryParse(institutionRoleID, out roleId))
            {
                BHLProvider provider = new BHLProvider();
                Institution institution = provider.InstitutionSelectAuto(institutionCode);
                InstitutionRole institutionRole = provider.InstitutionRoleSelectAuto(roleId);
                List<Book> books = provider.BookSelectByInstitutionAndRole(institutionCode, roleId, barcode, 1000000, 1, "CreationDate", "desc");

                this.WriteHttpHeaders(context, "text/csv", "ItemsByContentProviderAndRole" + DateTime.Now.ToString("yyyyMMdd") + ".csv");

                // Write file header
                StringBuilder csvString = new StringBuilder();
                csvString.AppendLine("\"Content Provider\",\"Role\",\"Item ID\",\"IA Identifier\",\"Title ID\",\"Title\",\"Volume\",\"Year\",\"Authors\",\"Holding Institution\",\"Rights Holder\",\"Added By\",\"Copyright Status\",\"Rights\",\"License Type\",\"Due Diligence\",\"Date Added\",\"Date Updated\"");
                context.Response.Write(csvString.ToString());
                context.Response.Flush();

                foreach (Book book in books)
                {
                    // Write record
                    csvString.Remove(0, csvString.Length);
                    csvString.Append("\"" + (institutionCode == "_A_L_L_" ? "- ASSIGNED -" : ((institution != null) ? institution.InstitutionName.Replace(", ", " ") : "- UNASSIGNED -")) + "\",");
                    csvString.Append("\"" + institutionRole.InstitutionRoleLabel.Replace(",", " ") + "\",");
                    csvString.Append("\"" + book.BookID.ToString() + "\",");
                    csvString.Append("\"" + book.BarCode + "\",");
                    csvString.Append("\"" + book.PrimaryTitleID + "\",");
                    csvString.Append("\"" + (book.TitleName ?? string.Empty).Replace(",", " ").Replace('"', '\'') + "\",");
                    csvString.Append("\"" + (book.Volume ?? string.Empty).Replace(",", " ").Replace('"', '\'') + "\",");
                    csvString.Append("\"" + (book.StartYear ?? string.Empty).Replace(",", " ").Replace('"', '\'') + "\",");
                    csvString.Append("\"" + book.AuthorListString.Replace(",", " ") + "\",");
                    csvString.Append("\"" + string.Join("|", book.InstitutionStrings).Replace(",", " ") + "\",");
                    csvString.Append("\"" + string.Join("|", book.RightsHolderStrings).Replace(",", " ") + "\",");
                    csvString.Append("\"" + string.Join("|", book.ScanningInstitutionStrings).Replace(",", " ") + "\",");
                    csvString.Append("\"" + book.CopyrightStatus.Replace(",", " ") + "\",");
                    csvString.Append("\"" + book.Rights.Replace(",", " ") + "\",");
                    csvString.Append("\"" + book.LicenseUrl.Replace(",", " ") + "\",");
                    csvString.Append("\"" + book.DueDiligence.Replace(",", " ") + "\",");
                    csvString.Append("\"" + book.CreationDate + "\",");
                    csvString.AppendLine("\"" + book.LastModifiedDate + "\"");

                    context.Response.Write(csvString.ToString());
                    context.Response.Flush();
                }
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
            context.Response.ContentEncoding = Encoding.UTF8;
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