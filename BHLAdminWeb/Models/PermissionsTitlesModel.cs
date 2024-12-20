using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace MOBOT.BHL.AdminWeb.Models
{
    [Serializable]
    public class PermissionsTitlesModel
    {
        public byte[] DownloadTitles { get; set; }

        /// <summary>
        /// Get permissions titles records
        /// </summary>
        /// <param name="numRows">Number of rows to return</param>
        /// <param name="startRow">First row to return (enables paging)</param>
        /// <param name="sortColumn">Column by which to sort data</param>
        /// <param name="sortDirection">Direction of sort</param>
        public PermissionsTitlesJson.Rootobject GetRecords(int numRows, int startRow, string sortColumn, string sortDirection)
        {
            List<PermissionsTitle> records = new BHLProvider().ReportSelectPermissionsTitles(numRows, startRow, sortColumn, sortDirection);

            PermissionsTitlesJson.Rootobject json = new PermissionsTitlesJson.Rootobject();
            json.iTotalRecords = (records.Count == 0) ? "0" : records[0].TotalRecords.ToString();
            json.iTotalDisplayRecords = json.iTotalRecords;

            PermissionsTitlesJson.Datum[] aaData = new PermissionsTitlesJson.Datum[records.Count];

            for (int x = 0; x < records.Count; x++)
            {
                aaData[x] = new PermissionsTitlesJson.Datum()
                {
                    id = records[x].TitleID.ToString(),
                    titleID = records[x].TitleID.ToString(),
                    fullTitle = records[x].FullTitle,
                    bibliographicLevel = records[x].BibliographicLevelName,
                    years = records[x].Years,
                    ISSN = (records[x].Issn.EndsWith("|") ? records[x].Issn.Substring(0, records[x].Issn.Length - 1) : records[x].Issn).Replace("|", "<br/>"),
                    OCLC = (records[x].Oclc.EndsWith("|") ? records[x].Oclc.Substring(0, records[x].Oclc.Length - 1) : records[x].Oclc).Replace("|", "<br/>"),
                    hasMovingWall = records[x].HasMovingWall ? "Yes" : "No",
                    hasDocumentation = records[x].HasDocumentation ? "Yes" : "No"
                };
            }
            json.aaData = aaData;

            return json;
        }

        public void GetPermissionsTitlesCSV()
        {
            // Get the data to be formatted as CSV
            List<PermissionsTitle> logs = new BHLProvider().ReportSelectPermissionsTitles(10000000, 1, "SortTitle", "asc");

            var data = new List<dynamic>();
            foreach (var log in logs)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Title ID", log.TitleID);
                record.Add("Full Title", log.FullTitle);
                record.Add("Type", log.BibliographicLevelName);
                record.Add("Years", log.Years);
                record.Add("ISSN", log.Issn.EndsWith("|") ? log.Issn.Substring(0, log.Issn.Length - 1) : log.Issn);
                record.Add("OCLC", log.Oclc.EndsWith("|") ? log.Oclc.Substring(0, log.Oclc.Length - 1) : log.Oclc);
                record.Add("Has Moving Wall", (log.HasMovingWall ? "Yes" : "No"));
                record.Add("Has Documents", (log.HasMovingWall ? "Yes" : "No"));
                data.Add(record);
            }

            DownloadTitles = new CSV().FormatCSVData(data);
        }

    }

    /// <summary>
    /// Class used to produce the JSON representation of citations records that is needed by jQuery DataTables
    /// </summary>
    [Serializable]
    public class PermissionsTitlesJson
    {
        public class Rootobject
        {
            public int sEcho { get; set; }
            public string iTotalRecords { get; set; }
            public string iTotalDisplayRecords { get; set; }
            public Datum[] aaData { get; set;}
        }

        public class Datum
        {
            public string id { get; set; }
            public string titleID { get; set; }
            public string fullTitle { get; set; }
            public string bibliographicLevel { get; set; }
            public string years { get; set; }
            public string ISSN { get; set; }
            public string OCLC { get; set; }
            public string hasMovingWall { get; set; }
            public string hasDocumentation { get; set; }
        }
    }
}
