using CustomDataAccess;
using System;
using System.Collections.Generic;

namespace MOBOT.BHL.DataObjects
{
    [Serializable]
    public class ImportRecordReview : ISetValues
    {
        public ImportRecordReview()
        {
        }

        ~ImportRecordReview()
        {
        }

		#region Properties

		private int _TotalRecords = 0;
		private int _ImportRecordID = default(int);
		private int? _SegmentID = null;
		private int? _ImportSegmentID = null;
		private int _ImportRecordStatusID = default(int);
		private string _StatusName = string.Empty;
		private List<ImportRecordErrorLog> _Errors = new List<ImportRecordErrorLog>();
		private List<ImportRecordErrorLog> _Warnings = new List<ImportRecordErrorLog>();
		private string _ErrorString = string.Empty;
		private string _WarningString = string.Empty;

		private int? _NCItemID = null;
		private string _NCTitle = string.Empty;
		private string _NCVolume = string.Empty;
		private string _NCSeries = string.Empty;
		private string _NCIssue = string.Empty;
		private string _NCEdition = string.Empty;
		private string _NCPublicationDetails = string.Empty;
		private string _NCPublisherName = string.Empty;
		private string _NCPublisherPlace = string.Empty;
		private string _NCYear = string.Empty;
		private string _NCRights = string.Empty;
		private string _NCCopyrightStatus = string.Empty;
		private string _NCLicenseUrl = string.Empty;

		private int? _ECItemID = null;
		private string _ECTitle = string.Empty;
		private string _ECVolume = string.Empty;
		private string _ECSeries = string.Empty;
		private string _ECIssue = string.Empty;
		private string _ECEdition = string.Empty;
		private string _ECPublicationDetails = string.Empty;
		private string _ECPublisherName = string.Empty;
		private string _ECPublisherPlace = string.Empty;
		private string _ECYear = string.Empty;
		private string _ECRights = string.Empty;
		private string _ECCopyrightStatus = string.Empty;
		private string _ECLicenseUrl = string.Empty;

		private string _NSGenre = string.Empty;
		private string _NSTitle = string.Empty;
		private string _NSTranslatedTitle = string.Empty;
		private string _NSJournalTitle = string.Empty;
		private string _NSVolume = string.Empty;
		private string _NSSeries = string.Empty;
		private string _NSIssue = string.Empty;
		private string _NSEdition = string.Empty;
		private string _NSPublicationDetails = string.Empty;
		private string _NSPublisherName = string.Empty;
		private string _NSPublisherPlace = string.Empty;
		private string _NSYear = string.Empty;
		private string _NSLanguage = string.Empty;
		private string _NSSummary = string.Empty;
		private string _NSNotes = string.Empty;
		private string _NSRights = string.Empty;
		private string _NSCopyrightStatus = string.Empty;
		private string _NSLicense = string.Empty;
		private string _NSLicenseUrl = string.Empty;
		private string _NSPageRange = string.Empty;
		private string _NSStartPage = string.Empty;
		private int? _NSStartPageID = null;
		private string _NSEndPage = string.Empty;
		private int? _NSEndPageID = null;
		private string _NSUrl = string.Empty;
		private string _NSDownloadUrl = string.Empty;
		private string _NSDOI = string.Empty;
		private string _NSISSN = string.Empty;
		private string _NSISBN = string.Empty;
		private string _NSOCLC = string.Empty;
		private string _NSLCCN = string.Empty;
		private string _NSARK = string.Empty;
		private string _NSBiostor = string.Empty;
		private string _NSJSTOR = string.Empty;
		private string _NSTL2 = string.Empty;
		private string _NSWikidata = string.Empty;
		private List<ImportRecordCreator> _NSAuthors = new List<ImportRecordCreator>();
		private List<ImportRecordKeyword> _NSKeywords = new List<ImportRecordKeyword>();
		private List<ImportRecordContributor> _NSContributors = new List<ImportRecordContributor>();
		private List<ImportRecordPage> _NSPages = new List<ImportRecordPage>();
		private string _NSAuthorString = string.Empty;
		private string _NSKeywordString = string.Empty;
		private string _NSContributorString = string.Empty;
		private string _NSPageString = string.Empty;

		private string _ESGenre = string.Empty;
		private string _ESTitle = string.Empty;
		private string _ESTranslatedTitle = string.Empty;
		private string _ESJournalTitle = string.Empty;
		private string _ESVolume = string.Empty;
		private string _ESSeries = string.Empty;
		private string _ESIssue = string.Empty;
		private string _ESEdition = string.Empty;
		private string _ESPublicationDetails = string.Empty;
		private string _ESPublisherName = string.Empty;
		private string _ESPublisherPlace = string.Empty;
		private string _ESYear = string.Empty;
		private string _ESLanguage = string.Empty;
		private string _ESSummary = string.Empty;
		private string _ESNotes = string.Empty;
		private string _ESRights = string.Empty;
		private string _ESCopyrightStatus = string.Empty;
		private string _ESLicense = string.Empty;
		private string _ESLicenseUrl = string.Empty;
		private string _ESPageRange = string.Empty;
		private string _ESStartPage = string.Empty;
		private int? _ESStartPageID = null;
		private string _ESEndPage = string.Empty;
		private int? _ESEndPageID = null;
		private string _ESUrl = string.Empty;
		private string _ESDownloadUrl = string.Empty;
		private string _ESDOI = string.Empty;
		private string _ESISSN = string.Empty;
		private string _ESISBN = string.Empty;
		private string _ESOCLC = string.Empty;
		private string _ESLCCN = string.Empty;
		private string _ESARK = string.Empty;
		private string _ESBiostor = string.Empty;
		private string _ESJSTOR = string.Empty;
		private string _ESTL2 = string.Empty;
		private string _ESWikidata = string.Empty;
		private List<ImportRecordCreator> _ESAuthors = new List<ImportRecordCreator>();
		private List<ImportRecordKeyword> _ESKeywords = new List<ImportRecordKeyword>();
		private List<ImportRecordContributor> _ESContributors = new List<ImportRecordContributor>();
		private List<ImportRecordPage> _ESPages = new List<ImportRecordPage>();
		private string _ESAuthorString = string.Empty;
		private string _ESKeywordString = string.Empty;
		private string _ESContributorString = string.Empty;
		private string _ESPageString = string.Empty;

		public int ImportRecordID { get => _ImportRecordID; set => _ImportRecordID = value; }
		public int TotalRecords { get => _TotalRecords; set => _TotalRecords = value; }
		public int? SegmentID { get => _SegmentID; set => _SegmentID = value; }
        public int? ImportSegmentID { get => _ImportSegmentID; set => _ImportSegmentID = value; }
        public int ImportRecordStatusID { get => _ImportRecordStatusID; set => _ImportRecordStatusID = value; }
        public string StatusName { get => _StatusName; set => _StatusName = value; }
        public List<ImportRecordErrorLog> Errors { get => _Errors; set => _Errors = value; }
        public List<ImportRecordErrorLog> Warnings { get => _Warnings; set => _Warnings = value; }
        public string ErrorString { get => _ErrorString; set => _ErrorString = value; }
        public string WarningString { get => _WarningString; set => _WarningString = value; }
		public int? NCItemID { get => _NCItemID; set => _NCItemID = value; }
		public string NCTitle { get => _NCTitle; set => _NCTitle = value; }
        public string NCVolume { get => _NCVolume; set => _NCVolume = value; }
        public string NCSeries { get => _NCSeries; set => _NCSeries = value; }
        public string NCIssue { get => _NCIssue; set => _NCIssue = value; }
        public string NCEdition { get => _NCEdition; set => _NCEdition = value; }
        public string NCPublicationDetails { get => _NCPublicationDetails; set => _NCPublicationDetails = value; }
        public string NCPublisherName { get => _NCPublisherName; set => _NCPublisherName = value; }
        public string NCPublisherPlace { get => _NCPublisherPlace; set => _NCPublisherPlace = value; }
        public string NCYear { get => _NCYear; set => _NCYear = value; }
        public string NCRights { get => _NCRights; set => _NCRights = value; }
        public string NCCopyrightStatus { get => _NCCopyrightStatus; set => _NCCopyrightStatus = value; }
        public string NCLicenseUrl { get => _NCLicenseUrl; set => _NCLicenseUrl = value; }
		public int? ECItemID { get => _ECItemID; set => _ECItemID = value; }
		public string ECTitle { get => _ECTitle; set => _ECTitle = value; }
        public string ECVolume { get => _ECVolume; set => _ECVolume = value; }
        public string ECSeries { get => _ECSeries; set => _ECSeries = value; }
        public string ECIssue { get => _ECIssue; set => _ECIssue = value; }
        public string ECEdition { get => _ECEdition; set => _ECEdition = value; }
        public string ECPublicationDetails { get => _ECPublicationDetails; set => _ECPublicationDetails = value; }
        public string ECPublisherName { get => _ECPublisherName; set => _ECPublisherName = value; }
        public string ECPublisherPlace { get => _ECPublisherPlace; set => _ECPublisherPlace = value; }
        public string ECYear { get => _ECYear; set => _ECYear = value; }
        public string ECRights { get => _ECRights; set => _ECRights = value; }
        public string ECCopyrightStatus { get => _ECCopyrightStatus; set => _ECCopyrightStatus = value; }
        public string ECLicenseUrl { get => _ECLicenseUrl; set => _ECLicenseUrl = value; }
        public string NSGenre { get => _NSGenre; set => _NSGenre = value; }
        public string NSTitle { get => _NSTitle; set => _NSTitle = value; }
        public string NSTranslatedTitle { get => _NSTranslatedTitle; set => _NSTranslatedTitle = value; }
        public string NSJournalTitle { get => _NSJournalTitle; set => _NSJournalTitle = value; }
        public string NSVolume { get => _NSVolume; set => _NSVolume = value; }
        public string NSSeries { get => _NSSeries; set => _NSSeries = value; }
        public string NSIssue { get => _NSIssue; set => _NSIssue = value; }
        public string NSEdition { get => _NSEdition; set => _NSEdition = value; }
        public string NSPublicationDetails { get => _NSPublicationDetails; set => _NSPublicationDetails = value; }
        public string NSPublisherName { get => _NSPublisherName; set => _NSPublisherName = value; }
        public string NSPublisherPlace { get => _NSPublisherPlace; set => _NSPublisherPlace = value; }
        public string NSYear { get => _NSYear; set => _NSYear = value; }
        public string NSLanguage { get => _NSLanguage; set => _NSLanguage = value; }
        public string NSSummary { get => _NSSummary; set => _NSSummary = value; }
        public string NSNotes { get => _NSNotes; set => _NSNotes = value; }
        public string NSRights { get => _NSRights; set => _NSRights = value; }
        public string NSCopyrightStatus { get => _NSCopyrightStatus; set => _NSCopyrightStatus = value; }
        public string NSLicense { get => _NSLicense; set => _NSLicense = value; }
        public string NSLicenseUrl { get => _NSLicenseUrl; set => _NSLicenseUrl = value; }
        public string NSPageRange { get => _NSPageRange; set => _NSPageRange = value; }
        public string NSStartPage { get => _NSStartPage; set => _NSStartPage = value; }
        public int? NSStartPageID { get => _NSStartPageID; set => _NSStartPageID = value; }
        public string NSEndPage { get => _NSEndPage; set => _NSEndPage = value; }
        public int? NSEndPageID { get => _NSEndPageID; set => _NSEndPageID = value; }
        public string NSUrl { get => _NSUrl; set => _NSUrl = value; }
        public string NSDownloadUrl { get => _NSDownloadUrl; set => _NSDownloadUrl = value; }
        public string NSDOI { get => _NSDOI; set => _NSDOI = value; }
        public string NSISSN { get => _NSISSN; set => _NSISSN = value; }
        public string NSISBN { get => _NSISBN; set => _NSISBN = value; }
        public string NSOCLC { get => _NSOCLC; set => _NSOCLC = value; }
        public string NSLCCN { get => _NSLCCN; set => _NSLCCN = value; }
        public string NSARK { get => _NSARK; set => _NSARK = value; }
        public string NSBiostor { get => _NSBiostor; set => _NSBiostor = value; }
        public string NSJSTOR { get => _NSJSTOR; set => _NSJSTOR = value; }
        public string NSTL2 { get => _NSTL2; set => _NSTL2 = value; }
        public string NSWikidata { get => _NSWikidata; set => _NSWikidata = value; }
        public List<ImportRecordCreator> NSAuthors { get => _NSAuthors; set => _NSAuthors = value; }
        public List<ImportRecordKeyword> NSKeywords { get => _NSKeywords; set => _NSKeywords = value; }
        public List<ImportRecordContributor> NSContributors { get => _NSContributors; set => _NSContributors = value; }
        public List<ImportRecordPage> NSPages { get => _NSPages; set => _NSPages = value; }
        public string NSAuthorString { get => _NSAuthorString; set => _NSAuthorString = value; }
        public string NSKeywordString { get => _NSKeywordString; set => _NSKeywordString = value; }
        public string NSContributorString { get => _NSContributorString; set => _NSContributorString = value; }
        public string NSPageString { get => _NSPageString; set => _NSPageString = value; }
        public string ESGenre { get => _ESGenre; set => _ESGenre = value; }
        public string ESTitle { get => _ESTitle; set => _ESTitle = value; }
        public string ESTranslatedTitle { get => _ESTranslatedTitle; set => _ESTranslatedTitle = value; }
        public string ESJournalTitle { get => _ESJournalTitle; set => _ESJournalTitle = value; }
        public string ESVolume { get => _ESVolume; set => _ESVolume = value; }
        public string ESSeries { get => _ESSeries; set => _ESSeries = value; }
        public string ESIssue { get => _ESIssue; set => _ESIssue = value; }
        public string ESEdition { get => _ESEdition; set => _ESEdition = value; }
        public string ESPublicationDetails { get => _ESPublicationDetails; set => _ESPublicationDetails = value; }
        public string ESPublisherName { get => _ESPublisherName; set => _ESPublisherName = value; }
        public string ESPublisherPlace { get => _ESPublisherPlace; set => _ESPublisherPlace = value; }
        public string ESYear { get => _ESYear; set => _ESYear = value; }
        public string ESLanguage { get => _ESLanguage; set => _ESLanguage = value; }
        public string ESSummary { get => _ESSummary; set => _ESSummary = value; }
        public string ESNotes { get => _ESNotes; set => _ESNotes = value; }
        public string ESRights { get => _ESRights; set => _ESRights = value; }
        public string ESCopyrightStatus { get => _ESCopyrightStatus; set => _ESCopyrightStatus = value; }
        public string ESLicense { get => _ESLicense; set => _ESLicense = value; }
        public string ESLicenseUrl { get => _ESLicenseUrl; set => _ESLicenseUrl = value; }
        public string ESPageRange { get => _ESPageRange; set => _ESPageRange = value; }
        public string ESStartPage { get => _ESStartPage; set => _ESStartPage = value; }
        public int? ESStartPageID { get => _ESStartPageID; set => _ESStartPageID = value; }
        public string ESEndPage { get => _ESEndPage; set => _ESEndPage = value; }
        public int? ESEndPageID { get => _ESEndPageID; set => _ESEndPageID = value; }
        public string ESUrl { get => _ESUrl; set => _ESUrl = value; }
        public string ESDownloadUrl { get => _ESDownloadUrl; set => _ESDownloadUrl = value; }
        public string ESDOI { get => _ESDOI; set => _ESDOI = value; }
        public string ESISSN { get => _ESISSN; set => _ESISSN = value; }
        public string ESISBN { get => _ESISBN; set => _ESISBN = value; }
        public string ESOCLC { get => _ESOCLC; set => _ESOCLC = value; }
        public string ESLCCN { get => _ESLCCN; set => _ESLCCN = value; }
        public string ESARK { get => _ESARK; set => _ESARK = value; }
        public string ESBiostor { get => _ESBiostor; set => _ESBiostor = value; }
        public string ESJSTOR { get => _ESJSTOR; set => _ESJSTOR = value; }
        public string ESTL2 { get => _ESTL2; set => _ESTL2 = value; }
        public string ESWikidata { get => _ESWikidata; set => _ESWikidata = value; }
        public List<ImportRecordCreator> ESAuthors { get => _ESAuthors; set => _ESAuthors = value; }
        public List<ImportRecordKeyword> ESKeywords { get => _ESKeywords; set => _ESKeywords = value; }
        public List<ImportRecordContributor> ESContributors { get => _ESContributors; set => _ESContributors = value; }
        public List<ImportRecordPage> ESPages { get => _ESPages; set => _ESPages = value; }
        public string ESAuthorString { get => _ESAuthorString; set => _ESAuthorString = value; }
        public string ESKeywordString { get => _ESKeywordString; set => _ESKeywordString = value; }
        public string ESContributorString { get => _ESContributorString; set => _ESContributorString = value; }
        public string ESPageString { get => _ESPageString; set => _ESPageString = value; }

        #endregion Properties

        public void SetValues(CustomDataAccess.CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
					case "TotalRecords":
						TotalRecords = Utility.ZeroIfNull(column.Value);
						break;
					case "ImportRecordID":
						ImportRecordID = (int)column.Value;
						break;
					case "SegmentID":
						SegmentID = (int?)column.Value;
						break;
					case "ImportSegmentID":
						ImportSegmentID = (int?)column.Value;
						break;
					case "ImportRecordStatusID":
						ImportRecordStatusID = (int)column.Value;
						break;
					case "StatusName":
						StatusName = Utility.EmptyIfNull(column.Value);
						break;
					case "Errors":
						ErrorString = Utility.EmptyIfNull(column.Value);
						break;
					case "Warnings":
						WarningString = Utility.EmptyIfNull(column.Value);
						break;
					case "NCItemID":
						NCItemID = (int?)column.Value;
						break;
					case "NCTitle":
						NCTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "NCVolume":
						NCVolume = Utility.EmptyIfNull(column.Value);
						break;
					case "NCSeries":
						NCSeries = Utility.EmptyIfNull(column.Value);
						break;
					case "NCIssue":
						NCIssue = Utility.EmptyIfNull(column.Value);
						break;
					case "NCEdition":
						NCEdition  = Utility.EmptyIfNull(column.Value);
						break;
					case "NCPublicationDetails":
						NCPublicationDetails = Utility.EmptyIfNull(column.Value);
						break;
					case "NCPublisherName":
						NCPublisherName = Utility.EmptyIfNull(column.Value);
						break;
					case "NCPublisherPlace":
						NCPublisherPlace = Utility.EmptyIfNull(column.Value);
						break;
					case "NCYear":
						NCYear = Utility.EmptyIfNull(column.Value);
						break;
					case "NCRights":
						NCRights = Utility.EmptyIfNull(column.Value);
						break;
					case "NCCopyrightStatus":
						NCCopyrightStatus = Utility.EmptyIfNull(column.Value);
						break;
					case "NCLicenseUrl":
						NCLicenseUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "ECItemID":
						ECItemID = (int?)column.Value;
						break;
					case "ECTitle":
						ECTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "ECVolume":
						ECVolume = Utility.EmptyIfNull(column.Value);
						break;
					case "ECSeries":
						ECSeries = Utility.EmptyIfNull(column.Value);
						break;
					case "ECIssue":
						ECIssue = Utility.EmptyIfNull(column.Value);
						break;
					case "ECEdition":
						ECEdition = Utility.EmptyIfNull(column.Value);
						break;
					case "ECPublicationDetails":
						ECPublicationDetails = Utility.EmptyIfNull(column.Value);
						break;
					case "ECPublisherName":
						ECPublisherName = Utility.EmptyIfNull(column.Value);
						break;
					case "ECPublisherPlace":
						ECPublisherPlace = Utility.EmptyIfNull(column.Value);
						break;
					case "ECYear":
						ECYear = Utility.EmptyIfNull(column.Value);
						break;
					case "ECRights":
						ECRights = Utility.EmptyIfNull(column.Value);
						break;
					case "ECCopyrightStatus":
						ECCopyrightStatus = Utility.EmptyIfNull(column.Value);
						break;
					case "ECLicenseUrl":
						ECLicenseUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "NSGenre":
						NSGenre = Utility.EmptyIfNull(column.Value);
						break;
					case "NSTitle":
						NSTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "NSTranslatedTitle":
						NSTranslatedTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "NSJournalTitle":
						NSJournalTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "NSVolume":
						NSVolume = Utility.EmptyIfNull(column.Value);
						break;
					case "NSSeries":
						NSSeries = Utility.EmptyIfNull(column.Value);
						break;
					case "NSIssue":
						NSIssue = Utility.EmptyIfNull(column.Value);
						break;
					case "NSEdition":
						NSEdition = Utility.EmptyIfNull(column.Value);
						break;
					case "NSPublicationDetails":
						NSPublicationDetails = Utility.EmptyIfNull(column.Value);
						break;
					case "NSPublisherName":
						NSPublisherName = Utility.EmptyIfNull(column.Value);
						break;
					case "NSPublisherPlace":
						NSPublisherPlace = Utility.EmptyIfNull(column.Value);
						break;
					case "NSYear":
						NSYear = Utility.EmptyIfNull(column.Value);
						break;
					case "NSLanguage":
						NSLanguage = Utility.EmptyIfNull(column.Value);
						break;
					case "NSSummary":
						NSSummary = Utility.EmptyIfNull(column.Value);
						break;
					case "NSNotes":
						NSNotes = Utility.EmptyIfNull(column.Value);
						break;
					case "NSRights":
						NSRights = Utility.EmptyIfNull(column.Value);
						break;
					case "NSCopyrightStatus":
						NSCopyrightStatus = Utility.EmptyIfNull(column.Value);
						break;
					case "NSLicense":
						NSLicense = Utility.EmptyIfNull(column.Value);
						break;
					case "NSLicenseUrl":
						NSLicenseUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "NSPageRange":
						NSPageRange = Utility.EmptyIfNull(column.Value);
						break;
					case "NSStartPage":
						NSStartPage = Utility.EmptyIfNull(column.Value);
						break;
					case "NSStartPageID":
						NSStartPageID = (int?)column.Value;
						break;
					case "NSEndPage":
						NSEndPage = Utility.EmptyIfNull(column.Value);
						break;
					case "NSEndPageID":
						NSEndPageID = (int?)column.Value;
						break;
					case "NSUrl":
						NSUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "NSDownloadUrl":
						NSDownloadUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "NSDOI":
						NSDOI = Utility.EmptyIfNull(column.Value);
						break;
					case "NSISSN":
						NSISSN = Utility.EmptyIfNull(column.Value);
						break;
					case "NSISBN":
						NSISBN = Utility.EmptyIfNull(column.Value);
						break;
					case "NSOCLC":
						NSOCLC = Utility.EmptyIfNull(column.Value);
						break;
					case "NSLCCN":
						NSLCCN = Utility.EmptyIfNull(column.Value);
						break;
					case "NSARK":
						NSARK = Utility.EmptyIfNull(column.Value);
						break;
					case "NSBiostor":
						NSBiostor = Utility.EmptyIfNull(column.Value);
						break;
					case "NSJSTOR":
						NSJSTOR = Utility.EmptyIfNull(column.Value);
						break;
					case "NSTL2":
						NSTL2 = Utility.EmptyIfNull(column.Value);
						break;
					case "NSWikidata":
						NSWikidata = Utility.EmptyIfNull(column.Value);
						break;
					case "NSAuthors":
						NSAuthorString = Utility.EmptyIfNull(column.Value);
						break;
					case "NSKeywords":
						NSKeywordString = Utility.EmptyIfNull(column.Value);
						break;
					case "NSContributors":
						NSContributorString = Utility.EmptyIfNull(column.Value);
						break;
					case "NSPages":
						NSPageString = Utility.EmptyIfNull(column.Value);
						break;
					case "ESGenre":
						ESGenre = Utility.EmptyIfNull(column.Value);
						break;
					case "ESTitle":
						ESTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "ESTranslatedTitle":
						ESTranslatedTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "ESJournalTitle":
						ESJournalTitle = Utility.EmptyIfNull(column.Value);
						break;
					case "ESVolume":
						ESVolume = Utility.EmptyIfNull(column.Value);
						break;
					case "ESSeries":
						ESSeries = Utility.EmptyIfNull(column.Value);
						break;
					case "ESIssue":
						ESIssue = Utility.EmptyIfNull(column.Value);
						break;
					case "ESEdition":
						ESEdition = Utility.EmptyIfNull(column.Value);
						break;
					case "ESPublicationDetails":
						ESPublicationDetails = Utility.EmptyIfNull(column.Value);
						break;
					case "ESPublisherName":
						ESPublisherName = Utility.EmptyIfNull(column.Value);
						break;
					case "ESPublisherPlace":
						ESPublisherPlace = Utility.EmptyIfNull(column.Value);
						break;
					case "ESYear":
						ESYear = Utility.EmptyIfNull(column.Value);
						break;
					case "ESLanguage":
						ESLanguage = Utility.EmptyIfNull(column.Value);
						break;
					case "ESSummary":
						ESSummary = Utility.EmptyIfNull(column.Value);
						break;
					case "ESNotes":
						ESNotes = Utility.EmptyIfNull(column.Value);
						break;
					case "ESRights":
						ESRights = Utility.EmptyIfNull(column.Value);
						break;
					case "ESCopyrightStatus":
						ESCopyrightStatus = Utility.EmptyIfNull(column.Value);
						break;
					case "ESLicense":
						ESLicense = Utility.EmptyIfNull(column.Value);
						break;
					case "ESLicenseUrl":
						ESLicenseUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "ESPageRange":
						_ESPageRange = Utility.EmptyIfNull(column.Value);
						break;
					case "ESStartPage":
						ESStartPage = Utility.EmptyIfNull(column.Value);
						break;
					case "ESStartPageID":
						ESStartPageID = (int?)column.Value;
						break;
					case "ESEndPage":
						_ESEndPage = Utility.EmptyIfNull(column.Value);
						break;
					case "ESEndPageID":
						ESEndPageID = (int?)column.Value;
						break;
					case "ESUrl":
						ESUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "ESDownloadUrl":
						ESDownloadUrl = Utility.EmptyIfNull(column.Value);
						break;
					case "ESDOI":
						ESDOI = Utility.EmptyIfNull(column.Value);
						break;
					case "ESISSN":
						ESISSN = Utility.EmptyIfNull(column.Value);
						break;
					case "ESISBN":
						ESISBN = Utility.EmptyIfNull(column.Value);
						break;
					case "ESOCLC":
						ESOCLC = Utility.EmptyIfNull(column.Value);
						break;
					case "ESLCCN":
						ESLCCN = Utility.EmptyIfNull(column.Value);
						break;
					case "ESARK":
						ESARK = Utility.EmptyIfNull(column.Value);
						break;
					case "ESBiostor":
						ESBiostor = Utility.EmptyIfNull(column.Value);
						break;
					case "ESJSTOR":
						ESJSTOR = Utility.EmptyIfNull(column.Value);
						break;
					case "ESTL2":
						ESTL2 = Utility.EmptyIfNull(column.Value);
						break;
					case "ESWikidata":
						ESWikidata = Utility.EmptyIfNull(column.Value);
						break;
					case "ESAuthors":
						ESAuthorString = Utility.EmptyIfNull(column.Value);
						break;
					case "ESKeywords":
						ESKeywordString = Utility.EmptyIfNull(column.Value);
						break;
					case "ESContributors":
						ESContributorString = Utility.EmptyIfNull(column.Value);
						break;
					case "ESPages":
						ESPageString = Utility.EmptyIfNull(column.Value);
						break;
				}
			}
        }
    }
}
