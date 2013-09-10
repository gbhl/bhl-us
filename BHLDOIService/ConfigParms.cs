using System;
using System.Collections.Generic;
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

        private string _bhlTitleUrlFormat = "http://www.biodiversitylibrary.org/bibliography/{0}";
        public string BhlTitleUrlFormat
        {
            get { return _bhlTitleUrlFormat; }
            set { _bhlTitleUrlFormat = value; }
        }

        private string _bhlItemUrlFormat = "http://www.biodiversitylibrary.org/item/{0}";
        public string BhlItemUrlFormat
        {
            get { return _bhlItemUrlFormat; }
            set { _bhlItemUrlFormat = value; }
        }

        private string _bhlPageUrlFormat = "http://www.biodiversitylibrary.org/page/{0}";
        public string BhlPageUrlFormat
        {
            get { return _bhlPageUrlFormat; }
            set { _bhlPageUrlFormat = value; }
        }

        private int _doiStatusNone = 10;
        public int DoiStatusNone
        {
            get { return _doiStatusNone; }
            set { _doiStatusNone = value; }
        }

        private int _doiStatusAssigned = 20;
        public int DoiStatusAssigned
        {
            get { return _doiStatusAssigned; }
            set { _doiStatusAssigned = value; }
        }

        private int _doiStatusResubmit = 30;
        public int DoiStatusResubmit
        {
            get { return _doiStatusResubmit; }
            set { _doiStatusResubmit = value; }
        }

        private int _doiStatusBatchAssigned = 40;
        public int DoiStatusBatchAssigned
        {
            get { return _doiStatusBatchAssigned; }
            set { _doiStatusBatchAssigned = value; }
        }

        private int _doiStatusSubmitted = 50;
        public int DoiStatusSubmitted
        {
            get { return _doiStatusSubmitted; }
            set { _doiStatusSubmitted = value; }
        }

        private int _doiStatusSubmitError = 60;
        public int DoiStatusSubmitError
        {
            get { return _doiStatusSubmitError; }
            set { _doiStatusSubmitError = value; }
        }

        private int _doiStatusGetLogError = 70;
        public int DoiStatusGetLogError
        {
            get { return _doiStatusGetLogError; }
            set { _doiStatusGetLogError = value; }
        }

        private int _doiStatusCrossRefError = 80;
        public int DoiStatusCrossRefError
        {
            get { return _doiStatusCrossRefError; }
            set { _doiStatusCrossRefError = value; }
        }

        private int _doiStatusCrossRefWarning = 90;
        public int DoiStatusCrossRefWarning
        {
            get { return _doiStatusCrossRefWarning; }
            set { _doiStatusCrossRefWarning = value; }
        }

        private int _doiStatusApproved = 100;
        public int DoiStatusApproved
        {
            get { return _doiStatusApproved; }
            set { _doiStatusApproved = value; }
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

        private int _titleIdentifierISSN = 2;
        public int TitleIdentifierISSN
        {
            get { return _titleIdentifierISSN; }
            set { _titleIdentifierISSN = value; }
        }

        private int _titleIdentifierISBN = 3;
        public int TitleIdentifierISBN
        {
            get { return _titleIdentifierISBN; }
            set { _titleIdentifierISBN = value; }
        }

        private int _titleIdentifierAbbreviation = 6;
        public int TitleIdentifierAbbreviation
        {
            get { return _titleIdentifierAbbreviation; }
            set { _titleIdentifierAbbreviation = value; }
        }

        private int _titleIdentifierCODEN = 10;
        public int TitleIdentifierCODEN
        {
            get { return _titleIdentifierCODEN; }
            set { _titleIdentifierCODEN = value; }
        }

        private int _titleVariantAbbreviated = 3;
        public int TitleVariantAbbreviated
        {
            get { return _titleVariantAbbreviated; }
            set { _titleVariantAbbreviated = value; }
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

        public void LoadAppConfig()
        {
            XmlDocument doc = new XmlDocument();
            string configPath = AppDomain.CurrentDomain.FriendlyName + ".config";
            doc.Load(configPath);
            foreach (XmlNode node in doc["configuration"]["appSettings"])
            {
                if (node.Name == "add")
                {
                    if (node.Attributes.GetNamedItem("key").Value == "Service_SubmitTitles")
                    {
                        this.SubmitTitles = (node.Attributes.GetNamedItem("value").Value.ToLower() == "true");
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "Service_ValidateSubmissions")
                    {
                        this.ValidateSubmissions = (node.Attributes.GetNamedItem("value").Value.ToLower() == "true");
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SMTPHost")
                    {
                        this.SMTPHost = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EmailFromAddress")
                    {
                        this.EmailFromAddress = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "EmailToAddress")
                    {
                        this.EmailToAddress = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIPrefix")
                    {
                        this.DoiPrefix = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIFormat")
                    {
                        this.DoiFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefDepositorName")
                    {
                        this.CrossrefDepositorName = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefDepositorEmail")
                    {
                        this.CrossrefDepositorEmail = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefRegistrantName")
                    {
                        this.CrossrefRegistrantName = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefLogin")
                    {
                        this.CrossrefLogin = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefPassword")
                    {
                        this.CrossrefPassword = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefDepositArea")
                    {
                        this.CrossrefDepositArea = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefDepositUrlBase")
                    {
                        this.CrossrefDepositUrlBase = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefDepositUrlQueryFormat")
                    {
                        this.CrossrefDepositUrlQueryFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "CrossRefCheckSubmissionUrlFormat")
                    {
                        this.CrossrefCheckSubmissionUrlFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "NumberToSubmit")
                    {
                        this.NumberToSubmit = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MinimumMinutesSinceSubmit")
                    {
                        this.MinMinutesSinceSubmit = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "MonographDepositTemplateFile")
                    {
                        this.MonographDepositTemplateFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "JournalDepositTemplateFile")
                    {
                        this.JournalDepositTemplateFile = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DepositFileFormat")
                    {
                        this.DepositFileFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DepositFolder")
                    {
                        this.DepositFolder = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SubmitLogFileFormat")
                    {
                        this.SubmitLogFileFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "SubmitLogFolder")
                    {
                        this.SubmitLogFolder = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BHLTitleUrlFormat")
                    {
                        this.BhlTitleUrlFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BHLItemUrlFormat")
                    {
                        this.BhlItemUrlFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BHLPageUrlFormat")
                    {
                        this.BhlPageUrlFormat = node.Attributes.GetNamedItem("value").Value;
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_None")
                    {
                        this.DoiStatusNone = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_Assigned")
                    {
                        this.DoiStatusAssigned = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_Resubmit")
                    {
                        this.DoiStatusResubmit = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_BatchAssigned")
                    {
                        this.DoiStatusBatchAssigned = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_Submitted")
                    {
                        this.DoiStatusSubmitted = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_SubmitError")
                    {
                        this.DoiStatusSubmitError = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_GetLogError")
                    {
                        this.DoiStatusGetLogError = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_CrossRefError")
                    {
                        this.DoiStatusCrossRefError = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_CrossRefWarning")
                    {
                        this.DoiStatusCrossRefWarning = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIStatus_Approved")
                    {
                        this.DoiStatusApproved = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIEntityType_Title")
                    {
                        this.DoiEntityTypeTitle = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIEntityType_Item")
                    {
                        this.DoiEntityTypeItem = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "DOIEntityType_Page")
                    {
                        this.DoiEntityTypePage = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibLevel_MonographComponent")
                    {
                        this.BibLevelMonographComponent = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibLevel_SerialComponent")
                    {
                        this.BibLevelSerialComponent = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibLevel_Collection")
                    {
                        this.BibLevelCollection = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibLevel_Monograph")
                    {
                        this.BibLevelMonograph = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "BibLevel_Serial")
                    {
                        this.BibLevelSerial = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleIdentifier_ISSN")
                    {
                        this.TitleIdentifierISSN = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleIdentifier_ISBN")
                    {
                        this.TitleIdentifierISBN = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleIdentifier_Abbreviation")
                    {
                        this.TitleIdentifierAbbreviation = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleIdentifier_CODEN")
                    {
                        this.TitleIdentifierCODEN = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "TitleVariant_Abbreviated")
                    {
                        this.TitleVariantAbbreviated = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorRole_100")
                    {
                        this.AuthorRole100 = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorRole_110")
                    {
                        this.AuthorRole110 = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorRole_111")
                    {
                        this.AuthorRole111 = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorRole_700")
                    {
                        this.AuthorRole700 = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorRole_710")
                    {
                        this.AuthorRole710 = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                    if (node.Attributes.GetNamedItem("key").Value == "AuthorRole_711")
                    {
                        this.AuthorRole711 = Convert.ToInt32(node.Attributes.GetNamedItem("value").Value);
                    }
                }
            }
        }
    }
}
