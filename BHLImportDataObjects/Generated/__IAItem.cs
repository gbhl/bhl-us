
// Generated 1/12/2024 1:10:08 PM
// Do not modify the contents of this code file.
// This abstract class __IAItem is based upon dbo.IAItem.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAItem : __IAItem
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
	public abstract class __IAItem : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAItem()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="iAIdentifierPrefix"></param>
		/// <param name="iAIdentifier"></param>
		/// <param name="sponsor"></param>
		/// <param name="sponsorName"></param>
		/// <param name="scanningCenter"></param>
		/// <param name="callNumber"></param>
		/// <param name="imageCount"></param>
		/// <param name="identifierAccessUrl"></param>
		/// <param name="volume"></param>
		/// <param name="note"></param>
		/// <param name="scanOperator"></param>
		/// <param name="scanDate"></param>
		/// <param name="externalStatus"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="iADateStamp"></param>
		/// <param name="iAAddedDate"></param>
		/// <param name="lastOAIDataHarvestDate"></param>
		/// <param name="lastXMLDataHarvestDate"></param>
		/// <param name="lastProductionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="shortTitle"></param>
		/// <param name="sponsorDate"></param>
		/// <param name="titleID"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="possibleCopyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="localFileFolder"></param>
		/// <param name="noMARCOk"></param>
		/// <param name="scanningInstitution"></param>
		/// <param name="rightsHolder"></param>
		/// <param name="itemDescription"></param>
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
		/// <param name="pageProgression"></param>
		/// <param name="createdUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="virtualVolume"></param>
		/// <param name="virtualTitleID"></param>
		/// <param name="summary"></param>
		/// <param name="genre"></param>
		/// <param name="issue"></param>
		public __IAItem(int itemID, 
			int itemStatusID, 
			string iAIdentifierPrefix, 
			string iAIdentifier, 
			string sponsor, 
			string sponsorName, 
			string scanningCenter, 
			string callNumber, 
			int? imageCount, 
			string identifierAccessUrl, 
			string volume, 
			string note, 
			string scanOperator, 
			string scanDate, 
			string externalStatus, 
			string mARCBibID, 
			string barCode, 
			DateTime? iADateStamp, 
			DateTime? iAAddedDate, 
			DateTime? lastOAIDataHarvestDate, 
			DateTime? lastXMLDataHarvestDate, 
			DateTime? lastProductionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate, 
			string shortTitle, 
			string sponsorDate, 
			string titleID, 
			string year, 
			string identifierBib, 
			string zQuery, 
			string licenseUrl, 
			string rights, 
			string dueDiligence, 
			string possibleCopyrightStatus, 
			string copyrightRegion, 
			string copyrightComment, 
			string copyrightEvidence, 
			string copyrightEvidenceOperator, 
			string copyrightEvidenceDate, 
			string localFileFolder, 
			byte noMARCOk, 
			string scanningInstitution, 
			string rightsHolder, 
			string itemDescription, 
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
			string pageProgression, 
			int createdUserID, 
			int lastModifiedUserID, 
			string virtualVolume, 
			int? virtualTitleID, 
			string summary, 
			string genre, 
			string issue) : this()
		{
			_ItemID = itemID;
			ItemStatusID = itemStatusID;
			IAIdentifierPrefix = iAIdentifierPrefix;
			IAIdentifier = iAIdentifier;
			Sponsor = sponsor;
			SponsorName = sponsorName;
			ScanningCenter = scanningCenter;
			CallNumber = callNumber;
			ImageCount = imageCount;
			IdentifierAccessUrl = identifierAccessUrl;
			Volume = volume;
			Note = note;
			ScanOperator = scanOperator;
			ScanDate = scanDate;
			ExternalStatus = externalStatus;
			MARCBibID = mARCBibID;
			BarCode = barCode;
			IADateStamp = iADateStamp;
			IAAddedDate = iAAddedDate;
			LastOAIDataHarvestDate = lastOAIDataHarvestDate;
			LastXMLDataHarvestDate = lastXMLDataHarvestDate;
			LastProductionDate = lastProductionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
			ShortTitle = shortTitle;
			SponsorDate = sponsorDate;
			TitleID = titleID;
			Year = year;
			IdentifierBib = identifierBib;
			ZQuery = zQuery;
			LicenseUrl = licenseUrl;
			Rights = rights;
			DueDiligence = dueDiligence;
			PossibleCopyrightStatus = possibleCopyrightStatus;
			CopyrightRegion = copyrightRegion;
			CopyrightComment = copyrightComment;
			CopyrightEvidence = copyrightEvidence;
			CopyrightEvidenceOperator = copyrightEvidenceOperator;
			CopyrightEvidenceDate = copyrightEvidenceDate;
			LocalFileFolder = localFileFolder;
			NoMARCOk = noMARCOk;
			ScanningInstitution = scanningInstitution;
			RightsHolder = rightsHolder;
			ItemDescription = itemDescription;
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
			PageProgression = pageProgression;
			CreatedUserID = createdUserID;
			LastModifiedUserID = lastModifiedUserID;
			VirtualVolume = virtualVolume;
			VirtualTitleID = virtualTitleID;
			Summary = summary;
			Genre = genre;
			Issue = issue;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAItem()
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
					case "ItemStatusID" :
					{
						_ItemStatusID = (int)column.Value;
						break;
					}
					case "IAIdentifierPrefix" :
					{
						_IAIdentifierPrefix = (string)column.Value;
						break;
					}
					case "IAIdentifier" :
					{
						_IAIdentifier = (string)column.Value;
						break;
					}
					case "Sponsor" :
					{
						_Sponsor = (string)column.Value;
						break;
					}
					case "SponsorName" :
					{
						_SponsorName = (string)column.Value;
						break;
					}
					case "ScanningCenter" :
					{
						_ScanningCenter = (string)column.Value;
						break;
					}
					case "CallNumber" :
					{
						_CallNumber = (string)column.Value;
						break;
					}
					case "ImageCount" :
					{
						_ImageCount = (int?)column.Value;
						break;
					}
					case "IdentifierAccessUrl" :
					{
						_IdentifierAccessUrl = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
					case "ScanOperator" :
					{
						_ScanOperator = (string)column.Value;
						break;
					}
					case "ScanDate" :
					{
						_ScanDate = (string)column.Value;
						break;
					}
					case "ExternalStatus" :
					{
						_ExternalStatus = (string)column.Value;
						break;
					}
					case "MARCBibID" :
					{
						_MARCBibID = (string)column.Value;
						break;
					}
					case "BarCode" :
					{
						_BarCode = (string)column.Value;
						break;
					}
					case "IADateStamp" :
					{
						_IADateStamp = (DateTime?)column.Value;
						break;
					}
					case "IAAddedDate" :
					{
						_IAAddedDate = (DateTime?)column.Value;
						break;
					}
					case "LastOAIDataHarvestDate" :
					{
						_LastOAIDataHarvestDate = (DateTime?)column.Value;
						break;
					}
					case "LastXMLDataHarvestDate" :
					{
						_LastXMLDataHarvestDate = (DateTime?)column.Value;
						break;
					}
					case "LastProductionDate" :
					{
						_LastProductionDate = (DateTime?)column.Value;
						break;
					}
					case "CreatedDate" :
					{
						_CreatedDate = (DateTime)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime)column.Value;
						break;
					}
					case "ShortTitle" :
					{
						_ShortTitle = (string)column.Value;
						break;
					}
					case "SponsorDate" :
					{
						_SponsorDate = (string)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (string)column.Value;
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
					case "ZQuery" :
					{
						_ZQuery = (string)column.Value;
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
					case "PossibleCopyrightStatus" :
					{
						_PossibleCopyrightStatus = (string)column.Value;
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
					case "LocalFileFolder" :
					{
						_LocalFileFolder = (string)column.Value;
						break;
					}
					case "NoMARCOk" :
					{
						_NoMARCOk = (byte)column.Value;
						break;
					}
					case "ScanningInstitution" :
					{
						_ScanningInstitution = (string)column.Value;
						break;
					}
					case "RightsHolder" :
					{
						_RightsHolder = (string)column.Value;
						break;
					}
					case "ItemDescription" :
					{
						_ItemDescription = (string)column.Value;
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
					case "PageProgression" :
					{
						_PageProgression = (string)column.Value;
						break;
					}
					case "CreatedUserID" :
					{
						_CreatedUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
					case "VirtualVolume" :
					{
						_VirtualVolume = (string)column.Value;
						break;
					}
					case "VirtualTitleID" :
					{
						_VirtualTitleID = (int?)column.Value;
						break;
					}
					case "Summary" :
					{
						_Summary = (string)column.Value;
						break;
					}
					case "Genre" :
					{
						_Genre = (string)column.Value;
						break;
					}
					case "Issue" :
					{
						_Issue = (string)column.Value;
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
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
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
		
		#region IAIdentifierPrefix
		
		private string _IAIdentifierPrefix = string.Empty;
		
		/// <summary>
		/// Column: IAIdentifierPrefix;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("IAIdentifierPrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string IAIdentifierPrefix
		{
			get
			{
				return _IAIdentifierPrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_IAIdentifierPrefix != value)
				{
					_IAIdentifierPrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IAIdentifierPrefix
		
		#region IAIdentifier
		
		private string _IAIdentifier = string.Empty;
		
		/// <summary>
		/// Column: IAIdentifier;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("IAIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=200)]
		public string IAIdentifier
		{
			get
			{
				return _IAIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_IAIdentifier != value)
				{
					_IAIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IAIdentifier
		
		#region Sponsor
		
		private string _Sponsor = string.Empty;
		
		/// <summary>
		/// Column: Sponsor;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Sponsor", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
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
		
		#region SponsorName
		
		private string _SponsorName = null;
		
		/// <summary>
		/// Column: SponsorName;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("SponsorName", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=50, IsNullable=true)]
		public string SponsorName
		{
			get
			{
				return _SponsorName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_SponsorName != value)
				{
					_SponsorName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SponsorName
		
		#region ScanningCenter
		
		private string _ScanningCenter = string.Empty;
		
		/// <summary>
		/// Column: ScanningCenter;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ScanningCenter", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=50)]
		public string ScanningCenter
		{
			get
			{
				return _ScanningCenter;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ScanningCenter != value)
				{
					_ScanningCenter = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningCenter
		
		#region CallNumber
		
		private string _CallNumber = string.Empty;
		
		/// <summary>
		/// Column: CallNumber;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50)]
		public string CallNumber
		{
			get
			{
				return _CallNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CallNumber != value)
				{
					_CallNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CallNumber
		
		#region ImageCount
		
		private int? _ImageCount = null;
		
		/// <summary>
		/// Column: ImageCount;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ImageCount", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
		public int? ImageCount
		{
			get
			{
				return _ImageCount;
			}
			set
			{
				if (_ImageCount != value)
				{
					_ImageCount = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImageCount
		
		#region IdentifierAccessUrl
		
		private string _IdentifierAccessUrl = null;
		
		/// <summary>
		/// Column: IdentifierAccessUrl;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("IdentifierAccessUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=100, IsNullable=true)]
		public string IdentifierAccessUrl
		{
			get
			{
				return _IdentifierAccessUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_IdentifierAccessUrl != value)
				{
					_IdentifierAccessUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierAccessUrl
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=50)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region Note
		
		private string _Note = string.Empty;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=255)]
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
		
		#region ScanOperator
		
		private string _ScanOperator = string.Empty;
		
		/// <summary>
		/// Column: ScanOperator;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("ScanOperator", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=100)]
		public string ScanOperator
		{
			get
			{
				return _ScanOperator;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ScanOperator != value)
				{
					_ScanOperator = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanOperator
		
		#region ScanDate
		
		private string _ScanDate = string.Empty;
		
		/// <summary>
		/// Column: ScanDate;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ScanDate", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=50)]
		public string ScanDate
		{
			get
			{
				return _ScanDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ScanDate != value)
				{
					_ScanDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanDate
		
		#region ExternalStatus
		
		private string _ExternalStatus = string.Empty;
		
		/// <summary>
		/// Column: ExternalStatus;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ExternalStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=50)]
		public string ExternalStatus
		{
			get
			{
				return _ExternalStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ExternalStatus != value)
				{
					_ExternalStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalStatus
		
		#region MARCBibID
		
		private string _MARCBibID = string.Empty;
		
		/// <summary>
		/// Column: MARCBibID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("MARCBibID", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=50)]
		public string MARCBibID
		{
			get
			{
				return _MARCBibID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MARCBibID != value)
				{
					_MARCBibID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCBibID
		
		#region BarCode
		
		private string _BarCode = string.Empty;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=200)]
		public string BarCode
		{
			get
			{
				return _BarCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_BarCode != value)
				{
					_BarCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BarCode
		
		#region IADateStamp
		
		private DateTime? _IADateStamp = null;
		
		/// <summary>
		/// Column: IADateStamp;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("IADateStamp", DbTargetType=SqlDbType.DateTime, Ordinal=18, IsNullable=true)]
		public DateTime? IADateStamp
		{
			get
			{
				return _IADateStamp;
			}
			set
			{
				if (_IADateStamp != value)
				{
					_IADateStamp = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IADateStamp
		
		#region IAAddedDate
		
		private DateTime? _IAAddedDate = null;
		
		/// <summary>
		/// Column: IAAddedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("IAAddedDate", DbTargetType=SqlDbType.DateTime, Ordinal=19, IsNullable=true)]
		public DateTime? IAAddedDate
		{
			get
			{
				return _IAAddedDate;
			}
			set
			{
				if (_IAAddedDate != value)
				{
					_IAAddedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IAAddedDate
		
		#region LastOAIDataHarvestDate
		
		private DateTime? _LastOAIDataHarvestDate = null;
		
		/// <summary>
		/// Column: LastOAIDataHarvestDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastOAIDataHarvestDate", DbTargetType=SqlDbType.DateTime, Ordinal=20, IsNullable=true)]
		public DateTime? LastOAIDataHarvestDate
		{
			get
			{
				return _LastOAIDataHarvestDate;
			}
			set
			{
				if (_LastOAIDataHarvestDate != value)
				{
					_LastOAIDataHarvestDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastOAIDataHarvestDate
		
		#region LastXMLDataHarvestDate
		
		private DateTime? _LastXMLDataHarvestDate = null;
		
		/// <summary>
		/// Column: LastXMLDataHarvestDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastXMLDataHarvestDate", DbTargetType=SqlDbType.DateTime, Ordinal=21, IsNullable=true)]
		public DateTime? LastXMLDataHarvestDate
		{
			get
			{
				return _LastXMLDataHarvestDate;
			}
			set
			{
				if (_LastXMLDataHarvestDate != value)
				{
					_LastXMLDataHarvestDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastXMLDataHarvestDate
		
		#region LastProductionDate
		
		private DateTime? _LastProductionDate = null;
		
		/// <summary>
		/// Column: LastProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=22, IsNullable=true)]
		public DateTime? LastProductionDate
		{
			get
			{
				return _LastProductionDate;
			}
			set
			{
				if (_LastProductionDate != value)
				{
					_LastProductionDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastProductionDate
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=23)]
		public DateTime CreatedDate
		{
			get
			{
				return _CreatedDate;
			}
			set
			{
				if (_CreatedDate != value)
				{
					_CreatedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatedDate
		
		#region LastModifiedDate
		
		private DateTime _LastModifiedDate;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=24)]
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
		
		#region ShortTitle
		
		private string _ShortTitle = null;
		
		/// <summary>
		/// Column: ShortTitle;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("ShortTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=255, IsNullable=true)]
		public string ShortTitle
		{
			get
			{
				return _ShortTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_ShortTitle != value)
				{
					_ShortTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ShortTitle
		
		#region SponsorDate
		
		private string _SponsorDate = null;
		
		/// <summary>
		/// Column: SponsorDate;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("SponsorDate", DbTargetType=SqlDbType.NVarChar, Ordinal=26, CharacterMaxLength=50, IsNullable=true)]
		public string SponsorDate
		{
			get
			{
				return _SponsorDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_SponsorDate != value)
				{
					_SponsorDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SponsorDate
		
		#region TitleID
		
		private string _TitleID = string.Empty;
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=50)]
		public string TitleID
		{
			get
			{
				return _TitleID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_TitleID != value)
				{
					_TitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleID
		
		#region Year
		
		private string _Year = string.Empty;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=28, CharacterMaxLength=20)]
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
		
		private string _IdentifierBib = string.Empty;
		
		/// <summary>
		/// Column: IdentifierBib;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("IdentifierBib", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=50)]
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
		
		#region ZQuery
		
		private string _ZQuery = string.Empty;
		
		/// <summary>
		/// Column: ZQuery;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("ZQuery", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=200)]
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
		
		#region LicenseUrl
		
		private string _LicenseUrl = string.Empty;
		
		/// <summary>
		/// Column: LicenseUrl;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("LicenseUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=1073741823)]
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
		
		private string _Rights = string.Empty;
		
		/// <summary>
		/// Column: Rights;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Rights", DbTargetType=SqlDbType.NVarChar, Ordinal=32, CharacterMaxLength=1073741823)]
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
		[ColumnDefinition("DueDiligence", DbTargetType=SqlDbType.NVarChar, Ordinal=33, CharacterMaxLength=1073741823)]
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
		
		#region PossibleCopyrightStatus
		
		private string _PossibleCopyrightStatus = string.Empty;
		
		/// <summary>
		/// Column: PossibleCopyrightStatus;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("PossibleCopyrightStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=34, CharacterMaxLength=1073741823)]
		public string PossibleCopyrightStatus
		{
			get
			{
				return _PossibleCopyrightStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_PossibleCopyrightStatus != value)
				{
					_PossibleCopyrightStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PossibleCopyrightStatus
		
		#region CopyrightRegion
		
		private string _CopyrightRegion = string.Empty;
		
		/// <summary>
		/// Column: CopyrightRegion;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CopyrightRegion", DbTargetType=SqlDbType.NVarChar, Ordinal=35, CharacterMaxLength=50)]
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
		
		private string _CopyrightComment = string.Empty;
		
		/// <summary>
		/// Column: CopyrightComment;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CopyrightComment", DbTargetType=SqlDbType.NVarChar, Ordinal=36, CharacterMaxLength=1073741823)]
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
		
		private string _CopyrightEvidence = string.Empty;
		
		/// <summary>
		/// Column: CopyrightEvidence;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CopyrightEvidence", DbTargetType=SqlDbType.NVarChar, Ordinal=37, CharacterMaxLength=1073741823)]
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
		
		private string _CopyrightEvidenceOperator = string.Empty;
		
		/// <summary>
		/// Column: CopyrightEvidenceOperator;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("CopyrightEvidenceOperator", DbTargetType=SqlDbType.NVarChar, Ordinal=38, CharacterMaxLength=100)]
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
		
		private string _CopyrightEvidenceDate = string.Empty;
		
		/// <summary>
		/// Column: CopyrightEvidenceDate;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("CopyrightEvidenceDate", DbTargetType=SqlDbType.NVarChar, Ordinal=39, CharacterMaxLength=30)]
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
		
		#region LocalFileFolder
		
		private string _LocalFileFolder = string.Empty;
		
		/// <summary>
		/// Column: LocalFileFolder;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("LocalFileFolder", DbTargetType=SqlDbType.NVarChar, Ordinal=40, CharacterMaxLength=200)]
		public string LocalFileFolder
		{
			get
			{
				return _LocalFileFolder;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_LocalFileFolder != value)
				{
					_LocalFileFolder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LocalFileFolder
		
		#region NoMARCOk
		
		private byte _NoMARCOk = default(byte);
		
		/// <summary>
		/// Column: NoMARCOk;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("NoMARCOk", DbTargetType=SqlDbType.TinyInt, Ordinal=41, NumericPrecision=3)]
		public byte NoMARCOk
		{
			get
			{
				return _NoMARCOk;
			}
			set
			{
				if (_NoMARCOk != value)
				{
					_NoMARCOk = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoMARCOk
		
		#region ScanningInstitution
		
		private string _ScanningInstitution = string.Empty;
		
		/// <summary>
		/// Column: ScanningInstitution;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("ScanningInstitution", DbTargetType=SqlDbType.NVarChar, Ordinal=42, CharacterMaxLength=500)]
		public string ScanningInstitution
		{
			get
			{
				return _ScanningInstitution;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_ScanningInstitution != value)
				{
					_ScanningInstitution = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningInstitution
		
		#region RightsHolder
		
		private string _RightsHolder = string.Empty;
		
		/// <summary>
		/// Column: RightsHolder;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("RightsHolder", DbTargetType=SqlDbType.NVarChar, Ordinal=43, CharacterMaxLength=500)]
		public string RightsHolder
		{
			get
			{
				return _RightsHolder;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_RightsHolder != value)
				{
					_RightsHolder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RightsHolder
		
		#region ItemDescription
		
		private string _ItemDescription = string.Empty;
		
		/// <summary>
		/// Column: ItemDescription;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("ItemDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=44, CharacterMaxLength=1073741823)]
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
		
		#region EndYear
		
		private string _EndYear = string.Empty;
		
		/// <summary>
		/// Column: EndYear;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("EndYear", DbTargetType=SqlDbType.NVarChar, Ordinal=45, CharacterMaxLength=20)]
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
		[ColumnDefinition("StartVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=46, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=47, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartIssue", DbTargetType=SqlDbType.NVarChar, Ordinal=48, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndIssue", DbTargetType=SqlDbType.NVarChar, Ordinal=49, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=50, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=51, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartSeries", DbTargetType=SqlDbType.NVarChar, Ordinal=52, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndSeries", DbTargetType=SqlDbType.NVarChar, Ordinal=53, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartPart", DbTargetType=SqlDbType.NVarChar, Ordinal=54, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndPart", DbTargetType=SqlDbType.NVarChar, Ordinal=55, CharacterMaxLength=10)]
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
		
		#region PageProgression
		
		private string _PageProgression = string.Empty;
		
		/// <summary>
		/// Column: PageProgression;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("PageProgression", DbTargetType=SqlDbType.NVarChar, Ordinal=56, CharacterMaxLength=10)]
		public string PageProgression
		{
			get
			{
				return _PageProgression;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_PageProgression != value)
				{
					_PageProgression = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageProgression
		
		#region CreatedUserID
		
		private int _CreatedUserID = default(int);
		
		/// <summary>
		/// Column: CreatedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreatedUserID", DbTargetType=SqlDbType.Int, Ordinal=57, NumericPrecision=10)]
		public int CreatedUserID
		{
			get
			{
				return _CreatedUserID;
			}
			set
			{
				if (_CreatedUserID != value)
				{
					_CreatedUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreatedUserID
		
		#region LastModifiedUserID
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=58, NumericPrecision=10)]
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
		
		#region VirtualVolume
		
		private string _VirtualVolume = string.Empty;
		
		/// <summary>
		/// Column: VirtualVolume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("VirtualVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=59, CharacterMaxLength=100)]
		public string VirtualVolume
		{
			get
			{
				return _VirtualVolume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_VirtualVolume != value)
				{
					_VirtualVolume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VirtualVolume
		
		#region VirtualTitleID
		
		private int? _VirtualTitleID = null;
		
		/// <summary>
		/// Column: VirtualTitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("VirtualTitleID", DbTargetType=SqlDbType.Int, Ordinal=60, NumericPrecision=10, IsNullable=true)]
		public int? VirtualTitleID
		{
			get
			{
				return _VirtualTitleID;
			}
			set
			{
				if (_VirtualTitleID != value)
				{
					_VirtualTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VirtualTitleID
		
		#region Summary
		
		private string _Summary = string.Empty;
		
		/// <summary>
		/// Column: Summary;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Summary", DbTargetType=SqlDbType.NVarChar, Ordinal=61, CharacterMaxLength=1073741823)]
		public string Summary
		{
			get
			{
				return _Summary;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Summary != value)
				{
					_Summary = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Summary
		
		#region Genre
		
		private string _Genre = string.Empty;
		
		/// <summary>
		/// Column: Genre;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Genre", DbTargetType=SqlDbType.NVarChar, Ordinal=62, CharacterMaxLength=50)]
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
		
		#region Issue
		
		private string _Issue = string.Empty;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=63, CharacterMaxLength=100)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__IAItem"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAItem"/>, 
		/// returns an instance of <see cref="__IAItem"/>; otherwise returns null.</returns>
		public static new __IAItem FromArray(byte[] byteArray)
		{
			__IAItem o = null;
			
			try
			{
				o = (__IAItem) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAItem"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAItem"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAItem)
			{
				__IAItem o = (__IAItem) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemID == ItemID &&
					o.ItemStatusID == ItemStatusID &&
					GetComparisonString(o.IAIdentifierPrefix) == GetComparisonString(IAIdentifierPrefix) &&
					GetComparisonString(o.IAIdentifier) == GetComparisonString(IAIdentifier) &&
					GetComparisonString(o.Sponsor) == GetComparisonString(Sponsor) &&
					GetComparisonString(o.SponsorName) == GetComparisonString(SponsorName) &&
					GetComparisonString(o.ScanningCenter) == GetComparisonString(ScanningCenter) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					o.ImageCount == ImageCount &&
					GetComparisonString(o.IdentifierAccessUrl) == GetComparisonString(IdentifierAccessUrl) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					GetComparisonString(o.ScanOperator) == GetComparisonString(ScanOperator) &&
					GetComparisonString(o.ScanDate) == GetComparisonString(ScanDate) &&
					GetComparisonString(o.ExternalStatus) == GetComparisonString(ExternalStatus) &&
					GetComparisonString(o.MARCBibID) == GetComparisonString(MARCBibID) &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					o.IADateStamp == IADateStamp &&
					o.IAAddedDate == IAAddedDate &&
					o.LastOAIDataHarvestDate == LastOAIDataHarvestDate &&
					o.LastXMLDataHarvestDate == LastXMLDataHarvestDate &&
					o.LastProductionDate == LastProductionDate &&
					o.CreatedDate == CreatedDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.ShortTitle) == GetComparisonString(ShortTitle) &&
					GetComparisonString(o.SponsorDate) == GetComparisonString(SponsorDate) &&
					GetComparisonString(o.TitleID) == GetComparisonString(TitleID) &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.IdentifierBib) == GetComparisonString(IdentifierBib) &&
					GetComparisonString(o.ZQuery) == GetComparisonString(ZQuery) &&
					GetComparisonString(o.LicenseUrl) == GetComparisonString(LicenseUrl) &&
					GetComparisonString(o.Rights) == GetComparisonString(Rights) &&
					GetComparisonString(o.DueDiligence) == GetComparisonString(DueDiligence) &&
					GetComparisonString(o.PossibleCopyrightStatus) == GetComparisonString(PossibleCopyrightStatus) &&
					GetComparisonString(o.CopyrightRegion) == GetComparisonString(CopyrightRegion) &&
					GetComparisonString(o.CopyrightComment) == GetComparisonString(CopyrightComment) &&
					GetComparisonString(o.CopyrightEvidence) == GetComparisonString(CopyrightEvidence) &&
					GetComparisonString(o.CopyrightEvidenceOperator) == GetComparisonString(CopyrightEvidenceOperator) &&
					GetComparisonString(o.CopyrightEvidenceDate) == GetComparisonString(CopyrightEvidenceDate) &&
					GetComparisonString(o.LocalFileFolder) == GetComparisonString(LocalFileFolder) &&
					o.NoMARCOk == NoMARCOk &&
					GetComparisonString(o.ScanningInstitution) == GetComparisonString(ScanningInstitution) &&
					GetComparisonString(o.RightsHolder) == GetComparisonString(RightsHolder) &&
					GetComparisonString(o.ItemDescription) == GetComparisonString(ItemDescription) &&
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
					GetComparisonString(o.PageProgression) == GetComparisonString(PageProgression) &&
					o.CreatedUserID == CreatedUserID &&
					o.LastModifiedUserID == LastModifiedUserID &&
					GetComparisonString(o.VirtualVolume) == GetComparisonString(VirtualVolume) &&
					o.VirtualTitleID == VirtualTitleID &&
					GetComparisonString(o.Summary) == GetComparisonString(Summary) &&
					GetComparisonString(o.Genre) == GetComparisonString(Genre) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) 
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
				throw new ArgumentException("Argument is not of type __IAItem");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAItem"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAItem"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAItem a, __IAItem b)
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
		/// <param name="a">The first <see cref="__IAItem"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAItem"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAItem a, __IAItem b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAItem"/> object to compare with the current <see cref="__IAItem"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAItem))
			{
				return false;
			}
			
			return this == (__IAItem) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAItem.SortColumn.ItemID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemID = "ItemID";	
			public const string ItemStatusID = "ItemStatusID";	
			public const string IAIdentifierPrefix = "IAIdentifierPrefix";	
			public const string IAIdentifier = "IAIdentifier";	
			public const string Sponsor = "Sponsor";	
			public const string SponsorName = "SponsorName";	
			public const string ScanningCenter = "ScanningCenter";	
			public const string CallNumber = "CallNumber";	
			public const string ImageCount = "ImageCount";	
			public const string IdentifierAccessUrl = "IdentifierAccessUrl";	
			public const string Volume = "Volume";	
			public const string Note = "Note";	
			public const string ScanOperator = "ScanOperator";	
			public const string ScanDate = "ScanDate";	
			public const string ExternalStatus = "ExternalStatus";	
			public const string MARCBibID = "MARCBibID";	
			public const string BarCode = "BarCode";	
			public const string IADateStamp = "IADateStamp";	
			public const string IAAddedDate = "IAAddedDate";	
			public const string LastOAIDataHarvestDate = "LastOAIDataHarvestDate";	
			public const string LastXMLDataHarvestDate = "LastXMLDataHarvestDate";	
			public const string LastProductionDate = "LastProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string ShortTitle = "ShortTitle";	
			public const string SponsorDate = "SponsorDate";	
			public const string TitleID = "TitleID";	
			public const string Year = "Year";	
			public const string IdentifierBib = "IdentifierBib";	
			public const string ZQuery = "ZQuery";	
			public const string LicenseUrl = "LicenseUrl";	
			public const string Rights = "Rights";	
			public const string DueDiligence = "DueDiligence";	
			public const string PossibleCopyrightStatus = "PossibleCopyrightStatus";	
			public const string CopyrightRegion = "CopyrightRegion";	
			public const string CopyrightComment = "CopyrightComment";	
			public const string CopyrightEvidence = "CopyrightEvidence";	
			public const string CopyrightEvidenceOperator = "CopyrightEvidenceOperator";	
			public const string CopyrightEvidenceDate = "CopyrightEvidenceDate";	
			public const string LocalFileFolder = "LocalFileFolder";	
			public const string NoMARCOk = "NoMARCOk";	
			public const string ScanningInstitution = "ScanningInstitution";	
			public const string RightsHolder = "RightsHolder";	
			public const string ItemDescription = "ItemDescription";	
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
			public const string PageProgression = "PageProgression";	
			public const string CreatedUserID = "CreatedUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";	
			public const string VirtualVolume = "VirtualVolume";	
			public const string VirtualTitleID = "VirtualTitleID";	
			public const string Summary = "Summary";	
			public const string Genre = "Genre";	
			public const string Issue = "Issue";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

