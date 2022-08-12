using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHL.Search.SQL
{
    internal class SQLField
    {
        public const string TOTALHITS = "TotalHits";

        // Item index
        public const string ASSOCIATIONS = "Associations";
        public const string AUTHORS = "Authors";
        public const string BOOKISVIRTUAL = "BookIsVirtual";
        public const string COLLECTIONS = "Collections";
        public const string CONTAINER = "ContainerTitle";
        public const string CONTRIBUTORS = "Contributors";
        public const string DATES = "Date";
        public const string DOI = "DOIName";
        public const string GENRE = "Genre";
        public const string HASEXTERNALCONTENT = "HasExternalContent";
        public const string HASILLUSTRATIONS = "HasIllustrations";
        public const string HASLOCALCONTENT = "HasLocalContent";
        public const string HASSEGMENTS = "HasSegments";
        public const string ISBN = "ISBN";
        public const string ISSN = "ISSN";
        public const string ISSUE = "Issue";
        public const string ITEMID = "ItemID";
        public const string KEYWORDS = "Subjects";
        public const string LANGUAGE = "LanguageName";
        public const string MATERIALTYPE = "MaterialTypeLabel";
        public const string OCLC = "OCLC";
        public const string PAGERANGE = "PageRange";
        public const string PUBLICATIONPLACE = "PublicationPlace";
        public const string PUBLISHER = "PublisherName";
        public const string SCORE = "Score";
        public const string SEARCHAUTHORS = "SearchAuthors";
        public const string SEGMENTID = "SegmentId";
        public const string SERIES = "Series";
        public const string SORTTITLE = "SortTitle";
        public const string STARTPAGEID = "StartPageId";
        public const string TITLE = "Title";
        public const string TITLEID = "TitleId";
        public const string TRANSLATEDTITLE = "TranslatedTitle";
        public const string UNIFORMTITLE = "UniformTitle";
        public const string URL = "Url";
        public const string VARIANTS = "Variants";
        public const string VOLUME = "Volume";

        /*
        // Page index
        public const string NAMES = "names";
        public const string PAGETYPES = "pageTypes";
        public const string SEGMENTS = "segments";
        public const string TEXTPATH = "textPath";
        */

        // Author index
        public const string AUTHORID = "AuthorID";
        public const string PRIMARYAUTHORNAME = "PrimaryAuthorName";

        // Keyword index
        public const string KEYWORDID = "KeywordID";
        public const string KEYWORD = "Keyword";

        // Name index
        public const string NAMERESOLVEDID = "NameResolvedID";
        public const string RESOLVEDNAMESTRING = "ResolvedNameString";
        public const string NAMECOUNT = "NameCount";
    }
}
