
// Generated 11/20/2013 3:49:07 PM
// Do not modify the contents of this code file.
// This abstract class __OAIRecord is based upon OAIRecord.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecord : __OAIRecord
//		{
//		}
// }

#endregion How To Implement

#region Using 

using System;
using System.Data;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{	
	[Serializable]
	public abstract class __OAIRecord : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecord()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordID"></param>
		/// <param name="harvestLogID"></param>
		/// <param name="oAIIdentifier"></param>
		/// <param name="oAIDateStamp"></param>
		/// <param name="oAIStatus"></param>
		/// <param name="recordType"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="contributor"></param>
		/// <param name="date"></param>
		/// <param name="language"></param>
		/// <param name="publisher"></param>
		/// <param name="publicationPlace"></param>
		/// <param name="publicationDate"></param>
		/// <param name="edition"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="startPage"></param>
		/// <param name="endPage"></param>
		/// <param name="callNumber"></param>
		/// <param name="issn"></param>
		/// <param name="isbn"></param>
		/// <param name="lccn"></param>
		/// <param name="doi"></param>
		/// <param name="url"></param>
		/// <param name="oAIRecordStatusID"></param>
		/// <param name="productionTitleID"></param>
		/// <param name="productionItemID"></param>
		/// <param name="productionSegmentID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIRecord(int oAIRecordID, 
			int harvestLogID, 
			string oAIIdentifier, 
			string oAIDateStamp, 
			string oAIStatus, 
			string recordType, 
			string title, 
			string containerTitle, 
			string contributor, 
			string date, 
			string language, 
			string publisher, 
			string publicationPlace, 
			string publicationDate, 
			string edition, 
			string volume, 
			string issue, 
			string startPage, 
			string endPage, 
			string callNumber, 
			string issn, 
			string isbn, 
			string lccn, 
			string doi, 
			string url, 
			int oAIRecordStatusID, 
			int? productionTitleID, 
			int? productionItemID, 
			int? productionSegmentID, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_OAIRecordID = oAIRecordID;
			HarvestLogID = harvestLogID;
			OAIIdentifier = oAIIdentifier;
			OAIDateStamp = oAIDateStamp;
			OAIStatus = oAIStatus;
			RecordType = recordType;
			Title = title;
			ContainerTitle = containerTitle;
			Contributor = contributor;
			Date = date;
			Language = language;
			Publisher = publisher;
			PublicationPlace = publicationPlace;
			PublicationDate = publicationDate;
			Edition = edition;
			Volume = volume;
			Issue = issue;
			StartPage = startPage;
			EndPage = endPage;
			CallNumber = callNumber;
			Issn = issn;
			Isbn = isbn;
			Lccn = lccn;
			Doi = doi;
			Url = url;
			OAIRecordStatusID = oAIRecordStatusID;
			ProductionTitleID = productionTitleID;
			ProductionItemID = productionItemID;
			ProductionSegmentID = productionSegmentID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecord()
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
					case "OAIRecordID" :
					{
						_OAIRecordID = (int)column.Value;
						break;
					}
					case "HarvestLogID" :
					{
						_HarvestLogID = (int)column.Value;
						break;
					}
					case "OAIIdentifier" :
					{
						_OAIIdentifier = (string)column.Value;
						break;
					}
					case "OAIDateStamp" :
					{
						_OAIDateStamp = (string)column.Value;
						break;
					}
					case "OAIStatus" :
					{
						_OAIStatus = (string)column.Value;
						break;
					}
					case "RecordType" :
					{
						_RecordType = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "ContainerTitle" :
					{
						_ContainerTitle = (string)column.Value;
						break;
					}
					case "Contributor" :
					{
						_Contributor = (string)column.Value;
						break;
					}
					case "Date" :
					{
						_Date = (string)column.Value;
						break;
					}
					case "Language" :
					{
						_Language = (string)column.Value;
						break;
					}
					case "Publisher" :
					{
						_Publisher = (string)column.Value;
						break;
					}
					case "PublicationPlace" :
					{
						_PublicationPlace = (string)column.Value;
						break;
					}
					case "PublicationDate" :
					{
						_PublicationDate = (string)column.Value;
						break;
					}
					case "Edition" :
					{
						_Edition = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "Issue" :
					{
						_Issue = (string)column.Value;
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
					case "CallNumber" :
					{
						_CallNumber = (string)column.Value;
						break;
					}
					case "Issn" :
					{
						_Issn = (string)column.Value;
						break;
					}
					case "Isbn" :
					{
						_Isbn = (string)column.Value;
						break;
					}
					case "Lccn" :
					{
						_Lccn = (string)column.Value;
						break;
					}
					case "Doi" :
					{
						_Doi = (string)column.Value;
						break;
					}
					case "Url" :
					{
						_Url = (string)column.Value;
						break;
					}
					case "OAIRecordStatusID" :
					{
						_OAIRecordStatusID = (int)column.Value;
						break;
					}
					case "ProductionTitleID" :
					{
						_ProductionTitleID = (int?)column.Value;
						break;
					}
					case "ProductionItemID" :
					{
						_ProductionItemID = (int?)column.Value;
						break;
					}
					case "ProductionSegmentID" :
					{
						_ProductionSegmentID = (int?)column.Value;
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
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region OAIRecordID
		
		private int _OAIRecordID = default(int);
		
		/// <summary>
		/// Column: OAIRecordID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int OAIRecordID
		{
			get
			{
				return _OAIRecordID;
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
		
		#endregion OAIRecordID
		
		#region HarvestLogID
		
		private int _HarvestLogID = default(int);
		
		/// <summary>
		/// Column: HarvestLogID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("HarvestLogID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int HarvestLogID
		{
			get
			{
				return _HarvestLogID;
			}
			set
			{
				if (_HarvestLogID != value)
				{
					_HarvestLogID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestLogID
		
		#region OAIIdentifier
		
		private string _OAIIdentifier = string.Empty;
		
		/// <summary>
		/// Column: OAIIdentifier;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("OAIIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string OAIIdentifier
		{
			get
			{
				return _OAIIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_OAIIdentifier != value)
				{
					_OAIIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OAIIdentifier
		
		#region OAIDateStamp
		
		private string _OAIDateStamp = string.Empty;
		
		/// <summary>
		/// Column: OAIDateStamp;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("OAIDateStamp", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=30)]
		public string OAIDateStamp
		{
			get
			{
				return _OAIDateStamp;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_OAIDateStamp != value)
				{
					_OAIDateStamp = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OAIDateStamp
		
		#region OAIStatus
		
		private string _OAIStatus = string.Empty;
		
		/// <summary>
		/// Column: OAIStatus;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("OAIStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=20)]
		public string OAIStatus
		{
			get
			{
				return _OAIStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_OAIStatus != value)
				{
					_OAIStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OAIStatus
		
		#region RecordType
		
		private string _RecordType = string.Empty;
		
		/// <summary>
		/// Column: RecordType;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("RecordType", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=20)]
		public string RecordType
		{
			get
			{
				return _RecordType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_RecordType != value)
				{
					_RecordType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RecordType
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=2000)]
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
		
		#region ContainerTitle
		
		private string _ContainerTitle = string.Empty;
		
		/// <summary>
		/// Column: ContainerTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("ContainerTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=2000)]
		public string ContainerTitle
		{
			get
			{
				return _ContainerTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_ContainerTitle != value)
				{
					_ContainerTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContainerTitle
		
		#region Contributor
		
		private string _Contributor = string.Empty;
		
		/// <summary>
		/// Column: Contributor;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Contributor", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=200)]
		public string Contributor
		{
			get
			{
				return _Contributor;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Contributor != value)
				{
					_Contributor = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Contributor
		
		#region Date
		
		private string _Date = string.Empty;
		
		/// <summary>
		/// Column: Date;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Date", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=20)]
		public string Date
		{
			get
			{
				return _Date;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Date != value)
				{
					_Date = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Date
		
		#region Language
		
		private string _Language = string.Empty;
		
		/// <summary>
		/// Column: Language;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("Language", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=30)]
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
		
		#region Publisher
		
		private string _Publisher = string.Empty;
		
		/// <summary>
		/// Column: Publisher;
		/// DBMS data type: nvarchar(250);
		/// </summary>
		[ColumnDefinition("Publisher", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=250)]
		public string Publisher
		{
			get
			{
				return _Publisher;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 250);
				if (_Publisher != value)
				{
					_Publisher = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Publisher
		
		#region PublicationPlace
		
		private string _PublicationPlace = string.Empty;
		
		/// <summary>
		/// Column: PublicationPlace;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("PublicationPlace", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=150)]
		public string PublicationPlace
		{
			get
			{
				return _PublicationPlace;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_PublicationPlace != value)
				{
					_PublicationPlace = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicationPlace
		
		#region PublicationDate
		
		private string _PublicationDate = string.Empty;
		
		/// <summary>
		/// Column: PublicationDate;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("PublicationDate", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=100)]
		public string PublicationDate
		{
			get
			{
				return _PublicationDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_PublicationDate != value)
				{
					_PublicationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicationDate
		
		#region Edition
		
		private string _Edition = string.Empty;
		
		/// <summary>
		/// Column: Edition;
		/// DBMS data type: nvarchar(450);
		/// </summary>
		[ColumnDefinition("Edition", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=450)]
		public string Edition
		{
			get
			{
				return _Edition;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 450);
				if (_Edition != value)
				{
					_Edition = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Edition
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=100)]
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
		
		#region Issue
		
		private string _Issue = string.Empty;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=100)]
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
		
		#region StartPage
		
		private string _StartPage = string.Empty;
		
		/// <summary>
		/// Column: StartPage;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("StartPage", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=20)]
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
		[ColumnDefinition("EndPage", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=20)]
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
		
		#region CallNumber
		
		private string _CallNumber = string.Empty;
		
		/// <summary>
		/// Column: CallNumber;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=100)]
		public string CallNumber
		{
			get
			{
				return _CallNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_CallNumber != value)
				{
					_CallNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CallNumber
		
		#region Issn
		
		private string _Issn = string.Empty;
		
		/// <summary>
		/// Column: Issn;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("Issn", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=125)]
		public string Issn
		{
			get
			{
				return _Issn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Issn != value)
				{
					_Issn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Issn
		
		#region Isbn
		
		private string _Isbn = string.Empty;
		
		/// <summary>
		/// Column: Isbn;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("Isbn", DbTargetType=SqlDbType.NVarChar, Ordinal=22, CharacterMaxLength=125)]
		public string Isbn
		{
			get
			{
				return _Isbn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Isbn != value)
				{
					_Isbn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Isbn
		
		#region Lccn
		
		private string _Lccn = string.Empty;
		
		/// <summary>
		/// Column: Lccn;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("Lccn", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=125)]
		public string Lccn
		{
			get
			{
				return _Lccn;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Lccn != value)
				{
					_Lccn = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Lccn
		
		#region Doi
		
		private string _Doi = string.Empty;
		
		/// <summary>
		/// Column: Doi;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Doi", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=50)]
		public string Doi
		{
			get
			{
				return _Doi;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Doi != value)
				{
					_Doi = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Doi
		
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
		
		#region OAIRecordStatusID
		
		private int _OAIRecordStatusID = default(int);
		
		/// <summary>
		/// Column: OAIRecordStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("OAIRecordStatusID", DbTargetType=SqlDbType.Int, Ordinal=26, NumericPrecision=10, IsInForeignKey=true)]
		public int OAIRecordStatusID
		{
			get
			{
				return _OAIRecordStatusID;
			}
			set
			{
				if (_OAIRecordStatusID != value)
				{
					_OAIRecordStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OAIRecordStatusID
		
		#region ProductionTitleID
		
		private int? _ProductionTitleID = null;
		
		/// <summary>
		/// Column: ProductionTitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionTitleID", DbTargetType=SqlDbType.Int, Ordinal=27, NumericPrecision=10, IsNullable=true)]
		public int? ProductionTitleID
		{
			get
			{
				return _ProductionTitleID;
			}
			set
			{
				if (_ProductionTitleID != value)
				{
					_ProductionTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionTitleID
		
		#region ProductionItemID
		
		private int? _ProductionItemID = null;
		
		/// <summary>
		/// Column: ProductionItemID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionItemID", DbTargetType=SqlDbType.Int, Ordinal=28, NumericPrecision=10, IsNullable=true)]
		public int? ProductionItemID
		{
			get
			{
				return _ProductionItemID;
			}
			set
			{
				if (_ProductionItemID != value)
				{
					_ProductionItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionItemID
		
		#region ProductionSegmentID
		
		private int? _ProductionSegmentID = null;
		
		/// <summary>
		/// Column: ProductionSegmentID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionSegmentID", DbTargetType=SqlDbType.Int, Ordinal=29, NumericPrecision=10, IsNullable=true)]
		public int? ProductionSegmentID
		{
			get
			{
				return _ProductionSegmentID;
			}
			set
			{
				if (_ProductionSegmentID != value)
				{
					_ProductionSegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionSegmentID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=30)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=31)]
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecord"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecord"/>, 
		/// returns an instance of <see cref="__OAIRecord"/>; otherwise returns null.</returns>
		public static new __OAIRecord FromArray(byte[] byteArray)
		{
			__OAIRecord o = null;
			
			try
			{
				o = (__OAIRecord) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecord"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecord"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecord)
			{
				__OAIRecord o = (__OAIRecord) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordID == OAIRecordID &&
					o.HarvestLogID == HarvestLogID &&
					GetComparisonString(o.OAIIdentifier) == GetComparisonString(OAIIdentifier) &&
					GetComparisonString(o.OAIDateStamp) == GetComparisonString(OAIDateStamp) &&
					GetComparisonString(o.OAIStatus) == GetComparisonString(OAIStatus) &&
					GetComparisonString(o.RecordType) == GetComparisonString(RecordType) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.ContainerTitle) == GetComparisonString(ContainerTitle) &&
					GetComparisonString(o.Contributor) == GetComparisonString(Contributor) &&
					GetComparisonString(o.Date) == GetComparisonString(Date) &&
					GetComparisonString(o.Language) == GetComparisonString(Language) &&
					GetComparisonString(o.Publisher) == GetComparisonString(Publisher) &&
					GetComparisonString(o.PublicationPlace) == GetComparisonString(PublicationPlace) &&
					GetComparisonString(o.PublicationDate) == GetComparisonString(PublicationDate) &&
					GetComparisonString(o.Edition) == GetComparisonString(Edition) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.StartPage) == GetComparisonString(StartPage) &&
					GetComparisonString(o.EndPage) == GetComparisonString(EndPage) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					GetComparisonString(o.Issn) == GetComparisonString(Issn) &&
					GetComparisonString(o.Isbn) == GetComparisonString(Isbn) &&
					GetComparisonString(o.Lccn) == GetComparisonString(Lccn) &&
					GetComparisonString(o.Doi) == GetComparisonString(Doi) &&
					GetComparisonString(o.Url) == GetComparisonString(Url) &&
					o.OAIRecordStatusID == OAIRecordStatusID &&
					o.ProductionTitleID == ProductionTitleID &&
					o.ProductionItemID == ProductionItemID &&
					o.ProductionSegmentID == ProductionSegmentID &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate 
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
				throw new ArgumentException("Argument is not of type __OAIRecord");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecord"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecord"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecord a, __OAIRecord b)
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
		/// <param name="a">The first <see cref="__OAIRecord"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecord"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecord a, __OAIRecord b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecord"/> object to compare with the current <see cref="__OAIRecord"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecord))
			{
				return false;
			}
			
			return this == (__OAIRecord) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecord.SortColumn.OAIRecordID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordID = "OAIRecordID";	
			public const string HarvestLogID = "HarvestLogID";	
			public const string OAIIdentifier = "OAIIdentifier";	
			public const string OAIDateStamp = "OAIDateStamp";	
			public const string OAIStatus = "OAIStatus";	
			public const string RecordType = "RecordType";	
			public const string Title = "Title";	
			public const string ContainerTitle = "ContainerTitle";	
			public const string Contributor = "Contributor";	
			public const string Date = "Date";	
			public const string Language = "Language";	
			public const string Publisher = "Publisher";	
			public const string PublicationPlace = "PublicationPlace";	
			public const string PublicationDate = "PublicationDate";	
			public const string Edition = "Edition";	
			public const string Volume = "Volume";	
			public const string Issue = "Issue";	
			public const string StartPage = "StartPage";	
			public const string EndPage = "EndPage";	
			public const string CallNumber = "CallNumber";	
			public const string Issn = "Issn";	
			public const string Isbn = "Isbn";	
			public const string Lccn = "Lccn";	
			public const string Doi = "Doi";	
			public const string Url = "Url";	
			public const string OAIRecordStatusID = "OAIRecordStatusID";	
			public const string ProductionTitleID = "ProductionTitleID";	
			public const string ProductionItemID = "ProductionItemID";	
			public const string ProductionSegmentID = "ProductionSegmentID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
