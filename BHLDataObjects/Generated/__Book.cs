
// Generated 4/27/2021 1:21:13 PM
// Do not modify the contents of this code file.
// This abstract class __Book is based upon dbo.Book.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Book : __Book
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
	public abstract class __Book : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Book()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="bookID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectBookID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="callNumber"></param>
		/// <param name="volume"></param>
		/// <param name="startYear"></param>
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
		/// <param name="identifierBib"></param>
		/// <param name="zQuery"></param>
		/// <param name="sponsor"></param>
		/// <param name="externalUrl"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rights"></param>
		/// <param name="dueDiligence"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="isVirtual"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="pageProgression"></param>
		public __Book(int bookID, 
			int itemID, 
			int? redirectBookID, 
			int? thumbnailPageID, 
			string languageCode, 
			string barCode, 
			string mARCItemID, 
			string callNumber, 
			string volume, 
			string startYear, 
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
			string identifierBib, 
			string zQuery, 
			string sponsor, 
			string externalUrl, 
			string licenseUrl, 
			string rights, 
			string dueDiligence, 
			string copyrightStatus, 
			string copyrightRegion, 
			string copyrightComment, 
			string copyrightEvidence, 
			string scanningUser, 
			DateTime? scanningDate, 
			int? paginationStatusID, 
			DateTime? paginationStatusDate, 
			int? paginationStatusUserID, 
			DateTime? paginationCompleteDate, 
			int? paginationCompleteUserID, 
			DateTime? lastPageNameLookupDate, 
			byte isVirtual, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID, 
			string pageProgression) : this()
		{
			_BookID = bookID;
			ItemID = itemID;
			RedirectBookID = redirectBookID;
			ThumbnailPageID = thumbnailPageID;
			LanguageCode = languageCode;
			BarCode = barCode;
			MARCItemID = mARCItemID;
			CallNumber = callNumber;
			Volume = volume;
			StartYear = startYear;
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
			IdentifierBib = identifierBib;
			ZQuery = zQuery;
			Sponsor = sponsor;
			ExternalUrl = externalUrl;
			LicenseUrl = licenseUrl;
			Rights = rights;
			DueDiligence = dueDiligence;
			CopyrightStatus = copyrightStatus;
			CopyrightRegion = copyrightRegion;
			CopyrightComment = copyrightComment;
			CopyrightEvidence = copyrightEvidence;
			ScanningUser = scanningUser;
			ScanningDate = scanningDate;
			PaginationStatusID = paginationStatusID;
			PaginationStatusDate = paginationStatusDate;
			PaginationStatusUserID = paginationStatusUserID;
			PaginationCompleteDate = paginationCompleteDate;
			PaginationCompleteUserID = paginationCompleteUserID;
			LastPageNameLookupDate = lastPageNameLookupDate;
			IsVirtual = isVirtual;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
			PageProgression = pageProgression;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Book()
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
					case "BookID" :
					{
						_BookID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "RedirectBookID" :
					{
						_RedirectBookID = (int?)column.Value;
						break;
					}
					case "ThumbnailPageID" :
					{
						_ThumbnailPageID = (int?)column.Value;
						break;
					}
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
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
					case "StartYear" :
					{
						_StartYear = (string)column.Value;
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
					case "Sponsor" :
					{
						_Sponsor = (string)column.Value;
						break;
					}
					case "ExternalUrl" :
					{
						_ExternalUrl = (string)column.Value;
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
					case "PaginationStatusID" :
					{
						_PaginationStatusID = (int?)column.Value;
						break;
					}
					case "PaginationStatusDate" :
					{
						_PaginationStatusDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationStatusUserID" :
					{
						_PaginationStatusUserID = (int?)column.Value;
						break;
					}
					case "PaginationCompleteDate" :
					{
						_PaginationCompleteDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationCompleteUserID" :
					{
						_PaginationCompleteUserID = (int?)column.Value;
						break;
					}
					case "LastPageNameLookupDate" :
					{
						_LastPageNameLookupDate = (DateTime?)column.Value;
						break;
					}
					case "IsVirtual" :
					{
						_IsVirtual = (byte)column.Value;
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
					case "PageProgression" :
					{
						_PageProgression = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region BookID
		
		private int _BookID = default(int);
		
		/// <summary>
		/// Column: BookID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("BookID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int BookID
		{
			get
			{
				return _BookID;
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
		
		#endregion BookID
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
			}
			set
			{
				if (_ItemID != value)
				{
					_ItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemID
		
		#region RedirectBookID
		
		private int? _RedirectBookID = null;
		
		/// <summary>
		/// Column: RedirectBookID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectBookID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? RedirectBookID
		{
			get
			{
				return _RedirectBookID;
			}
			set
			{
				if (_RedirectBookID != value)
				{
					_RedirectBookID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectBookID
		
		#region ThumbnailPageID
		
		private int? _ThumbnailPageID = null;
		
		/// <summary>
		/// Column: ThumbnailPageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ThumbnailPageID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
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
		
		#region LanguageCode
		
		private string _LanguageCode = null;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region BarCode
		
		private string _BarCode = string.Empty;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=200)]
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
		
		#region MARCItemID
		
		private string _MARCItemID = null;
		
		/// <summary>
		/// Column: MARCItemID;
		/// DBMS data type: nvarchar(200); Nullable;
		/// </summary>
		[ColumnDefinition("MARCItemID", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=200, IsNullable=true)]
		public string MARCItemID
		{
			get
			{
				return _MARCItemID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
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
		[ColumnDefinition("CallNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=100, IsNullable=true)]
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
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region StartYear
		
		private string _StartYear = null;
		
		/// <summary>
		/// Column: StartYear;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("StartYear", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=20, IsNullable=true)]
		public string StartYear
		{
			get
			{
				return _StartYear;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_StartYear != value)
				{
					_StartYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartYear
		
		#region EndYear
		
		private string _EndYear = string.Empty;
		
		/// <summary>
		/// Column: EndYear;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("EndYear", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=20)]
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
		[ColumnDefinition("StartVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndVolume", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartIssue", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndIssue", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartSeries", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndSeries", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=10)]
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
		[ColumnDefinition("StartPart", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=10)]
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
		[ColumnDefinition("EndPart", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=10)]
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
		
		#region IdentifierBib
		
		private string _IdentifierBib = null;
		
		/// <summary>
		/// Column: IdentifierBib;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("IdentifierBib", DbTargetType=SqlDbType.NVarChar, Ordinal=22, CharacterMaxLength=50, IsNullable=true)]
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
		[ColumnDefinition("ZQuery", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=200, IsNullable=true)]
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
		[ColumnDefinition("Sponsor", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region ExternalUrl
		
		private string _ExternalUrl = null;
		
		/// <summary>
		/// Column: ExternalUrl;
		/// DBMS data type: nvarchar(500); Nullable;
		/// </summary>
		[ColumnDefinition("ExternalUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=500, IsNullable=true)]
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
		
		#region ScanningUser
		
		private string _ScanningUser = null;
		
		/// <summary>
		/// Column: ScanningUser;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("ScanningUser", DbTargetType=SqlDbType.NVarChar, Ordinal=33, CharacterMaxLength=100, IsNullable=true)]
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
		[ColumnDefinition("ScanningDate", DbTargetType=SqlDbType.DateTime, Ordinal=34, IsNullable=true)]
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
		
		#region PaginationStatusID
		
		private int? _PaginationStatusID = null;
		
		/// <summary>
		/// Column: PaginationStatusID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusID", DbTargetType=SqlDbType.Int, Ordinal=35, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region PaginationStatusDate
		
		private DateTime? _PaginationStatusDate = null;
		
		/// <summary>
		/// Column: PaginationStatusDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusDate", DbTargetType=SqlDbType.DateTime, Ordinal=36, IsNullable=true)]
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
		
		#region PaginationStatusUserID
		
		private int? _PaginationStatusUserID = null;
		
		/// <summary>
		/// Column: PaginationStatusUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusUserID", DbTargetType=SqlDbType.Int, Ordinal=37, NumericPrecision=10, IsNullable=true)]
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
		
		#region PaginationCompleteDate
		
		private DateTime? _PaginationCompleteDate = null;
		
		/// <summary>
		/// Column: PaginationCompleteDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteDate", DbTargetType=SqlDbType.DateTime, Ordinal=38, IsNullable=true)]
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
		
		#region PaginationCompleteUserID
		
		private int? _PaginationCompleteUserID = null;
		
		/// <summary>
		/// Column: PaginationCompleteUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteUserID", DbTargetType=SqlDbType.Int, Ordinal=39, NumericPrecision=10, IsNullable=true)]
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
		
		#region IsVirtual
		
		private byte _IsVirtual = default(byte);
		
		/// <summary>
		/// Column: IsVirtual;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("IsVirtual", DbTargetType=SqlDbType.TinyInt, Ordinal=41, NumericPrecision=3)]
		public byte IsVirtual
		{
			get
			{
				return _IsVirtual;
			}
			set
			{
				if (_IsVirtual != value)
				{
					_IsVirtual = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsVirtual
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=42, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=43, IsNullable=true)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=44, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=45, NumericPrecision=10, IsNullable=true)]
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
		
		#region PageProgression
		
		private string _PageProgression = string.Empty;
		
		/// <summary>
		/// Column: PageProgression;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("PageProgression", DbTargetType=SqlDbType.NVarChar, Ordinal=46, CharacterMaxLength=10)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Book"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Book"/>, 
		/// returns an instance of <see cref="__Book"/>; otherwise returns null.</returns>
		public static new __Book FromArray(byte[] byteArray)
		{
			__Book o = null;
			
			try
			{
				o = (__Book) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Book"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Book"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Book)
			{
				__Book o = (__Book) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.BookID == BookID &&
					o.ItemID == ItemID &&
					o.RedirectBookID == RedirectBookID &&
					o.ThumbnailPageID == ThumbnailPageID &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.MARCItemID) == GetComparisonString(MARCItemID) &&
					GetComparisonString(o.CallNumber) == GetComparisonString(CallNumber) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.StartYear) == GetComparisonString(StartYear) &&
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
					GetComparisonString(o.IdentifierBib) == GetComparisonString(IdentifierBib) &&
					GetComparisonString(o.ZQuery) == GetComparisonString(ZQuery) &&
					GetComparisonString(o.Sponsor) == GetComparisonString(Sponsor) &&
					GetComparisonString(o.ExternalUrl) == GetComparisonString(ExternalUrl) &&
					GetComparisonString(o.LicenseUrl) == GetComparisonString(LicenseUrl) &&
					GetComparisonString(o.Rights) == GetComparisonString(Rights) &&
					GetComparisonString(o.DueDiligence) == GetComparisonString(DueDiligence) &&
					GetComparisonString(o.CopyrightStatus) == GetComparisonString(CopyrightStatus) &&
					GetComparisonString(o.CopyrightRegion) == GetComparisonString(CopyrightRegion) &&
					GetComparisonString(o.CopyrightComment) == GetComparisonString(CopyrightComment) &&
					GetComparisonString(o.CopyrightEvidence) == GetComparisonString(CopyrightEvidence) &&
					GetComparisonString(o.ScanningUser) == GetComparisonString(ScanningUser) &&
					o.ScanningDate == ScanningDate &&
					o.PaginationStatusID == PaginationStatusID &&
					o.PaginationStatusDate == PaginationStatusDate &&
					o.PaginationStatusUserID == PaginationStatusUserID &&
					o.PaginationCompleteDate == PaginationCompleteDate &&
					o.PaginationCompleteUserID == PaginationCompleteUserID &&
					o.LastPageNameLookupDate == LastPageNameLookupDate &&
					o.IsVirtual == IsVirtual &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID &&
					GetComparisonString(o.PageProgression) == GetComparisonString(PageProgression) 
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
				throw new ArgumentException("Argument is not of type __Book");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Book"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Book"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Book a, __Book b)
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
		/// <param name="a">The first <see cref="__Book"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Book"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Book a, __Book b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Book"/> object to compare with the current <see cref="__Book"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Book))
			{
				return false;
			}
			
			return this == (__Book) obj;
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
		/// list.Sort(SortOrder.Ascending, __Book.SortColumn.BookID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string BookID = "BookID";	
			public const string ItemID = "ItemID";	
			public const string RedirectBookID = "RedirectBookID";	
			public const string ThumbnailPageID = "ThumbnailPageID";	
			public const string LanguageCode = "LanguageCode";	
			public const string BarCode = "BarCode";	
			public const string MARCItemID = "MARCItemID";	
			public const string CallNumber = "CallNumber";	
			public const string Volume = "Volume";	
			public const string StartYear = "StartYear";	
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
			public const string IdentifierBib = "IdentifierBib";	
			public const string ZQuery = "ZQuery";	
			public const string Sponsor = "Sponsor";	
			public const string ExternalUrl = "ExternalUrl";	
			public const string LicenseUrl = "LicenseUrl";	
			public const string Rights = "Rights";	
			public const string DueDiligence = "DueDiligence";	
			public const string CopyrightStatus = "CopyrightStatus";	
			public const string CopyrightRegion = "CopyrightRegion";	
			public const string CopyrightComment = "CopyrightComment";	
			public const string CopyrightEvidence = "CopyrightEvidence";	
			public const string ScanningUser = "ScanningUser";	
			public const string ScanningDate = "ScanningDate";	
			public const string PaginationStatusID = "PaginationStatusID";	
			public const string PaginationStatusDate = "PaginationStatusDate";	
			public const string PaginationStatusUserID = "PaginationStatusUserID";	
			public const string PaginationCompleteDate = "PaginationCompleteDate";	
			public const string PaginationCompleteUserID = "PaginationCompleteUserID";	
			public const string LastPageNameLookupDate = "LastPageNameLookupDate";	
			public const string IsVirtual = "IsVirtual";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";	
			public const string PageProgression = "PageProgression";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

