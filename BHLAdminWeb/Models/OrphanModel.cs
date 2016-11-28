using CustomDataAccess;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MOBOT.BHL.AdminWeb.Models
{
    public class OrphanModel
    {
        public List<Orphan> OrphanList{ get; set; }
        public byte[] DownloadOrphans { get; set; }

        public OrphanModel()
        {
            OrphanList = GetOrphans();
        }

        public List<Orphan> GetOrphans()
        {
            CustomGenericList<ReportOrphan> orphanList = new BHLProvider().ReportSelectOrphanedEntities();

            List<Orphan> orphans = new List<Orphan>();
            foreach (ReportOrphan o in orphanList)
            {
                Orphan orphan = new Orphan(o.Type, o.ID, o.Status, o.ReplacedBy, o.Contributor, 
                    o.HasActiveTitles, o.HasActiveItems, o.HasActiveSegments);
                orphans.Add(orphan);
            }

            return orphans;
        }

        public void GetCSV()
        {
            // Get the data to be formatted as CSV
            GetOrphans();

            StringBuilder sb = new StringBuilder();

            // Add CSV header
            sb.AppendLine("\"Type\",\"ID\",\"Status\",\"Replaced By\",\"Contributor\",\"Active Titles\",\"Active Items\",\"Active Segments\"");

            // Add CSV data
            foreach (Orphan orphan in OrphanList)
            {
                sb.Append("\"" + orphan.Type + "\"");
                sb.Append(",\"" + orphan.ID.ToString() + "\"");
                sb.Append(",\"" + orphan.Status + "\"");
                sb.Append(",\"" + orphan.ReplacedBy.ToString() + "\"");
                sb.Append(",\"" + orphan.Contributor.Replace("\"", "'") + "\"");
                sb.Append(",\"" + ((orphan.HasActiveTitles == null) ? "" : ((orphan.HasActiveTitles == true) ? "Yes" : "No")) + "\"");
                sb.Append(",\"" + ((orphan.HasActiveItems == null) ? "" : ((orphan.HasActiveItems == true) ? "Yes" : "No")) + "\"");
                sb.Append(",\"" + ((orphan.HasActiveSegments == null) ? "" : ((orphan.HasActiveSegments == true) ? "Yes" : "No")) + "\"");
                sb.AppendLine();
            }

            // Convert CSV to byte array
            DownloadOrphans = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        }

        public class Orphan
        {
            public string Type { get; set; }
            public int ID { get; set; }
            public string Url { get; set; }
            public string Status { get; set; }
            public int? ReplacedBy { get; set; }
            public string Contributor { get; set; }
            public bool? HasActiveTitles { get; set; }
            public bool? HasActiveItems { get; set; }
            public bool? HasActiveSegments { get; set; }

            public Orphan() { }

            public Orphan(string type, int id, string status, int? replacedBy, string contributor,
                bool? hasActiveTitles, bool? hasActiveItems, bool? hasActiveSegments)
            {
                Type = type;
                ID = id;
                Status = status;
                ReplacedBy = replacedBy;
                Contributor = contributor;
                HasActiveTitles = hasActiveTitles;
                HasActiveItems = hasActiveItems;
                HasActiveSegments = hasActiveSegments;

                switch (type)
                {
                    case "Title":
                        Url = "/TitleEdit.aspx?id=" + id.ToString();
                        break;
                    case "Item":
                        Url = "/ItemEdit.aspx?id=" + id.ToString();
                        break;
                    case "Segment":
                        Url = "/SegmentEdit.aspx?id=" + id.ToString();
                        break;
                }
            }
        }
    }
}