
// Generated 1/5/2021 2:16:27 PM
// Do not modify the contents of this code file.
// This abstract class __IndicatedPage is based upon dbo.IndicatedPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IndicatedPage : __IndicatedPage
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
	public abstract class __IndicatedPage : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IndicatedPage()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="indicatedPageID"></param>
		/// <param name="barCode"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="sequence"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="externalCreationUser"></param>
		/// <param name="externalLastModifiedUser"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IndicatedPage(int indicatedPageID, 
			string barCode, 
			string fileNamePrefix, 
			int? sequenceOrder, 
			short? sequence, 
			int importStatusID, 
			int? importSourceID, 
			string pagePrefix, 
			string pageNumber, 
			bool implied, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			int? externalCreationUser, 
			int? externalLastModifiedUser, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_IndicatedPageID = indicatedPageID;
			BarCode = barCode;
			FileNamePrefix = fileNamePrefix;
			SequenceOrder = sequenceOrder;
			Sequence = sequence;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			PagePrefix = pagePrefix;
			PageNumber = pageNumber;
			Implied = implied;
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
		~__IndicatedPage()
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
					case "IndicatedPageID" :
					{
						_IndicatedPageID = (int)column.Value;
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
					case "Sequence" :
					{
						_Sequence = (short?)column.Value;
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
					case "PagePrefix" :
					{
						_PagePrefix = (string)column.Value;
						break;
					}
					case "PageNumber" :
					{
						_PageNumber = (string)column.Value;
						break;
					}
					case "Implied" :
					{
						_Implied = (bool)column.Value;
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
		
		#region IndicatedPageID
		
		private int _IndicatedPageID = default(int);
		
		/// <summary>
		/// Column: IndicatedPageID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("IndicatedPageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int IndicatedPageID
		{
			get
			{
				return _IndicatedPageID;
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
		
		#endregion IndicatedPageID
		
		#region BarCode
		
		private string _BarCode = string.Empty;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=40)]
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
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("FileNamePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=200)]
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
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
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
		
		#region Sequence
		
		private short? _Sequence = null;
		
		/// <summary>
		/// Column: Sequence;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("Sequence", DbTargetType=SqlDbType.SmallInt, Ordinal=5, NumericPrecision=5, IsNullable=true)]
		public short? Sequence
		{
			get
			{
				return _Sequence;
			}
			set
			{
				if (_Sequence != value)
				{
					_Sequence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Sequence
		
		#region ImportStatusID
		
		private int _ImportStatusID = default(int);
		
		/// <summary>
		/// Column: ImportStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportStatusID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsInForeignKey=true)]
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
		[ColumnDefinition("ImportSourceID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region PagePrefix
		
		private string _PagePrefix = null;
		
		/// <summary>
		/// Column: PagePrefix;
		/// DBMS data type: nvarchar(40); Nullable;
		/// </summary>
		[ColumnDefinition("PagePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=40, IsNullable=true)]
		public string PagePrefix
		{
			get
			{
				return _PagePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_PagePrefix != value)
				{
					_PagePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PagePrefix
		
		#region PageNumber
		
		private string _PageNumber = null;
		
		/// <summary>
		/// Column: PageNumber;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("PageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=20, IsNullable=true)]
		public string PageNumber
		{
			get
			{
				return _PageNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_PageNumber != value)
				{
					_PageNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageNumber
		
		#region Implied
		
		private bool _Implied = false;
		
		/// <summary>
		/// Column: Implied;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("Implied", DbTargetType=SqlDbType.Bit, Ordinal=10)]
		public bool Implied
		{
			get
			{
				return _Implied;
			}
			set
			{
				if (_Implied != value)
				{
					_Implied = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Implied
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=11, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=12, IsNullable=true)]
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
		[ColumnDefinition("ExternalCreationUser", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedUser", DbTargetType=SqlDbType.Int, Ordinal=14, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IndicatedPage"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IndicatedPage"/>, 
		/// returns an instance of <see cref="__IndicatedPage"/>; otherwise returns null.</returns>
		public static new __IndicatedPage FromArray(byte[] byteArray)
		{
			__IndicatedPage o = null;
			
			try
			{
				o = (__IndicatedPage) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IndicatedPage"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IndicatedPage"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IndicatedPage)
			{
				__IndicatedPage o = (__IndicatedPage) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.IndicatedPageID == IndicatedPageID &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.FileNamePrefix) == GetComparisonString(FileNamePrefix) &&
					o.SequenceOrder == SequenceOrder &&
					o.Sequence == Sequence &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.PagePrefix) == GetComparisonString(PagePrefix) &&
					GetComparisonString(o.PageNumber) == GetComparisonString(PageNumber) &&
					o.Implied == Implied &&
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
				throw new ArgumentException("Argument is not of type __IndicatedPage");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IndicatedPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IndicatedPage"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IndicatedPage a, __IndicatedPage b)
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
		/// <param name="a">The first <see cref="__IndicatedPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IndicatedPage"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IndicatedPage a, __IndicatedPage b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IndicatedPage"/> object to compare with the current <see cref="__IndicatedPage"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IndicatedPage))
			{
				return false;
			}
			
			return this == (__IndicatedPage) obj;
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
		/// list.Sort(SortOrder.Ascending, __IndicatedPage.SortColumn.IndicatedPageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string IndicatedPageID = "IndicatedPageID";	
			public const string BarCode = "BarCode";	
			public const string FileNamePrefix = "FileNamePrefix";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string Sequence = "Sequence";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string PagePrefix = "PagePrefix";	
			public const string PageNumber = "PageNumber";	
			public const string Implied = "Implied";	
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

