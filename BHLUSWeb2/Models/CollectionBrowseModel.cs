using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Web2.Models
{
    public class CollectionBrowseModel
    {
        public Collection Collection { get; set; }
        public bool ShowVolume { get; set; } = false;
        public string Start { get; set; }
        public string DisplayStart { get; set; }
        public int NumTitles { get; set; }
        public int NumBooks { get; set; }
        public int NumPages { get; set; }
        public string Sort { get; set; }
        public int BookPage { get; set; }
        public int NumPerPage { get; set; }
        public int TotalBooks { get; set; }
        public List<SearchBookResult> BookResults { get; set; } = new List<SearchBookResult>();
    }
}