using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class TitleBrowseModel
    {
        public string Start { get; set; }
        public string DisplayStart { get; set; }
        public string Sort { get; set; }
        public int BookPage { get; set; }
        public int PartPage { get; set; }
        public int NumPerPage { get; set; }
        public int TotalBooks { get; set; } = 0;
        public int TotalSegments { get; set; } = 0;
        public List<SearchBookResult> BookResults { get; set; } = new List<SearchBookResult>();
        public List<Segment> SegmentResults { get; set; } = new List<Segment>();
    }
}