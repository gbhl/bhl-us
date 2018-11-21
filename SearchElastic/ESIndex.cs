using System.Configuration;

namespace BHL.Search
{
    public static class ESIndex
    {
        private static string _DEFAULT = ConfigurationManager.AppSettings["ESDefaultIndex"] as string ?? "_all";
        private static string _ALL = ConfigurationManager.AppSettings["ESAllIndex"] as string ?? "items,authors,keywords,names";
        private static string _CATALOG = ConfigurationManager.AppSettings["ESCatalogIndex"] as string ?? "catalog";
        private static string _ITEMS = ConfigurationManager.AppSettings["ESItemsIndex"] as string ?? "items";
        private static string _PAGES = ConfigurationManager.AppSettings["ESPagesIndex"] as string ?? "pages";
        private static string _AUTHORS = ConfigurationManager.AppSettings["ESAuthorsIndex"] as string ?? "authors";
        private static string _KEYWORDS = ConfigurationManager.AppSettings["ESKeywordsIndex"] as string ?? "keywords";
        private static string _NAMES = ConfigurationManager.AppSettings["ESNamesIndex"] as string ?? "names";

        public static string DEFAULT { get { return _DEFAULT; } }
        public static string ALL { get { return _ALL; } }
        public static string CATALOG { get { return _CATALOG; } }
        public static string ITEMS { get { return _ITEMS; } }
        public static string PAGES { get { return _PAGES; } }
        public static string AUTHORS { get { return _AUTHORS; } }
        public static string KEYWORDS { get { return _KEYWORDS; } }
        public static string NAMES { get { return _NAMES; } }
    }
}
