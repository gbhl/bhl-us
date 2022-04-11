using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

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
            List<ReportOrphan> orphanList = new BHLProvider().ReportSelectOrphanedEntities();

            List<Orphan> orphans = new List<Orphan>();
            foreach (ReportOrphan o in orphanList)
            {
                Orphan orphan = new Orphan(o.Type, o.ID, o.Status, o.ReplacedBy, o.HoldingInstitution, 
                    o.HasActiveTitles, o.HasActiveItems, o.HasActiveSegments);
                orphans.Add(orphan);
            }

            return orphans;
        }

        public void GetCSV()
        {
            // Get the data to be formatted as CSV
            GetOrphans();

            var data = new List<dynamic>();
            foreach (Orphan orphan in OrphanList)
            {
                var record = new ExpandoObject() as IDictionary<string, Object>;
                record.Add("Type", orphan.Type);
                record.Add("ID", orphan.ID.ToString());
                record.Add("Status", orphan.Status);
                record.Add("Replaced By", orphan.ReplacedBy.ToString());
                record.Add("Content Provider", orphan.HoldingInstitution);
                record.Add("Active Titles", ((orphan.HasActiveTitles == null) ? "" : ((orphan.HasActiveTitles == true) ? "Yes" : "No")));
                record.Add("Active Items", ((orphan.HasActiveItems == null) ? "" : ((orphan.HasActiveItems == true) ? "Yes" : "No")));
                record.Add("Active Segments", ((orphan.HasActiveSegments == null) ? "" : ((orphan.HasActiveSegments == true) ? "Yes" : "No")));
                data.Add(record);
            }

            DownloadOrphans = new CSV().FormatCSVData(data);
        }

        public class Orphan
        {
            public string Type { get; set; }
            public int ID { get; set; }
            public string Url { get; set; }
            public string Status { get; set; }
            public int? ReplacedBy { get; set; }
            public string HoldingInstitution { get; set; }
            public bool? HasActiveTitles { get; set; }
            public bool? HasActiveItems { get; set; }
            public bool? HasActiveSegments { get; set; }

            public Orphan() { }

            public Orphan(string type, int id, string status, int? replacedBy, string holdingInstitution,
                bool? hasActiveTitles, bool? hasActiveItems, bool? hasActiveSegments)
            {
                Type = type;
                ID = id;
                Status = status;
                ReplacedBy = replacedBy;
                HoldingInstitution = holdingInstitution;
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