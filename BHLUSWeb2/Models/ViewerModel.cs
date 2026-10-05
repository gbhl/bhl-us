using MOBOT.BHL.DataObjects;
using MOBOT.BHL.DataObjects.Enum;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class ViewerModel
    {
        public ItemType Type { get; set; }
        public int Status { get; set; }
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int? ContainerID { get; set; }
        public int ContainerItemID { get; set; }
        public int? StartPageID { get; set; }
        public int TitleID { get; set; }
        public string RequestedTitleID { get; set; }
        public string TitleGenre { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string DOI { get; set; }
        public string BarCode { get; set; }
        public string FullTitle { get; set; }
        public string ShortTitle { get; set; }
        public string ArticleTitle { get; set; }
        public string Volume { get; set; }
        public string PublicationDetails { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Description { get; set; }
        public string Sponsor { get; set; }
        public string LicenseUrl { get; set; }
        public string Rights { get; set; }
        public string DueDiligence { get; set; }
        public string CopyrightStatus { get; set; }
        public string DownloadUrl { get; set; }
        public string PageProgression { get; set; } = string.Empty;
        public string HasAnnotations { get; set; } = "false";
        public int PageSequence { get; set; }
        public int PageCount { get; set; }
        public string Pages { get; set; }
        public string SurrogateText { get; set; } = string.Empty;
        public string ContributorName { get; set; } = string.Empty;
        public string ContributorUrl { get; set; } = string.Empty;
        public string AnnotationContent { get; set; } = string.Empty;
        public string PageSequenceString { get; set; } = string.Empty;
        public List<Tuple<string, string>> PageList = new List<Tuple<string, string>>();
        public List<Tuple<string, string>> SegmentList = new List<Tuple<string, string>>();
        public Dictionary<string, string> Volumes { get; set; } = new Dictionary<string, string>();
        public List<KeyValuePair<string, string>> ScholarTags { get; set; } = new List<KeyValuePair<string, string>>();
        public int VolumeSelectedIndex { get; set; } = 0;
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Author> AdditionalAuthors { get; set; } = new List<Author>();
        public List<Segment> Children { get; set; } = new List<Segment>();
        public List<Institution> Institutions { get; set; } = new List<Institution>();
        public List<Title> Titles { get; set; } = new List<Title>();
        public COinSModel COinS { get; set; } = new COinSModel();
    }
}
