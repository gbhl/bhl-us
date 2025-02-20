﻿using MOBOT.BHL.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class ContributorBrowseModel
    {
        public Institution Institution { get; set; }
        public string Start { get; set; }
        public string DisplayStart { get; set; }
        public int NumTitles { get; set; }
        public int NumBooks { get; set; }
        public int NumPages { get; set; }
        public int NumSegments { get; set; }
        public string Sort { get; set; }
        public int BookPage { get; set; }
        public int PartPage { get; set; }
        public int NumPerPage { get; set; }
        public int TotalBooks { get; set; }
        public int TotalSegments { get; set; }
        public List<SearchBookResult> BookResults { get; set; } = new List<SearchBookResult>();
        public List<Segment> SegmentResults { get; set; } = new List<Segment>();
    }
}
