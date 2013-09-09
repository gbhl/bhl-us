
// Generated 12/18/2008 2:14:22 PM
// Do not modify the contents of this code file.
// This abstract class __Item is based upon Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
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

namespace MOBOT.BHLImport.DataObjects
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
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="mARCBibID"></param>
		/// <param name="barCode"></param>
		/// <param name="itemSequence"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="institutionCode"></param>
		/// <param name="languageCode"></param>
		/// <param name="sponsor"></param>
		/// <param name="itemDescription"></param>
		/// <param name="scannedBy"></param>
		/// <param name="pDFSize"></param>
		/// <param name="vaultID"></param>
		/// <param name="numberOfFiles"></param>
		/// <param name="note"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="year"></param>
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="copyrightEvidenceOperator"></param>
		/// <param name="copyrightEvidenceDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __Item(int itemID, 
			string importKey, 
			int importStatusID, 
			int? importSourceID, 
			string mARCBibID, 
			string barCode, 
			short? itemSequence, 
			string mARCItemID, 
			string callNumber, 
			string volume, 
			string institutionCode, 
			string languageCode, 
			string sponsor, 
			string itemDescription, 
			int? scannedBy, 
			int? pDFSize, 
			int? vaultID, 
			short? numberOfFiles, 
			string note, 
			int itemStatusID, 
			string scanningUser, 
			DateTime? scanningDate, 
			string year, 
			string identifierBib, 
			string zQuery, 
			string licenseUrl, 
			string rights, 
			string dueDiligence, 
			string copyrightStatus, 
			string copyrightRegion, 
			string copyrightComment, 
			string copyrightEvidence, 
			string copyrightEvidenceOperator, 
			string copyrightEvidenceDate, 
			int? paginationCompleteUserID, 
			DateTime? paginationCompleteDate, 
			int? paginationStatusID, 
			int? paginationStatusUserID, 
			DateTime? paginationStatusDate, 
			DateTime? lastPageNameLookupDate, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			int? externalCreationUser, 
			int? externalLastModifiedUser, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_ItemID = itemID;
			ImportKey = importKey;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			MARCBibID = mARCBibID;
			BarCode = barCode;
			ItemSequence = itemSequence;
			MARCItemID = mARCItemID;
			CallNumber = callNumber;
			Volume = volume;
			InstitutionCode = institutionCode;
			LanguageCode = languageCode;
			Sponsor = sponsor;
			ItemDescription = itemDescription;
			ScannedBy = scannedBy;
			PDFSize = pDFSize;
			VaultID = vaultID;
			NumberOfFiles = numberOfFiles;
			Note = note;
			ItemStatusID = itemStatusID;
			ScanningUser = scanningUser;
			ScanningDate = scanningDate;
			Year = year;
			IdentifierBib = identifierBib;
			ZQuery = zQuery;
			LicenseUrl = licenseUrl;
			Rights = rights;
			DueDiligence = dueDiligence;
			CopyrightStatus = copyrightStatus;
			CopyrightRegion = copyrightRegion;
			CopyrightComment = copyrightComment;
			CopyrightEvidence = copyrightEvidence;
			CopyrightEvidenceOperator = copyrightEvidenceOperator;
			CopyrightEvidenceDate = copyrightEvidenceDate;
			PaginationCompleteUserID = paginationCompleteUserID;
			PaginationCompleteDate = paginationCompleteDate;
			PaginationStatusID = paginationStatusID;
			PaginationStatusUserID = paginationStatusUserID;
			PaginationStatusDate = paginationStatusDate;
			LastPageNameLookupDate = lastPageNameLookupDate;
			ExternalCreationDate = externalCreationDate;
			ExternalLastModifiedDate = externalLastModifiedDate;
			ExternalCreationUser = externalCreationUser;
			ExternalLastModifiedUser = externalLastModifiedUser;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
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
					case "ImportKey" :
					{
						_ImportKey = (string)column.Value;
						break;
					}
					case "ImportStatusID" :
					{
						_ImportStatusID = (int)column.Value;
						break;
					}
					case "ImportSourceID" :
					{
						_ImportSourceID = (int?)column.Value;
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
					case "ItemSequence" :
					{
						_ItemSequence = (short?)column.Value;
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
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
						break;
					}
					case "Sponsor" :
					{
						_Sponsor = (string)column.Value;
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
					case "NumberOfFiles" :
					{
						_NumberOfFiles = (short?)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
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
					case "ExternalCreationDate" :
					{
						_ExternalCreationDate = (DateTime?)column.Value;
						break;
					}
					case "ExternalLastModifiedDate" :
					{
						_ExternalLastModifiedDate = (DateTime?)column.Value;
						break;
					}
					case "ExternalCreationUser" :
					{
						_ExternalCreationUser = (int?)column.Value;
						break;
					}
					case "ExternalLastModifiedUser" :
					{
						_ExternalLastModifiedUser = (int?)column.Value;
						break;
					}
					case "ProductionDate" :
					{
						_ProductionDate = (DateTime?)column.Value;
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
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
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
		
		#region ImportKey
		
		private string _ImportKey = string.Empty;
		
		/// <summary>
		/// Column: ImportKey;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ImportKey", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string ImportKey
		{
			get
			{
				return _ImportKey;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ImportKey != value)
				{
					_ImportKey = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportKey
		
		#region ImportStatusID
		
		private int _ImportStatusID = default(int);
		
		/// <summary>
		/// Column: ImportStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportStatusID
		{
			get
			{
				return _ImportStatusID;
			}
			set
			{
				if (_ImportStatusID != value)
				{
					_ImportStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportStatusID
		
		#region ImportSourceID
		
		private int? _ImportSourceID = null;
		
		/// <summary>
		/// Column: ImportSourceID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? ImportSourceID
		{
			get
			{
				return _ImportSourceID;
			}
			set
			{
				if (_ImportSourceID != value)
				{
					_ImportSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportSourceID
		
		#region MARCBibID
		
		private string _MARCBibID = string.Empty;
		
		/// <summary>
		/// Column: MARCBibID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("MARCBibID", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
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
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=40)]
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
		
		#region ItemSequence
		
		private short? _ItemSequence = null;
		
		/// <summary>
		/// Column: ItemSequence;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("ItemSequence", DbTargetType=SqlDbType.SmallInt, Ordinal=7, NumericPrecision=5, IsNullable=true)]
		public short? ItemSequence
		{
			get
			{
				return _ItemSequence;
			}
			set
			{
				if (_ItemSequence != value)
				{
					_ItemSequence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemSequence
		
		#region MARCItemID
		
		private string _MARCItemID = null;
		
		/// <summary>
		/// Column: MARCItemID;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("MARCItemID", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50, IsNullable=true)]
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
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=100, IsNullable=true)]
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
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=10, IsNullable=true)]
		public string InstitutionCode
		{
			get
			{
				return _InstitutionCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_InstitutionCode != value)
				{
					_InstitutionCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionCode
		
		#region LanguageCode
		
		private string _LanguageCode = null;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=10, IsNullable=true)]
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
		
		#region Sponsor
		
		private string _Sponsor = null;
		
		/// <summary>
		/// Column: Sponsor;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("Sponsor", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region ItemDescription
		
		private string _ItemDescription = null;
		
		/// <summary>
		/// Column: ItemDescription;
		/// DBMS data type: ntext; Nullable;
		/// </summary>
		[ColumnDefinition("ItemDescription", DbTargetType=SqlDbType.NText, Ordinal=14, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("ScannedBy", DbTargetType=SqlDbType.Int, Ordinal=15, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("PDFSize", DbTargetType=SqlDbType.Int, Ordinal=16, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("VaultID", DbTargetType=SqlDbType.Int, Ordinal=17, NumericPrecision=10, IsNullable=true)]
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
		
		#region NumberOfFiles
		
		private short? _NumberOfFiles = null;
		
		/// <summary>
		/// Column: NumberOfFiles;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("NumberOfFiles", DbTargetType=SqlDbType.SmallInt, Ordinal=18, NumericPrecision=5, IsNullable=true)]
		public short? NumberOfFiles
		{
			get
			{
				return _NumberOfFiles;
			}
			set
			{
				if (_NumberOfFiles != value)
				{
					_NumberOfFiles = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NumberOfFiles
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=255, IsNullable=true)]
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
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=20, NumericPrecision=10)]
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
		[ColumnDefinition("ScanningUser", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=100, IsNullable=true)]
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
		[ColumnDefinition("ScanningDate", DbTargetType=SqlDbType.DateTime, Ordinal=22, IsNullable=true)]
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
		
		#region Year
		
		private string _Year = null;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=20, IsNullable=true)]
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
		[ColumnDefinition("IdentifierBib", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=50, IsNullable=true)]
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
		
		private string _ZQuery = null;
		
		/// <summary>
		/// Column: ZQuery;
		/// DBMS data type: nvarchar(200); Nullable;
		/// </summary>
		[ColumnDefinition("ZQuery", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=200, IsNullable=true)]
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
		
		private string _LicenseUrl = null;
		
		/// <summary>
		/// Column: LicenseUrl;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("LicenseUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=26, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("Rights", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("DueDiligence", DbTargetType=SqlDbType.NVarChar, Ordinal=28, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("CopyrightStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("CopyrightRegion", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=50, IsNullable=true)]
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
		[ColumnDefinition("CopyrightComment", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("CopyrightEvidence", DbTargetType=SqlDbType.NVarChar, Ordinal=32, CharacterMaxLength=1073741823, IsNullable=true)]
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
		[ColumnDefinition("CopyrightEvidenceOperator", DbTargetType=SqlDbType.NVarChar, Ordinal=33, CharacterMaxLength=100, IsNullable=true)]
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
		[ColumnDefinition("CopyrightEvidenceDate", DbTargetType=SqlDbType.NVarChar, Ordinal=34, CharacterMaxLength=30, IsNullable=true)]
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
		
		#region PaginationCompleteUserID
		
		private int? _PaginationCompleteUserID = null;
		
		/// <summary>
		/// Column: PaginationCompleteUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteUserID", DbTargetType=SqlDbType.Int, Ordinal=35, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("PaginationCompleteDate", DbTargetType=SqlDbType.DateTime, Ordinal=36, IsNullable=true)]
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
		[ColumnDefinition("PaginationStatusID", DbTargetType=SqlDbType.Int, Ordinal=37, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("PaginationStatusUserID", DbTargetType=SqlDbType.Int, Ordinal=38, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("PaginationStatusDate", DbTargetType=SqlDbType.DateTime, Ordinal=39, IsNullable=true)]
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
		[ColumnDefinition("LastPageNameLookupDate", DbTargetType=SqlDbType.DateTime, Ordinal=40, IsNullable=true)]
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
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=41, IsNullable=true)]
		public DateTime? ExternalCreationDate
		{
			get
			{
				return _ExternalCreationDate;
			}
			set
			{
				if (_ExternalCreationDate != value)
				{
					_ExternalCreationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalCreationDate
		
		#region ExternalLastModifiedDate
		
		private DateTime? _ExternalLastModifiedDate = null;
		
		/// <summary>
		/// Column: ExternalLastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=42, IsNullable=true)]
		public DateTime? ExternalLastModifiedDate
		{
			get
			{
				return _ExternalLastModifiedDate;
			}
			set
			{
				if (_ExternalLastModifiedDate != value)
				{
					_ExternalLastModifiedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalLastModifiedDate
		
		#region ExternalCreationUser
		
		private int? _ExternalCreationUser = null;
		
		/// <summary>
		/// Column: ExternalCreationUser;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationUser", DbTargetType=SqlDbType.Int, Ordinal=43, NumericPrecision=10, IsNullable=true)]
		public int? ExternalCreationUser
		{
			get
			{
				return _ExternalCreationUser;
			}
			set
			{
				if (_ExternalCreationUser != value)
				{
					_ExternalCreationUser = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalCreationUser
		
		#region ExternalLastModifiedUser
		
		private int? _ExternalLastModifiedUser = null;
		
		/// <summary>
		/// Column: ExternalLastModifiedUser;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalLastModifiedUser", DbTargetType=SqlDbType.Int, Ordinal=44, NumericPrecision=10, IsNullable=true)]
		public int? ExternalLastModifiedUser
		{
			get
			{
				return _ExternalLastModifiedUser;
			}
			set
			{
				if (_ExternalLastModifiedUser != value)
				{
					_ExternalLastModifiedUser = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalLastModifiedUser
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=45, IsNullable=true)]
		public DateTime? ProductionDate
		{
			get
			{
				return _ProductionDate;
			}
			set
			{
				if (_ProductionDate != value)
				{
					_ProductionDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ProductionDate
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=46)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=47)]
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
					GetComparisonString(o.ImportKey) == GetComparisonString(ImportKey) &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.MARCBibID) == GetComparisonString(MARCBibID) &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					o.ItemSequence == ItemSequence &&
					GetComparisonString(o.MARCItemID) == GetComparisonString(MARCItemID) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					GetComparisonString(o.Sponsor) == GetComparisonString(Sponsor) &&
					GetComparisonString(o.ItemDescription) == GetComparisonString(ItemDescription) &&
					o.ScannedBy == ScannedBy &&
					o.PDFSize == PDFSize &&
					o.VaultID == VaultID &&
					o.NumberOfFiles == NumberOfFiles &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					o.ItemStatusID == ItemStatusID &&
					GetComparisonString(o.ScanningUser) == GetComparisonString(ScanningUser) &&
					o.ScanningDate == ScanningDate &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.IdentifierBib) == GetComparisonString(IdentifierBib) &&
					GetComparisonString(o.ZQuery) == GetComparisonString(ZQuery) &&
					GetComparisonString(o.LicenseUrl) == GetComparisonString(LicenseUrl) &&
					GetComparisonString(o.Rights) == GetComparisonString(Rights) &&
					GetComparisonString(o.DueDiligence) == GetComparisonString(DueDiligence) &&
					GetComparisonString(o.CopyrightStatus) == GetComparisonString(CopyrightStatus) &&
					GetComparisonString(o.CopyrightRegion) == GetComparisonString(CopyrightRegion) &&
					GetComparisonString(o.CopyrightComment) == GetComparisonString(CopyrightComment) &&
					GetComparisonString(o.CopyrightEvidence) == GetComparisonString(CopyrightEvidence) &&
					GetComparisonString(o.CopyrightEvidenceOperator) == GetComparisonString(CopyrightEvidenceOperator) &&
					GetComparisonString(o.CopyrightEvidenceDate) == GetComparisonString(CopyrightEvidenceDate) &&
					o.PaginationCompleteUserID == PaginationCompleteUserID &&
					o.PaginationCompleteDate == PaginationCompleteDate &&
					o.PaginationStatusID == PaginationStatusID &&
					o.PaginationStatusUserID == PaginationStatusUserID &&
					o.PaginationStatusDate == PaginationStatusDate &&
					o.LastPageNameLookupDate == LastPageNameLookupDate &&
					o.ExternalCreationDate == ExternalCreationDate &&
					o.ExternalLastModifiedDate == ExternalLastModifiedDate &&
					o.ExternalCreationUser == ExternalCreationUser &&
					o.ExternalLastModifiedUser == ExternalLastModifiedUser &&
					o.ProductionDate == ProductionDate &&
					o.CreatedDate == CreatedDate &&
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
			public const string ImportKey = "ImportKey";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string MARCBibID = "MARCBibID";	
			public const string BarCode = "BarCode";	
			public const string ItemSequence = "ItemSequence";	
			public const string MARCItemID = "MARCItemID";	
			public const string CallNumber = "CallNumber";	
			public const string Volume = "Volume";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string LanguageCode = "LanguageCode";	
			public const string Sponsor = "Sponsor";	
			public const string ItemDescription = "ItemDescription";	
			public const string ScannedBy = "ScannedBy";	
			public const string PDFSize = "PDFSize";	
			public const string VaultID = "VaultID";	
			public const string NumberOfFiles = "NumberOfFiles";	
			public const string Note = "Note";	
			public const string ItemStatusID = "ItemStatusID";	
			public const string ScanningUser = "ScanningUser";	
			public const string ScanningDate = "ScanningDate";	
			public const string Year = "Year";	
			public const string IdentifierBib = "IdentifierBib";	
			public const string ZQuery = "ZQuery";	
			public const string LicenseUrl = "LicenseUrl";	
			public const string Rights = "Rights";	
			public const string DueDiligence = "DueDiligence";	
			public const string CopyrightStatus = "CopyrightStatus";	
			public const string CopyrightRegion = "CopyrightRegion";	
			public const string CopyrightComment = "CopyrightComment";	
			public const string CopyrightEvidence = "CopyrightEvidence";	
			public const string CopyrightEvidenceOperator = "CopyrightEvidenceOperator";	
			public const string CopyrightEvidenceDate = "CopyrightEvidenceDate";	
			public const string PaginationCompleteUserID = "PaginationCompleteUserID";	
			public const string PaginationCompleteDate = "PaginationCompleteDate";	
			public const string PaginationStatusID = "PaginationStatusID";	
			public const string PaginationStatusUserID = "PaginationStatusUserID";	
			public const string PaginationStatusDate = "PaginationStatusDate";	
			public const string LastPageNameLookupDate = "LastPageNameLookupDate";	
			public const string ExternalCreationDate = "ExternalCreationDate";	
			public const string ExternalLastModifiedDate = "ExternalLastModifiedDate";	
			public const string ExternalCreationUser = "ExternalCreationUser";	
			public const string ExternalLastModifiedUser = "ExternalLastModifiedUser";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
