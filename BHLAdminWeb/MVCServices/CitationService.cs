using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.AdminWeb.MVCServices
{
    public class CitationService
    {
        public class ImportFileException : Exception
        {
            public ImportFileException() : base() { }
            public ImportFileException(string message) : base(message) { }
            public ImportFileException(string message, System.Exception inner) : base(message, inner) { }
        }

        #region DropDownList data

        /// <summary>
        /// Provide the list of data sources to be used during the segment import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> DataSourceTypeList()
        {
            Dictionary<string, string> dataSourceTypes = new Dictionary<string, string>();
            dataSourceTypes.Add("text/plain", "Tab-Delimited Text File");
            dataSourceTypes.Add("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel (.xlsx)");
            dataSourceTypes.Add("application/vnd.ms-excel", "Excel 97-2003 (.xls)");

            return dataSourceTypes;
        }

        /// <summary>
        /// Provide the list of users that have uploaded segments
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> UserList()
        {
            BHLProvider provider = new BHLProvider();
            return provider.AspNetUserSelectWithImportFiles();
        }

        public List<SegmentGenre> GenreList()
        {
            BHLProvider provider = new BHLProvider();
            return provider.SegmentGenreSelectAll();
        }

        public List<ImportFileStatus> ImportFileStatusList()
        {
            List<ImportFileStatus> statusList = new BHLProvider().ImportFileStatusSelectAll();
            for (int x = statusList.Count - 1; x >= 0; x--)
            {
                if (statusList[x].StatusName == "Loading") statusList.RemoveAt(x);
            }

            return statusList;
        }

        /// <summary>
        /// Returns the list of record statuses (encoded as JSON) to be used during the segment review process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetImportRecordStatuses()
        {
            Dictionary<string, string> statusList = new Dictionary<string, string>();

            List<ImportRecordStatus> statuses = new BHLProvider().ImportRecordStatusSelectAll();
            foreach (ImportRecordStatus status in statuses)
            {
                if (status.StatusName != "Imported") statusList.Add(status.ImportRecordStatusID.ToString(), status.StatusName);
            }

            return statusList;
        }

        /// <summary>
        /// Provide the list of row/column delimiters to be used during the segment import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> RowColumnDelimiterList()
        {
            Dictionary<string, string> delimiters = new Dictionary<string, string>();
            delimiters.Add("\\r\\n", "{CR}{LF}");
            delimiters.Add("\\r", "{CR}");
            delimiters.Add("\\n", "{LF}");
            delimiters.Add(";", "Semicolon");
            delimiters.Add(":", "Colon");
            delimiters.Add(",", "Comma");
            delimiters.Add("\\t", "Tab");
            delimiters.Add("|", "Vertical Bar");

            return delimiters;
        }

        /// <summary>
        /// Provide the list of code pages to be used during the segment import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> CodePageList()
        {
            Dictionary<string, string> codePages = new Dictionary<string, string>();
            codePages.Add("037", "037: IBM EBCDIC US-Canada");
            codePages.Add("437", "437: OEM United States");
            codePages.Add("500", "500: IBM EBCDIC International");
            codePages.Add("708", "708: Arabic (ASMO 708)");
            codePages.Add("709", "709: Arabic (ASMO-449+, BCON V4)");
            codePages.Add("710", "710: Arabic - Transparent Arabic");
            codePages.Add("720", "720: Arabic (Transparent ASMO); Arabic (DOS)");
            codePages.Add("737", "737: OEM Greek (formerly 437G); Greek (DOS)");
            codePages.Add("775", "775: OEM Baltic; Baltic (DOS)");
            codePages.Add("850", "850: OEM Multilingual Latin 1; Western European (DOS)");
            codePages.Add("852", "852: OEM Latin 2; Central European (DOS)");
            codePages.Add("855", "855: OEM Cyrillic (primarily Russian)");
            codePages.Add("857", "857: OEM Turkish; Turkish (DOS)");
            codePages.Add("858", "858: OEM Multilingual Latin 1 + Euro symbol");
            codePages.Add("860", "860: OEM Portuguese; Portuguese (DOS)");
            codePages.Add("861", "861: OEM Icelandic; Icelandic (DOS)");
            codePages.Add("862", "862: OEM Hebrew; Hebrew (DOS)");
            codePages.Add("863", "863: OEM French Canadian; French Canadian (DOS)");
            codePages.Add("864", "864: OEM Arabic; Arabic (864)");
            codePages.Add("865", "865: OEM Nordic; Nordic (DOS)");
            codePages.Add("866", "866: OEM Russian; Cyrillic (DOS)");
            codePages.Add("869", "869: OEM Modern Greek; Greek, Modern (DOS)");
            codePages.Add("870", "870: IBM EBCDIC Multilingual/ROECE (Latin 2); IBM EBCDIC Multilingual Latin 2");
            codePages.Add("874", "874: ANSI/OEM Thai (ISO 8859-11); Thai (Windows)");
            codePages.Add("875", "875: IBM EBCDIC Greek Modern");
            codePages.Add("932", "932: ANSI/OEM Japanese; Japanese (Shift-JIS)");
            codePages.Add("936", "936: ANSI/OEM Simplified Chinese (PRC, Singapore); Chinese Simplified (GB2312)");
            codePages.Add("949", "949: ANSI/OEM Korean (Unified Hangul Code)");
            codePages.Add("950", "950: ANSI/OEM Traditional Chinese (Taiwan; Hong Kong SAR, PRC); Chinese Traditional (Big5)");
            codePages.Add("1026", "1026: IBM EBCDIC Turkish (Latin 5)");
            codePages.Add("1047", "1047: IBM EBCDIC Latin 1/Open System");
            codePages.Add("1140", "1140: IBM EBCDIC US-Canada (037 + Euro symbol); IBM EBCDIC (US-Canada-Euro)");
            codePages.Add("1141", "1141: IBM EBCDIC Germany (20273 + Euro symbol); IBM EBCDIC (Germany-Euro)");
            codePages.Add("1142", "1142: IBM EBCDIC Denmark-Norway (20277 + Euro symbol); IBM EBCDIC (Denmark-Norway-Euro)");
            codePages.Add("1143", "1143: IBM EBCDIC Finland-Sweden (20278 + Euro symbol); IBM EBCDIC (Finland-Sweden-Euro)");
            codePages.Add("1144", "1144: IBM EBCDIC Italy (20280 + Euro symbol); IBM EBCDIC (Italy-Euro)");
            codePages.Add("1145", "1145: IBM EBCDIC Latin America-Spain (20284 + Euro symbol); IBM EBCDIC (Spain-Euro)");
            codePages.Add("1146", "1146: IBM EBCDIC United Kingdom (20285 + Euro symbol); IBM EBCDIC (UK-Euro)");
            codePages.Add("1147", "1147: IBM EBCDIC France (20297 + Euro symbol); IBM EBCDIC (France-Euro)");
            codePages.Add("1148", "1148: IBM EBCDIC International (500 + Euro symbol); IBM EBCDIC (International-Euro)");
            codePages.Add("1149", "1149: IBM EBCDIC Icelandic (20871 + Euro symbol); IBM EBCDIC (Icelandic-Euro)");
            codePages.Add("1200", "1200: Unicode UTF-16, little endian byte order (BMP of ISO 10646)");
            codePages.Add("1201", "1201: Unicode UTF-16, big endian byte order");
            codePages.Add("1250", "1250: ANSI Central European; Central European (Windows)");
            codePages.Add("1251", "1251: ANSI Cyrillic; Cyrillic (Windows)");
            codePages.Add("1252", "1252: ANSI Latin 1; Western European (Windows)");
            codePages.Add("1253", "1253: ANSI Greek; Greek (Windows)");
            codePages.Add("1254", "1254: ANSI Turkish; Turkish (Windows)");
            codePages.Add("1255", "1255: ANSI Hebrew; Hebrew (Windows)");
            codePages.Add("1256", "1256: ANSI Arabic; Arabic (Windows)");
            codePages.Add("1257", "1257: ANSI Baltic; Baltic (Windows)");
            codePages.Add("1258", "1258: ANSI/OEM Vietnamese; Vietnamese (Windows)");
            codePages.Add("1361", "1361: Korean (Johab)");
            codePages.Add("10000", "10000: MAC Roman; Western European (Mac)");
            codePages.Add("10001", "10001: Japanese (Mac)");
            codePages.Add("10002", "10002: MAC Traditional Chinese (Big5); Chinese Traditional (Mac)");
            codePages.Add("10003", "10003: Korean (Mac)");
            codePages.Add("10004", "10004: Arabic (Mac)");
            codePages.Add("10005", "10005: Hebrew (Mac)");
            codePages.Add("10006", "10006: Greek (Mac)");
            codePages.Add("10007", "10007: Cyrillic (Mac)");
            codePages.Add("10008", "10008: MAC Simplified Chinese (GB 2312); Chinese Simplified (Mac)");
            codePages.Add("10010", "10010: Romanian (Mac)");
            codePages.Add("10017", "10017: Ukrainian (Mac)");
            codePages.Add("10021", "10021: Thai (Mac)");
            codePages.Add("10029", "10029: MAC Latin 2; Central European (Mac)");
            codePages.Add("10079", "10079: Icelandic (Mac)");
            codePages.Add("10081", "10081: Turkish (Mac)");
            codePages.Add("10082", "10082: Croatian (Mac)");
            codePages.Add("12000", "12000: Unicode UTF-32, little endian byte order");
            codePages.Add("12001", "12001: Unicode UTF-32, big endian byte order");
            codePages.Add("20000", "20000: CNS Taiwan; Chinese Traditional (CNS)");
            codePages.Add("20001", "20001: TCA Taiwan");
            codePages.Add("20002", "20002: Eten Taiwan; Chinese Traditional (Eten)");
            codePages.Add("20003", "20003: IBM5550 Taiwan");
            codePages.Add("20004", "20004: TeleText Taiwan");
            codePages.Add("20005", "20005: Wang Taiwan");
            codePages.Add("20105", "20105: IA5 (IRV International Alphabet No. 5, 7-bit); Western European (IA5)");
            codePages.Add("20106", "20106: IA5 German (7-bit)");
            codePages.Add("20107", "20107: IA5 Swedish (7-bit)");
            codePages.Add("20108", "20108; IA5 Norwegian (7-bit)");
            codePages.Add("20127", "20127: US-ASCII (7-bit)");
            codePages.Add("20261", "20261: T.61");
            codePages.Add("20269", "20269: ISO 6937 Non-Spacing Accent");
            codePages.Add("20273", "20273: IBM EBCDIC Germany");
            codePages.Add("20277", "20277: IBM EBCDIC Denmark-Norway");
            codePages.Add("20278", "20278: IBM EBCDIC Finland-Sweden");
            codePages.Add("20280", "20280: IBM EBCDIC Italy");
            codePages.Add("20284", "20282: IBM EBCDIC Latin America-Spain");
            codePages.Add("20285", "20285: IBM EBCDIC United Kingdom");
            codePages.Add("20290", "20290: IBM EBCDIC Japanese Katakana Extended");
            codePages.Add("20297", "20297: IBM EBCDIC France");
            codePages.Add("20420", "20420: IBM EBCDIC Arabic");
            codePages.Add("20423", "20423: IBM EBCDIC Greek");
            codePages.Add("20424", "20424: IBM EBCDIC Hebrew");
            codePages.Add("20833", "20833: IBM EBCDIC Korean Extended");
            codePages.Add("20838", "20838: IBM EBCDIC Thai");
            codePages.Add("20866", "20866: Russian (KOI8-R); Cyrillic (KOI8-R)");
            codePages.Add("20871", "20871: IBM EBCDIC Icelandic");
            codePages.Add("20880", "20880: IBM EBCDIC Cyrillic Russian");
            codePages.Add("20905", "20905: IBM EBCDIC Turkish");
            codePages.Add("20924", "20924: IBM EBCDIC Latin 1/Open System (1047 + Euro symbol)");
            codePages.Add("20932", "20932: Japanese (JIS 0208-1990 and 0212-1990)");
            codePages.Add("20936", "20936: Simplified Chinese (GB2312); Chinese Simplified (GB2312-80)");
            codePages.Add("20949", "20949: Korean Wansung");
            codePages.Add("21025", "21025: IBM EBCDIC Cyrillic Serbian-Bulgarian");
            codePages.Add("21866", "21866: Ukrainian (KOI8-U); Cyrillic (KOI8-U)");
            codePages.Add("28591", "28591: ISO 8859-1 Latin 1; Western European (ISO)");
            codePages.Add("28592", "28592: ISO 8859-2 Central European; Central European (ISO)");
            codePages.Add("28593", "28593: ISO 8859-3 Latin 3");
            codePages.Add("28594", "28594: ISO 8859-4 Baltic");
            codePages.Add("28595", "28595: ISO 8859-5 Cyrillic");
            codePages.Add("28596", "28596: ISO 8859-6 Arabic");
            codePages.Add("28597", "28597: ISO 8859-7 Greek");
            codePages.Add("28598", "28598: ISO 8859-8 Hebrew; Hebrew (ISO-Visual)");
            codePages.Add("28599", "28599: ISO 8859-9 Turkish");
            codePages.Add("28603", "28603: ISO 8859-13 Estonian");
            codePages.Add("28605", "28605: ISO 8859-15 Latin 9");
            codePages.Add("29001", "29001; Europa 3");
            codePages.Add("38598", "38598: ISO 8859-8 Hebrew; Hebrew (ISO-Logical)");
            codePages.Add("50220", "50220: ISO 2022 Japanese with no halfwidth Katakana; Japanese (JIS)");
            codePages.Add("50221", "50221: ISO 2022 Japanese with halfwidth Katakana; Japanese (JIS-Allow 1 byte Kana)");
            codePages.Add("50222", "50222: ISO 2022 Japanese JIS X 0201-1989; Japanese (JIS-Allow 1 byte Kana - SO/SI)");
            codePages.Add("50225", "50225: 28593: ISO 2022 Korean");
            codePages.Add("50227", "50227: ISO 2022 Simplified Chinese; Chinese Simplified (ISO 2022)");
            codePages.Add("50229", "50229: ISO 2022 Traditional Chinese");
            codePages.Add("50930", "50930: EBCDIC Japanese (Katakana) Extended");
            codePages.Add("50931", "50931: EBCDIC US-Canada and Japanese");
            codePages.Add("50933", "50933: EBCDIC Korean Extended and Korean");
            codePages.Add("50935", "50935: EBCDIC Simplified Chinese Extended and Simplified Chinese");
            codePages.Add("50936", "50936: EBCDIC Simplified Chinese");
            codePages.Add("50937", "50937: EBCDIC US-Canada and Traditional Chinese");
            codePages.Add("50939", "50939: EBCDIC Japanese (Latin) Extended and Japanese");
            codePages.Add("51932", "51932: EUC Japanese");
            codePages.Add("51936", "51936: EUC Simplified Chinese; Chinese Simplified (EUC)");
            codePages.Add("51949", "51949: EUC Korean");
            codePages.Add("51950", "51950: EUC Traditional Chinese");
            codePages.Add("52936", "52936: HZ-GB2312 Simplified Chinese; Chinese Simplified (HZ)");
            codePages.Add("54936", "54936: Windows XP and later: GB18030 Simplified Chinese (4 byte); Chinese Simplified (GB18030)");
            codePages.Add("57002", "57002: ISCII Devanagari");
            codePages.Add("57003", "57003: ISCII Bengali");
            codePages.Add("57004", "57004: ISCII Tamil");
            codePages.Add("57005", "57005: ISCII Telugu");
            codePages.Add("57006", "57006: ISCII Assamese");
            codePages.Add("57007", "57007: ISCII Oriya");
            codePages.Add("57008", "57008: ISCII Kannada");
            codePages.Add("57009", "57009: ISCII Malayalam");
            codePages.Add("57010", "57010: ISCII Gujarati");
            codePages.Add("57011", "57011: ISCII Punjabi");
            codePages.Add("65000", "65000: UTF-7");
            codePages.Add("65001", "65001: UTF-8");

            return codePages;
        }

        /// <summary>
        /// Provide the list of columns available for mapping during the segment import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> MappedColumnList()
        {
            Dictionary<string, string> mappedColumns = new Dictionary<string, string>();

            mappedColumns.Add(Models.CitationImportModel.MappedColumn.NONE.name.ToString(), Models.CitationImportModel.MappedColumn.NONE.name);

            //mappedColumns.Add(Models.CitationImportModel.MappedColumn.AUTHORENDDATE.name.ToString(), Models.CitationImportModel.MappedColumn.AUTHORENDDATE.name);
            //mappedColumns.Add(Models.CitationImportModel.MappedColumn.AUTHORFIRSTNAME.name.ToString(), Models.CitationImportModel.MappedColumn.AUTHORFIRSTNAME.name);
            //mappedColumns.Add(Models.CitationImportModel.MappedColumn.AUTHORLASTNAME.name.ToString(), Models.CitationImportModel.MappedColumn.AUTHORLASTNAME.name);
            //mappedColumns.Add(Models.CitationImportModel.MappedColumn.AUTHORSTARTDATE.name.ToString(), Models.CitationImportModel.MappedColumn.AUTHORSTARTDATE.name);
            //mappedColumns.Add(Models.CitationImportModel.MappedColumn.AUTHORTYPE.name.ToString(), Models.CitationImportModel.MappedColumn.AUTHORTYPE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ABSTRACT.name.ToString(), Models.CitationImportModel.MappedColumn.ABSTRACT.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ADDITIONALPAGES.name.ToString(), Models.CitationImportModel.MappedColumn.ADDITIONALPAGES.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARK.name.ToString(), Models.CitationImportModel.MappedColumn.ARK.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.DOI.name.ToString(), Models.CitationImportModel.MappedColumn.DOI.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARTICLEPAGERANGE.name.ToString(), Models.CitationImportModel.MappedColumn.ARTICLEPAGERANGE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARTICLEENDPAGE.name.ToString(), Models.CitationImportModel.MappedColumn.ARTICLEENDPAGE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARTICLEENDPAGEID.name.ToString(), Models.CitationImportModel.MappedColumn.ARTICLEENDPAGEID.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARTICLESTARTPAGE.name.ToString(), Models.CitationImportModel.MappedColumn.ARTICLESTARTPAGE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARTICLESTARTPAGEID.name.ToString(), Models.CitationImportModel.MappedColumn.ARTICLESTARTPAGEID.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ARTICLETITLE.name.ToString(), Models.CitationImportModel.MappedColumn.ARTICLETITLE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.AUTHORNAMES.name.ToString(), Models.CitationImportModel.MappedColumn.AUTHORNAMES.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.BIOSTOR.name.ToString(), Models.CitationImportModel.MappedColumn.BIOSTOR.name);

            // In order to allow "Book/Journal Title" (i.e. Segment.ContainerTitle) to be updated in bulk, there
            // would need to be rules in place to allow this only for segments that point to externally-hosted 
            // content.  For BHL-hosted content, this field is never used, so updating it makes no sense.
            // For now (July 11, 2023), bulk updates of this column will be disabled.

            //mappedColumns.Add(Models.CitationImportModel.MappedColumn.BOOKJOURNALTITLE.name.ToString(), Models.CitationImportModel.MappedColumn.BOOKJOURNALTITLE.name);

            mappedColumns.Add(Models.CitationImportModel.MappedColumn.CONTRIBUTORS.name.ToString(), Models.CitationImportModel.MappedColumn.CONTRIBUTORS.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.COPYRIGHTSTATUS.name.ToString(), Models.CitationImportModel.MappedColumn.COPYRIGHTSTATUS.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.DOWNLOADURL.name.ToString(), Models.CitationImportModel.MappedColumn.DOWNLOADURL.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.DUEDILIGENCE.name.ToString(), Models.CitationImportModel.MappedColumn.DUEDILIGENCE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.EDITION.name.ToString(), Models.CitationImportModel.MappedColumn.EDITION.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ISBN.name.ToString(), Models.CitationImportModel.MappedColumn.ISBN.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ISSN.name.ToString(), Models.CitationImportModel.MappedColumn.ISSN.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ISSUE.name.ToString(), Models.CitationImportModel.MappedColumn.ISSUE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.ITEMID.name.ToString(), Models.CitationImportModel.MappedColumn.ITEMID.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.JSTOR.name.ToString(), Models.CitationImportModel.MappedColumn.JSTOR.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.KEYWORDS.name.ToString(), Models.CitationImportModel.MappedColumn.KEYWORDS.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.LANGUAGE.name.ToString(), Models.CitationImportModel.MappedColumn.LANGUAGE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.LCCN.name.ToString(), Models.CitationImportModel.MappedColumn.LCCN.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.LICENSE.name.ToString(), Models.CitationImportModel.MappedColumn.LICENSE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.LICENSEURL.name.ToString(), Models.CitationImportModel.MappedColumn.LICENSEURL.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.NOTES.name.ToString(), Models.CitationImportModel.MappedColumn.NOTES.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.OCLC.name.ToString(), Models.CitationImportModel.MappedColumn.OCLC.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.PUBLICATIONDETAILS.name.ToString(), Models.CitationImportModel.MappedColumn.PUBLICATIONDETAILS.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.PUBLISHERNAME.name.ToString(), Models.CitationImportModel.MappedColumn.PUBLISHERNAME.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.PUBLISHERPLACE.name.ToString(), Models.CitationImportModel.MappedColumn.PUBLISHERPLACE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.RIGHTS.name.ToString(), Models.CitationImportModel.MappedColumn.RIGHTS.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.SEGMENTID.name.ToString(), Models.CitationImportModel.MappedColumn.SEGMENTID.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.SERIES.name.ToString(), Models.CitationImportModel.MappedColumn.SERIES.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.TL2.name.ToString(), Models.CitationImportModel.MappedColumn.TL2.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.TRANSLATEDTITLE.name.ToString(), Models.CitationImportModel.MappedColumn.TRANSLATEDTITLE.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.URL.name.ToString(), Models.CitationImportModel.MappedColumn.URL.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.VOLUME.name.ToString(), Models.CitationImportModel.MappedColumn.VOLUME.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.WIKIDATA.name.ToString(), Models.CitationImportModel.MappedColumn.WIKIDATA.name);
            mappedColumns.Add(Models.CitationImportModel.MappedColumn.YEAR.name.ToString(), Models.CitationImportModel.MappedColumn.YEAR.name);

            return mappedColumns;
        }

        /// <summary>
        /// Provide the list of data sources to be used during the segment import process
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ReportDateRangeList()
        {
            Dictionary<string, string> dataSourceTypes = new Dictionary<string, string>();
            dataSourceTypes.Add("1", "Last Day");
            dataSourceTypes.Add("30", "Last 30 Days");
            dataSourceTypes.Add("90", "Last 90 Days");
            dataSourceTypes.Add("180", "Last 180 Days");
            dataSourceTypes.Add("365", "Last Year");

            return dataSourceTypes;
        }

        #endregion DropDownList data

    }
}