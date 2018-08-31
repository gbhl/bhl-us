using System;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    [Serializable]
    public class BHLType
    {
        static private string _title = "Title";
        static private string _item = "Item";
        static private string _part= "Part";
        static public string Title { get { return _title; } }
        static public string Item { get { return _item; } }
        static public string Part { get { return _part; } }
    }
}
