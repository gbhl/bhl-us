namespace MOBOT.BHL.DocumentResolver
{
    public class ResolutionResult
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _document = string.Empty;

        public string Document
        {
            get { return _document; }
            set { _document = value; }
        }

        private double _score;

        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private bool? _match;

        public bool? Match
        {
            get { return _match; }
            set { _match = value; }
        }
    }
}
