using Nest;
using System.Collections.Generic;

namespace BHL.SearchIndexer
{
    [ElasticsearchType(Name = "item")]
    public class Item
    {
        public Item()
        {
            authors = new List<string>();
            searchAuthors = new List<string>();
            keywords = new List<string>();
            associations = new List<string>();
            variants = new List<string>();
            contributors = new List<string>();
            titleContributors = new List<string>();
            collections = new List<string>();
            dates = new List<string>();
            dateRanges = new List<string>();
            oclc = new List<string>();
            issn = new List<string>();
            isbn = new List<string>();
        }

        public int titleId { get; set; }
        public int itemId { get; set; }
        public int segmentId { get; set; }
        public int startPageId { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string translatedTitle { get; set; }
        public string uniformTitle { get; set; }
        public string sortTitle { get; set; }
        public string genre { get; set; }
        public string materialType { get; set; }
        public List<string> authors { get; set; }
        public List<string> facetAuthors { get; set; }
        public List<string> searchAuthors { get; set; }
        public List<string> keywords { get; set; }
        public List<string> associations { get; set; }
        public List<string> variants { get; set; }
        public List<string> contributors { get; set; }
        public string volume { get; set; }
        public string issue { get; set; }
        public string series { get; set; }
        public string publisher { get; set; }
        public string publicationPlace { get; set; }
        public string language { get; set; }
        public List<string> dates { get; set; }
        public List<string> dateRanges { get; set; }
        public List<string> oclc { get; set; }
        public List<string> issn { get; set; }
        public List<string> isbn { get; set; }
        public string doi { get; set; }
        public List<string> collections { get; set; }
        public string url { get; set; }
        public string pageRange { get; set; }
        public string container { get; set; }
        public string text { get; set; }
        public bool hasSegments { get; set; }
        public bool hasLocalContent { get; set; }
        public bool hasExternalContent { get; set; }
        public bool hasIllustrations { get; set; }

        [Keyword(Ignore = true)]
        public string barcode { get; set; }

        [Keyword(Ignore = true)]
        public string publicationDetails { get; set; }

        [Keyword(Ignore = true)]
        public string editionStatement { get; set; }

        [Keyword(Ignore = true)]
        public List<string> titleContributors { get; set; }

        [Keyword(Ignore = true)]
        public int? firstPageId { get; set; }
    }
}
