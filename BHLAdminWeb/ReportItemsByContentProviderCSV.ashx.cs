﻿using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
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

            int roleId;
            // Make sure roleId is a valid integer value
            if (Int32.TryParse(institutionRoleID, out roleId))
            {
                BHLProvider provider = new BHLProvider();
                Institution institution = provider.InstitutionSelectAuto(institutionCode);
                InstitutionRole institutionRole = provider.InstitutionRoleSelectAuto(roleId);
                CustomGenericList<Item> items = provider.ItemSelectByInstitutionAndRole(institutionCode, roleId, 1000000, 1, "TitleName", "asc");

                this.WriteHttpHeaders(context, "text/csv", "ItemsByContentProviderAndRole" + DateTime.Now.ToString("yyyyMMdd") + ".csv");

                // Write file header
                StringBuilder csvString = new StringBuilder();
                csvString.AppendLine("\"Content Provider\",\"Role\",\"Item ID\",\"IA Identifier\",\"Title\",\"Volume\",\"Year\",\"Authors\",\"Copyright Status\",\"Rights\",\"License Type\",\"Due Diligence\",\"Date Added\"");
                context.Response.Write(csvString.ToString());
                context.Response.Flush();

                foreach (Item item in items)
                {
                    // Write record
                    csvString.Remove(0, csvString.Length);
                    csvString.Append("\"" + institution.InstitutionName.Replace(",", " ") + "\",");
                    csvString.Append("\"" + institutionRole.InstitutionRoleLabel.Replace(",", " ") + "\",");
                    csvString.Append("\"" + item.ItemID.ToString() + "\",");
                    csvString.Append("\"" + item.BarCode + "\",");
                    csvString.Append("\"" + (item.TitleName ?? string.Empty).Replace(",", " ").Replace('"', '\'') + "\",");
                    csvString.Append("\"" + (item.Volume ?? string.Empty).Replace(",", " ").Replace('"', '\'') + "\",");
                    csvString.Append("\"" + (item.Year ?? string.Empty).Replace(",", " ").Replace('"', '\'') + "\",");
                    csvString.Append("\"" + item.AuthorListString.Replace(",", " ") + "\",");
                    csvString.Append("\"" + item.CopyrightStatus.Replace(",", " ") + "\",");
                    csvString.Append("\"" + item.Rights.Replace(",", " ") + "\",");
                    csvString.Append("\"" + item.LicenseUrl.Replace(",", " ") + "\",");
                    csvString.Append("\"" + item.DueDiligence.Replace(",", " ") + "\",");
                    csvString.AppendLine("\"" + item.CreationDate + "\"");

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