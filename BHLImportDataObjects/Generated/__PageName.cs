
// Generated 1/16/2008 1:54:48 PM
// Do not modify the contents of this code file.
// This abstract class __PageName is based upon PageName.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class PageName : __PageName
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
	public abstract class __PageName : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageName()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageNameID"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="source"></param>
		/// <param name="nameFound"></param>
		/// <param name="nameConfirmed"></param>
		/// <param name="nameBankID"></param>
		/// <param name="active"></param>
		/// <param name="externalCreateDate"></param>
		/// <param name="externalLastUpdateDate"></param>
		/// <param name="isCommonName"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __PageName(int pageNameID, 
			int importStatusID, 
			int? importSourceID, 
			string barCode, 
			string fileNamePrefix, 
			int? sequenceOrder, 
			string source, 
			string nameFound, 
			string nameConfirmed, 
			int? nameBankID, 
			bool? active, 
			DateTime? externalCreateDate, 
			DateTime? externalLastUpdateDate, 
			bool? isCommonName, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_PageNameID = pageNameID;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			BarCode = barCode;
			FileNamePrefix = fileNamePrefix;
			SequenceOrder = sequenceOrder;
			Source = source;
			NameFound = nameFound;
			NameConfirmed = nameConfirmed;
			NameBankID = nameBankID;
			Active = active;
			ExternalCreateDate = externalCreateDate;
			ExternalLastUpdateDate = externalLastUpdateDate;
			IsCommonName = isCommonName;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PageName()
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
					case "PageNameID" :
					{
						_PageNameID = (int)column.Value;
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
					case "Source" :
					{
						_Source = (string)column.Value;
						break;
					}
					case "NameFound" :
					{
						_NameFound = (string)column.Value;
						break;
					}
					case "NameConfirmed" :
					{
						_NameConfirmed = (string)column.Value;
						break;
					}
					case "NameBankID" :
					{
						_NameBankID = (int?)column.Value;
						break;
					}
					case "Active" :
					{
						_Active = (bool?)column.Value;
						break;
					}
					case "ExternalCreateDate" :
					{
						_ExternalCreateDate = (DateTime?)column.Value;
						break;
					}
					case "ExternalLastUpdateDate" :
					{
						_ExternalLastUpdateDate = (DateTime?)column.Value;
						break;
					}
					case "IsCommonName" :
					{
						_IsCommonName = (bool?)column.Value;
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
		
		#region PageNameID
		
		private int _PageNameID = default(int);
		
		/// <summary>
		/// Column: PageNameID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageNameID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PageNameID
		{
			get
			{
				return _PageNameID;
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
		
		#endregion PageNameID
		
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
		[ColumnDefinition("FileNamePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=200)]
		public string FileNamePrefix
		{
			get
			{
				return _FileNamePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
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
		
		#region Source
		
		private string _Source = null;
		
		/// <summary>
		/// Column: Source;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("Source", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=50, IsNullable=true)]
		public string Source
		{
			get
			{
				return _Source;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Source != value)
				{
					_Source = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Source
		
		#region NameFound
		
		private string _NameFound = null;
		
		/// <summary>
		/// Column: NameFound;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("NameFound", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=100, IsNullable=true)]
		public string NameFound
		{
			get
			{
				return _NameFound;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_NameFound != value)
				{
					_NameFound = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NameFound
		
		#region NameConfirmed
		
		private string _NameConfirmed = null;
		
		/// <summary>
		/// Column: NameConfirmed;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("NameConfirmed", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=100, IsNullable=true)]
		public string NameConfirmed
		{
			get
			{
				return _NameConfirmed;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_NameConfirmed != value)
				{
					_NameConfirmed = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NameConfirmed
		
		#region NameBankID
		
		private int? _NameBankID = null;
		
		/// <summary>
		/// Column: NameBankID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("NameBankID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? NameBankID
		{
			get
			{
				return _NameBankID;
			}
			set
			{
				if (_NameBankID != value)
				{
					_NameBankID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NameBankID
		
		#region Active
		
		private bool? _Active = null;
		
		/// <summary>
		/// Column: Active;
		/// DBMS data type: bit; Nullable;
		/// </summary>
		[ColumnDefinition("Active", DbTargetType=SqlDbType.Bit, Ordinal=11, IsNullable=true)]
		public bool? Active
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
		
		#region ExternalCreateDate
		
		private DateTime? _ExternalCreateDate = null;
		
		/// <summary>
		/// Column: ExternalCreateDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreateDate", DbTargetType=SqlDbType.DateTime, Ordinal=12, IsNullable=true)]
		public DateTime? ExternalCreateDate
		{
			get
			{
				return _ExternalCreateDate;
			}
			set
			{
				if (_ExternalCreateDate != value)
				{
					_ExternalCreateDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalCreateDate
		
		#region ExternalLastUpdateDate
		
		private DateTime? _ExternalLastUpdateDate = null;
		
		/// <summary>
		/// Column: ExternalLastUpdateDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalLastUpdateDate", DbTargetType=SqlDbType.DateTime, Ordinal=13, IsNullable=true)]
		public DateTime? ExternalLastUpdateDate
		{
			get
			{
				return _ExternalLastUpdateDate;
			}
			set
			{
				if (_ExternalLastUpdateDate != value)
				{
					_ExternalLastUpdateDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalLastUpdateDate
		
		#region IsCommonName
		
		private bool? _IsCommonName = null;
		
		/// <summary>
		/// Column: IsCommonName;
		/// DBMS data type: bit; Nullable;
		/// </summary>
		[ColumnDefinition("IsCommonName", DbTargetType=SqlDbType.Bit, Ordinal=14, IsNullable=true)]
		public bool? IsCommonName
		{
			get
			{
				return _IsCommonName;
			}
			set
			{
				if (_IsCommonName != value)
				{
					_IsCommonName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsCommonName
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=15, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=16)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=17)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__PageName"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageName"/>, 
		/// returns an instance of <see cref="__PageName"/>; otherwise returns null.</returns>
		public static new __PageName FromArray(byte[] byteArray)
		{
			__PageName o = null;
			
			try
			{
				o = (__PageName) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageName"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageName"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageName)
			{
				__PageName o = (__PageName) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageNameID == PageNameID &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.FileNamePrefix) == GetComparisonString(FileNamePrefix) &&
					o.SequenceOrder == SequenceOrder &&
					GetComparisonString(o.Source) == GetComparisonString(Source) &&
					GetComparisonString(o.NameFound) == GetComparisonString(NameFound) &&
					GetComparisonString(o.NameConfirmed) == GetComparisonString(NameConfirmed) &&
					o.NameBankID == NameBankID &&
					o.Active == Active &&
					o.ExternalCreateDate == ExternalCreateDate &&
					o.ExternalLastUpdateDate == ExternalLastUpdateDate &&
					o.IsCommonName == IsCommonName &&
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
				throw new ArgumentException("Argument is not of type __PageName");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageName"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageName"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageName a, __PageName b)
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
		/// <param name="a">The first <see cref="__PageName"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageName"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageName a, __PageName b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageName"/> object to compare with the current <see cref="__PageName"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageName))
			{
				return false;
			}
			
			return this == (__PageName) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageName.SortColumn.PageNameID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageNameID = "PageNameID";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string BarCode = "BarCode";	
			public const string FileNamePrefix = "FileNamePrefix";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string Source = "Source";	
			public const string NameFound = "NameFound";	
			public const string NameConfirmed = "NameConfirmed";	
			public const string NameBankID = "NameBankID";	
			public const string Active = "Active";	
			public const string ExternalCreateDate = "ExternalCreateDate";	
			public const string ExternalLastUpdateDate = "ExternalLastUpdateDate";	
			public const string IsCommonName = "IsCommonName";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
