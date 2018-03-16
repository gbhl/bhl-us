using Nest;
using System.Collections.Generic;

namespace BHL.SearchIndexer
{
    [ElasticsearchType(Name = "author")]
    public class Author
    {
        public Author()
        {
            authorNames = new List<string>();
        }

        public int id { get; set; }
        public List<string> authorNames { get; set; }
        public string primaryAuthorName { get; set; }
    }
}
