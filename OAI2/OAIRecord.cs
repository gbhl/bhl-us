using System;
using System.Collections.Generic;
using System.Text;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Server;
using CustomDataAccess;

namespace MOBOT.BHL.OAI2
{
    public class OAIRecord
    {
        bool _includeExtraDetail = false;

        #region Constructors

        public OAIRecord()
        {
        }

        public OAIRecord(bool includeExtraDetail)
        {
            _includeExtraDetail = includeExtraDetail;
        }

        public OAIRecord(String identifier)
        {
            this.Load(identifier);
        }

        public OAIRecord(String identifier, bool includeExtraDetail)
        {
            _includeExtraDetail = includeExtraDetail;
            this.Load(identifier);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Populate on loading error
        /// </summary>
        String _errorMessage = String.Empty;

        public String ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        String _oaiStatus = String.Empty;

        public String OaiStatus
        {
            get { return _oaiStatus; }
            set { _oaiStatus = value; }
        }

        String _oaiIdentifier = String.Empty;

        public String OaiIdentifier
        {
            get { return _oaiIdentifier; }
            set { _oaiIdentifier = value; }
        }

        String _oaiDateStamp = String.Empty;

        public String OaiDateStamp
        {
            get { return _oaiDateStamp; }
            set { _oaiDateStamp = value; }
        }

        private RecordType _type = RecordType.BookJournal;

        public RecordType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        String _marcLeader = String.Empty;

        public String MarcLeader
        {
            get { return _marcLeader; }
            set { _marcLeader = value; }
        }

        String _title = String.Empty;

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        String _partNumber = String.Empty;

        public String PartNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; }
        }

        String _partName = String.Empty;

        public String PartName
        {
            get { return _partName; }
            set { _partName = value; }
        }

        String _contributor = String.Empty;

        public String Contributor
        {
            get { return _contributor; }
            set { _contributor = value; }
        }

        String _date = String.Empty;

        public String Date
        {
            get { return _date; }
            set { _date = value; }
        }

        String _callNumber = String.Empty;

        public String CallNumber
        {
            get { return _callNumber; }
            set { _callNumber = value; }
        }

        String _abstract = String.Empty;

        public String Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }

        List<KeyValuePair<String, String>> _notes = new List<KeyValuePair<String, String>>();

        public List<KeyValuePair<String, String>> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        List<KeyValuePair<String, OAIRecord.Creator>> _creators = new List<KeyValuePair<string, OAIRecord.Creator>>();

        public List<KeyValuePair<String, OAIRecord.Creator>> Creators
        {
            get { return _creators; }
            set { _creators = value; }
        }

        List<KeyValuePair<String, String>> _subjects = new List<KeyValuePair<string, string>>();

        public List<KeyValuePair<string, string>> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        List<KeyValuePair<String, OAIRecord.TitleVariant>> _titleVariants = new List<KeyValuePair<string, OAIRecord.TitleVariant>>();

        public List<KeyValuePair<String, OAIRecord.TitleVariant>> TitleVariants
        {
            get { return _titleVariants; }
            set { _titleVariants = value; }
        }

        List<KeyValuePair<string, OAIRecord.Page>> _pages = new List<KeyValuePair<string, Page>>();

        public List<KeyValuePair<string, OAIRecord.Page>> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        List<String> _languages = new List<string>();

        public List<String> Languages
        {
            get { return _languages; }
            set { _languages = value; }
        }

        String _publicationDetails = String.Empty;

        public String PublicationDetails
        {
            get { return _publicationDetails; }
            set { _publicationDetails = value; }
        }

        String _publisher = String.Empty;

        public String Publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        String _publicationPlace = String.Empty;

        public String PublicationPlace
        {
            get { return _publicationPlace; }
            set { _publicationPlace = value; }
        }

        String _publicationDates = String.Empty;

        public String PublicationDates
        {
            get { return _publicationDates; }
            set { _publicationDates = value; }
        }

        String _publicationStartYear = String.Empty;

        public String PublicationStartYear
        {
            get { return _publicationStartYear; }
            set { _publicationStartYear = value; }
        }

        String _publicationEndYear = String.Empty;

        public String PublicationEndYear
        {
            get { return _publicationEndYear; }
            set { _publicationEndYear = value; }
        }

        String _edition = String.Empty;

        public String Edition
        {
            get { return _edition; }
            set { _edition = value; }
        }

        String _originalCatalogingSource = String.Empty;

        public String OriginalCatalogingSource
        {
            get { return _originalCatalogingSource; }
            set { _originalCatalogingSource = value; }
        }

        String _publicationFrequency = string.Empty;

        public String PublicationFrequency
        {
            get { return _publicationFrequency; }
            set { _publicationFrequency = value; }
        }

        List<String> _identifiers = new List<string>();

        String _url = String.Empty;

        public String Url
        {
            get { return _url; }
            set { _url = value; }
        }

        String _parentUrl = String.Empty;

        public String ParentUrl
        {
            get { return _parentUrl; }
            set { _parentUrl = value; }
        }

        String _thumbnailUrl = String.Empty;

        public String ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { _thumbnailUrl = value; }
        }

        String _sequence = String.Empty;

        public String Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        List<String> _oclcNumbers = new List<string>();

        public List<String> oclcNumbers
        {
            get { return _oclcNumbers; }
            set { _oclcNumbers = value; }
        }

        String _issn = String.Empty;

        public String Issn
        {
            get { return _issn; }
            set { _issn = value; }
        }

        String _isbn = String.Empty;

        public String Isbn
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        String _llc = String.Empty;

        public String Llc
        {
            get { return _llc; }
            set { _llc = value; }
        }

        String _ddc = String.Empty;

        public String Ddc
        {
            get { return _ddc; }
            set { _ddc = value; }
        }

        String _nlm = String.Empty;

        public String Nlm
        {
            get { return _nlm; }
            set { _nlm = value; }
        }

        String _doi = String.Empty;

        public String Doi
        {
            get { return _doi; }
            set { _doi = value; }
        }

        List<String> _formats = new List<string>();

        public List<String> Formats
        {
            get { return _formats; }
            set { _formats = value; }
        }

        List<String> _types = new List<string>();

        public List<String> Types
        {
            get { return _types; }
            set { _types = value; }
        }

        List<String> _descriptions = new List<string>();

        public List<String> Descriptions
        {
            get { return _descriptions; }
            set { _descriptions = value; }
        }

        List<String> _rights = new List<string>();

        public List<String> Rights
        {
            get { return _rights; }
            set { _rights = value; }
        }

        String _numberOfPages = String.Empty;

        public String NumberOfPages
        {
            get { return _numberOfPages; }
            set { _numberOfPages = value; }
        }

        String _source = String.Empty;

        public String Source
        {
            get { return _source; }
            set { _source = value; }
        }

        List<KeyValuePair<String, OAIRecord>> _relatedTitles = new List<KeyValuePair<string, OAIRecord>>();

        public List<KeyValuePair<String, OAIRecord>> RelatedTitles
        {
            get { return _relatedTitles; }
            set { _relatedTitles = value; }
        }

        private String _journalTitle = String.Empty;

        public String JournalTitle
        {
            get { return _journalTitle; }
            set { _journalTitle = value; }
        }

        private String _journalVolume = String.Empty;

        public String JournalVolume
        {
            get { return _journalVolume; }
            set { _journalVolume = value; }
        }

        private String _journalIssue = String.Empty;

        public String JournalIssue
        {
            get { return _journalIssue; }
            set { _journalIssue = value; }
        }

        private String _articleStartPage = String.Empty;

        public String ArticleStartPage
        {
            get { return _articleStartPage; }
            set { _articleStartPage = value; }
        }

        private String _articleEndPage = String.Empty;

        public String ArticleEndPage
        {
            get { return _articleEndPage; }
            set { _articleEndPage = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the metadata for the specified identifier.  It is assumed that the 
        /// identifier has already been validated.
        /// </summary>
        /// <param name="identifier"></param>
        public bool Load(String identifier)
        {
            bool isLoaded = false;
            String scheme, namespaceIdentifier, localIdentifier;
            int intID = int.MinValue;

            try
            {
                // Get the BHL item identifier for which to search
                bool ret = OAI2Util.ParseOAIIdentifier(identifier, out scheme, out namespaceIdentifier, out localIdentifier);

                // "localIdentifier" should be in the form prefix/id (i.e. item/1000)
                String[] idSplit = localIdentifier.Split('/');
                intID = Convert.ToInt32(idSplit[1]);

                // Look up title, item, or article, as appropriate
                switch (idSplit[0])
                {
                    case OAI2Util.IDPrefix.ITEM:
                        this.Type = RecordType.Issue;
                        LoadItem(intID);
                        break;
                    case OAI2Util.IDPrefix.TITLE:
                        this.Type = RecordType.BookJournal;
                        LoadTitle(intID);
                        break;
                    case OAI2Util.IDPrefix.ARTICLE:
                        this.Type = RecordType.Article;
                        LoadPdf(intID);
                        break;
                    case OAI2Util.IDPrefix.SEGMENT:
                        this.Type = RecordType.Segment;
                        LoadSegment(intID);
                        break;
                }

                isLoaded = true;
            }
            catch (Exception ex)
            {
                this._errorMessage = ex.Message;
            }

            return isLoaded;
        }

        /// <summary>
        /// Load data for the item with the specified identifier
        /// </summary>
        /// <param name="identifier"></param>
        private void LoadItem(int identifier)
        {
            BHLProvider provider = new BHLProvider();
            Item item = provider.ItemSelectAuto(identifier);

            if (item != null)
            {
                this.Descriptions.Add(item.Volume);
                this.JournalVolume = item.Volume;
                this.Date = item.Year;

                if (!String.IsNullOrEmpty(item.InstitutionCode))
                {
                    this.Contributor = provider.InstitutionSelectAuto(item.InstitutionCode).InstitutionName;
                }

                if (!String.IsNullOrEmpty(item.CopyrightStatus)) this.Rights.Add(item.CopyrightStatus);
                //if (!String.IsNullOrEmpty(item.Rights)) this.Rights.Add(item.Rights);
                //if (!String.IsNullOrEmpty(item.LicenseUrl)) this.Rights.Add(item.LicenseUrl);

                CustomGenericList<ItemLanguage> itemLanguages = provider.ItemLanguageSelectByItemID(item.ItemID);
                if (itemLanguages.Count > 0)
                {
                    foreach (ItemLanguage itemLanguage in itemLanguages)
                    {
                        this.Languages.Add(itemLanguage.LanguageName);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.LanguageCode))
                    {
                        Language lang = provider.LanguageSelectAuto(item.LanguageCode);
                        if (lang != null) this.Languages.Add(lang.LanguageName);
                    }
                }

                this.Sequence = "1";
                CustomGenericList<TitleItem> titleItems = provider.TitleItemSelectByItem(item.ItemID);
                foreach (TitleItem titleItem in titleItems)
                {
                    if (titleItem.TitleID == item.PrimaryTitleID) this.Sequence = titleItem.ItemSequence.ToString();
                }

                this.Url = "http://www.biodiversitylibrary.org/item/" + item.ItemID.ToString();
                this.ParentUrl = "http://www.biodiversitylibrary.org/bibliography/" + item.PrimaryTitleID.ToString();
                if (item.ThumbnailPageID != null) this.ThumbnailUrl = "http://www.biodiversitylibrary.org/pagethumb/" + item.ThumbnailPageID.ToString();

                this.Types.Add("text");

                if (_includeExtraDetail)
                {
                    CustomGenericList<CustomDataRow> pages = provider.NameMetadataSelectByItemID(item.ItemID);

                    OAIRecord.Page oaiPage = null;
                    int currentPageID = 0;
                    foreach (CustomDataRow page in pages)
                    {
                        if (currentPageID != (int)page["PageID"].Value)
                        {
                            if (oaiPage != null) this.Pages.Add(new KeyValuePair<string, OAIRecord.Page>(currentPageID.ToString(), oaiPage));
                            currentPageID = (int)page["PageID"].Value;

                            oaiPage = new OAIRecord.Page();
                            oaiPage.Url = "http://www.biodiversitylibrary.org/page/" + currentPageID.ToString();
                            oaiPage.ImageUrl = "http://www.biodiversitylibrary.org/pageimage/" + currentPageID.ToString();
                            oaiPage.Sequence = (int)page["SequenceOrder"].Value;
                            oaiPage.PageType = page["PageTypes"].Value.ToString().Split(',')[0];
                            oaiPage.PageLabel = page["IndicatedPages"].Value.ToString();
                        }

                        if (!page["NameResolvedID"].IsDbNull)
                        {
                            OAIRecord.Name oaiName = new OAIRecord.Name();
                            oaiName.ScientificName = page["ResolvedNameString"].Value.ToString();
                            oaiPage.Names.Add(new KeyValuePair<string, OAIRecord.Name>(page["ResolvedNameString"].Value.ToString(), oaiName));
                        }
                    }
                    if (oaiPage != null) this.Pages.Add(new KeyValuePair<string, OAIRecord.Page>(currentPageID.ToString(), oaiPage));
                }

                Title title = provider.TitleSelectAuto(item.PrimaryTitleID);
                if (title != null)
                {
                    this.PublicationDetails = title.PublicationDetails;
                    this.Publisher = title.Datafield_260_b;
                    this.PublicationPlace = title.Datafield_260_a;
                    this.PublicationDates = title.Datafield_260_c;
                    this.PublicationStartYear = (title.StartYear == null) ? String.Empty : title.StartYear.ToString();
                    this.PublicationEndYear = (title.EndYear == null) ? String.Empty : title.EndYear.ToString();
                    this.Edition = (title.EditionStatement == null) ? String.Empty : title.EditionStatement;
                    this.OriginalCatalogingSource = (title.OriginalCatalogingSource == null) ? String.Empty : title.OriginalCatalogingSource;
                    this.PublicationFrequency = (title.CurrentPublicationFrequency == null) ? String.Empty : title.CurrentPublicationFrequency;
                    this.Title = title.FullTitle;
                    this.PartNumber = (title.PartNumber == null) ? String.Empty : title.PartNumber;
                    this.PartName = (title.PartName == null) ? String.Empty : title.PartName;
                    this.CallNumber = (title.CallNumber == null) ? String.Empty : title.CallNumber;
                    this.MarcLeader = title.MARCLeader;
                    //if (String.IsNullOrEmpty(this.Date) && title.StartYear != null) this.Date = title.StartYear.ToString();

                    switch (title.BibliographicLevelID)
                    {
                        case 2: // Serial component part
                        case 5: // Serial
                            this.Types.Add("Journal");
                            break;
                        default:
                            this.Types.Add("Book");
                            break;
                    }

                    CustomGenericList<TitleKeyword> subjects = provider.TitleKeywordSelectByTitleID(item.PrimaryTitleID);
                    foreach (TitleKeyword subject in subjects)
                    {
                        this.Subjects.Add(new KeyValuePair<string, string>(subject.MarcDataFieldTag + "|" + subject.MarcSubFieldCode, subject.Keyword));
                    }

                    CustomGenericList<DataObjects.Author> authors = provider.AuthorSelectByTitleId(item.PrimaryTitleID);
                    foreach (DataObjects.Author author in authors)
                    {
                        OAIRecord.Creator creator = new OAIRecord.Creator(author.FullName, (string.IsNullOrEmpty(author.Numeration) ? author.Unit : author.Numeration), 
                            (string.IsNullOrEmpty(author.Title) ? author.Location : author.Title), author.Dates, author.Relationship, author.FullerForm, 
                            author.TitleOfWork, author.NameExtended);
                        KeyValuePair<string, OAIRecord.Creator> authorData = new KeyValuePair<string, OAIRecord.Creator>(author.MarcDataFieldTag, creator);
                        this.Creators.Add(authorData);
                    }

                    if (!string.IsNullOrEmpty(title.UniformTitle))
                    {
                        this.TitleVariants.Add(new KeyValuePair<string, 
                            OAIRecord.TitleVariant>("uniform", new OAIRecord.TitleVariant(title.UniformTitle, string.Empty, string.Empty)));
                    }
                    CustomGenericList<DataObjects.TitleVariant> variants = provider.TitleVariantSelectByTitleID(item.PrimaryTitleID);
                    foreach (DataObjects.TitleVariant variant in variants)
                    {
                        OAIRecord.TitleVariant newVariant = new TitleVariant((variant.Title + " " + variant.TitleRemainder).Trim(), variant.PartNumber, variant.PartName);
                        this.TitleVariants.Add(new KeyValuePair<string,OAIRecord.TitleVariant>(variant.TitleVariantLabel.ToLower(), newVariant));
                    }

                    CustomGenericList<Title_Identifier> titleIdentifiers = provider.Title_IdentifierSelectForDisplayByTitleID(item.PrimaryTitleID);
                    this.LoadIdentifiers(titleIdentifiers, this);

                    CustomGenericList<DOI> dois = provider.DOISelectValidForTitle(title.TitleID);
                    foreach (DOI doi in dois)
                    {
                        this.SetIdentifier("doi", doi.DOIName, this);
                    }

                    CustomGenericList<TitleAssociation> titleAssociations = provider.TitleAssociationSelectExtendedForTitle(title.TitleID);
                    foreach (TitleAssociation titleAssociation in titleAssociations)
                    {
                        OAIRecord association = new OAIRecord();
                        association.Title = (titleAssociation.Title + " " + titleAssociation.Section + " " + 
                            titleAssociation.Volume + " " + titleAssociation.Heading + " " + titleAssociation.Publication + " " + 
                            titleAssociation.Relationship).Trim();
                        if (titleAssociation.AssociatedTitleID != null)
                        {
                            association.Url = "http://www.biodiversitylibrary.org/bibliography/" + titleAssociation.AssociatedTitleID.ToString();
                        }

                        // Add identifiers associated with the association
                        this.LoadIdentifiers(titleAssociation.TitleAssociationIdentifiers, association);

                        this.RelatedTitles.Add(
                            new KeyValuePair<string, OAIRecord>(
                                titleAssociation.TitleAssociationLabel, association));
                    }
                }
            }
        }

        /// <summary>
        /// Load data for the title with the specified identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        private void LoadTitle(int identifier)
        {
            BHLProvider provider = new BHLProvider();
            Title title = provider.TitleSelectAuto(identifier);

            if (title != null)
            {
                this.Title = title.FullTitle;
                this.PartNumber = (title.PartNumber == null) ? String.Empty : title.PartNumber;
                this.PartName = (title.PartName == null) ? String.Empty : title.PartName;
                this.CallNumber = (title.CallNumber == null) ? String.Empty : title.CallNumber;
                this.MarcLeader = title.MARCLeader;
                this.PublicationDetails = title.PublicationDetails;
                this.Publisher = title.Datafield_260_b;
                this.PublicationPlace = title.Datafield_260_a;
                this.PublicationDates = title.Datafield_260_c;
                this.PublicationStartYear = (title.StartYear == null) ? String.Empty : title.StartYear.ToString();
                this.PublicationEndYear = (title.EndYear == null) ? String.Empty : title.EndYear.ToString();
                this.Edition = (title.EditionStatement == null) ? String.Empty : title.EditionStatement;
                this.OriginalCatalogingSource = (title.OriginalCatalogingSource == null) ? String.Empty : title.OriginalCatalogingSource;
                this.PublicationFrequency = (title.CurrentPublicationFrequency == null) ? String.Empty : title.CurrentPublicationFrequency;
                if (String.IsNullOrEmpty(this.Date) && title.StartYear != null) this.Date = title.StartYear.ToString();
                this.Url = "http://www.biodiversitylibrary.org/bibliography/" + title.TitleID.ToString();
                this.Date = title.StartYear.ToString() + (String.IsNullOrEmpty(title.EndYear.ToString()) ? "" : "-" + title.EndYear.ToString());
                this.Sequence = "1";

                switch (title.BibliographicLevelID)
                {
                    case 2: // Serial component part
                    case 5: // Serial
                        this.Types.Add("Journal");
                        break;
                    default:
                        this.Types.Add("Book");
                        break;
                }
                this.Types.Add("text");

                CustomGenericList<TitleKeyword> subjects = provider.TitleKeywordSelectByTitleID(title.TitleID);
                foreach (TitleKeyword subject in subjects)
                {
                    this.Subjects.Add(new KeyValuePair<string, string>(subject.MarcDataFieldTag + "|" + subject.MarcSubFieldCode, subject.Keyword));
                }

                CustomGenericList<TitleNote> notes = provider.TitleNoteSelectByTitleID(title.TitleID);
                foreach(TitleNote note in notes)
                {
                    this.Notes.Add(new KeyValuePair<string, string>(this.GetNoteType(note.MarcDataFieldTag), note.NoteText));
                }

                CustomGenericList<DataObjects.Author> authors = provider.AuthorSelectByTitleId(title.TitleID);
                foreach (DataObjects.Author author in authors)
                {
                    OAIRecord.Creator creator = new OAIRecord.Creator(author.FullName, (string.IsNullOrEmpty(author.Numeration) ? author.Unit : author.Numeration),
                        (string.IsNullOrEmpty(author.Title) ? author.Location : author.Title), author.Dates, author.Relationship, author.FullerForm, 
                        author.TitleOfWork, author.NameExtended);
                    KeyValuePair<string, OAIRecord.Creator> authorData = new KeyValuePair<string, OAIRecord.Creator>(author.MarcDataFieldTag, creator);
                    this.Creators.Add(authorData);
                }

                CustomGenericList<TitleLanguage> titleLanguages = provider.TitleLanguageSelectByTitleID(title.TitleID);
                if (titleLanguages.Count > 0)
                {
                    foreach (TitleLanguage titleLanguage in titleLanguages)
                    {
                        this.Languages.Add(titleLanguage.LanguageName);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(title.LanguageCode))
                    {
                        Language lang = provider.LanguageSelectAuto(title.LanguageCode);
                        if (lang != null) this.Languages.Add(lang.LanguageName);
                    }
                }

                if (!String.IsNullOrEmpty(title.InstitutionCode))
                {
                    this.Contributor = provider.InstitutionSelectAuto(title.InstitutionCode).InstitutionName;
                }

                if (!string.IsNullOrEmpty(title.UniformTitle))
                {
                    this.TitleVariants.Add(new KeyValuePair<string,
                        OAIRecord.TitleVariant>("uniform", new OAIRecord.TitleVariant(title.UniformTitle, string.Empty, string.Empty)));
                }
                CustomGenericList<DataObjects.TitleVariant> variants = provider.TitleVariantSelectByTitleID(title.TitleID);
                foreach (DataObjects.TitleVariant variant in variants)
                {
                    OAIRecord.TitleVariant newVariant = new TitleVariant((variant.Title + " " + variant.TitleRemainder).Trim(), variant.PartNumber, variant.PartName);
                    this.TitleVariants.Add(new KeyValuePair<string, OAIRecord.TitleVariant>(variant.TitleVariantLabel.ToLower(), newVariant));
                }

                CustomGenericList<Title_Identifier> titleIdentifiers = provider.Title_IdentifierSelectForDisplayByTitleID(title.TitleID);
                this.LoadIdentifiers(titleIdentifiers, this);

                CustomGenericList<DOI> dois = provider.DOISelectValidForTitle(title.TitleID);
                foreach(DOI doi in dois)
                {
                    this.SetIdentifier("doi", doi.DOIName, this);
                }

                CustomGenericList<TitleAssociation> titleAssociations = provider.TitleAssociationSelectExtendedForTitle(title.TitleID);
                foreach (TitleAssociation titleAssociation in titleAssociations)
                {
                    OAIRecord association = new OAIRecord();
                    association.Title = (titleAssociation.Title + " " + titleAssociation.Section + " " +
                        titleAssociation.Volume + " " + titleAssociation.Heading + " " + titleAssociation.Publication + " " +
                        titleAssociation.Relationship).Trim();
                    if (titleAssociation.AssociatedTitleID != null)
                    {
                        association.Url = "http://www.biodiversitylibrary.org/bibliography/" + titleAssociation.AssociatedTitleID.ToString();
                    }

                    // Add identifiers associated with the association
                    this.LoadIdentifiers(titleAssociation.TitleAssociationIdentifiers, association);

                    this.RelatedTitles.Add(
                        new KeyValuePair<string, OAIRecord>(
                            titleAssociation.TitleAssociationLabel, association));
                }
            }
        }

        /// <summary>
        /// Load data for the PDF with the specified identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        private void LoadPdf(int identifier)
        {
            BHLProvider provider = new BHLProvider();

            PDF pdf = provider.PDFSelectAuto(identifier);
            if (pdf != null)
            {
                this.Title = pdf.ArticleTitle;
                this.Url= pdf.FileUrl;
                this.Formats.Add("application/pdf");
                this.Types.Add("Journal Article");
                this.Types.Add("text");

                if (!String.IsNullOrEmpty(pdf.ArticleCreators))
                {
                    String[] authors = pdf.ArticleCreators.Split(',');
                    foreach (String author in authors)
                    {
                        OAIRecord.Creator creator = new OAIRecord.Creator(string.Empty, string.Empty, string.Empty,
                            string.Empty, string.Empty, string.Empty, string.Empty, author);
                        KeyValuePair<string, OAIRecord.Creator> authorData = new KeyValuePair<string, OAIRecord.Creator>(string.Empty, creator);
                        this.Creators.Add(authorData);
                    }
                }

                if (!String.IsNullOrEmpty(pdf.ArticleTags))
                {
                    String[] subjects = pdf.ArticleTags.Split(',');
                    foreach (String subject in subjects)
                    {
                        this.Subjects.Add(new KeyValuePair<string, string>(string.Empty, subject));
                    }
                }

                CustomGenericList<PDFPage> pdfPages = provider.PDFPageSelectForPdfID(pdf.PdfID);
                if (pdfPages != null)
                {
                    if (pdfPages.Count > 0)
                    {
                        this.NumberOfPages = pdfPages.Count.ToString();

                        foreach (PDFPage pdfPage in pdfPages)
                        {
                            // Assume the first non-"title" page is the first page of the article
                            if (pdfPage.PageTypeName != "Title Page")
                            {
                                this.ArticleStartPage = pdfPage.PageNumber;
                                break;
                            }
                        }
                        this.ArticleEndPage = pdfPages[pdfPages.Count - 1].PageNumber;
                    }
                }

                Item item = provider.ItemSelectAuto(pdf.ItemID);
                if (item != null)
                {
                    this.Descriptions.Add(item.Volume);
                    this.JournalVolume = item.Volume;
                    this.Date = item.Year;

                    if (!String.IsNullOrEmpty(item.InstitutionCode))
                    {
                        this.Contributor = provider.InstitutionSelectAuto(item.InstitutionCode).InstitutionName;
                    }

                    if (!String.IsNullOrEmpty(item.CopyrightStatus)) this.Rights.Add(item.CopyrightStatus);

                    Title title = provider.TitleSelectAuto(item.PrimaryTitleID);
                    if (title != null)
                    {
                        this.JournalTitle = title.FullTitle;
                        this.PartNumber = (title.PartNumber == null) ? String.Empty : title.PartNumber;
                        this.PartName = (title.PartName == null) ? String.Empty : title.PartName;
                        this.CallNumber = (title.CallNumber == null) ? String.Empty : title.CallNumber;
                        this.MarcLeader = title.MARCLeader;
                        this.PublicationDetails = title.PublicationDetails;
                        this.Publisher = title.Datafield_260_b;
                        this.PublicationPlace = title.Datafield_260_a;
                        this.PublicationDates = title.Datafield_260_c;
                        this.PublicationStartYear = (title.StartYear == null) ? String.Empty : title.StartYear.ToString();
                        this.PublicationEndYear = (title.EndYear == null) ? String.Empty : title.EndYear.ToString();
                        this.Edition = (title.EditionStatement == null) ? String.Empty : title.EditionStatement;
                        this.OriginalCatalogingSource = (title.OriginalCatalogingSource == null) ? String.Empty : title.OriginalCatalogingSource;
                        this.PublicationFrequency = (title.CurrentPublicationFrequency == null) ? String.Empty : title.CurrentPublicationFrequency;
                        if (String.IsNullOrEmpty(this.Date) && title.StartYear != null) this.Date = title.StartYear.ToString();

                        CustomGenericList<Title_Identifier> titleIdentifiers = provider.Title_IdentifierSelectForDisplayByTitleID(title.TitleID);
                        this.LoadIdentifiers(titleIdentifiers, this);
                    }
                }
            }
        }

        /// <summary>
        /// Load data for the segment with the specified identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        private void LoadSegment(int identifier)
        {
            Segment segment = new BHLProvider().SegmentSelectExtended(identifier);

            if (segment != null)
            {
                switch (segment.GenreName)
                {
                    case OAI2Util.SegmentGenre.ARTICLE:
                    case OAI2Util.SegmentGenre.CHAPTER:
                    case OAI2Util.SegmentGenre.TREATMENT:
                        this.Type = RecordType.Segment;
                        break;
                    case OAI2Util.SegmentGenre.ISSUE:
                        this.Type = RecordType.Issue;
                        break;
                    default:
                        this.Type = RecordType.BookJournal;
                        break;
                }

                this.Title = segment.Title;
                this.Url = "http://www.biodiversitylibrary.org/part/" + segment.SegmentID.ToString();
                this.Sequence = "1";

                if (segment.ItemID != null)
                {
                    this.ParentUrl = "http://www.biodiversitylibrary.org/item/" + segment.ItemID.ToString();
                    this.Sequence = segment.SequenceOrder.ToString();
                }

                this.Types.Add(segment.GenreName);
                this.Types.Add("text");
                this.NumberOfPages = segment.PageList.Count.ToString();
                this.ArticleStartPage = segment.StartPageNumber;
                this.ArticleEndPage = segment.EndPageNumber;
                if (!string.IsNullOrEmpty(segment.Volume)) this.Descriptions.Add("Volume: " + segment.Volume);
                if (!string.IsNullOrEmpty(segment.StartPageNumber)) this.Descriptions.Add("Start Page: " + segment.StartPageNumber);
                if (!string.IsNullOrEmpty(segment.EndPageNumber)) this.Descriptions.Add("End Page: " + segment.EndPageNumber);
                this.JournalVolume = segment.Volume;
                this.Date = segment.Date;
                this.Contributor = segment.ContributorName;
                if (!string.IsNullOrEmpty(segment.RightsStatus)) this.Rights.Add(segment.RightsStatus);
                this.JournalTitle = segment.ContainerTitle;
                this.Publisher = segment.PublisherName;
                this.PublicationPlace = segment.PublisherPlace;
                this.Abstract = segment.Summary;
                if (!string.IsNullOrWhiteSpace(segment.Notes)) this.Notes.Add(new KeyValuePair<string, string>("", segment.Notes));

                if (segment.PublicationDetails != string.Empty ||
                    segment.PublisherPlace != string.Empty ||
                    segment.PublisherName != string.Empty)
                {
                    this.PublicationDetails = (segment.PublicationDetails == string.Empty ? segment.PublisherPlace + " " + segment.PublisherName : segment.PublicationDetails);
                }

                foreach (SegmentAuthor author in segment.AuthorList)
                {
                    OAIRecord.Creator creator = new OAIRecord.Creator(string.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, author.FullName);
                    KeyValuePair<string, OAIRecord.Creator> authorData = new KeyValuePair<string, OAIRecord.Creator>(string.Empty, creator);
                    this.Creators.Add(authorData);
                }

                foreach (SegmentKeyword keyword in segment.KeywordList)
                {
                    this.Subjects.Add(new KeyValuePair<string, string>(string.Empty, keyword.Keyword));
                }

                this.LoadIdentifiers(segment.IdentifierList, this);
                if (!string.IsNullOrWhiteSpace(segment.DOIName)) this.SetIdentifier("doi", segment.DOIName, this);
            }
        }


        /// <summary>
        /// Load the identifiers from the list into the title record
        /// </summary>
        /// <param name="identifierList"></param>
        /// <param name="record"></param>
        private void LoadIdentifiers(CustomGenericList<Title_Identifier> identifierList, OAIRecord record)
        {
            foreach (Title_Identifier titleIdentifier in identifierList)
            {
                this.SetIdentifier(titleIdentifier.IdentifierName, titleIdentifier.IdentifierValue, record);
            }
        }

        /// <summary>
        /// Load the identifiers from the list into the association record
        /// </summary>
        /// <param name="identifierList"></param>
        /// <param name="record"></param>
        private void LoadIdentifiers(CustomGenericList<TitleAssociation_TitleIdentifier> identifierList, OAIRecord record)
        {
            foreach (TitleAssociation_TitleIdentifier titleAssociationId in identifierList)
            {
                this.SetIdentifier(titleAssociationId.IdentifierName, titleAssociationId.IdentifierValue, record);
            }
        }

        /// <summary>
        /// Load the identifiers from the list
        /// </summary>
        /// <param name="identifierList"></param>
        /// <param name="record"></param>
        private void LoadIdentifiers(CustomGenericList<SegmentIdentifier> identifierList, OAIRecord record)
        {
            foreach (SegmentIdentifier segmentIdentifier in identifierList)
            {
                // Only include identifiers of the actual article (not the container object)
                if (segmentIdentifier.IsContainerIdentifier == 0)
                {
                    this.SetIdentifier(segmentIdentifier.IdentifierName, segmentIdentifier.IdentifierValue, record);
                }
            }
        }

        /// <summary>
        /// Set the identifier in the specified record
        /// </summary>
        /// <param name="identifierName"></param>
        /// <param name="identifierValue"></param>
        /// <param name="record"></param>
        private void SetIdentifier(string identifierName, string identifierValue, OAIRecord record)
        {
            switch (identifierName.ToLower())
            {
                case "oclc":
                    record.oclcNumbers.Add(identifierValue);
                    break;
                case "issn":
                    record.Issn = identifierValue;
                    break;
                case "isbn":
                    record.Isbn = identifierValue;
                    break;
                case "dlc":
                    record.Llc = identifierValue;
                    break;
                case "ddc":
                    record.Ddc = identifierValue;
                    break;
                case "nlm":
                    record.Nlm = identifierValue;
                    break;
                case "doi":
                    record.Doi = identifierValue;
                    break;
            }
        }

        /// <summary>
        /// Return the MODS note type for the specified MARC tag
        /// </summary>
        /// <param name="marcTag"></param>
        /// <returns></returns>
        private string GetNoteType(string marcTag)
        {
            string noteType = string.Empty;

            switch (marcTag)
            {
                case "502": noteType = "thesis"; break;
                case "504": noteType = "biography"; break;
                case "506": noteType = "reproduction"; break;
                case "508": noteType = "creation/production credits"; break;
                case "510": noteType = "citation/reference"; break;
                case "511": noteType = "performers"; break;
                case "515": noteType = "numbering"; break;
                case "524": noteType = "preferred citation"; break;
                case "530": noteType = "additional physical form"; break;
                case "534": noteType = "original version"; break;
                case "535": noteType = "original location"; break;
                case "536": noteType = "funding"; break;
                case "538": noteType = "system details"; break;
                case "541": noteType = "acquisition"; break;
                case "545": noteType = "biographical/historical"; break;
                case "546": noteType = "language"; break;
                case "561": noteType = "ownership"; break;
                case "562": noteType = "version identification"; break;
                case "581": noteType = "publications"; break;
                case "583": noteType = "action"; break;
                case "585": noteType = "exhibitions"; break;
                default: break;
            }

            return noteType;
        }

        #endregion Methods

        #region Internal classes

        /// <summary>
        /// This class holds the detail for a single creator (author)
        /// </summary>
        public class Creator
        {
            public Creator() {   }

            public Creator(string name, string numeration, string location, string dates, string relationship, string fullerName, string titleOfWork, string fullName)
            {
                _name = name;
                _numeration = numeration;
                _location = location;
                _dates = dates;
                _relationship = relationship;
                _fullerForm = fullerName;
                _titleOfWork = titleOfWork;
                _fullName = fullName;
            }

            private string _name;       // MARC attribute a
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _numeration; // MARC attribute b
            public string Numeration
            {
                get { return _numeration; }
                set { _numeration = value; }
            }

            private string _location;   // MARC attribute c
            public string Location
            {
                get { return _location; }
                set { _location = value; }
            }

            private string _dates;      // MARC attribute d
            public string Dates
            {
                get { return _dates; }
                set { _dates = value; }
            }

            private string _relationship;   // MARC attribute e
            public string Relationship
            {
                get { return _relationship; }
                set { _relationship = value; }
            }

            private string _fullerForm; // MARC attribute q
            public string FullerForm
            {
                get { return _fullerForm; }
                set { _fullerForm = value; }
            }

            private string _titleOfWork;    // MARC attribute t
            public string TitleOfWork
            {
                get { return _titleOfWork; }
                set { _titleOfWork = value; }
            }

            private string _fullName;   // Contatenation of all MARC fields
            public string FullName
            {
                get { return _fullName; }
                set { _fullName = value; }
            }

        }

        /// <summary>
        /// This class holds the detail for a single title variant (alternate, uniform, abbreviated, translated)
        /// </summary>
        public class TitleVariant
        {
            public TitleVariant() { }

            public TitleVariant(string title, string partNumber, string partName)
            {
                _title = title;
                _partNumber = partNumber;
                _partName = partName;
            }

            private string _title;
            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            private string _partNumber;
            public string PartNumber
            {
                get { return _partNumber; }
                set { _partNumber = value; }
            }

            private string _partName;
            public string PartName
            {
                get { return _partName; }
                set { _partName = value; }
            }
        }

        public class Page
        {
            public Page() { }

            public Page(int sequence, string pageType, string pageLabel, string url, string imageUrl,
                List<KeyValuePair<string, OAIRecord.Name>> names)
            {
                _sequence = sequence;
                _pageType = pageType;
                _pageLabel = pageLabel;
                _url = url;
                _imageUrl = imageUrl;
                _names = names;
            }

            private int _sequence;
            public int Sequence
            {
                get { return _sequence; }
                set { _sequence = value; }
            }

            private string _pageType;
            public string PageType
            {
                get { return _pageType; }
                set { _pageType = value; }
            }

            private string _pageLabel;
            public string PageLabel
            {
                get { return _pageLabel; }
                set { _pageLabel = value; }
            }

            private string _url;
            public string Url
            {
                get { return _url; }
                set { _url = value; }
            }

            private string _imageUrl;
            public string ImageUrl
            {
                get { return _imageUrl; }
                set { _imageUrl = value; }
            }

            List<KeyValuePair<String, OAIRecord.Name>> _names = new List<KeyValuePair<string, OAIRecord.Name>>();
            public List<KeyValuePair<String, OAIRecord.Name>> Names
            {
                get { return _names; }
                set { _names = value; }
            }
        }

        public class Name
        {
            public Name() { }

            public Name(string source, string scientificName)
            {
                _source = source;
                _scientificName = scientificName;
            }

            private string _source;
            public string Source
            {
                get { return _source; }
                set { _source = value; }
            }

            private string _scientificName;
            public string ScientificName
            {
                get { return _scientificName; }
                set { _scientificName = value; }
            }
        }

        #endregion Internal classes

        #region Enums

        public enum RecordType
        {
            BookJournal,
            Issue,
            Article,
            Segment,
            Unknown
        }

        #endregion Enums


    }
}
