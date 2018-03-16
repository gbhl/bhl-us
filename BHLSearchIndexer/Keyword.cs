using Nest;

namespace BHL.SearchIndexer
{
    [ElasticsearchType(Name = "keyword")]
    public class Keyword
    {
        public int id { get; set; }
        public string keyword { get; set; }
    }
}
