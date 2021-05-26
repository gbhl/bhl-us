using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
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

        public Convert(string modsRecord)
        {
            _oaiRecord = new OAIRecord();

            // Parse the supplied MODS and store the values in _oaiRecord
            XDocument xml = XDocument.Load(new MemoryStream(Encoding.UTF8.GetBytes(modsRecord)));
            XElement root = xml.Root;
            XNamespace ns = root.Name.Namespace;

            // Title
            XElement nonSort = null;
            XElement title = null;
            XElement subTitle = null;

            var titleInfoList = from t in root.Elements(ns + "titleInfo") select t;
            foreach (XElement titleInfo in titleInfoList)
            {
                XAttribute typeAttribute = titleInfo.Attribute("type");

                string titleString = string.Empty;
                nonSort = titleInfo.Element(ns + "nonSort");
                title = titleInfo.Element(ns + "title");
                subTitle = titleInfo.Element(ns + "subTitle");

                if (nonSort != null) titleString = nonSort.Value + " ";
                if (title != null) titleString += title.Value + " ";
                if (subTitle != null) titleString += subTitle.Value + " ";
                titleString = titleString.Trim();

                if (typeAttribute == null)
                {
                    _oaiRecord.Title = titleString;
                }
                else
                {
                    OAIRecord relatedTitle = new OAIRecord();
                    relatedTitle.Title = titleString;
                    _oaiRecord.RelatedTitles.Add(new KeyValuePair<string, OAIRecord>(typeAttribute.Value, relatedTitle));
                }
            }

            // Genre/Type
            XElement genre = root.Element(ns + "genre");
            if (genre != null)
            {
                switch (genre.Value.ToLower())
                {
                    case "book":
                    case "monograph":
                    case "multivolume monograph":
                    case "journal":
                    case "series":
                        _oaiRecord.Type = OAIRecord.RecordType.BookJournal;
                        break;
                    case "issue":
                        _oaiRecord.Type = OAIRecord.RecordType.Issue;
                        break;
                    case "article":
                    case "research article":
                    case "review article":
                    case "short communication":
                    case "short communications":
                    case "data paper":
                    case "letter to the editor":
                    case "letters to the editor":
                    case "checklist":
                    case "editorial":
                    case "editorials":
                    case "book review":
                    case "catalogue":
                    case "catalogues":
                    case "in memoriam":
                    case "forum paper":
                    case "commentary":
                    case "bibliography":
                    case "correspondence":
                    case "discussion paper":
                    case "project description":
                    case "editorial pages":
                    case "corrigenda":
                        _oaiRecord.Type = OAIRecord.RecordType.Segment;
                        break;
                    default:
                        _oaiRecord.Type = OAIRecord.RecordType.Unknown;
                        break;
                }
            }
            else
            {
                _oaiRecord.Type = OAIRecord.RecordType.Unknown;
            }

            // Subjects
            var subjects = from s in root.Elements(ns + "subject") select s;
            List<string> subjectStrings = new List<string>();
            foreach (XElement s in subjects)
            {
                foreach (XElement e in s.Elements())
                {
                    // If this is a subject string of the form "subject1 -- subject2 -- subject 3",
                    // then split this into separate subjects (subject1, subject2, and subject3).
                    // Only keep unique subject terms.
                    if (e.Value.IndexOf("--") >= 0)
                    {
                        string[] subjectList = e.Value.Replace("--", "~").Split('~');
                        foreach (string subject in subjectList)
                        {
                            if (!subjectStrings.Contains(subject.Trim())) subjectStrings.Add(subject.Trim());
                        }
                    }
                    else
                    {
                        if (!subjectStrings.Contains(e.Value)) subjectStrings.Add(e.Value);
                    }
                }
            }
            // Save all of the accumulated subjects to the OAIRecord
            foreach(string subjectString in subjectStrings.Distinct().ToArray())
            {
                // Strip off trailing periods
                string cleanSubject = subjectString;
                if (cleanSubject.Substring(cleanSubject.Length - 1) == ".") cleanSubject = cleanSubject.Substring(0, cleanSubject.Length - 1);
                _oaiRecord.Subjects.Add(new KeyValuePair<string, string>(cleanSubject, cleanSubject));
            }

            // Authors
            var authors = from a in root.Elements(ns + "name") select a;
            foreach (XElement author in authors)
            {
                OAIRecord.Creator creator = new OAIRecord.Creator();

                string authorType = "personal";
                XAttribute typeAttribute = author.Attribute("type");
                if (typeAttribute != null) authorType = typeAttribute.Value;

                var nameList = from n in author.Elements(ns + "namePart") select n;

                string familyName = string.Empty;
                string givenName = string.Empty;
                foreach (XElement nameInfo in nameList)
                {
                    XAttribute type = nameInfo.Attribute("type");
                    if (type == null)
                    {
                        creator.FullName = nameInfo.Value;
                    }
                    else
                    {
                        switch (type.Value)
                        {
                            case "date":
                                creator.Dates = nameInfo.Value;
                                break;
                            case "family":
                                familyName = nameInfo.Value;
                                break;
                            case "given":
                                givenName = nameInfo.Value;
                                break;
                            case "termsOfAddress":
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(familyName) || !string.IsNullOrWhiteSpace(givenName))
                {
                    creator.FullName = string.Format("{0}, {1}", familyName, givenName);
                }

                _oaiRecord.Creators.Add(new KeyValuePair<string, OAIRecord.Creator>(authorType, creator));
            }

            // Language
            XElement language = root.Element(ns + "language");
            if (language != null)
            {
                XElement languageTerm = language.Element(ns + "languageTerm");
                if (languageTerm != null) _oaiRecord.Languages.Add(languageTerm.Value);
            }

            // Abstract
            XElement itemAbstract = root.Element(ns + "abstract");
            if (itemAbstract != null) _oaiRecord.Abstract = itemAbstract.Value;

            // Notes
            var notesList = from p in root.Elements(ns + "note") select p;
            foreach (XElement note in notesList)
            {
                string noteType = string.Empty;
                XAttribute noteTypeAttribute = note.Attribute("type");
                if (noteTypeAttribute != null) noteType = noteTypeAttribute.Value;
                _oaiRecord.Notes.Add(new KeyValuePair<string, string>(noteType, note.Value));
            }

            // Publisher info
            var publisherInfoList = from p in root.Elements(ns + "originInfo") select p;
            foreach (XElement publisherInfo in publisherInfoList)
            {
                if (publisherInfo.Element(ns + "dateIssued") != null)
                    _oaiRecord.PublicationDates = publisherInfo.Element(ns + "dateIssued").Value;
                if (publisherInfo.Element(ns + "publisher") != null)
                    _oaiRecord.Publisher = publisherInfo.Element(ns + "publisher").Value;
                if (publisherInfo.Element(ns + "edition") != null)
                    _oaiRecord.Edition = publisherInfo.Element(ns + "edition").Value;
                if (publisherInfo.Element(ns + "place") != null)
                {
                    XElement place = publisherInfo.Element(ns + "place");
                    if (place.Element(ns + "placeTerm") != null)
                        _oaiRecord.PublicationPlace = place.Element(ns + "placeTerm").Value;
                }
            }

            // Call number
            XAttribute authorityAttribute = null;
            XElement callNumber = root.Element(ns + "classification");
            if (callNumber != null) authorityAttribute = callNumber.Attribute("authority");
            if (authorityAttribute != null)
            {
                if (authorityAttribute.Value == "lcc") _oaiRecord.CallNumber = callNumber.Value;
            }

            // Copyright info
            var accessConditionList = from a in root.Elements(ns + "accessCondition") select a;
            foreach (XElement accessCondition in accessConditionList) _oaiRecord.Rights.Add(accessCondition.Value);

            // Related items (article containers and associated titles)
            var relatedItems = from r in root.Elements(ns + "relatedItem") 
                               where r.Attribute("type") != null
                               select r;

            foreach(XElement relatedItem in relatedItems)
            {
                if (_oaiRecord.Type == OAIRecord.RecordType.Segment)
                {
                    if (relatedItem.Attribute("type").Value == "host")
                    {
                        // Article container info (title, volume, pages, issue)
                        XElement containerTitle = null;
                        XElement containerTitleInfo = relatedItem.Element(ns + "titleInfo");
                        if (containerTitleInfo != null) containerTitle = containerTitleInfo.Element(ns + "title");
                        if (containerTitle != null) _oaiRecord.JournalTitle = containerTitle.Value;

                        XElement part = relatedItem.Element(ns + "part");
                        if (part != null)
                        {
                            var volume = from v in part.Elements(ns + "detail") where v.Attribute("type").Value == "volume" select v;
                            if (volume.Count() > 0) _oaiRecord.JournalVolume = volume.First().Value;

                            var issue = from i in part.Elements(ns + "detail") where i.Attribute("type").Value == "issue" select i;
                            if (issue.Count() > 0) _oaiRecord.JournalIssue = issue.First().Value;

                            var pages = from p in part.Elements(ns + "extent") where p.Attribute("unit").Value == "pages" select p;
                            if (pages.Count() > 0)
                            {
                                XElement start = pages.First().Element(ns + "start");
                                XElement end = pages.First().Element(ns + "end");
                                if (start != null) _oaiRecord.ArticleStartPage = start.Value;
                                if (end != null) _oaiRecord.ArticleEndPage = end.Value;
                            }
                        }
                    }
                }
                else
                {
                    // Associated titles
                    OAIRecord relatedTitle = new OAIRecord();

                    XElement relatedItemTitle = null;
                    XElement relatedItemInfo = relatedItem.Element(ns + "titleInfo");
                    if (relatedItemInfo != null) relatedItemTitle = relatedItemInfo.Element(ns + "title");
                    if (relatedItemTitle != null)
                    {
                        relatedTitle.Title = relatedItemTitle.Value;
                        _oaiRecord.RelatedTitles.Add(new KeyValuePair<string, OAIRecord>(relatedItem.Attribute("type").Value, relatedTitle));
                    }
                }
            }

            // Identifiers
            var doi = from d in root.Elements(ns + "identifier")
                        where d.Attribute("type").Value == "doi"
                        select d;
            if (doi.Count() > 0) _oaiRecord.Doi = doi.First().Value;

            var isbn = from d in root.Elements(ns + "identifier")
                        where d.Attribute("type").Value == "isbn"
                        select d;
            if (isbn.Count() > 0) _oaiRecord.Isbn = isbn.First().Value;

            var issn = from d in root.Elements(ns + "identifier")
                        where d.Attribute("type").Value == "issn"
                        select d;
            if (issn.Count() > 0) _oaiRecord.Issn = issn.First().Value;

            var lccn = from d in root.Elements(ns + "identifier")
                        where d.Attribute("type").Value == "lccn"
                        select d;
            if (lccn.Count() > 0) _oaiRecord.Llc = lccn.First().Value;

            var uris = from d in root.Elements(ns + "identifier")
                        where d.Attribute("type").Value == "uri"
                        select d;
            foreach (XElement uri in uris)
            {
                // See if a DOI is embedded within the URI
                string uriValue = uri.Value;
                if (!string.IsNullOrWhiteSpace(_oaiRecord.Doi)) _oaiRecord.Url = uriValue;  // Already have a DOI
                else
                {
                    if (uriValue.Contains("http://dx.doi.org/")) _oaiRecord.Doi = uriValue.Replace("http://dx.doi.org/", "");
                    else if (uriValue.Contains("https://dx.doi.org/")) _oaiRecord.Doi = uriValue.Replace("https://dx.doi.org/", "");
                    else if (uriValue.Contains("http://doi.org/")) _oaiRecord.Doi = uriValue.Replace("http://doi.org/", "");
                    else if (uriValue.Contains("https://doi.org/")) _oaiRecord.Doi = uriValue.Replace("https://doi.org/", "");
                    else _oaiRecord.Url = uriValue;
                }
            }

            // Volume and Url
            XElement location = root.Element(ns + "location");
            if (location != null)
            {
                // Book/journal/issue volume
                if (_oaiRecord.Type == OAIRecord.RecordType.BookJournal || _oaiRecord.Type == OAIRecord.RecordType.Issue)
                {
                    XElement copyInformation = null;
                    XElement enumerationAndChronology = null;
                    XElement holdingSimple = location.Element(ns + "holdingSimple");
                    if (holdingSimple != null) copyInformation = holdingSimple.Element(ns + "copyInformation");
                    if (copyInformation != null) enumerationAndChronology = copyInformation.Element(ns + "enumerationAndChronology");
                    if (enumerationAndChronology != null) _oaiRecord.JournalVolume = enumerationAndChronology.Value;
                }

                // Url (this overrides any Uri values we previously identified)
                XElement url = location.Element(ns + "url");
                if (url != null) _oaiRecord.Url = url.Value;
            }

            if (_oaiRecord.Type == OAIRecord.RecordType.BookJournal || _oaiRecord.Type == OAIRecord.RecordType.Issue)
            {
                // Book/journal/issue volume ("part" is the preferred location for volume info, so this overrides values read from other locations)
                XElement rootPart = root.Element(ns + "part");
                if (rootPart != null)
                {
                    XElement partDetail = rootPart.Element(ns + "detail");
                    if (partDetail != null)
                    {
                        XAttribute partType = partDetail.Attribute("type");
                        if (partType != null)
                        {
                            if (partType.Value == "volume")
                            {
                                XElement partNumber = partDetail.Element(ns + "number");
                                if (partNumber != null) _oaiRecord.JournalVolume = partNumber.Value;
                            }
                        }
                    }
                }
            }
        }

        #region ToOAIRecord

        public OAIRecord ToOAIRecord()
        {
            return _oaiRecord;
        }

        #endregion ToOAIRecord

        #region ToString

        public new String ToString()
        {
            StringBuilder sb = new StringBuilder();

            string mods30Header = "<mods xmlns:xlink=\"http://www.w3.org/1999/xlink\" version=\"3.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.loc.gov/mods/v3\" xsi:schemaLocation=\"http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-0.xsd\">\n";
            string mods33Header = "<mods xmlns:xlink=\"http://www.w3.org/1999/xlink\" version=\"3.3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.loc.gov/mods/v3\" xsi:schemaLocation=\"http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-3.xsd\">\n";
            string mods37Header = "<mods xmlns:xlink=\"http://www.w3.org/1999/xlink\" version=\"3.7\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.loc.gov/mods/v3\" xsi:schemaLocation=\"http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-7.xsd\">\n";

            // MODS needs to be formatted differently for articles, issues, and books, so check the type
            // of record we have and get the appropriate MODS body
            switch (_oaiRecord.Type)
            {
                case OAIRecord.RecordType.Article:
                case OAIRecord.RecordType.Segment:
                    sb.Append(mods37Header);
                    sb.Append(this.ArticleToString());
                    break;
                case OAIRecord.RecordType.Issue:
                    sb.Append(mods37Header);
                    sb.Append(this.IssueToString());
                    break;
                default:
                    sb.Append(mods37Header);
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

            // Physical Description
            sb.Append(this.GetPhysicalDescriptionElement());

            // Language
            sb.Append(this.GetLanguageElement());

            // Note
            sb.Append(this.GetNoteElement());

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

            // Note
            sb.Append(this.GetNoteElement());

            // Publisher
            sb.Append(this.GetOriginInfoElement());

            // Part
            sb.Append(this.GetPartElement());

            // Physical Description
            sb.Append(this.GetPhysicalDescriptionElement());

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
            sb.Append(this.GetLocationElement(_oaiRecord));

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

            // Abstract
            sb.Append(this.GetAbstractElement());

            // Note
            sb.Append(this.GetNoteElement());

            // Subject
            sb.Append(this.GetSubjectElement());

            // RelatedItem (journal information)
            sb.Append(this.GetRelatedItemHostElement());

            // Identifier
            sb.Append(this.GetIdentifierUriElement());
            sb.Append(this.GetIdentifierElement(_oaiRecord));

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

                String creatorContributorRole = String.Empty;
                switch (creatorData.Key)
                {
                    case "100":
                    case "110":
                    case "111":
                        creatorContributorRole = "\t<role>\t\t<roleTerm type=\"text\">creator</roleTerm>\n</role>\n";
                        break;
                    case "700":
                    case "710":
                    case "711":
                    case "720":
                        creatorContributorRole = "\t<role>\t\t<roleTerm type=\"text\">contributor</roleTerm>\n</role>\n";
                        break;
                    default:
                        break;
                }

                string name = (creatorData.Value.Name + ' ' + creatorData.Value.Numeration + ' ' + 
                    creatorData.Value.Location + ' ' + creatorData.Value.FullerForm).Trim();
                name = String.IsNullOrEmpty(name) ? creatorData.Value.FullName : name;
                string date = creatorData.Value.Dates;
                string titleOfWork = creatorData.Value.TitleOfWork;
                string role = creatorData.Value.Relationship;
                sb.Append("<name" + type + ">\n");  // Add "type" attribute here
                sb.Append("\t<namePart>" + HttpUtility.HtmlEncode(name) + "</namePart>\n");
                if (date != string.Empty) sb.Append("\t<namePart type=\"date\">" + HttpUtility.HtmlEncode(date) + "</namePart>\n");
                if (titleOfWork != string.Empty) sb.Append("\t<affiliation>" + HttpUtility.HtmlEncode(titleOfWork) + "</affiliation>\n");
                if (role != string.Empty) sb.Append("\t<role>\t\t<roleTerm type=\"text\">" + HttpUtility.HtmlEncode(role) + "</roleTerm>\n</role>\n");
                sb.Append(creatorContributorRole);
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
            string typeOfResource = string.Empty;
            string manuscript = string.Empty;
            GetTypeOfResource(out typeOfResource, out manuscript);

            StringBuilder sb = new StringBuilder();
            sb.Append("<typeOfResource");
            if (!string.IsNullOrWhiteSpace(manuscript)) sb.Append(" manuscript=\"" + manuscript + "\"");
            sb.Append(">");
            sb.Append(typeOfResource);
            sb.Append("</typeOfResource>\n");

            return sb.ToString();
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
                        string keyDateAttrib = string.Empty;
                        if (String.IsNullOrEmpty(_oaiRecord.Date) || _oaiRecord.Type != OAIRecord.RecordType.Issue) keyDateAttrib = " keyDate=\"yes\"";
                        sb.Append("\t<dateIssued encoding=\"marc\" point=\"start\"" + keyDateAttrib + ">" + HttpUtility.HtmlEncode(_oaiRecord.PublicationStartYear) + "</dateIssued>\n");
                    }
                    if (!String.IsNullOrEmpty(_oaiRecord.PublicationEndYear))
                    {
                        sb.Append("\t<dateIssued encoding=\"marc\" point=\"end\">" + HttpUtility.HtmlEncode(_oaiRecord.PublicationEndYear) + "</dateIssued>\n");
                    }
                }

                if (!String.IsNullOrEmpty(_oaiRecord.Date) && _oaiRecord.Type == OAIRecord.RecordType.Issue)
                {
                    sb.Append("\t<dateOther type=\"issueDate\" keyDate=\"yes\">" + HttpUtility.HtmlEncode(_oaiRecord.Date) + "</dateOther>\n");
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
        /// Build the part element
        /// </summary>
        /// <returns></returns>
        private string GetPartElement()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(_oaiRecord.JournalVolume) ||
                !string.IsNullOrWhiteSpace(_oaiRecord.JournalIssue) ||
                !string.IsNullOrWhiteSpace(_oaiRecord.ArticleStartPage) ||
                !string.IsNullOrWhiteSpace(_oaiRecord.ArticleEndPage) ||
                !string.IsNullOrWhiteSpace(_oaiRecord.Date))
            {

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
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the physical description element
        /// </summary>
        /// <returns></returns>
        private string GetPhysicalDescriptionElement()
        {
            return "<physicalDescription>\n\t<form authority=\"marcform\">print</form>\n</physicalDescription>";
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
        /// Build the abstract element
        /// </summary>
        /// <returns></returns>
        private String GetAbstractElement()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(_oaiRecord.Abstract))
            {
                sb.Append("<abstract>" + HttpUtility.HtmlEncode(_oaiRecord.Abstract) + "</abstract>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the note element
        /// </summary>
        /// <returns></returns>
        private String GetNoteElement()
        {
            StringBuilder sb = new StringBuilder();

            foreach(KeyValuePair<string, string> note in _oaiRecord.Notes)
            {
                string noteTypeAttrib = string.IsNullOrWhiteSpace(note.Key) ? string.Empty : " type=\"" + HttpUtility.HtmlEncode(note.Key) + "\"";
                sb.Append("<note" + noteTypeAttrib + ">" + HttpUtility.HtmlEncode(note.Value) + "</note>\n");
            }

            if (_oaiRecord.Type == OAIRecord.RecordType.Issue)
            {
                foreach (string contributor in _oaiRecord.Contributors)
                {
                    sb.Append("<note type=\"ownership\">" + HttpUtility.HtmlEncode(contributor) + "</note>\n");
                }
            }

            if (!string.IsNullOrWhiteSpace(_oaiRecord.JournalVolume))
            {
                sb.Append("<note type=\"content\">" + HttpUtility.HtmlEncode(_oaiRecord.JournalVolume) + "</note>\n");
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

                if (marcCode == "z" || (marcTag == "651" && marcCode == "a"))
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
            sb.Append(this.GetPartElement());

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
                    case "preceded by":
                        typeAttrib = " type=\"preceding\"";
                        break;
                    case "succeeded by":
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
                sb.Append(this.GetLocationElement(relatedTitle.Value));
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
            if (!String.IsNullOrWhiteSpace(title.Doi))
            {
                sb.Append("<identifier type=\"doi\">" + HttpUtility.HtmlEncode(title.Doi) + "</identifier>\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Build the location element
        /// </summary>
        /// <returns></returns>
        private String GetLocationElement(OAIRecord oaiRecord)
        {
            StringBuilder sb = new StringBuilder();

            //if (!String.IsNullOrEmpty(_oaiRecord.Contributor) || !String.IsNullOrEmpty(_oaiRecord.JournalVolume))
            //{
            //    sb.Append("<location>\n");

                foreach(string contributor in oaiRecord.Contributors)
                {
                    sb.Append("\t<physicalLocation>" + HttpUtility.HtmlEncode(contributor) + "</physicalLocation>\n");
                }

                if (!string.IsNullOrWhiteSpace(oaiRecord.Url)) sb.Append("\t<url access=\"raw object\" usage=\"primary\">" + HttpUtility.HtmlEncode(oaiRecord.Url) + "</url>\n");
                if (!string.IsNullOrWhiteSpace(oaiRecord.ThumbnailUrl)) sb.Append("\t<url access=\"object in context\" usage=\"primary display\">" + HttpUtility.HtmlEncode(oaiRecord.ThumbnailUrl) + "</url>\n");

                if (!String.IsNullOrEmpty(oaiRecord.JournalVolume))
                {
                    sb.Append("\t<holdingSimple>\n");
                    sb.Append("\t\t<copyInformation>\n");
                    sb.Append("\t\t\t<enumerationAndChronology>" + HttpUtility.HtmlEncode(oaiRecord.JournalVolume) + "</enumerationAndChronology>\n");
                    sb.Append("\t\t</copyInformation>\n");
                    sb.Append("\t</holdingSimple>\n");
                }

                if (sb.Length > 0)
                {
                    sb.Insert(0, "<location>\n");
                    sb.Append("</location>\n");
                }

            //    sb.Append("</location>\n");
            //}

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

        private void GetTypeOfResource(out string typeOfResource, out string manuscript)
        {
            typeOfResource = "text";
            manuscript = "";
            switch (_oaiRecord.MaterialCode)
            {
                case "a": typeOfResource = "text"; break;
                case "c": typeOfResource = "notated music"; break;
                case "d": typeOfResource = "notated music"; manuscript = "yes"; break;
                case "e": typeOfResource = "cartographic"; break;
                case "f": typeOfResource = "cartographic"; manuscript = "yes"; break;
                case "g": typeOfResource = "moving image"; break;
                case "i": typeOfResource = "sound recording-nonmusical"; break;
                case "j": typeOfResource = "sound recording-musical"; break;
                case "k": typeOfResource = "still image"; break;
                case "m": typeOfResource = "software, multimedia"; break;
                case "p": typeOfResource = "mixed material"; break;
                case "r": typeOfResource = "three dimensional object"; break;
                case "t": typeOfResource = "text"; manuscript = "yes"; break;
                default: typeOfResource = "text"; break;
            }
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
