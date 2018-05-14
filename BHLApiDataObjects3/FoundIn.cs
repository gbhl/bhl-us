using System;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class FoundIn
    {
        static private string _metadata = "Metadata";
        static private string _text = "Text";
        static private string _both = "Both";
        static public string Metadata { get { return _metadata; } }
        static public string Text { get { return _text; } }
        static public string Both { get { return _both; } }
    }
}
