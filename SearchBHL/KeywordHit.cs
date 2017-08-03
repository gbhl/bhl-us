namespace BHL.Search
{
    public class KeywordHit : Hit
    {
        private string _keyword = string.Empty;

        public string Keyword
        {
            get { return _keyword; }
            set {_keyword = value ?? string.Empty; }
        }
    }
}
