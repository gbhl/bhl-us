using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

                var data = new List<dynamic>();
                foreach (Book book in books)
                {
                    var record = new ExpandoObject() as IDictionary<string, Object>;
                    record.Add("Content Provider", (institutionCode == "_A_L_L_" ? "- ASSIGNED -" : ((institution != null) ? institution.InstitutionName.Replace(", ", " ") : "- UNASSIGNED -")));
                    record.Add("Role", institutionRole.InstitutionRoleLabel);
                    record.Add("Item ID", book.BookID.ToString());
                    record.Add("IA Identifier", book.BarCode);
                    record.Add("Title ID", book.PrimaryTitleID);
                    record.Add("Title", (book.TitleName ?? string.Empty));
                    record.Add("Volume", (book.Volume ?? String.Empty));
                    record.Add("Year", (book.StartYear ?? String.Empty));
                    record.Add("Authors", book.AuthorListString);
                    record.Add("Holding Institution", string.Join("|", book.InstitutionStrings));
                    record.Add("Rights Holder", string.Join("|", book.RightsHolderStrings));
                    record.Add("Added By", string.Join("|", book.ScanningInstitutionStrings));
                    record.Add("Copyright Status", book.CopyrightStatus);
                    record.Add("Rights", book.Rights);
                    record.Add("License Type", book.LicenseUrl);
                    record.Add("Due Diligence", book.DueDiligence);
                    record.Add("Date Added", book.CreationDate);
                    record.Add("Date Updated", book.LastModifiedDate);
                    data.Add(record);
                }

                byte[] csvBytes = new CSV().FormatCSVData(data);
                context.Response.Write(Encoding.UTF8.GetString(csvBytes, 0, csvBytes.Length));
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