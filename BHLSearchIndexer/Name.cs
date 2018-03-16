using Nest;

namespace BHL.SearchIndexer
{
    [ElasticsearchType(Name = "name")]
    public class Name
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
    }
}
