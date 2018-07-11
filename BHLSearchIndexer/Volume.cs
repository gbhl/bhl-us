namespace BHL.SearchIndexer
{
    public class Volume
    {
        public string id { get; set; }
        public int itemId { get; set; }
        public string volume { get; set; }
        public string date { get; set; }
        public bool hasExternalContent { get; set; }
        public bool hasIllustrations { get; set; }
        public bool hasLocalContent { get; set; }
        public bool hasSegments { get; set; }
    }
}
