using BHL.WebServiceREST.v1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

namespace MOBOT.BHL.BHLDOIService
{
    class ConfigParms
    {
        private bool _submitTitles = true;
        public bool SubmitTitles
        {
            get { return _submitTitles; }
            set { _submitTitles = value; }
        }

        private bool _submitSegments = true;
        public bool SubmitSegments
        {
            get { return _submitSegments; }
            set { _submitSegments = value; }
        }

        private bool _validateSubmissions = true;
        public bool ValidateSubmissions
        {
            get { return _validateSubmissions; }
            set { _validateSubmissions = value; }
        }

        private string _smtpHost = string.Empty;
        public string SMTPHost
        {
            get { return _smtpHost; }
            set { _smtpHost = value; }
        }

        private string _emailFromAddress = string.Empty;
        public string EmailFromAddress
        {
            get { return _emailFromAddress; }
            set { _emailFromAddress = value; }
        }

        private string _emailToAddress = string.Empty;
        public string EmailToAddress
        {
            get { return _emailToAddress; }
            set { _emailToAddress = value; }
        }

        private bool _emailOnError = false;
        public bool EmailOnError
        {
            get { return _emailOnError; }
            set { _emailOnError = value; }
        }

        private string _doiPrefix = string.Empty;
        public string DoiPrefix
        {
            get { return _doiPrefix; }
            set { _doiPrefix = value; }
        }

        private string _doiFormat = string.Empty;
        public string DoiFormat
        {
            get { return _doiFormat; }
            set { _doiFormat = value; }
        }

        private string _crossrefDepositorName = string.Empty;
        public string CrossrefDepositorName
        {
            get { return _crossrefDepositorName; }
            set { _crossrefDepositorName = value; }
        }

        private string _crossrefDepositorEmail = string.Empty;
        public string CrossrefDepositorEmail
        {
            get { return _crossrefDepositorEmail; }
            set { _crossrefDepositorEmail = value; }
        }

        private string _crossrefRegistrantName = string.Empty;
        public string CrossrefRegistrantName
        {
            get { return _crossrefRegistrantName; }
            set { _crossrefRegistrantName = value; }
        }

        private string _crossrefLogin = string.Empty;
        public string CrossrefLogin
        {
            get { return _crossrefLogin; }
            set { _crossrefLogin = value; }
        }

        private string _crossrefPassword = string.Empty;
        public string CrossrefPassword
        {
            get { return _crossrefPassword; }
            set { _crossrefPassword = value; }
        }

        private string _crossrefDepositArea = string.Empty;
        public string CrossrefDepositArea
        {
            get { return _crossrefDepositArea; }
            set { _crossrefDepositArea = value; }
        }

        private string _crossrefDepositUrlBase = string.Empty;
        public string CrossrefDepositUrlBase
        {
            get { return _crossrefDepositUrlBase; }
            set { _crossrefDepositUrlBase = value; }
        }

        private string _crossrefDepositUrlQueryFormat = string.Empty;
        public string CrossrefDepositUrlQueryFormat
        {
            get { return _crossrefDepositUrlQueryFormat; }
            set { _crossrefDepositUrlQueryFormat = value; }
        }

        private string _crossrefCheckSubmissionUrlFormat = string.Empty;
        public string CrossrefCheckSubmissionUrlFormat
        {
            get { return _crossrefCheckSubmissionUrlFormat; }
            set { _crossrefCheckSubmissionUrlFormat = value; }
        }

        private string _crossrefXmlQueryUrlBase = string.Empty;
        public string CrossrefXmlQueryUrlBase
        {
            get { return _crossrefXmlQueryUrlBase; }
            set { _crossrefXmlQueryUrlBase = value; }
        }

        private string _crossrefXmlQueryFormat = string.Empty;
        public string CrossrefXmlQueryFormat
        {
            get { return _crossrefXmlQueryFormat; }
            set { _crossrefXmlQueryFormat = value; }
        }


        private int _numberToSubmit = 10;
        public int NumberToSubmit
        {
            get { return _numberToSubmit; }
            set { _numberToSubmit = value; }
        }

        private int _minMinutesSinceSubmit = 60;
        public int MinMinutesSinceSubmit
        {
            get { return _minMinutesSinceSubmit; }
            set { _minMinutesSinceSubmit = value; }
        }

        private bool _checkForMonoSeries = true;
        public bool CheckForMonoSeries
        {
            get { return _checkForMonoSeries; }
            set { _checkForMonoSeries = value; }
        }

        private string _monographDepositTemplateFile = "MonographDepositTemplate.xml";
        public string MonographDepositTemplateFile
        {
            get { return _monographDepositTemplateFile; }
            set { _monographDepositTemplateFile = value; }
        }

        private string _journalDepositTemplateFile = "JournalDepositTemplate.xml";
        public string JournalDepositTemplateFile
        {
            get { return _journalDepositTemplateFile; }
            set { _journalDepositTemplateFile = value; }
        }

        private string _articleDepositTemplateFile = "ArticleDepositTemplate.xml";
        public string ArticleDepositTemplateFile
        {
            get { return _articleDepositTemplateFile; }
            set { _articleDepositTemplateFile = value; }
        }

        private string _queryTemplateFile = "QueryTemplateFile.xml";
        public string QueryTemplateFile
        {
            get { return _queryTemplateFile; }
            set { _queryTemplateFile = value; }
        }

        private string _depositFileFormat = "{0}.xml";
        public string DepositFileFormat
        {
            get { return _depositFileFormat; }
            set { _depositFileFormat = value; }
        }

        private string _depositFolder = "deposit/";
        public string DepositFolder
        {
            get { return _depositFolder; }
            set { _depositFolder = value; }
        }

        private string _submitLogFileFormat = "{0}.log.xml";
        public string SubmitLogFileFormat
        {
            get { return _submitLogFileFormat; }
            set { _submitLogFileFormat = value; }
        }

        private string _submitLogFolder = "submitlog/";
        public string SubmitLogFolder
        {
            get { return _submitLogFolder; }
            set { _submitLogFolder = value; }
        }

        private string _bhlTitleUrlFormat = "https://www.biodiversitylibrary.org/bibliography/{0}";
        public string BhlTitleUrlFormat
        {
            get { return _bhlTitleUrlFormat; }
            set { _bhlTitleUrlFormat = value; }
        }

        private string _bhlItemUrlFormat = "https://www.biodiversitylibrary.org/item/{0}";
        public string BhlItemUrlFormat
        {
            get { return _bhlItemUrlFormat; }
            set { _bhlItemUrlFormat = value; }
        }

        private string _bhlPageUrlFormat = "https://www.biodiversitylibrary.org/page/{0}";
        public string BhlPageUrlFormat
        {
            get { return _bhlPageUrlFormat; }
            set { _bhlPageUrlFormat = value; }
        }

        private string _bhlPartUrlFormat = "https://www.biodiversitylibrary.org/part/{0}";
        public string BhlPartUrlFormat
        {
            get { return _bhlPartUrlFormat; }
            set { _bhlPartUrlFormat = value; }
        }

        private int _doiStatusNull = 0;
        public int DoiStatusNull
        {
            get { return _doiStatusNull; }
            set { _doiStatusNull = value; }
        }

        private int _doiStatusQueued = 30;
        public int DoiStatusQueued
        {
            get { return _doiStatusQueued; }
            set { _doiStatusQueued = value; }
        }

        private int _doiStatusSubmitted = 50;
        public int DoiStatusSubmitted
        {
            get { return _doiStatusSubmitted; }
            set { _doiStatusSubmitted = value; }
        }

        private int _doiStatusError = 80;
        public int DoiStatusError
        {
            get { return _doiStatusError; }
            set { _doiStatusError = value; }
        }

        private int _doiStatusApproved = 100;
        public int DoiStatusApproved
        {
            get { return _doiStatusApproved; }
            set { _doiStatusApproved = value; }
        }

        private int _doiStatusExternal = 200;
        public int DoiStatusExternal
        {
            get { return _doiStatusExternal; }
            set { _doiStatusExternal = value; }
        }

        private int _doiEntityTypeTitle = 10;
        public int DoiEntityTypeTitle
        {
            get { return _doiEntityTypeTitle; }
            set { _doiEntityTypeTitle = value; }
        }

        private int _doiEntityTypeItem = 20;
        public int DoiEntityTypeItem
        {
            get { return _doiEntityTypeItem; }
            set { _doiEntityTypeItem = value; }
        }

        private int _doiEntityTypePage = 30;
        public int DoiEntityTypePage
        {
            get { return _doiEntityTypePage; }
            set { _doiEntityTypePage = value; }
        }

        private int _doiEntityTypeSegment = 40;
        public int DoiEntityTypeSegment
        {
            get { return _doiEntityTypeSegment; }
            set { _doiEntityTypeSegment = value; }
        }

        private int _bibLevelMonographComponent = 1;
        public int BibLevelMonographComponent
        {
            get { return _bibLevelMonographComponent; }
            set { _bibLevelMonographComponent = value; }
        }

        private int _bibLevelSerialComponent = 2;
        public int BibLevelSerialComponent
        {
            get { return _bibLevelSerialComponent; }
            set { _bibLevelSerialComponent = value; }
        }

        private int _bibLevelCollection = 3;
        public int BibLevelCollection
        {
            get { return _bibLevelCollection; }
            set { _bibLevelCollection = value; }
        }

        private int _bibLevelMonograph = 4;
        public int BibLevelMonograph
        {
            get { return _bibLevelMonograph; }
            set { _bibLevelMonograph = value; }
        }

        private int _bibLevelSerial = 5;
        public int BibLevelSerial
        {
            get { return _bibLevelSerial; }
            set { _bibLevelSerial = value; }
        }

        private int _titleVariantAbbreviated = 3;
        public int TitleVariantAbbreviated
        {
            get { return _titleVariantAbbreviated; }
            set { _titleVariantAbbreviated = value; }
        }

        private string _segmentGenreChapter = "Chapter";
        public string SegmentGenreChapter
        {
            get { return _segmentGenreChapter; }
            set { _segmentGenreChapter = value; }
        }

        private int _authorRole100 = 1;
        public int AuthorRole100
        {
            get { return _authorRole100; }
            set { _authorRole100 = value; }
        }

        private int _authorRole110 = 2;
        public int AuthorRole110
        {
            get { return _authorRole110; }
            set { _authorRole110 = value; }
        }

        private int _authorRole111 = 3;
        public int AuthorRole111
        {
            get { return _authorRole111; }
            set { _authorRole111 = value; }
        }

        private int _authorRole700 = 4;
        public int AuthorRole700
        {
            get { return _authorRole700; }
            set { _authorRole700 = value; }
        }

        private int _authorRole710 = 5;
        public int AuthorRole710
        {
            get { return _authorRole710; }
            set { _authorRole710 = value; }
        }

        private int _authorRole711 = 6;
        public int AuthorRole711
        {
            get { return _authorRole711; }
            set { _authorRole711 = value; }
        }

        private int _authorTypePerson = 1;
        public int AuthorTypePerson
        {  
            get { return _authorTypePerson; } 
            set { _authorTypePerson = value; }
        }

        private int _authorTypeCorporation = 2;
        public int AuthorTypeCorporation
        {
            get { return _authorTypeCorporation; }
            set { _authorTypeCorporation = value; }
        }

        private int _authorTypeMeeting = 3;
        public int AuthorTypeMeeting
        {
            get { return _authorTypeMeeting; }
            set { _authorTypeMeeting = value; }
        }

        private string _bhlwsRestEndpoint = string.Empty;
        public string BHLWSRestEndpoint { get => _bhlwsRestEndpoint; set => _bhlwsRestEndpoint = value; }

        public void LoadAppConfig()
        {
            SubmitTitles = ConfigurationManager.AppSettings["Service_SubmitTitles"] == "true";
            SubmitSegments = ConfigurationManager.AppSettings["Service_SubmitSegments"] == "true";
            ValidateSubmissions = ConfigurationManager.AppSettings["Service_ValidateSubmissions"] == "true";
            SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            EmailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            EmailOnError = ConfigurationManager.AppSettings["EmailOnError"].ToLower() == "true";
            DoiPrefix = ConfigurationManager.AppSettings["DOIPrefix"];
            DoiFormat = ConfigurationManager.AppSettings["DOIFormat"];
            CrossrefDepositorName = ConfigurationManager.AppSettings["CrossRefDepositorName"];
            CrossrefDepositorEmail = ConfigurationManager.AppSettings["CrossRefDepositorEmail"];
            CrossrefRegistrantName = ConfigurationManager.AppSettings["CrossRefRegistrantName"];
            CrossrefLogin = ConfigurationManager.AppSettings["CrossRefLogin"];
            CrossrefPassword = ConfigurationManager.AppSettings["CrossRefPassword"];
            CrossrefDepositArea = ConfigurationManager.AppSettings["CrossRefDepositArea"];
            CrossrefDepositUrlBase = ConfigurationManager.AppSettings["CrossRefDepositUrlBase"];
            CrossrefDepositUrlQueryFormat = ConfigurationManager.AppSettings["CrossRefDepositUrlQueryFormat"];
            CrossrefCheckSubmissionUrlFormat = ConfigurationManager.AppSettings["CrossRefCheckSubmissionUrlFormat"];
            CrossrefXmlQueryUrlBase = ConfigurationManager.AppSettings["CrossRefXmlQueryUrlBase"];
            CrossrefXmlQueryFormat = ConfigurationManager.AppSettings["CrossRefXmlQueryFormat"];
            NumberToSubmit = Convert.ToInt32(ConfigurationManager.AppSettings["NumberToSubmit"]);
            MinMinutesSinceSubmit = Convert.ToInt32(ConfigurationManager.AppSettings["MinimumMinutesSinceSubmit"]);
            CheckForMonoSeries = ConfigurationManager.AppSettings["CheckForMonoSeries"].ToLower() == "true";
            MonographDepositTemplateFile = ConfigurationManager.AppSettings["MonographDepositTemplateFile"];
            JournalDepositTemplateFile = ConfigurationManager.AppSettings["JournalDepositTemplateFile"];
            ArticleDepositTemplateFile = ConfigurationManager.AppSettings["ArticleDepositTemplateFile"];
            QueryTemplateFile = ConfigurationManager.AppSettings["QueryTemplateFile"];
            DepositFileFormat = ConfigurationManager.AppSettings["DepositFileFormat"];
            DepositFolder = ConfigurationManager.AppSettings["DepositFolder"];
            SubmitLogFileFormat = ConfigurationManager.AppSettings["SubmitLogFileFormat"];
            SubmitLogFolder = ConfigurationManager.AppSettings["SubmitLogFolder"];
            BhlTitleUrlFormat = ConfigurationManager.AppSettings["BHLTitleUrlFormat"];
            BhlItemUrlFormat = ConfigurationManager.AppSettings["BHLItemUrlFormat"];
            BhlPageUrlFormat = ConfigurationManager.AppSettings["BHLPageUrlFormat"];
            BhlPartUrlFormat = ConfigurationManager.AppSettings["BHLPartUrlFormat"];
            DoiStatusNull= Convert.ToInt32(ConfigurationManager.AppSettings["DOIStatus_Null"]);
            DoiStatusQueued = Convert.ToInt32(ConfigurationManager.AppSettings["DOIStatus_Queued"]);
            DoiStatusSubmitted = Convert.ToInt32(ConfigurationManager.AppSettings["DOIStatus_Submitted"]);
            DoiStatusError = Convert.ToInt32(ConfigurationManager.AppSettings["DOIStatus_Error"]);
            DoiStatusApproved = Convert.ToInt32(ConfigurationManager.AppSettings["DOIStatus_Approved"]);
            DoiStatusExternal = Convert.ToInt32(ConfigurationManager.AppSettings["DOIStatus_External"]);
            DoiEntityTypeTitle = Convert.ToInt32(ConfigurationManager.AppSettings["DOIEntityType_Title"]);
            DoiEntityTypeItem = Convert.ToInt32(ConfigurationManager.AppSettings["DOIEntityType_Item"]);
            DoiEntityTypePage = Convert.ToInt32(ConfigurationManager.AppSettings["DOIEntityType_Page"]);
            DoiEntityTypeSegment = Convert.ToInt32(ConfigurationManager.AppSettings["DOIEntityType_Segment"]);
            BibLevelMonographComponent = Convert.ToInt32(ConfigurationManager.AppSettings["BibLevel_MonographComponent"]);
            BibLevelSerialComponent= Convert.ToInt32(ConfigurationManager.AppSettings["BibLevel_SerialComponent"]);
            BibLevelCollection = Convert.ToInt32(ConfigurationManager.AppSettings["BibLevel_Collection"]);
            BibLevelMonograph = Convert.ToInt32(ConfigurationManager.AppSettings["BibLevel_Monograph"]);
            BibLevelSerial = Convert.ToInt32(ConfigurationManager.AppSettings["BibLevel_Serial"]);
            TitleVariantAbbreviated = Convert.ToInt32(ConfigurationManager.AppSettings["TitleVariant_Abbreviated"]);
            AuthorRole100 = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorRole_100"]);
            AuthorRole110 = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorRole_110"]);
            AuthorRole111 = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorRole_111"]);
            AuthorRole700 = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorRole_700"]);
            AuthorRole710 = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorRole_710"]);
            AuthorRole711 = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorRole_711"]);
            AuthorTypePerson = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorTypePerson"]);
            AuthorTypeCorporation = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorTypeCorporation"]);
            AuthorTypeMeeting = Convert.ToInt32(ConfigurationManager.AppSettings["AuthorTypeMeeting"]);
            BHLWSRestEndpoint = ConfigurationManager.AppSettings["BHLWSUrl"];
        }
    }
}
