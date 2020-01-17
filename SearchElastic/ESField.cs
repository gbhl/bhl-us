namespace BHL.Search.Elastic
{
    public static class ESField
    {
        public const string ALL = "_all";

        // Item index
        public const string ASSOCIATIONS = "associations";
        public const string ASSOCIATIONS_ABBR = "associations.abbr";
        public const string AUTHORS = "authors";
        public const string COLLECTIONS = "collections";
        public const string CONTAINER = "container";
        public const string CONTAINER_RAW = "container.raw";
        public const string CONTAINER_ABBR = "container.abbr";
        public const string CONTRIBUTORS = "contributors";
        public const string CONTRIBUTORS_RAW = "contributors.raw";
        public const string DATERANGES = "dateRanges";
        public const string DATES = "dates";
        public const string DOI = "doi";
        public const string FACETAUTHORS = "facetAuthors";
        public const string GENRE = "genre";
        public const string HASEXTERNALCONTENT = "hasExternalContent";
        public const string HASLOCALCONTENT = "hasLocalContent";
        public const string HASSEGMENTS = "hasSegments";
        public const string ID = "id";
        public const string ISBN = "isbn";
        public const string ISSN = "issn";
        public const string ISSUE = "issue";
        public const string ITEMID = "itemId";
        public const string KEYWORDS = "keywords";
        public const string KEYWORDS_RAW = "keywords.raw";
        public const string LANGUAGE = "language";
        public const string MATERIALTYPE = "materialType";
        public const string NOTES = "notes";
        public const string OCLC = "oclc";
        public const string PAGERANGE = "pageRange";
        public const string PUBLICATIONPLACE = "publicationPlace";
        public const string PUBLICATIONPLACE_RAW = "publicationPlace.raw";
        public const string PUBLISHER = "publisher";
        public const string PUBLISHER_RAW = "publisher.raw";
        public const string SCORE = "score";
        public const string SEARCHAUTHORS = "searchAuthors";
        public const string SEGMENTID = "segmentId";
        public const string SERIES = "series";
        public const string SORTTITLE = "sortTitle";
        public const string STARTPAGEID = "startPageId";
        public const string TEXT = "text";
        public const string TITLE = "title";
        public const string TITLE_RAW = "title.raw";
        public const string TITLE_ABBR = "title.abbr";
        public const string TITLEID = "titleId";
        public const string TRANSLATEDTITLE = "translatedTitle";
        public const string TRANSLATEDTITLE_ABBR = "translatedTitle.abbr";
        public const string UNIFORMTITLE = "uniformTitle";
        public const string UNIFORMTITLE_ABBR = "uniformTitle.abbr";
        public const string URL = "url";
        public const string VARIANTS = "variants";
        public const string VARIANTS_ABBR = "variants.abbr";
        public const string VOLUME = "volume";

        // Page index
        public const string NAMES = "names";
        public const string NAMES_RAW = "names.raw";
        public const string PAGEINDICATORS = "pageIndicators";
        public const string PAGETYPES = "pageTypes";
        public const string SEGMENTS = "segments";
        public const string SEQUENCE = "sequence";
        public const string TEXTPATH = "textPath";

        // Author index
        public const string AUTHORNAMES = "authorNames";
        public const string PRIMARYAUTHORNAME = "primaryAuthorName";

        // Keyword index
        public const string KEYWORD = "keyword";
        public const string KEYWORD_RAW = "keyword.raw";

        // Name index
        public const string COUNT = "count";
        public const string NAME = "name";
    }
}
