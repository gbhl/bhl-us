using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using MOBOT.BHL.OAI2;

namespace MOBOT.BHL.OAIMODS
{
    public class Convert
    {
        OAIRecord _oaiRecord;

        public Convert(OAIRecord oaiRecord)
        {
            _oaiRecord = oaiRecord;
        }

        #region ToString

        public new String ToString()
        {
            StringBuilder sb = new StringBuilder();

            string mods30Header = "<mods xmlns:xlink=\"http://www.w3.org/1999/xlink\" version=\"3.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.loc.gov/mods/v3\" xsi:schemaLocation=\"http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-0.xsd\">\n";
            string mods33Header = "<mods xmlns:xlink=\"http://www.w3.org/1999/xlink\" version=\"3.3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.loc.gov/mods/v3\" xsi:schemaLocation=\"http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-3.xsd\">\n";

            // MODS needs to be formatted differently for articles, issues, and books, so check the type
            // of record we have and get the appropriate MODS body
            switch (_oaiRecord.Type)
            {
                case OAIRecord.RecordType.Article:
                case OAIRecord.RecordType.Segment:
                    sb.Append(mods30Header);
                    sb.Append(this.ArticleToString());
                    break;
                case OAIRecord.RecordType.Issue:
                    sb.Append(mods33Header);
                    sb.Append(this.IssueToString());
                    break;
                default:
                    sb.Append(mods30Header);
                    sb.Append(this.BookToString());
                    break;
            }

            sb.Append("</mods>\n");

            return sb.ToString();
        }

        private String BookToString()
        {
            StringBuilder sb = new StringBuilder();

            // Title
            sb.Append(this.GetTitleElement(_oaiRecord.Title, false));

            // Creator
            sb.Append(this.GetNameElement());

            // Type of Resource
            sb.Append(this.GetTypeOfResourceElement());

            // Genre
            sb.Append(this.GetGenreElement());

            // Publisher
            sb.Append(this.GetOriginInfoElement());

            // Language
            sb.Append(this.GetLanguageElement());

            // Subject
            sb.Append(this.GetSubjectElement());

            // Classification
            sb.Append(this.GetClassificationElement(_oaiRecord));

            // RelatedItem (series information)
            sb.Append(this.GetRelatedItemSeriesElement());

            // Identifier
            sb.Append(this.GetIdentifierUriElement());
            sb.Append(this.GetIdentifierElement(_oaiRecord));

            //  AccessCondition
            sb.Append(this.GetAccessConditionElement());

            // RecordInfo
            sb.Append(this.GetRecordInfoElement());

            return sb.ToString();
        }

        private string IssueToString()
        {
            StringBuilder sb = new StringBuilder();

            // Title
            sb.Append(this.GetTitleElement(_oaiRecord.Title, false));

            // Creator
            sb.Append(this.GetNameElement());

            // Type of Resource
            sb.Append(this.GetTypeOfResourceElement());

            // Genre
            sb.Append(this.GetGenreElement());

            // Publisher
            sb.Append(this.GetOriginInfoElement());

            // Language
            sb.Append(this.GetLanguageElement());

            // Subject
            sb.Append(this.GetSubjectElement());

            // Classification
            sb.Append(this.GetClassificationElement(_oaiRecord));

            // RelatedItem (series information)
            sb.Append(this.GetRelatedItemSeriesElement());

            // Identifier
            sb.Append(this.GetIdentifierUriElement());
            sb.Append(this.GetIdentifierElement(_oaiRecord));

            // Location
            sb.Append(this.GetLocationElement());

            //  AccessCondition
            sb.Append(this.GetAccessConditionElement());

            // RecordInfo
            sb.Append(this.GetRecordInfoElement());

            return sb.ToString();
        }

        private String ArticleToString()
        {
            StringBuilder sb = new StringBuilder();

            // Title
            sb.Append(this.GetTitleElement(_oaiRecord.Title, false));

            // Creator
            sb.Append(this.GetNameElement());

            // Type of Resource
            sb.Append(this.GetTypeOfResourceElement());

            // Genre
            sb.Append(this.GetGenreElement());

            // Language
            sb.Append(this.GetLanguageElement());

            // Subject
            sb.Append(this.GetSubjectElement());

            // RelatedItem (journal information)
            sb.Append(this.GetRelatedItemHostElement());

            // Identifier
            sb.Append(this.GetIdentifierUriElement());

            //  AccessCondition
            sb.Append(this.GetAccessConditionElement());

            // RecordInfo
            sb.Append(this.GetRecordInfoElement());

            return sb.ToString();
        }

        #endregion ToString

        #region Elements

        /// <summary>
        /// Build the title element
        /// </summary>
        /// <returns></returns>
        private String GetTitleElement(String title, bool titleOnly)
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(title))
            {
                sb.Append("<titleInfo>\n");
                sb.Append("\t<title>" + HttpUtility.HtmlEncode(title) + "</title>\n");
                if (!String.IsNullOrEmpty(_oaiRecord.PartNumber) && !titleOnly)
                {
                    sb.Append("\t<partNumber>" + HttpUtility.HtmlEncode(_oaiRecord.PartNumber) + "</partNumber>\n");
                }
                if (!String.IsNullOrEmpty(_oaiRecord.PartName) && !titleOnly)
                {
                    sb.Append("\t<partName>" + HttpUtility.HtmlEncode(_oaiRecord.PartName) + "</partName>\n");
                }
                sb.Append("</titleInfo>\n");
            }

            if (!titleOnly)
            {
                foreach (KeyValuePair<string, OAIRecord.TitleVariant> titleVariant in _oaiRecord.TitleVariants)
                {
                    sb.Append("<titleInfo type=\"" + HttpUtility.HtmlEncode(titleVariant.Key) + "\">\n");
                    sb.Append("\t<title>" + HttpUtility.HtmlEncode(titleVariant.Value.Title) + "</title>\n");
                    if (!String.IsNullOrEmpty(titleVariant.Value.PartNumber))
                    {
                        sb.Append("\t<partNumber>" + HttpUtility.HtmlEncode(titleVariant.Value.PartNumber) + "</partNumber>\n");
                    }
                    if (!String.IsNullOrEmpty(titleVariant.Value.PartName))
                    {
                        sb.Append("\t<partName>" + HttpUtility.HtmlEncode(titleVariant.Value.PartName) + "</partName>\n");
                    }
                    sb.Append("</titleInfo>\n");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the name element
        /// </summary>
        /// <returns></returns>
        private string GetNameElement()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, OAIRecord.Creator> creatorData in _oaiRecord.Creators)
            {
                String type = String.Empty;
                switch (creatorData.Key)
                {
                    case "100": // main entry - personal name
                    case "700": // added entry - personal name
                        type = " type=\"personal\"";
                        break;
                    case "110": // main entry - corporate name
                    case "710": // added entry - corporate name
                        type = " type=\"corporate\"";
                        break;
                    case "111": // main entry - meeting name
                    case "711": // added entry - meeting name
                        type = " type=\"conference\"";
                        break;
                    case "720": // added entry - uncontrolled name
                        break;
                    default:
                        break;
                }

                string name = (creatorData.Value.Name + ' ' + creatorData.Value.Numeration + ' ' + 
                    creatorData.Value.Location + ' ' + creatorData.Value.FullerForm).Trim();
                name = String.IsNullOrEmpty(name) ? creatorData.Value.FullName : name;
                string date = creatorData.Value.Dates;
                sb.Append("<name" + type + ">\n");  // Add "type" attribute here
                sb.Append("\t<namePart>" + HttpUtility.HtmlEncode(name) + "</namePart>\n");
                if (date != string.Empty) sb.Append("\t<namePart type=\"date\">" + HttpUtility.HtmlEncode(date) + "</namePart>\n");
                sb.Append("</name>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the typeOfResource element
        /// </summary>
        /// <returns></returns>
        private String GetTypeOfResourceElement()
        {
            return "<typeOfResource>text</typeOfResource>\n";
        }

        /// <summary>
        /// Build the genre element
        /// </summary>
        /// <returns></returns>
        private String GetGenreElement()
        {
            StringBuilder sb = new StringBuilder();
            String genre = String.Empty;
            ItemType itemType = this.GetItemType();

            switch (_oaiRecord.Type)
            {
                case OAIRecord.RecordType.BookJournal:
                    switch (itemType)
                    {
                        case ItemType.Monograph:
                            genre = "book";
                            break;
                        case ItemType.MultivolumeMonograph:
                            genre = "multivolume monograph";
                            break;
                        case ItemType.Serial:
                            genre = "series";
                            break;
                        default:
                            genre = "book";
                            break;
                    }
                    break;
                case OAIRecord.RecordType.Issue:
                    switch (itemType)
                    {
                        case ItemType.Serial:
                        case ItemType.MultivolumeMonograph:
                            genre = "issue";
                            break;
                        default:
                            genre = "book";
                            break;
                    }

                    break;
                case OAIRecord.RecordType.Article:
                    genre = "article";
                    break;
            }

            sb.Append("<genre authority=\"marcgt\">" + HttpUtility.HtmlEncode(genre) + "</genre>\n");

            return sb.ToString();
        }

        /// <summary>
        /// Build the originInfo element
        /// </summary>
        /// <returns></returns>
        private String GetOriginInfoElement()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(_oaiRecord.PublicationDetails))
            {
                sb.Append("<originInfo>\n");

                // Add publisher place, name, dates
                if (!String.IsNullOrEmpty(_oaiRecord.PublicationPlace))
                {
                    sb.Append("\t<place>\n");
                    sb.Append("\t\t<placeTerm type=\"text\">" + HttpUtility.HtmlEncode(_oaiRecord.PublicationPlace) + "</placeTerm>\n");
                    sb.Append("\t</place>\n");
                }

                if (!String.IsNullOrEmpty(_oaiRecord.Publisher))
                {
                    sb.Append("\t<publisher>" + HttpUtility.HtmlEncode(_oaiRecord.Publisher) + "</publisher>\n");
                }

                if (!String.IsNullOrEmpty(_oaiRecord.PublicationDates))
                {
                    sb.Append("\t<dateIssued>" + HttpUtility.HtmlEncode(_oaiRecord.PublicationDates) + "</dateIssued>\n");
                    if (!String.IsNullOrEmpty(_oaiRecord.PublicationStartYear))
                    {
                        sb.Append("\t<dateIssued encoding=\"marc\" point=\"start\">" + HttpUtility.HtmlEncode(_oaiRecord.PublicationStartYear) + "</dateIssued>\n");
                    }
                    if (!String.IsNullOrEmpty(_oaiRecord.PublicationEndYear))
                    {
                        sb.Append("\t<dateIssued encoding=\"marc\" point=\"end\">" + HttpUtility.HtmlEncode(_oaiRecord.PublicationEndYear) + "</dateIssued>\n");
                    }
                }

                if (!String.IsNullOrEmpty(_oaiRecord.Date) && _oaiRecord.Type == OAIRecord.RecordType.Issue)
                {
                    sb.Append("\t<dateOther type=\"issueDate\">" + HttpUtility.HtmlEncode(_oaiRecord.Date) + "</dateOther>\n");
                }

                if (!String.IsNullOrEmpty(_oaiRecord.Edition))
                {
                    sb.Append("\t<edition>" + HttpUtility.HtmlEncode(_oaiRecord.Edition) + "</edition>\n");
                }

                if (!String.IsNullOrEmpty(_oaiRecord.PublicationFrequency))
                {
                    sb.Append("\t<frequency>" + HttpUtility.HtmlEncode(_oaiRecord.PublicationFrequency) + "</frequency>\n");
                }

                sb.Append("</originInfo>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the language element
        /// </summary>
        /// <returns></returns>
        private String GetLanguageElement()
        {
            StringBuilder sb = new StringBuilder();

            foreach (String language in _oaiRecord.Languages)
            {
                sb.Append("<language>\n");
                sb.Append("\t<languageTerm authority=\"iso639-2b\" type=\"text\">" + HttpUtility.HtmlEncode(language) + "</languageTerm>\n");
                sb.Append("</language>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the subject element
        /// </summary>
        /// <returns></returns>
        private String GetSubjectElement()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> subject in _oaiRecord.Subjects)
            {
                sb.Append("<subject>\n");

                string marcTag = string.Empty;
                string marcCode = string.Empty;

                if (subject.Key != string.Empty)
                {
                    marcTag = subject.Key.Split('|')[0];
                    marcCode = subject.Key.Split('|')[1];
                }

                if (marcCode == "z" || marcTag == "651")
                {
                    sb.Append("\t<geographic>" + HttpUtility.HtmlEncode(subject.Value) + "</geographic>\n");
                }
                else if (marcCode == "y" || marcTag == "648")
                {
                    sb.Append("\t<temporal>" + HttpUtility.HtmlEncode(subject.Value) + "</temporal>\n");
                }
                else if (marcCode == "v")
                {
                    sb.Append("\t<genre>" + HttpUtility.HtmlEncode(subject.Value) + "</genre>\n");
                }
                else if (marcTag == "656")
                {
                    sb.Append("\t<occupation>" + HttpUtility.HtmlEncode(subject.Value) + "</occupation>\n");
                }
                else
                {
                    sb.Append("\t<topic>" + HttpUtility.HtmlEncode(subject.Value) + "</topic>\n");
                }

                sb.Append("</subject>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the classification element
        /// </summary>
        /// <returns></returns>
        private String GetClassificationElement(OAIRecord title)
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(title.CallNumber))
            {
                sb.Append("<classification authority=\"lcc\">" + HttpUtility.HtmlEncode(title.CallNumber) + "</classification>\n");
            }
            if (!String.IsNullOrEmpty(title.Ddc))
            {
                sb.Append("<classification authority=\"ddc\">" + HttpUtility.HtmlEncode(title.Ddc) + "</classification>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the relatedItem element with type of "host"
        /// </summary>
        /// <returns></returns>
        private String GetRelatedItemHostElement()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<relatedItem type=\"host\">\n");

            sb.Append(this.GetTitleElement(_oaiRecord.JournalTitle, true));
            sb.Append(this.GetOriginInfoElement());
            sb.Append(this.GetClassificationElement(_oaiRecord));
            sb.Append(this.GetIdentifierElement(_oaiRecord));

            sb.Append("<part>\n");

            if (!String.IsNullOrEmpty(_oaiRecord.JournalVolume))
            {
                sb.Append("\t<detail type=\"volume\">\n");
                sb.Append("\t\t<number>" + HttpUtility.HtmlEncode(_oaiRecord.JournalVolume) + "</number>\n");
                sb.Append("\t</detail>\n");
            }

            if (!String.IsNullOrEmpty(_oaiRecord.JournalIssue))
            {
                sb.Append("\t<detail type=\"issue\">\n");
                sb.Append("\t\t<number>" + HttpUtility.HtmlEncode(_oaiRecord.JournalIssue) + "</number>\n");
                sb.Append("\t</detail>\n");
            }

            if (!String.IsNullOrEmpty(_oaiRecord.ArticleStartPage) && !String.IsNullOrEmpty(_oaiRecord.ArticleEndPage))
            {
                sb.Append("\t<extent unit=\"pages\">\n");
                if (!String.IsNullOrEmpty(_oaiRecord.ArticleStartPage)) sb.Append("\t\t<start>" + HttpUtility.HtmlEncode(_oaiRecord.ArticleStartPage) + "</start>\n");
                if (!String.IsNullOrEmpty(_oaiRecord.ArticleEndPage)) sb.Append("\t\t<end>" + HttpUtility.HtmlEncode(_oaiRecord.ArticleEndPage) + "</end>\n");
                sb.Append("\t</extent>\n");
            }

            if (!String.IsNullOrEmpty(_oaiRecord.Date))
            {
                sb.Append("\t<date>" + HttpUtility.HtmlEncode(_oaiRecord.Date) + "</date>\n");
            }

            sb.Append("</part>\n");
            sb.Append("</relatedItem>\n");

            return sb.ToString();
        }

        /// <summary>
        /// Build the relatedItem element with type of "preceeding", "succeeding", or "series"
        /// </summary>
        /// <returns></returns>
        private String GetRelatedItemSeriesElement()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, OAIRecord> relatedTitle in _oaiRecord.RelatedTitles)
            {
                String typeAttrib = String.Empty;
                switch (relatedTitle.Key.ToLower())
                {
                    case "series":
                        typeAttrib = " type=\"series\"";
                        break;
                    case "preceding":
                        typeAttrib = " type=\"preceding\"";
                        break;
                    case "succeeding":
                        typeAttrib = " type=\"succeeding\"";
                        break;
                    case "contained in":
                        typeAttrib = " type=\"host\"";
                        break;
                    case "supplement":
                        typeAttrib = " type=\"constituent\"";
                        break;
                    case "parent":
                        typeAttrib = " type=\"host\"";
                        break;
                    case "with":
                        typeAttrib = "";
                        break;
                    case "other":
                        typeAttrib = "";
                        break;
                }

                sb.Append("<relatedItem" + typeAttrib + ">\n");
                sb.Append(this.GetTitleElement(relatedTitle.Value.Title, true));
                sb.Append(this.GetClassificationElement(relatedTitle.Value));
                sb.Append(this.GetIdentifierElement(relatedTitle.Value));
                sb.Append("</relatedItem>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the identifier element with type = 'uri'
        /// </summary>
        /// <returns></returns>
        private String GetIdentifierUriElement()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(_oaiRecord.Url))
            {
                sb.Append("<identifier type=\"uri\">" + HttpUtility.HtmlEncode(_oaiRecord.Url) + "</identifier>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the identifier elements with type not equal to 'uri'
        /// </summary>
        /// <returns></returns>
        private String GetIdentifierElement(OAIRecord title)
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(title.Isbn))
            {
                sb.Append("<identifier type=\"isbn\">" + HttpUtility.HtmlEncode(title.Isbn) + "</identifier>\n");
            }
            if (!String.IsNullOrEmpty(title.Issn))
            {
                sb.Append("<identifier type=\"issn\">" + HttpUtility.HtmlEncode(title.Issn) + "</identifier>\n");
            }
            foreach (String oclcNumber in title.oclcNumbers)
            {
                sb.Append("<identifier type=\"oclc\">" + HttpUtility.HtmlEncode(oclcNumber) + "</identifier>\n");
            }
            if (!String.IsNullOrEmpty(title.Nlm))
            {
                sb.Append("<identifier type=\"nlm\">" + HttpUtility.HtmlEncode(title.Nlm) + "</identifier>\n");
            }
            if (!String.IsNullOrEmpty(title.Llc))
            {
                sb.Append("<identifier type=\"lccn\">" + HttpUtility.HtmlEncode(title.Llc) + "</identifier>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the location element
        /// </summary>
        /// <returns></returns>
        private String GetLocationElement()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(_oaiRecord.Contributor) || !String.IsNullOrEmpty(_oaiRecord.JournalVolume))
            {
                sb.Append("<location>\n");

                if (!String.IsNullOrEmpty(_oaiRecord.Contributor))
                {
                    sb.Append("\t<physicalLocation>" + HttpUtility.HtmlEncode(_oaiRecord.Contributor) + "</physicalLocation>\n");
                }

                sb.Append("\t<url>" + HttpUtility.HtmlEncode(_oaiRecord.Url) + "</url>\n");

                if (!String.IsNullOrEmpty(_oaiRecord.JournalVolume))
                {
                    sb.Append("\t<holdingSimple>\n");
                    sb.Append("\t\t<copyInformation>\n");
                    sb.Append("\t\t\t<enumerationAndChronology>" + HttpUtility.HtmlEncode(_oaiRecord.JournalVolume) + "</enumerationAndChronology>\n");
                    sb.Append("\t\t</copyInformation>\n");
                    sb.Append("\t</holdingSimple>\n");
                }

                sb.Append("</location>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the accessCondition element
        /// </summary>
        /// <returns></returns>
        private String GetAccessConditionElement()
        {
            StringBuilder sb = new StringBuilder();

            foreach (String right in _oaiRecord.Rights)
            {
                sb.Append("<accessCondition type=\"useAndReproduction\">");
                sb.Append(HttpUtility.HtmlEncode(right));
                sb.Append("</accessCondition>\n");
            }

            return sb.ToString();
        }

        private String GetRecordInfoElement()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(_oaiRecord.OriginalCatalogingSource))
            {
                sb.Append("<recordInfo>\n");
                sb.Append("\t<recordContentSource authority=\"marcorg\">");
                sb.Append(HttpUtility.HtmlEncode(_oaiRecord.OriginalCatalogingSource));
                sb.Append("</recordContentSource>\n");
                sb.Append("</recordInfo>\n");
            }

            return sb.ToString();
        }

        #endregion Elements

        #region Utils

        /// <summary>
        /// Determine what type of item we're outputting (article or book, monograph or serial)
        /// </summary>
        /// <returns></returns>
        private ItemType GetItemType()
        {
            if (String.IsNullOrEmpty(_oaiRecord.MarcLeader)) return ItemType.Monograph;
            if (_oaiRecord.MarcLeader.Length < 8) return ItemType.Monograph;

            ItemType type = ItemType.Monograph;
            switch (_oaiRecord.MarcLeader.Substring(7, 1))
            {
                case "a":
                    type = ItemType.MultivolumeMonograph;
                    break;
                case "m":
                    type = ItemType.Monograph;
                    break;
                case "b":
                case "s":
                    type = ItemType.Serial;
                    break;
                case "c":
                case "d":
                case "i":
                    type = ItemType.Monograph;
                    break;
            }

            return type;
        }

        private enum ItemType
        {
            Article,
            Monograph,
            MultivolumeMonograph,
            Serial
        }

        #endregion Utils
    }
}
