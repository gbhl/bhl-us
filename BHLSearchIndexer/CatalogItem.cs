using Nest;
using System.Collections.Generic;

namespace BHL.SearchIndexer
{
    [ElasticsearchType(Name = "catalogitem")]
    public class CatalogItem
    {
        public CatalogItem()
        {
            associations = new List<string>();
            authors = new List<string>();
            collections = new List<string>();
            contributors = new List<string>();
            dateRanges = new List<string>();
            dates = new List<string>();
            isbn = new List<string>();
            issn = new List<string>();
            keywords = new List<string>();
            oclc = new List<string>();
            notes = new List<string>();
            searchAuthors = new List<string>();
            variants = new List<string>();
            volumes = new List<Volume>();
        }

        public List<string> associations { get; set; }
        public List<string> authors { get; set; }
        public List<string> collections { get; set; }
        public string container { get; set; }
        public List<string> contributors { get; set; }
        public List<string> dateRanges { get; set; }
        public List<string> dates { get; set; }
        public string doi { get; set; }
        public List<string> facetAuthors { get; set; }
        public string genre { get; set; }
        public string id { get; set; }
        public List<string> isbn { get; set; }
        public List<string> issn { get; set; }
        public string issue { get; set; }
        public int? itemId { get; set; }
        public List<string> keywords { get; set; }
        public string language { get; set; }
        public string materialType { get; set; }
        public List<string> oclc { get; set; }
        public List<string> notes { get; set; }
        public string pageRange { get; set; }
        public string publicationPlace { get; set; }
        public string publisher { get; set; }
        public List<string> searchAuthors { get; set; }
        public int? segmentId { get; set; }
        public string series { get; set; }
        public string sortTitle { get; set; }
        public int? startPageId { get; set; }
        public string title { get; set; }
        public int? titleId { get; set; }
        public string translatedTitle { get; set; }
        public string uniformTitle { get; set; }
        public string url { get; set; }
        public List<string> variants { get; set; }
        public string volume { get; set; }
        public List<Volume> volumes { get; set; }
    }
}
