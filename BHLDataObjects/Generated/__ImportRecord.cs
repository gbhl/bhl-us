
// Generated 1/15/2014 9:26:48 AM
// Do not modify the contents of this code file.
// This abstract class __ImportRecord is based upon ImportRecord.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ImportRecord : __ImportRecord
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{	
	[Serializable]
	public abstract class __ImportRecord : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ImportRecord()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="importRecordID"></param>
		/// <param name="importFileID"></param>
		/// <param name="importRecordStatusID"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="journalTitle"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="year"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="language"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="license"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="dOI"></param>
		/// <param name="iSSN"></param>
		/// <param name="iSBN"></param>
		/// <param name="oCLC"></param>
		/// <param name="lCCN"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __ImportRecord(int importRecordID, 
			int importFileID, 
			int importRecordStatusID, 
			string genre, 
			string title, 
			string translatedTitle, 
			string journalTitle, 
			string volume, 
			string series, 
			string issue, 
			string publicationDetails, 
			string publisherName, 
			string publisherPlace, 
			string year, 
			short? startYear, 
			short? endYear, 
			string language, 
			string rights, 
			string dueDiligence, 
			string copyrightStatus, 
			string license, 
			string licenseUrl, 
			string startPage, 
			string endPage, 
			string url, 
			string downloadUrl, 
			string dOI, 
			string iSSN, 
			string iSBN, 
			string oCLC, 
			string lCCN, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_ImportRecordID = importRecordID;
			ImportFileID = importFileID;
			ImportRecordStatusID = importRecordStatusID;
			Genre = genre;
			Title = title;
			TranslatedTitle = translatedTitle;
			JournalTitle = journalTitle;
			Volume = volume;
			Series = series;
			Issue = issue;
			PublicationDetails = publicationDetails;
			PublisherName = publisherName;
			PublisherPlace = publisherPlace;
			Year = year;
			StartYear = startYear;
			EndYear = endYear;
			Language = language;
			Rights = rights;
			DueDiligence = dueDiligence;
			CopyrightStatus = copyrightStatus;
			License = license;
			LicenseUrl = licenseUrl;
			StartPage = startPage;
			EndPage = endPage;
			Url = url;
			DownloadUrl = downloadUrl;
			DOI = dOI;
			ISSN = iSSN;
			ISBN = iSBN;
			OCLC = oCLC;
			LCCN = lCCN;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ImportRecord()
		{
		}
		
		#endregion Destructor
		
		#region Set Values
		
		/// <summary>
		/// Set the property values of this instance from the specified <see cref="CustomDataRow"/>.
		/// </summary>
		public virtual void SetValues(CustomDataRow row)
		{
			foreach (CustomDataColumn column in row)
			{
				switch (column.Name)
				{
					case "ImportRecordID" :
					{
						_ImportRecordID = (int)column.Value;
						break;
					}
					case "ImportFileID" :
					{
						_ImportFileID = (int)column.Value;
						break;
					}
					case "ImportRecordStatusID" :
					{
						_ImportRecordStatusID = (int)column.Value;
						break;
					}
					case "Genre" :
					{
						_Genre = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "TranslatedTitle" :
					{
						_TranslatedTitle = (string)column.Value;
						break;
					}
					case "JournalTitle" :
					{
						_JournalTitle = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "Series" :
					{
						_Series = (string)column.Value;
						break;
					}
					case "Issue" :
					{
						_Issue = (string)column.Value;
						break;
					}
					case "PublicationDetails" :
					{
						_PublicationDetails = (string)column.Value;
						break;
					}
					case "PublisherName" :
					{
						_PublisherName = (string)column.Value;
						break;
					}
					case "PublisherPlace" :
					{
						_PublisherPlace = (string)column.Value;
						break;
					}
					case "Year" :
					{
						_Year = (string)column.Value;
						break;
					}
					case "StartYear" :
					{
						_StartYear = (short?)column.Value;
						break;
					}
					case "EndYear" :
					{
						_EndYear = (short?)column.Value;
						break;
					}
					case "Language" :
					{
						_Language = (string)column.Value;
						break;
					}
					case "Rights" :
					{
						_Rights = (string)column.Value;
						break;
					}
					case "DueDiligence" :
					{
						_DueDiligence = (string)column.Value;
						break;
					}
					case "CopyrightStatus" :
					{
						_CopyrightStatus = (string)column.Value;
						break;
					}
					case "License" :
					{
						_License = (string)column.Value;
						break;
					}
					case "LicenseUrl" :
					{
						_LicenseUrl = (string)column.Value;
						break;
					}
					case "StartPage" :
					{
						_StartPage = (string)column.Value;
						break;
					}
					case "EndPage" :
					{
						_EndPage = (string)column.Value;
						break;
					}
					case "Url" :
					{
						_Url = (string)column.Value;
						break;
					}
					case "DownloadUrl" :
					{
						_DownloadUrl = (string)column.Value;
						break;
					}
					case "DOI" :
					{
						_DOI = (string)column.Value;
						break;
					}
					case "ISSN" :
					{
						_ISSN = (string)column.Value;
						break;
					}
					case "ISBN" :
					{
						_ISBN = (string)column.Value;
						break;
					}
					case "OCLC" :
					{
						_OCLC = (string)column.Value;
						break;
					}
					case "LCCN" :
					{
						_LCCN = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region ImportRecordID
		
		private int _ImportRecordID = default(int);
		
		/// <summary>
		/// Column: ImportRecordID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ImportRecordID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ImportRecordID
		{
			get
			{
				return _ImportRecordID;
			}
			set
			{
				// NOTE: This dummy setter provides a work-around for the following: Read-only properties cannot be exposed by XML Web Services
				// see http://support.microsoft.com/kb/313584
				// Cause: When an object is passed i.e. marshalled to or from a Web service, it must be serialized into an XML stream and then deserialized back into an object.
				// The XML Serializer cannot deserialize the XML back into an object because it cannot load the read-only properties. 
				// Thus the read-only properties are not exposed through the Web Services Description Language (WSDL). 
				// Because the Web service proxy is generated from the WSDL, the proxy also excludes any read-only properties.
			}
		}
		
		#endregion ImportRecordID
		
		#region ImportFileID
		
		private int _ImportFileID = default(int);
		
		/// <summary>
		/// Column: ImportFileID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportFileID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportFileID
		{
			get
			{
				return _ImportFileID;
			}
			set
			{
				if (_ImportFileID != value)
				{
					_ImportFileID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportFileID
		
		#region ImportRecordStatusID
		
		private int _ImportRecordStatusID = default(int);
		
		/// <summary>
		/// Column: ImportRecordStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportRecordStatusID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportRecordStatusID
		{
			get
			{
				return _ImportRecordStatusID;
			}
			set
			{
				if (_ImportRecordStatusID != value)
				{
					_ImportRecordStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportRecordStatusID
		
		#region Genre
		
		private string _Genre = string.Empty;
		
		/// <summary>
		/// Column: Genre;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Genre", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string Genre
		{
			get
			{
				return _Genre;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Genre != value)
				{
					_Genre = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Genre
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=2000)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region TranslatedTitle
		
		private string _TranslatedTitle = string.Empty;
		
		/// <summary>
		/// Column: TranslatedTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("TranslatedTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=2000)]
		public string TranslatedTitle
		{
			get
			{
				return _TranslatedTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_TranslatedTitle != value)
				{
					_TranslatedTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TranslatedTitle
		
		#region JournalTitle
		
		private string _JournalTitle = string.Empty;
		
		/// <summary>
		/// Column: JournalTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("JournalTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=2000)]
		public string JournalTitle
		{
			get
			{
				return _JournalTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_JournalTitle != value)
				{
					_JournalTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion JournalTitle
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=100)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region Series
		
		private string _Series = string.Empty;
		
		/// <summary>
		/// Column: Series;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Series", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=100)]
		public string Series
		{
			get
			{
				return _Series;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Series != value)
				{
					_Series = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Series
		
		#region Issue
		
		private string _Issue = string.Empty;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=100)]
		public string Issue
		{
			get
			{
				return _Issue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Issue != value)
				{
					_Issue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Issue
		
		#region PublicationDetails
		
		private string _PublicationDetails = string.Empty;
		
		/// <summary>
		/// Column: PublicationDetails;
		/// DBMS data type: nvarchar(400);
		/// </summary>
		[ColumnDefinition("PublicationDetails", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=400)]
		public string PublicationDetails
		{
			get
			{
				return _PublicationDetails;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 400);
				if (_PublicationDetails != value)
				{
					_PublicationDetails = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicationDetails
		
		#region PublisherName
		
		private string _PublisherName = string.Empty;
		
		/// <summary>
		/// Column: PublisherName;
		/// DBMS data type: nvarchar(250);
		/// </summary>
		[ColumnDefinition("PublisherName", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=250)]
		public string PublisherName
		{
			get
			{
				return _PublisherName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 250);
				if (_PublisherName != value)
				{
					_PublisherName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublisherName
		
		#region PublisherPlace
		
		private string _PublisherPlace = string.Empty;
		
		/// <summary>
		/// Column: PublisherPlace;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("PublisherPlace", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=150)]
		public string PublisherPlace
		{
			get
			{
				return _PublisherPlace;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_PublisherPlace != value)
				{
					_PublisherPlace = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublisherPlace
		
		#region Year
		
		private string _Year = string.Empty;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=20)]
		public string Year
		{
			get
			{
				return _Year;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Year != value)
				{
					_Year = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Year
		
		#region StartYear
		
		private short? _StartYear = null;
		
		/// <summary>
		/// Column: StartYear;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("StartYear", DbTargetType=SqlDbType.SmallInt, Ordinal=15, NumericPrecision=5, IsNullable=true)]
		public short? StartYear
		{
			get
			{
				return _StartYear;
			}
			set
			{
				if (_StartYear != value)
				{
					_StartYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartYear
		
		#region EndYear
		
		private short? _EndYear = null;
		
		/// <summary>
		/// Column: EndYear;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("EndYear", DbTargetType=SqlDbType.SmallInt, Ordinal=16, NumericPrecision=5, IsNullable=true)]
		public short? EndYear
		{
			get
			{
				return _EndYear;
			}
			set
			{
				if (_EndYear != value)
				{
					_EndYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndYear
		
		#region Language
		
		private string _Language = null;
		
		/// <summary>
		/// Column: Language;
		/// DBMS data type: nvarchar(30); Nullable;
		/// </summary>
		[ColumnDefinition("Language", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=30, IsNullable=true)]
		public string Language
		{
			get
			{
				return _Language;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_Language != value)
				{
					_Language = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Language
		
		#region Rights
		
		private string _Rights = string.Empty;
		
		/// <summary>
		/// Column: Rights;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Rights", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=1073741823)]
		public string Rights
		{
			get
			{
				return _Rights;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Rights != value)
				{
					_Rights = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Rights
		
		#region DueDiligence
		
		private string _DueDiligence = string.Empty;
		
		/// <summary>
		/// Column: DueDiligence;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("DueDiligence", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=1073741823)]
		public string DueDiligence
		{
			get
			{
				return _DueDiligence;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_DueDiligence != value)
				{
					_DueDiligence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DueDiligence
		
		#region CopyrightStatus
		
		private string _CopyrightStatus = string.Empty;
		
		/// <summary>
		/// Column: CopyrightStatus;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CopyrightStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=1073741823)]
		public string CopyrightStatus
		{
			get
			{
				return _CopyrightStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CopyrightStatus != value)
				{
					_CopyrightStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightStatus
		
		#region License
		
		private string _License = string.Empty;
		
		/// <summary>
		/// Column: License;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("License", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=1073741823)]
		public string License
		{
			get
			{
				return _License;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_License != value)
				{
					_License = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion License
		
		#region LicenseUrl
		
		private string _LicenseUrl = string.Empty;
		
		/// <summary>
		/// Column: LicenseUrl;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("LicenseUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=22, CharacterMaxLength=200)]
		public string LicenseUrl
		{
			get
			{
				return _LicenseUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_LicenseUrl != value)
				{
					_LicenseUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LicenseUrl
		
		#region StartPage
		
		private string _StartPage = string.Empty;
		
		/// <summary>
		/// Column: StartPage;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("StartPage", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=20)]
		public string StartPage
		{
			get
			{
				return _StartPage;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_StartPage != value)
				{
					_StartPage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartPage
		
		#region EndPage
		
		private string _EndPage = string.Empty;
		
		/// <summary>
		/// Column: EndPage;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("EndPage", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=20)]
		public string EndPage
		{
			get
			{
				return _EndPage;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_EndPage != value)
				{
					_EndPage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndPage
		
		#region Url
		
		private string _Url = string.Empty;
		
		/// <summary>
		/// Column: Url;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Url", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=200)]
		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Url != value)
				{
					_Url = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Url
		
		#region DownloadUrl
		
		private string _DownloadUrl = string.Empty;
		
		/// <summary>
		/// Column: DownloadUrl;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("DownloadUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=26, CharacterMaxLength=200)]
		public string DownloadUrl
		{
			get
			{
				return _DownloadUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_DownloadUrl != value)
				{
					_DownloadUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DownloadUrl
		
		#region DOI
		
		private string _DOI = string.Empty;
		
		/// <summary>
		/// Column: DOI;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("DOI", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=125)]
		public string DOI
		{
			get
			{
				return _DOI;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_DOI != value)
				{
					_DOI = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOI
		
		#region ISSN
		
		private string _ISSN = string.Empty;
		
		/// <summary>
		/// Column: ISSN;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("ISSN", DbTargetType=SqlDbType.NVarChar, Ordinal=28, CharacterMaxLength=125)]
		public string ISSN
		{
			get
			{
				return _ISSN;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_ISSN != value)
				{
					_ISSN = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ISSN
		
		#region ISBN
		
		private string _ISBN = string.Empty;
		
		/// <summary>
		/// Column: ISBN;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("ISBN", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=125)]
		public string ISBN
		{
			get
			{
				return _ISBN;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_ISBN != value)
				{
					_ISBN = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ISBN
		
		#region OCLC
		
		private string _OCLC = string.Empty;
		
		/// <summary>
		/// Column: OCLC;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("OCLC", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=125)]
		public string OCLC
		{
			get
			{
				return _OCLC;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_OCLC != value)
				{
					_OCLC = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OCLC
		
		#region LCCN
		
		private string _LCCN = string.Empty;
		
		/// <summary>
		/// Column: LCCN;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("LCCN", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=125)]
		public string LCCN
		{
			get
			{
				return _LCCN;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_LCCN != value)
				{
					_LCCN = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LCCN
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=32)]
		public DateTime CreationDate
		{
			get
			{
				return _CreationDate;
			}
			set
			{
				if (_CreationDate != value)
				{
					_CreationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreationDate
		
		#region LastModifiedDate
		
		private DateTime _LastModifiedDate;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=33)]
		public DateTime LastModifiedDate
		{
			get
			{
				return _LastModifiedDate;
			}
			set
			{
				if (_LastModifiedDate != value)
				{
					_LastModifiedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifiedDate
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=34, NumericPrecision=10)]
		public int CreationUserID
		{
			get
			{
				return _CreationUserID;
			}
			set
			{
				if (_CreationUserID != value)
				{
					_CreationUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreationUserID
		
		#region LastModifiedUserID
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=35, NumericPrecision=10)]
		public int LastModifiedUserID
		{
			get
			{
				return _LastModifiedUserID;
			}
			set
			{
				if (_LastModifiedUserID != value)
				{
					_LastModifiedUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifiedUserID
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ImportRecord"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ImportRecord"/>, 
		/// returns an instance of <see cref="__ImportRecord"/>; otherwise returns null.</returns>
		public static new __ImportRecord FromArray(byte[] byteArray)
		{
			__ImportRecord o = null;
			
			try
			{
				o = (__ImportRecord) CustomObjectBase.FromArray(byteArray);
			}
			catch (Exception e)
			{
				throw e;
			}

			return o;
		}
		
		#endregion From Array serialization

		#region CompareTo
		
		/// <summary>
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ImportRecord"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ImportRecord"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ImportRecord)
			{
				__ImportRecord o = (__ImportRecord) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ImportRecordID == ImportRecordID &&
					o.ImportFileID == ImportFileID &&
					o.ImportRecordStatusID == ImportRecordStatusID &&
					GetComparisonString(o.Genre) == GetComparisonString(Genre) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.TranslatedTitle) == GetComparisonString(TranslatedTitle) &&
					GetComparisonString(o.JournalTitle) == GetComparisonString(JournalTitle) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Series) == GetComparisonString(Series) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.PublicationDetails) == GetComparisonString(PublicationDetails) &&
					GetComparisonString(o.PublisherName) == GetComparisonString(PublisherName) &&
					GetComparisonString(o.PublisherPlace) == GetComparisonString(PublisherPlace) &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					o.StartYear == StartYear &&
					o.EndYear == EndYear &&
					GetComparisonString(o.Language) == GetComparisonString(Language) &&
					GetComparisonString(o.Rights) == GetComparisonString(Rights) &&
					GetComparisonString(o.DueDiligence) == GetComparisonString(DueDiligence) &&
					GetComparisonString(o.CopyrightStatus) == GetComparisonString(CopyrightStatus) &&
					GetComparisonString(o.License) == GetComparisonString(License) &&
					GetComparisonString(o.LicenseUrl) == GetComparisonString(LicenseUrl) &&
					GetComparisonString(o.StartPage) == GetComparisonString(StartPage) &&
					GetComparisonString(o.EndPage) == GetComparisonString(EndPage) &&
					GetComparisonString(o.Url) == GetComparisonString(Url) &&
					GetComparisonString(o.DownloadUrl) == GetComparisonString(DownloadUrl) &&
					GetComparisonString(o.DOI) == GetComparisonString(DOI) &&
					GetComparisonString(o.ISSN) == GetComparisonString(ISSN) &&
					GetComparisonString(o.ISBN) == GetComparisonString(ISBN) &&
					GetComparisonString(o.OCLC) == GetComparisonString(OCLC) &&
					GetComparisonString(o.LCCN) == GetComparisonString(LCCN) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID 
				)
				{
					o = null;
					return 0; // true
				}
				else
				{
					o = null;
					return -1; // false
				}
			}
			else
			{
				throw new ArgumentException("Argument is not of type __ImportRecord");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportRecord"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportRecord"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ImportRecord a, __ImportRecord b)
		{
			if (((Object) a) == null || ((Object) b) == null)
			{
				if (((Object) a) == null && ((Object) b) == null)
				{
					return true;
				}
			}
			else
			{
				int r = a.CompareTo(b);
				
				if (r == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Inequality operator (!=) returns false if its operands are equal, true otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportRecord"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportRecord"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ImportRecord a, __ImportRecord b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ImportRecord"/> object to compare with the current <see cref="__ImportRecord"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ImportRecord))
			{
				return false;
			}
			
			return this == (__ImportRecord) obj;
		}
	
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance as an integer.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
		#endregion Operators
		
		#region SortColumn
		
		/// <summary>
		/// Use when defining sort columns for a collection sort request.
		/// For example where list is a instance of <see cref="CustomGenericList">, 
		/// list.Sort(SortOrder.Ascending, __ImportRecord.SortColumn.ImportRecordID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ImportRecordID = "ImportRecordID";	
			public const string ImportFileID = "ImportFileID";	
			public const string ImportRecordStatusID = "ImportRecordStatusID";	
			public const string Genre = "Genre";	
			public const string Title = "Title";	
			public const string TranslatedTitle = "TranslatedTitle";	
			public const string JournalTitle = "JournalTitle";	
			public const string Volume = "Volume";	
			public const string Series = "Series";	
			public const string Issue = "Issue";	
			public const string PublicationDetails = "PublicationDetails";	
			public const string PublisherName = "PublisherName";	
			public const string PublisherPlace = "PublisherPlace";	
			public const string Year = "Year";	
			public const string StartYear = "StartYear";	
			public const string EndYear = "EndYear";	
			public const string Language = "Language";	
			public const string Rights = "Rights";	
			public const string DueDiligence = "DueDiligence";	
			public const string CopyrightStatus = "CopyrightStatus";	
			public const string License = "License";	
			public const string LicenseUrl = "LicenseUrl";	
			public const string StartPage = "StartPage";	
			public const string EndPage = "EndPage";	
			public const string Url = "Url";	
			public const string DownloadUrl = "DownloadUrl";	
			public const string DOI = "DOI";	
			public const string ISSN = "ISSN";	
			public const string ISBN = "ISBN";	
			public const string OCLC = "OCLC";	
			public const string LCCN = "LCCN";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
