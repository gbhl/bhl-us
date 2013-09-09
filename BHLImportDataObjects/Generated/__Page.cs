
// Generated 2/26/2008 3:15:49 PM
// Do not modify the contents of this code file.
// This abstract class __Page is based upon Page.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class Page : __Page
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
	public abstract class __Page : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Page()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageID"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="pageDescription"></param>
		/// <param name="illustration"></param>
		/// <param name="note"></param>
		/// <param name="fileSize_Temp"></param>
		/// <param name="fileExtension"></param>
		/// <param name="active"></param>
		/// <param name="year"></param>
		/// <param name="series"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="externalURL"></param>
		/// <param name="altExternalURL"></param>
		/// <param name="issuePrefix"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="paginationUserID"></param>
		/// <param name="paginationDate"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __Page(int pageID, 
			int importStatusID, 
			int? importSourceID, 
			string barCode, 
			string fileNamePrefix, 
			int? sequenceOrder, 
			string pageDescription, 
			bool? illustration, 
			string note, 
			int? fileSize_Temp, 
			string fileExtension, 
			bool active, 
			string year, 
			string series, 
			string volume, 
			string issue, 
			string externalURL, 
			string altExternalURL, 
			string issuePrefix, 
			DateTime? lastPageNameLookupDate, 
			int? paginationUserID, 
			DateTime? paginationDate, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			int? externalCreationUser, 
			int? externalLastModifiedUser, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_PageID = pageID;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			BarCode = barCode;
			FileNamePrefix = fileNamePrefix;
			SequenceOrder = sequenceOrder;
			PageDescription = pageDescription;
			Illustration = illustration;
			Note = note;
			FileSize_Temp = fileSize_Temp;
			FileExtension = fileExtension;
			Active = active;
			Year = year;
			Series = series;
			Volume = volume;
			Issue = issue;
			ExternalURL = externalURL;
			AltExternalURL = altExternalURL;
			IssuePrefix = issuePrefix;
			LastPageNameLookupDate = lastPageNameLookupDate;
			PaginationUserID = paginationUserID;
			PaginationDate = paginationDate;
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
		~__Page()
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
					case "PageID" :
					{
						_PageID = (int)column.Value;
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
					case "BarCode" :
					{
						_BarCode = (string)column.Value;
						break;
					}
					case "FileNamePrefix" :
					{
						_FileNamePrefix = (string)column.Value;
						break;
					}
					case "SequenceOrder" :
					{
						_SequenceOrder = (int?)column.Value;
						break;
					}
					case "PageDescription" :
					{
						_PageDescription = (string)column.Value;
						break;
					}
					case "Illustration" :
					{
						_Illustration = (bool?)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
						break;
					}
					case "FileSize_Temp" :
					{
						_FileSize_Temp = (int?)column.Value;
						break;
					}
					case "FileExtension" :
					{
						_FileExtension = (string)column.Value;
						break;
					}
					case "Active" :
					{
						_Active = (bool)column.Value;
						break;
					}
					case "Year" :
					{
						_Year = (string)column.Value;
						break;
					}
					case "Series" :
					{
						_Series = (string)column.Value;
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
					case "ExternalURL" :
					{
						_ExternalURL = (string)column.Value;
						break;
					}
					case "AltExternalURL" :
					{
						_AltExternalURL = (string)column.Value;
						break;
					}
					case "IssuePrefix" :
					{
						_IssuePrefix = (string)column.Value;
						break;
					}
					case "LastPageNameLookupDate" :
					{
						_LastPageNameLookupDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationUserID" :
					{
						_PaginationUserID = (int?)column.Value;
						break;
					}
					case "PaginationDate" :
					{
						_PaginationDate = (DateTime?)column.Value;
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
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PageID
		{
			get
			{
				return _PageID;
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
		
		#endregion PageID
		
		#region ImportStatusID
		
		private int _ImportStatusID = default(int);
		
		/// <summary>
		/// Column: ImportStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
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
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region BarCode
		
		private string _BarCode = string.Empty;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=40)]
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
		
		#region FileNamePrefix
		
		private string _FileNamePrefix = string.Empty;
		
		/// <summary>
		/// Column: FileNamePrefix;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("FileNamePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
		public string FileNamePrefix
		{
			get
			{
				return _FileNamePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_FileNamePrefix != value)
				{
					_FileNamePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileNamePrefix
		
		#region SequenceOrder
		
		private int? _SequenceOrder = null;
		
		/// <summary>
		/// Column: SequenceOrder;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? SequenceOrder
		{
			get
			{
				return _SequenceOrder;
			}
			set
			{
				if (_SequenceOrder != value)
				{
					_SequenceOrder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SequenceOrder
		
		#region PageDescription
		
		private string _PageDescription = null;
		
		/// <summary>
		/// Column: PageDescription;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PageDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=255, IsNullable=true)]
		public string PageDescription
		{
			get
			{
				return _PageDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PageDescription != value)
				{
					_PageDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageDescription
		
		#region Illustration
		
		private bool? _Illustration = null;
		
		/// <summary>
		/// Column: Illustration;
		/// DBMS data type: bit; Nullable;
		/// </summary>
		[ColumnDefinition("Illustration", DbTargetType=SqlDbType.Bit, Ordinal=8, IsNullable=true)]
		public bool? Illustration
		{
			get
			{
				return _Illustration;
			}
			set
			{
				if (_Illustration != value)
				{
					_Illustration = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Illustration
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=255, IsNullable=true)]
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
		
		#region FileSize_Temp
		
		private int? _FileSize_Temp = null;
		
		/// <summary>
		/// Column: FileSize_Temp;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("FileSize_Temp", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? FileSize_Temp
		{
			get
			{
				return _FileSize_Temp;
			}
			set
			{
				if (_FileSize_Temp != value)
				{
					_FileSize_Temp = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileSize_Temp
		
		#region FileExtension
		
		private string _FileExtension = null;
		
		/// <summary>
		/// Column: FileExtension;
		/// DBMS data type: nvarchar(5); Nullable;
		/// </summary>
		[ColumnDefinition("FileExtension", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=5, IsNullable=true)]
		public string FileExtension
		{
			get
			{
				return _FileExtension;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 5);
				if (_FileExtension != value)
				{
					_FileExtension = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileExtension
		
		#region Active
		
		private bool _Active = false;
		
		/// <summary>
		/// Column: Active;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("Active", DbTargetType=SqlDbType.Bit, Ordinal=12)]
		public bool Active
		{
			get
			{
				return _Active;
			}
			set
			{
				if (_Active != value)
				{
					_Active = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Active
		
		#region Year
		
		private string _Year = null;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=20, IsNullable=true)]
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
		
		#region Series
		
		private string _Series = null;
		
		/// <summary>
		/// Column: Series;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Series", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=20, IsNullable=true)]
		public string Series
		{
			get
			{
				return _Series;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Series != value)
				{
					_Series = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Series
		
		#region Volume
		
		private string _Volume = null;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=20, IsNullable=true)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region Issue
		
		private string _Issue = null;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=20, IsNullable=true)]
		public string Issue
		{
			get
			{
				return _Issue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Issue != value)
				{
					_Issue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Issue
		
		#region ExternalURL
		
		private string _ExternalURL = null;
		
		/// <summary>
		/// Column: ExternalURL;
		/// DBMS data type: nvarchar(500); Nullable;
		/// </summary>
		[ColumnDefinition("ExternalURL", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=500, IsNullable=true)]
		public string ExternalURL
		{
			get
			{
				return _ExternalURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_ExternalURL != value)
				{
					_ExternalURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalURL
		
		#region AltExternalURL
		
		private string _AltExternalURL = null;
		
		/// <summary>
		/// Column: AltExternalURL;
		/// DBMS data type: nvarchar(500); Nullable;
		/// </summary>
		[ColumnDefinition("AltExternalURL", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=500, IsNullable=true)]
		public string AltExternalURL
		{
			get
			{
				return _AltExternalURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_AltExternalURL != value)
				{
					_AltExternalURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AltExternalURL
		
		#region IssuePrefix
		
		private string _IssuePrefix = null;
		
		/// <summary>
		/// Column: IssuePrefix;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("IssuePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=20, IsNullable=true)]
		public string IssuePrefix
		{
			get
			{
				return _IssuePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_IssuePrefix != value)
				{
					_IssuePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IssuePrefix
		
		#region LastPageNameLookupDate
		
		private DateTime? _LastPageNameLookupDate = null;
		
		/// <summary>
		/// Column: LastPageNameLookupDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastPageNameLookupDate", DbTargetType=SqlDbType.DateTime, Ordinal=20, IsNullable=true)]
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
		
		#region PaginationUserID
		
		private int? _PaginationUserID = null;
		
		/// <summary>
		/// Column: PaginationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationUserID", DbTargetType=SqlDbType.Int, Ordinal=21, NumericPrecision=10, IsNullable=true)]
		public int? PaginationUserID
		{
			get
			{
				return _PaginationUserID;
			}
			set
			{
				if (_PaginationUserID != value)
				{
					_PaginationUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationUserID
		
		#region PaginationDate
		
		private DateTime? _PaginationDate = null;
		
		/// <summary>
		/// Column: PaginationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationDate", DbTargetType=SqlDbType.DateTime, Ordinal=22, IsNullable=true)]
		public DateTime? PaginationDate
		{
			get
			{
				return _PaginationDate;
			}
			set
			{
				if (_PaginationDate != value)
				{
					_PaginationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationDate
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=23, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=24, IsNullable=true)]
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
		[ColumnDefinition("ExternalCreationUser", DbTargetType=SqlDbType.Int, Ordinal=25, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedUser", DbTargetType=SqlDbType.Int, Ordinal=26, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=27, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=28)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=29)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Page"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Page"/>, 
		/// returns an instance of <see cref="__Page"/>; otherwise returns null.</returns>
		public static new __Page FromArray(byte[] byteArray)
		{
			__Page o = null;
			
			try
			{
				o = (__Page) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Page"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Page"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Page)
			{
				__Page o = (__Page) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageID == PageID &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.FileNamePrefix) == GetComparisonString(FileNamePrefix) &&
					o.SequenceOrder == SequenceOrder &&
					GetComparisonString(o.PageDescription) == GetComparisonString(PageDescription) &&
					o.Illustration == Illustration &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
					o.FileSize_Temp == FileSize_Temp &&
					GetComparisonString(o.FileExtension) == GetComparisonString(FileExtension) &&
					o.Active == Active &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.Series) == GetComparisonString(Series) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.ExternalURL) == GetComparisonString(ExternalURL) &&
					GetComparisonString(o.AltExternalURL) == GetComparisonString(AltExternalURL) &&
					GetComparisonString(o.IssuePrefix) == GetComparisonString(IssuePrefix) &&
					o.LastPageNameLookupDate == LastPageNameLookupDate &&
					o.PaginationUserID == PaginationUserID &&
					o.PaginationDate == PaginationDate &&
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
				throw new ArgumentException("Argument is not of type __Page");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Page"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Page"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Page a, __Page b)
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
		/// <param name="a">The first <see cref="__Page"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Page"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Page a, __Page b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Page"/> object to compare with the current <see cref="__Page"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Page))
			{
				return false;
			}
			
			return this == (__Page) obj;
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
		/// list.Sort(SortOrder.Ascending, __Page.SortColumn.PageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageID = "PageID";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string BarCode = "BarCode";	
			public const string FileNamePrefix = "FileNamePrefix";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string PageDescription = "PageDescription";	
			public const string Illustration = "Illustration";	
			public const string Note = "Note";	
			public const string FileSize_Temp = "FileSize_Temp";	
			public const string FileExtension = "FileExtension";	
			public const string Active = "Active";	
			public const string Year = "Year";	
			public const string Series = "Series";	
			public const string Volume = "Volume";	
			public const string Issue = "Issue";	
			public const string ExternalURL = "ExternalURL";	
			public const string AltExternalURL = "AltExternalURL";	
			public const string IssuePrefix = "IssuePrefix";	
			public const string LastPageNameLookupDate = "LastPageNameLookupDate";	
			public const string PaginationUserID = "PaginationUserID";	
			public const string PaginationDate = "PaginationDate";	
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
