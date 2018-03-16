using Nest;
using System.Collections.Generic;

namespace BHL.SearchIndexer
{
    [ElasticsearchType(Name = "page")]
    public class Page
    {
        public Page()
        {
            pageTypes = new List<string>();
            names = new List<string>();
            segments = new List<int>();
        }

        public int id { get; set; }
        public int sequence { get; set; }
        public int itemId { get; set; }
        public List<string> pageIndicators { get; set; }
        public List<string> pageTypes { get; set; }
        public List<string> names { get; set; }
        public List<int> segments { get; set; }
        public string text { get; set; }
        public string textPath { get; set; }
    }
}
