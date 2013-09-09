
// Generated 9/4/2008 2:16:32 PM
// Do not modify the contents of this code file.
// This abstract class __Title_TitleIdentifier is based upon Title_TitleIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class Title_TitleIdentifier : __Title_TitleIdentifier
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
	public abstract class __Title_TitleIdentifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Title_TitleIdentifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="title_TitleIdentifierID"></param>
		/// <param name="importKey"></param>
		/// <param name="importStatusID"></param>
		/// <param name="importSourceID"></param>
		/// <param name="identifierName"></param>
		/// <param name="identifierValue"></param>
		/// <param name="externalCreationDate"></param>
		/// <param name="externalLastModifiedDate"></param>
		/// <param name="productionDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __Title_TitleIdentifier(int title_TitleIdentifierID, 
			string importKey, 
			int importStatusID, 
			int? importSourceID, 
			string identifierName, 
			string identifierValue, 
			DateTime? externalCreationDate, 
			DateTime? externalLastModifiedDate, 
			DateTime? productionDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_Title_TitleIdentifierID = title_TitleIdentifierID;
			ImportKey = importKey;
			ImportStatusID = importStatusID;
			ImportSourceID = importSourceID;
			IdentifierName = identifierName;
			IdentifierValue = identifierValue;
			ExternalCreationDate = externalCreationDate;
			ExternalLastModifiedDate = externalLastModifiedDate;
			ProductionDate = productionDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Title_TitleIdentifier()
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
					case "Title_TitleIdentifierID" :
					{
						_Title_TitleIdentifierID = (int)column.Value;
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
					case "IdentifierName" :
					{
						_IdentifierName = (string)column.Value;
						break;
					}
					case "IdentifierValue" :
					{
						_IdentifierValue = (string)column.Value;
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
		
		#region Title_TitleIdentifierID
		
		private int _Title_TitleIdentifierID = default(int);
		
		/// <summary>
		/// Column: Title_TitleIdentifierID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("Title_TitleIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int Title_TitleIdentifierID
		{
			get
			{
				return _Title_TitleIdentifierID;
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
		
		#endregion Title_TitleIdentifierID
		
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
		
		#region IdentifierName
		
		private string _IdentifierName = string.Empty;
		
		/// <summary>
		/// Column: IdentifierName;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("IdentifierName", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=40)]
		public string IdentifierName
		{
			get
			{
				return _IdentifierName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_IdentifierName != value)
				{
					_IdentifierName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierName
		
		#region IdentifierValue
		
		private string _IdentifierValue = string.Empty;
		
		/// <summary>
		/// Column: IdentifierValue;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("IdentifierValue", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=125)]
		public string IdentifierValue
		{
			get
			{
				return _IdentifierValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_IdentifierValue != value)
				{
					_IdentifierValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierValue
		
		#region ExternalCreationDate
		
		private DateTime? _ExternalCreationDate = null;
		
		/// <summary>
		/// Column: ExternalCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ExternalCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7, IsNullable=true)]
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
		[ColumnDefinition("ExternalLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8, IsNullable=true)]
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
		
		#region ProductionDate
		
		private DateTime? _ProductionDate = null;
		
		/// <summary>
		/// Column: ProductionDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ProductionDate", DbTargetType=SqlDbType.DateTime, Ordinal=9, IsNullable=true)]
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
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=11)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Title_TitleIdentifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Title_TitleIdentifier"/>, 
		/// returns an instance of <see cref="__Title_TitleIdentifier"/>; otherwise returns null.</returns>
		public static new __Title_TitleIdentifier FromArray(byte[] byteArray)
		{
			__Title_TitleIdentifier o = null;
			
			try
			{
				o = (__Title_TitleIdentifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Title_TitleIdentifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Title_TitleIdentifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Title_TitleIdentifier)
			{
				__Title_TitleIdentifier o = (__Title_TitleIdentifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.Title_TitleIdentifierID == Title_TitleIdentifierID &&
					GetComparisonString(o.ImportKey) == GetComparisonString(ImportKey) &&
					o.ImportStatusID == ImportStatusID &&
					o.ImportSourceID == ImportSourceID &&
					GetComparisonString(o.IdentifierName) == GetComparisonString(IdentifierName) &&
					GetComparisonString(o.IdentifierValue) == GetComparisonString(IdentifierValue) &&
					o.ExternalCreationDate == ExternalCreationDate &&
					o.ExternalLastModifiedDate == ExternalLastModifiedDate &&
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
				throw new ArgumentException("Argument is not of type __Title_TitleIdentifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Title_TitleIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Title_TitleIdentifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Title_TitleIdentifier a, __Title_TitleIdentifier b)
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
		/// <param name="a">The first <see cref="__Title_TitleIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Title_TitleIdentifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Title_TitleIdentifier a, __Title_TitleIdentifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Title_TitleIdentifier"/> object to compare with the current <see cref="__Title_TitleIdentifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Title_TitleIdentifier))
			{
				return false;
			}
			
			return this == (__Title_TitleIdentifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __Title_TitleIdentifier.SortColumn.Title_TitleIdentifierID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string Title_TitleIdentifierID = "Title_TitleIdentifierID";	
			public const string ImportKey = "ImportKey";	
			public const string ImportStatusID = "ImportStatusID";	
			public const string ImportSourceID = "ImportSourceID";	
			public const string IdentifierName = "IdentifierName";	
			public const string IdentifierValue = "IdentifierValue";	
			public const string ExternalCreationDate = "ExternalCreationDate";	
			public const string ExternalLastModifiedDate = "ExternalLastModifiedDate";	
			public const string ProductionDate = "ProductionDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
