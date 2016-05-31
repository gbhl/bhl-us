
// Generated 6/2/2016 9:32:20 AM
// Do not modify the contents of this code file.
// This abstract class __Item is based upon dbo.Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Item : __Item
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
	public abstract class __Item : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Item()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="primaryTitleID"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="languageCode"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="note"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="redirectItemID"></param>
		/// <param name="externalUrl"></param>
		/// <param name="endYear"></param>
		/// <param name="startVolume"></param>
		/// <param name="endVolume"></param>
		/// <param name="startIssue"></param>
		/// <param name="endIssue"></param>
		/// <param name="startNumber"></param>
		/// <param name="endNumber"></param>
		/// <param name="startSeries"></param>
		/// <param name="endSeries"></param>
		/// <param name="startPart"></param>
		/// <param name="endPart"></param>
		/// <param name="volumeReviewed"></param>
		/// <param name="volumeReviewedDate"></param>
		/// <param name="volumeReviewedUserID"></param>
		public __Item(int itemID, 
			int primaryTitleID, 
			string barCode, 
			string mARCItemID, 
			string callNumber, 
			string volume, 
			string languageCode, 
			string itemDescription, 
			int? scannedBy, 
			int? pDFSize, 
			int? vaultID, 
			string note, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID, 
			int itemStatusID, 
			string scanningUser, 
			DateTime? scanningDate, 
			int? paginationCompleteUserID, 
			DateTime? paginationCompleteDate, 
			int? paginationStatusID, 
			int? paginationStatusUserID, 
			DateTime? paginationStatusDate, 
			DateTime? lastPageNameLookupDate, 
			int? itemSourceID, 
			string year, 
			string identifierBib, 
			string fileRootFolder, 
			string zQuery, 
			string sponsor, 
			string licenseUrl, 
			string rights, 
			string dueDiligence, 
			string copyrightStatus, 
			string copyrightRegion, 
			string copyrightComment, 
			string copyrightEvidence, 
			string copyrightEvidenceOperator, 
			string copyrightEvidenceDate, 
			int? thumbnailPageID, 
			int? redirectItemID, 
			string externalUrl, 
			string endYear, 
			string startVolume, 
			string endVolume, 
			string startIssue, 
			string endIssue, 
			string startNumber, 
			string endNumber, 
			string startSeries, 
			string endSeries, 
			string startPart, 
			string endPart, 
			byte volumeReviewed, 
			DateTime? volumeReviewedDate, 
			int? volumeReviewedUserID) : this()
		{
			_ItemID = itemID;
			PrimaryTitleID = primaryTitleID;
			BarCode = barCode;
			MARCItemID = mARCItemID;
			CallNumber = callNumber;
			Volume = volume;
			LanguageCode = languageCode;
			ItemDescription = itemDescription;
			ScannedBy = scannedBy;
			PDFSize = pDFSize;
			VaultID = vaultID;
			Note = note;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
			ItemStatusID = itemStatusID;
			ScanningUser = scanningUser;
			ScanningDate = scanningDate;
			PaginationCompleteUserID = paginationCompleteUserID;
			PaginationCompleteDate = paginationCompleteDate;
			PaginationStatusID = paginationStatusID;
			PaginationStatusUserID = paginationStatusUserID;
			PaginationStatusDate = paginationStatusDate;
			LastPageNameLookupDate = lastPageNameLookupDate;
			ItemSourceID = itemSourceID;
			Year = year;
			IdentifierBib = identifierBib;
			FileRootFolder = fileRootFolder;
			ZQuery = zQuery;
			Sponsor = sponsor;
			LicenseUrl = licenseUrl;
			Rights = rights;
			DueDiligence = dueDiligence;
			CopyrightStatus = copyrightStatus;
			CopyrightRegion = copyrightRegion;
			CopyrightComment = copyrightComment;
			CopyrightEvidence = copyrightEvidence;
			CopyrightEvidenceOperator = copyrightEvidenceOperator;
			CopyrightEvidenceDate = copyrightEvidenceDate;
			ThumbnailPageID = thumbnailPageID;
			RedirectItemID = redirectItemID;
			ExternalUrl = externalUrl;
			EndYear = endYear;
			StartVolume = startVolume;
			EndVolume = endVolume;
			StartIssue = startIssue;
			EndIssue = endIssue;
			StartNumber = startNumber;
			EndNumber = endNumber;
			StartSeries = startSeries;
			EndSeries = endSeries;
			StartPart = startPart;
			EndPart = endPart;
			VolumeReviewed = volumeReviewed;
			VolumeReviewedDate = volumeReviewedDate;
			VolumeReviewedUserID = volumeReviewedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Item()
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
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "PrimaryTitleID" :
					{
						_PrimaryTitleID = (int)column.Value;
						break;
					}
					case "BarCode" :
					{
						_BarCode = (string)column.Value;
						break;
					}
					case "MARCItemID" :
					{
						_MARCItemID = (string)column.Value;
						break;
					}
					case "CallNumber" :
					{
						_CallNumber = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
						break;
					}
					case "ItemDescription" :
					{
						_ItemDescription = (string)column.Value;
						break;
					}
					case "ScannedBy" :
					{
						_ScannedBy = (int?)column.Value;
						break;
					}
					case "PDFSize" :
					{
						_PDFSize = (int?)column.Value;
						break;
					}
					case "VaultID" :
					{
						_VaultID = (int?)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime?)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime?)column.Value;
						break;
					}
					case "CreationUserID" :
					{
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
					case "ItemStatusID" :
					{
						_ItemStatusID = (int)column.Value;
						break;
					}
					case "ScanningUser" :
					{
						_ScanningUser = (string)column.Value;
						break;
					}
					case "ScanningDate" :
					{
						_ScanningDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationCompleteUserID" :
					{
						_PaginationCompleteUserID = (int?)column.Value;
						break;
					}
					case "PaginationCompleteDate" :
					{
						_PaginationCompleteDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationStatusID" :
					{
						_PaginationStatusID = (int?)column.Value;
						break;
					}
					case "PaginationStatusUserID" :
					{
						_PaginationStatusUserID = (int?)column.Value;
						break;
					}
					case "PaginationStatusDate" :
					{
						_PaginationStatusDate = (DateTime?)column.Value;
						break;
					}
					case "LastPageNameLookupDate" :
					{
						_LastPageNameLookupDate = (DateTime?)column.Value;
						break;
					}
					case "ItemSourceID" :
					{
						_ItemSourceID = (int?)column.Value;
						break;
					}
					case "Year" :
					{
						_Year = (string)column.Value;
						break;
					}
					case "IdentifierBib" :
					{
						_IdentifierBib = (string)column.Value;
						break;
					}
					case "FileRootFolder" :
					{
						_FileRootFolder = (string)column.Value;
						break;
					}
					case "ZQuery" :
					{
						_ZQuery = (string)column.Value;
						break;
					}
					case "Sponsor" :
					{
						_Sponsor = (string)column.Value;
						break;
					}
					case "LicenseUrl" :
					{
						_LicenseUrl = (string)column.Value;
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
					case "CopyrightRegion" :
					{
						_CopyrightRegion = (string)column.Value;
						break;
					}
					case "CopyrightComment" :
					{
						_CopyrightComment = (string)column.Value;
						break;
					}
					case "CopyrightEvidence" :
					{
						_CopyrightEvidence = (string)column.Value;
						break;
					}
					case "CopyrightEvidenceOperator" :
					{
						_CopyrightEvidenceOperator = (string)column.Value;
						break;
					}
					case "CopyrightEvidenceDate" :
					{
						_CopyrightEvidenceDate = (string)column.Value;
						break;
					}
					case "ThumbnailPageID" :
					{
						_ThumbnailPageID = (int?)column.Value;
						break;
					}
					case "RedirectItemID" :
					{
						_RedirectItemID = (int?)column.Value;
						break;
					}
					case "ExternalUrl" :
					{
						_ExternalUrl = (string)column.Value;
						break;
					}
					case "EndYear" :
					{
						_EndYear = (string)column.Value;
						break;
					}
					case "StartVolume" :
					{
						_StartVolume = (string)column.Value;
						break;
					}
					case "EndVolume" :
					{
						_EndVolume = (string)column.Value;
						break;
					}
					case "StartIssue" :
					{
						_StartIssue = (string)column.Value;
						break;
					}
					case "EndIssue" :
					{
						_EndIssue = (string)column.Value;
						break;
					}
					case "StartNumber" :
					{
						_StartNumber = (string)column.Value;
						break;
					}
					case "EndNumber" :
					{
						_EndNumber = (string)column.Value;
						break;
					}
					case "StartSeries" :
					{
						_StartSeries = (string)column.Value;
						break;
					}
					case "EndSeries" :
					{
						_EndSeries = (string)column.Value;
						break;
					}
					case "StartPart" :
					{
						_StartPart = (string)column.Value;
						break;
					}
					case "EndPart" :
					{
						_EndPart = (string)column.Value;
						break;
					}
					case "VolumeReviewed" :
					{
						_VolumeReviewed = (byte)column.Value;
						break;
					}
					case "VolumeReviewedDate" :
					{
						_VolumeReviewedDate = (DateTime?)column.Value;
						break;
					}
					case "VolumeReviewedUserID" :
					{
						_VolumeReviewedUserID = (int?)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
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
		
		#endregion ItemID
		
		#region PrimaryTitleID
		
		private int _PrimaryTitleID = default(int);
		
		/// <summary>
		/// Column: PrimaryTitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PrimaryTitleID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int PrimaryTitleID
		{
			get
			{
				return _PrimaryTitleID;
			}
			set
			{
				if (_PrimaryTitleID != value)
				{
					_PrimaryTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PrimaryTitleID
		
		#region BarCode
		
		private string _BarCode = string.Empty;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40)]
		public string BarCode
		{
			get
			{
				return _BarCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_BarCode != value)
				{
					_BarCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BarCode
		
		#region MARCItemID
		
		private string _MARCItemID = null;
		
		/// <summary>
		/// Column: MARCItemID;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("MARCItemID", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50, IsNullable=true)]
		public string MARCItemID
		{
			get
			{
				return _MARCItemID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MARCItemID != value)
				{
					_MARCItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCItemID
		
		#region CallNumber
		
		private string _CallNumber = null;
		
		/// <summary>
		/// Column: CallNumber;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region Volume
		
		private string _Volume = null;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region LanguageCode
		
		private string _LanguageCode = null;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
		public string LanguageCode
		{
			get
			{
				return _LanguageCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_LanguageCode != value)
				{
					_LanguageCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LanguageCode
		
		#region ItemDescription
		
		private string _ItemDescription = null;
		
		/// <summary>
		/// Column: ItemDescription;
		/// DBMS data type: ntext(1073741823); Nullable;
		/// </summary>
		[ColumnDefinition("ItemDescription", DbTargetType=SqlDbType.NText, Ordinal=8, CharacterMaxLength=1073741823, IsNullable=true)]
		public string ItemDescription
		{
			get
			{
				return _ItemDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ItemDescription != value)
				{
					_ItemDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemDescription
		
		#region ScannedBy
		
		private int? _ScannedBy = null;
		
		/// <summary>
		/// Column: ScannedBy;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ScannedBy", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
		public int? ScannedBy
		{
			get
			{
				return _ScannedBy;
			}
			set
			{
				if (_ScannedBy != value)
				{
					_ScannedBy = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScannedBy
		
		#region PDFSize
		
		private int? _PDFSize = null;
		
		/// <summary>
		/// Column: PDFSize;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PDFSize", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? PDFSize
		{
			get
			{
				return _PDFSize;
			}
			set
			{
				if (_PDFSize != value)
				{
					_PDFSize = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PDFSize
		
		#region VaultID
		
		private int? _VaultID = null;
		
		/// <summary>
		/// Column: VaultID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("VaultID", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? VaultID
		{
			get
			{
				return _VaultID;
			}
			set
			{
				if (_VaultID != value)
				{
					_VaultID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VaultID
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=255, IsNullable=true)]
		public string Note
		{
			get
			{
				return _Note;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_Note != value)
				{
					_Note = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Note
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=13, IsNullable=true)]
		public DateTime? CreationDate
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
		
		private DateTime? _LastModifiedDate = null;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=14, IsNullable=true)]
		public DateTime? LastModifiedDate
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
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=15, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
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
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=16, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
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
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=17, NumericPrecision=10, IsInForeignKey=true)]
		public int ItemStatusID
		{
			get
			{
				return _ItemStatusID;
			}
			set
			{
				if (_ItemStatusID != value)
				{
					_ItemStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemStatusID
		
		#region ScanningUser
		
		private string _ScanningUser = null;
		
		/// <summary>
		/// Column: ScanningUser;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("ScanningUser", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=100, IsNullable=true)]
		public string ScanningUser
		{
			get
			{
				return _ScanningUser;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ScanningUser != value)
				{
					_ScanningUser = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningUser
		
		#region ScanningDate
		
		private DateTime? _ScanningDate = null;
		
		/// <summary>
		/// Column: ScanningDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ScanningDate", DbTargetType=SqlDbType.DateTime, Ordinal=19, IsNullable=true)]
		public DateTime? ScanningDate
		{
			get
			{
				return _ScanningDate;
			}
			set
			{
				if (_ScanningDate != value)
				{
					_ScanningDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningDate
		
		#region PaginationCompleteUserID
		
		private int? _PaginationCompleteUserID = null;
		
		/// <summary>
		/// Column: PaginationCompleteUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteUserID", DbTargetType=SqlDbType.Int, Ordinal=20, NumericPrecision=10, IsNullable=true)]
		public int? PaginationCompleteUserID
		{
			get
			{
				return _PaginationCompleteUserID;
			}
			set
			{
				if (_PaginationCompleteUserID != value)
				{
					_PaginationCompleteUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationCompleteUserID
		
		#region PaginationCompleteDate
		
		private DateTime? _PaginationCompleteDate = null;
		
		/// <summary>
		/// Column: PaginationCompleteDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteDate", DbTargetType=SqlDbType.DateTime, Ordinal=21, IsNullable=true)]
		public DateTime? PaginationCompleteDate
		{
			get
			{
				return _PaginationCompleteDate;
			}
			set
			{
				if (_PaginationCompleteDate != value)
				{
					_PaginationCompleteDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationCompleteDate
		
		#region PaginationStatusID
		
		private int? _PaginationStatusID = null;
		
		/// <summary>
		/// Column: PaginationStatusID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusID", DbTargetType=SqlDbType.Int, Ordinal=22, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? PaginationStatusID
		{
			get
			{
				return _PaginationStatusID;
			}
			set
			{
				if (_PaginationStatusID != value)
				{
					_PaginationStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationStatusID
		
		#region PaginationStatusUserID
		
		private int? _PaginationStatusUserID = null;
		
		/// <summary>
		/// Column: PaginationStatusUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusUserID", DbTargetType=SqlDbType.Int, Ordinal=23, NumericPrecision=10, IsNullable=true)]
		public int? PaginationStatusUserID
		{
			get
			{
				return _PaginationStatusUserID;
			}
			set
			{
				if (_PaginationStatusUserID != value)
				{
					_PaginationStatusUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationStatusUserID
		
		#region PaginationStatusDate
		
		private DateTime? _PaginationStatusDate = null;
		
		/// <summary>
		/// Column: PaginationStatusDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusDate", DbTargetType=SqlDbType.DateTime, Ordinal=24, IsNullable=true)]
		public DateTime? PaginationStatusDate
		{
			get
			{
				return _PaginationStatusDate;
			}
			set
			{
				if (_PaginationStatusDate != value)
				{
					_PaginationStatusDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationStatusDate
		
		#region LastPageNameLookupDate
		
		private DateTime? _LastPageNameLookupDate = null;
		
		/// <summary>
		/// Column: LastPageNameLookupDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastPageNameLookupDate", DbTargetType=SqlDbType.DateTime, Ordinal=25, IsNullable=true)]
		public DateTime? LastPageNameLookupDate
		{
			get
			{
				return _LastPageNameLookupDate;
			}
			set
			{
				if (_LastPageNameLookupDate != value)
				{
					_LastPageNameLookupDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastPageNameLookupDate
		
		#region ItemSourceID
		
		private int? _ItemSourceID = null;
		
		/// <summary>
		/// Column: ItemSourceID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ItemSourceID", DbTargetType=SqlDbType.Int, Ordinal=26, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? ItemSourceID
		{
			get
			{
				return _ItemSourceID;
			}
			set
			{
				if (_ItemSourceID != value)
				{
					_ItemSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemSourceID
		
		#region Year
		
		private string _Year = null;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=20, IsNullable=true)]
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
		
		#region IdentifierBib
		
		private string _IdentifierBib = null;
		
		/// <summary>
		/// Column: IdentifierBib;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("IdentifierBib", DbTargetType=SqlDbType.NVarChar, Ordinal=28, CharacterMaxLength=50, IsNullable=true)]
		public string IdentifierBib
		{
			get
			{
				return _IdentifierBib;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_IdentifierBib != value)
				{
					_IdentifierBib = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierBib
		
		#region FileRootFolder
		
		private string _FileRootFolder = null;
		
		/// <summary>
		/// Column: FileRootFolder;
		/// DBMS data type: nvarchar(250); Nullable;
		/// </summary>
		[ColumnDefinition("FileRootFolder", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=250, IsNullable=true)]
		public string FileRootFolder
		{
			get
			{
				return _FileRootFolder;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 250);
				if (_FileRootFolder != value)
				{
					_FileRootFolder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileRootFolder
		
		#region ZQuery
		
		private string _ZQuery = null;
		
		/// <summary>
		/// Column: ZQuery;
		/// DBMS data type: nvarchar(200); Nullable;
		/// </summary>
		[ColumnDefinition("ZQuery", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=200, IsNullable=true)]
		public string ZQuery
		{
			get
			{
				return _ZQuery;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_ZQuery != value)
				{
					_ZQuery = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ZQuery
		
		#region Sponsor
		
		private string _Sponsor = null;
		
		/// <summary>
		/// Column: Sponsor;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("Sponsor", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=100, IsNullable=true)]
		public string Sponsor
		{
			get
			{
				return _Sponsor;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Sponsor != value)
				{
					_Sponsor = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Sponsor
		
		#region LicenseUrl
		
		private string _LicenseUrl = null;
		
		/// <summary>
		/// Column: LicenseUrl;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("LicenseUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=32, IsNullable=true)]
		public string LicenseUrl
		{
			get
			{
				return _LicenseUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_LicenseUrl != value)
				{
					_LicenseUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LicenseUrl
		
		#region Rights
		
		private string _Rights = null;
		
		/// <summary>
		/// Column: Rights;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("Rights", DbTargetType=SqlDbType.NVarChar, Ordinal=33, IsNullable=true)]
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
		
		private string _DueDiligence = null;
		
		/// <summary>
		/// Column: DueDiligence;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("DueDiligence", DbTargetType=SqlDbType.NVarChar, Ordinal=34, IsNullable=true)]
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
		
		private string _CopyrightStatus = null;
		
		/// <summary>
		/// Column: CopyrightStatus;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("CopyrightStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=35, IsNullable=true)]
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
		
		#region CopyrightRegion
		
		private string _CopyrightRegion = null;
		
		/// <summary>
		/// Column: CopyrightRegion;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("CopyrightRegion", DbTargetType=SqlDbType.NVarChar, Ordinal=36, CharacterMaxLength=50, IsNullable=true)]
		public string CopyrightRegion
		{
			get
			{
				return _CopyrightRegion;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CopyrightRegion != value)
				{
					_CopyrightRegion = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightRegion
		
		#region CopyrightComment
		
		private string _CopyrightComment = null;
		
		/// <summary>
		/// Column: CopyrightComment;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("CopyrightComment", DbTargetType=SqlDbType.NVarChar, Ordinal=37, IsNullable=true)]
		public string CopyrightComment
		{
			get
			{
				return _CopyrightComment;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CopyrightComment != value)
				{
					_CopyrightComment = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightComment
		
		#region CopyrightEvidence
		
		private string _CopyrightEvidence = null;
		
		/// <summary>
		/// Column: CopyrightEvidence;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("CopyrightEvidence", DbTargetType=SqlDbType.NVarChar, Ordinal=38, IsNullable=true)]
		public string CopyrightEvidence
		{
			get
			{
				return _CopyrightEvidence;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CopyrightEvidence != value)
				{
					_CopyrightEvidence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightEvidence
		
		#region CopyrightEvidenceOperator
		
		private string _CopyrightEvidenceOperator = null;
		
		/// <summary>
		/// Column: CopyrightEvidenceOperator;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("CopyrightEvidenceOperator", DbTargetType=SqlDbType.NVarChar, Ordinal=39, CharacterMaxLength=100, IsNullable=true)]
		public string CopyrightEvidenceOperator
		{
			get
			{
				return _CopyrightEvidenceOperator;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_CopyrightEvidenceOperator != value)
				{
					_CopyrightEvidenceOperator = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightEvidenceOperator
		
		#region CopyrightEvidenceDate
		
		private string _CopyrightEvidenceDate = null;
		
		/// <summary>
		/// Column: CopyrightEvidenceDate;
		/// DBMS data type: nvarchar(30); Nullable;
		/// </summary>
		[ColumnDefinition("CopyrightEvidenceDate", DbTargetType=SqlDbType.NVarChar, Ordinal=40, CharacterMaxLength=30, IsNullable=true)]
		public string CopyrightEvidenceDate
		{
			get
			{
				return _CopyrightEvidenceDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_CopyrightEvidenceDate != value)
				{
					_CopyrightEvidenceDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightEvidenceDate
		
		#region ThumbnailPageID
		
		private int? _ThumbnailPageID = null;
		
		/// <summary>
		/// Column: ThumbnailPageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ThumbnailPageID", DbTargetType=SqlDbType.Int, Ordinal=41, NumericPrecision=10, IsNullable=true)]
		public int? ThumbnailPageID
		{
			get
			{
				return _ThumbnailPageID;
			}
			set
			{
				if (_ThumbnailPageID != value)
				{
					_ThumbnailPageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ThumbnailPageID
		
		#region RedirectItemID
		
		private int? _RedirectItemID = null;
		
		/// <summary>
		/// Column: RedirectItemID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectItemID", DbTargetType=SqlDbType.Int, Ordinal=42, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? RedirectItemID
		{
			get
			{
				return _RedirectItemID;
			}
			set
			{
				if (_RedirectItemID != value)
				{
					_RedirectItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectItemID
		
		#region ExternalUrl
		
		private string _ExternalUrl = null;
		
		/// <summary>
		/// Column: ExternalUrl;
		/// DBMS data type: nvarchar(500); Nullable;
		/// </summary>
		[ColumnDefinition("ExternalUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=43, CharacterMaxLength=500, IsNullable=true)]
		public string ExternalUrl
		{
			get
			{
				return _ExternalUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_ExternalUrl != value)
				{
					_ExternalUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalUrl
		
		#region EndYear
		
		private string _EndYear = string.Empty;
		
		/// <summary>
		/// Column: EndYear;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("EndYear", DbTargetType=SqlDbType.NVarChar, Ordinal=44, CharacterMaxLength=20)]
		public string EndYear
		{
			get
			{
				return _EndYear;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_EndYear != value)
				{
					_EndYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndYear
		
		#region StartVolume
		
		private string _StartVolume = string.Empty;
		
		/// <summary>
		/// Column: StartVolume;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("StartVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=45, CharacterMaxLength=10)]
		public string StartVolume
		{
			get
			{
				return _StartVolume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_StartVolume != value)
				{
					_StartVolume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartVolume
		
		#region EndVolume
		
		private string _EndVolume = string.Empty;
		
		/// <summary>
		/// Column: EndVolume;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("EndVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=46, CharacterMaxLength=10)]
		public string EndVolume
		{
			get
			{
				return _EndVolume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_EndVolume != value)
				{
					_EndVolume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndVolume
		
		#region StartIssue
		
		private string _StartIssue = string.Empty;
		
		/// <summary>
		/// Column: StartIssue;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("StartIssue", DbTargetType=SqlDbType.NVarChar, Ordinal=47, CharacterMaxLength=10)]
		public string StartIssue
		{
			get
			{
				return _StartIssue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_StartIssue != value)
				{
					_StartIssue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartIssue
		
		#region EndIssue
		
		private string _EndIssue = string.Empty;
		
		/// <summary>
		/// Column: EndIssue;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("EndIssue", DbTargetType=SqlDbType.NVarChar, Ordinal=48, CharacterMaxLength=10)]
		public string EndIssue
		{
			get
			{
				return _EndIssue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_EndIssue != value)
				{
					_EndIssue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndIssue
		
		#region StartNumber
		
		private string _StartNumber = string.Empty;
		
		/// <summary>
		/// Column: StartNumber;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("StartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=49, CharacterMaxLength=10)]
		public string StartNumber
		{
			get
			{
				return _StartNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_StartNumber != value)
				{
					_StartNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartNumber
		
		#region EndNumber
		
		private string _EndNumber = string.Empty;
		
		/// <summary>
		/// Column: EndNumber;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("EndNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=50, CharacterMaxLength=10)]
		public string EndNumber
		{
			get
			{
				return _EndNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_EndNumber != value)
				{
					_EndNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndNumber
		
		#region StartSeries
		
		private string _StartSeries = string.Empty;
		
		/// <summary>
		/// Column: StartSeries;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("StartSeries", DbTargetType=SqlDbType.NVarChar, Ordinal=51, CharacterMaxLength=10)]
		public string StartSeries
		{
			get
			{
				return _StartSeries;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_StartSeries != value)
				{
					_StartSeries = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartSeries
		
		#region EndSeries
		
		private string _EndSeries = string.Empty;
		
		/// <summary>
		/// Column: EndSeries;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("EndSeries", DbTargetType=SqlDbType.NVarChar, Ordinal=52, CharacterMaxLength=10)]
		public string EndSeries
		{
			get
			{
				return _EndSeries;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_EndSeries != value)
				{
					_EndSeries = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndSeries
		
		#region StartPart
		
		private string _StartPart = string.Empty;
		
		/// <summary>
		/// Column: StartPart;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("StartPart", DbTargetType=SqlDbType.NVarChar, Ordinal=53, CharacterMaxLength=10)]
		public string StartPart
		{
			get
			{
				return _StartPart;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_StartPart != value)
				{
					_StartPart = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartPart
		
		#region EndPart
		
		private string _EndPart = string.Empty;
		
		/// <summary>
		/// Column: EndPart;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("EndPart", DbTargetType=SqlDbType.NVarChar, Ordinal=54, CharacterMaxLength=10)]
		public string EndPart
		{
			get
			{
				return _EndPart;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_EndPart != value)
				{
					_EndPart = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndPart
		
		#region VolumeReviewed
		
		private byte _VolumeReviewed = default(byte);
		
		/// <summary>
		/// Column: VolumeReviewed;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("VolumeReviewed", DbTargetType=SqlDbType.TinyInt, Ordinal=55, NumericPrecision=3)]
		public byte VolumeReviewed
		{
			get
			{
				return _VolumeReviewed;
			}
			set
			{
				if (_VolumeReviewed != value)
				{
					_VolumeReviewed = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VolumeReviewed
		
		#region VolumeReviewedDate
		
		private DateTime? _VolumeReviewedDate = null;
		
		/// <summary>
		/// Column: VolumeReviewedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("VolumeReviewedDate", DbTargetType=SqlDbType.DateTime, Ordinal=56, IsNullable=true)]
		public DateTime? VolumeReviewedDate
		{
			get
			{
				return _VolumeReviewedDate;
			}
			set
			{
				if (_VolumeReviewedDate != value)
				{
					_VolumeReviewedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VolumeReviewedDate
		
		#region VolumeReviewedUserID
		
		private int? _VolumeReviewedUserID = null;
		
		/// <summary>
		/// Column: VolumeReviewedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("VolumeReviewedUserID", DbTargetType=SqlDbType.Int, Ordinal=57, NumericPrecision=10, IsNullable=true)]
		public int? VolumeReviewedUserID
		{
			get
			{
				return _VolumeReviewedUserID;
			}
			set
			{
				if (_VolumeReviewedUserID != value)
				{
					_VolumeReviewedUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VolumeReviewedUserID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Item"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Item"/>, 
		/// returns an instance of <see cref="__Item"/>; otherwise returns null.</returns>
		public static new __Item FromArray(byte[] byteArray)
		{
			__Item o = null;
			
			try
			{
				o = (__Item) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Item"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Item"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Item)
			{
				__Item o = (__Item) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemID == ItemID &&
					o.PrimaryTitleID == PrimaryTitleID &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.MARCItemID) == GetComparisonString(MARCItemID) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					GetComparisonString(o.ItemDescription) == GetComparisonString(ItemDescription) &&
					o.ScannedBy == ScannedBy &&
					o.PDFSize == PDFSize &&
					o.VaultID == VaultID &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID &&
					o.ItemStatusID == ItemStatusID &&
					GetComparisonString(o.ScanningUser) == GetComparisonString(ScanningUser) &&
					o.ScanningDate == ScanningDate &&
					o.PaginationCompleteUserID == PaginationCompleteUserID &&
					o.PaginationCompleteDate == PaginationCompleteDate &&
					o.PaginationStatusID == PaginationStatusID &&
					o.PaginationStatusUserID == PaginationStatusUserID &&
					o.PaginationStatusDate == PaginationStatusDate &&
					o.LastPageNameLookupDate == LastPageNameLookupDate &&
					o.ItemSourceID == ItemSourceID &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.IdentifierBib) == GetComparisonString(IdentifierBib) &&
					GetComparisonString(o.FileRootFolder) == GetComparisonString(FileRootFolder) &&
					GetComparisonString(o.ZQuery) == GetComparisonString(ZQuery) &&
					GetComparisonString(o.Sponsor) == GetComparisonString(Sponsor) &&
					GetComparisonString(o.LicenseUrl) == GetComparisonString(LicenseUrl) &&
					GetComparisonString(o.Rights) == GetComparisonString(Rights) &&
					GetComparisonString(o.DueDiligence) == GetComparisonString(DueDiligence) &&
					GetComparisonString(o.CopyrightStatus) == GetComparisonString(CopyrightStatus) &&
					GetComparisonString(o.CopyrightRegion) == GetComparisonString(CopyrightRegion) &&
					GetComparisonString(o.CopyrightComment) == GetComparisonString(CopyrightComment) &&
					GetComparisonString(o.CopyrightEvidence) == GetComparisonString(CopyrightEvidence) &&
					GetComparisonString(o.CopyrightEvidenceOperator) == GetComparisonString(CopyrightEvidenceOperator) &&
					GetComparisonString(o.CopyrightEvidenceDate) == GetComparisonString(CopyrightEvidenceDate) &&
					o.ThumbnailPageID == ThumbnailPageID &&
					o.RedirectItemID == RedirectItemID &&
					GetComparisonString(o.ExternalUrl) == GetComparisonString(ExternalUrl) &&
					GetComparisonString(o.EndYear) == GetComparisonString(EndYear) &&
					GetComparisonString(o.StartVolume) == GetComparisonString(StartVolume) &&
					GetComparisonString(o.EndVolume) == GetComparisonString(EndVolume) &&
					GetComparisonString(o.StartIssue) == GetComparisonString(StartIssue) &&
					GetComparisonString(o.EndIssue) == GetComparisonString(EndIssue) &&
					GetComparisonString(o.StartNumber) == GetComparisonString(StartNumber) &&
					GetComparisonString(o.EndNumber) == GetComparisonString(EndNumber) &&
					GetComparisonString(o.StartSeries) == GetComparisonString(StartSeries) &&
					GetComparisonString(o.EndSeries) == GetComparisonString(EndSeries) &&
					GetComparisonString(o.StartPart) == GetComparisonString(StartPart) &&
					GetComparisonString(o.EndPart) == GetComparisonString(EndPart) &&
					o.VolumeReviewed == VolumeReviewed &&
					o.VolumeReviewedDate == VolumeReviewedDate &&
					o.VolumeReviewedUserID == VolumeReviewedUserID 
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
				throw new ArgumentException("Argument is not of type __Item");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Item"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Item"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Item a, __Item b)
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
		/// <param name="a">The first <see cref="__Item"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Item"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Item a, __Item b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Item"/> object to compare with the current <see cref="__Item"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Item))
			{
				return false;
			}
			
			return this == (__Item) obj;
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
		/// list.Sort(SortOrder.Ascending, __Item.SortColumn.ItemID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemID = "ItemID";	
			public const string PrimaryTitleID = "PrimaryTitleID";	
			public const string BarCode = "BarCode";	
			public const string MARCItemID = "MARCItemID";	
			public const string CallNumber = "CallNumber";	
			public const string Volume = "Volume";	
			public const string LanguageCode = "LanguageCode";	
			public const string ItemDescription = "ItemDescription";	
			public const string ScannedBy = "ScannedBy";	
			public const string PDFSize = "PDFSize";	
			public const string VaultID = "VaultID";	
			public const string Note = "Note";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";	
			public const string ItemStatusID = "ItemStatusID";	
			public const string ScanningUser = "ScanningUser";	
			public const string ScanningDate = "ScanningDate";	
			public const string PaginationCompleteUserID = "PaginationCompleteUserID";	
			public const string PaginationCompleteDate = "PaginationCompleteDate";	
			public const string PaginationStatusID = "PaginationStatusID";	
			public const string PaginationStatusUserID = "PaginationStatusUserID";	
			public const string PaginationStatusDate = "PaginationStatusDate";	
			public const string LastPageNameLookupDate = "LastPageNameLookupDate";	
			public const string ItemSourceID = "ItemSourceID";	
			public const string Year = "Year";	
			public const string IdentifierBib = "IdentifierBib";	
			public const string FileRootFolder = "FileRootFolder";	
			public const string ZQuery = "ZQuery";	
			public const string Sponsor = "Sponsor";	
			public const string LicenseUrl = "LicenseUrl";	
			public const string Rights = "Rights";	
			public const string DueDiligence = "DueDiligence";	
			public const string CopyrightStatus = "CopyrightStatus";	
			public const string CopyrightRegion = "CopyrightRegion";	
			public const string CopyrightComment = "CopyrightComment";	
			public const string CopyrightEvidence = "CopyrightEvidence";	
			public const string CopyrightEvidenceOperator = "CopyrightEvidenceOperator";	
			public const string CopyrightEvidenceDate = "CopyrightEvidenceDate";	
			public const string ThumbnailPageID = "ThumbnailPageID";	
			public const string RedirectItemID = "RedirectItemID";	
			public const string ExternalUrl = "ExternalUrl";	
			public const string EndYear = "EndYear";	
			public const string StartVolume = "StartVolume";	
			public const string EndVolume = "EndVolume";	
			public const string StartIssue = "StartIssue";	
			public const string EndIssue = "EndIssue";	
			public const string StartNumber = "StartNumber";	
			public const string EndNumber = "EndNumber";	
			public const string StartSeries = "StartSeries";	
			public const string EndSeries = "EndSeries";	
			public const string StartPart = "StartPart";	
			public const string EndPart = "EndPart";	
			public const string VolumeReviewed = "VolumeReviewed";	
			public const string VolumeReviewedDate = "VolumeReviewedDate";	
			public const string VolumeReviewedUserID = "VolumeReviewedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

