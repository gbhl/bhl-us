
// Generated 2/15/2011 12:02:06 PM
// Do not modify the contents of this code file.
// This abstract class __TitleVariant is based upon TitleVariant.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleVariant : __TitleVariant
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
	public abstract class __TitleVariant : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleVariant()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleVariantID"></param>
		/// <param name="titleID"></param>
		/// <param name="titleVariantTypeID"></param>
		/// <param name="title"></param>
		/// <param name="titleRemainder"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __TitleVariant(int titleVariantID, 
			int titleID, 
			int titleVariantTypeID, 
			string title, 
			string titleRemainder, 
			string partNumber, 
			string partName, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_TitleVariantID = titleVariantID;
			TitleID = titleID;
			TitleVariantTypeID = titleVariantTypeID;
			Title = title;
			TitleRemainder = titleRemainder;
			PartNumber = partNumber;
			PartName = partName;
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
		~__TitleVariant()
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
					case "TitleVariantID" :
					{
						_TitleVariantID = (int)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int)column.Value;
						break;
					}
					case "TitleVariantTypeID" :
					{
						_TitleVariantTypeID = (int)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "TitleRemainder" :
					{
						_TitleRemainder = (string)column.Value;
						break;
					}
					case "PartNumber" :
					{
						_PartNumber = (string)column.Value;
						break;
					}
					case "PartName" :
					{
						_PartName = (string)column.Value;
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
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region TitleVariantID
		
		private int _TitleVariantID = default(int);
		
		/// <summary>
		/// Column: TitleVariantID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleVariantID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleVariantID
		{
			get
			{
				return _TitleVariantID;
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
		
		#endregion TitleVariantID
		
		#region TitleID
		
		private int _TitleID = default(int);
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int TitleID
		{
			get
			{
				return _TitleID;
			}
			set
			{
				if (_TitleID != value)
				{
					_TitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleID
		
		#region TitleVariantTypeID
		
		private int _TitleVariantTypeID = default(int);
		
		/// <summary>
		/// Column: TitleVariantTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleVariantTypeID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int TitleVariantTypeID
		{
			get
			{
				return _TitleVariantTypeID;
			}
			set
			{
				if (_TitleVariantTypeID != value)
				{
					_TitleVariantTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleVariantTypeID
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1073741823)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region TitleRemainder
		
		private string _TitleRemainder = string.Empty;
		
		/// <summary>
		/// Column: TitleRemainder;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("TitleRemainder", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823)]
		public string TitleRemainder
		{
			get
			{
				return _TitleRemainder;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_TitleRemainder != value)
				{
					_TitleRemainder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleRemainder
		
		#region PartNumber
		
		private string _PartNumber = string.Empty;
		
		/// <summary>
		/// Column: PartNumber;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("PartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=255)]
		public string PartNumber
		{
			get
			{
				return _PartNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PartNumber != value)
				{
					_PartNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PartNumber
		
		#region PartName
		
		private string _PartName = string.Empty;
		
		/// <summary>
		/// Column: PartName;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("PartName", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=255)]
		public string PartName
		{
			get
			{
				return _PartName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PartName != value)
				{
					_PartName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PartName
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10, IsNullable=true)]
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__TitleVariant"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleVariant"/>, 
		/// returns an instance of <see cref="__TitleVariant"/>; otherwise returns null.</returns>
		public static new __TitleVariant FromArray(byte[] byteArray)
		{
			__TitleVariant o = null;
			
			try
			{
				o = (__TitleVariant) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleVariant"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleVariant"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleVariant)
			{
				__TitleVariant o = (__TitleVariant) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleVariantID == TitleVariantID &&
					o.TitleID == TitleID &&
					o.TitleVariantTypeID == TitleVariantTypeID &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.TitleRemainder) == GetComparisonString(TitleRemainder) &&
					GetComparisonString(o.PartNumber) == GetComparisonString(PartNumber) &&
					GetComparisonString(o.PartName) == GetComparisonString(PartName) &&
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
				throw new ArgumentException("Argument is not of type __TitleVariant");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleVariant"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleVariant"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleVariant a, __TitleVariant b)
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
		/// <param name="a">The first <see cref="__TitleVariant"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleVariant"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleVariant a, __TitleVariant b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleVariant"/> object to compare with the current <see cref="__TitleVariant"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleVariant))
			{
				return false;
			}
			
			return this == (__TitleVariant) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleVariant.SortColumn.TitleVariantID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleVariantID = "TitleVariantID";	
			public const string TitleID = "TitleID";	
			public const string TitleVariantTypeID = "TitleVariantTypeID";	
			public const string Title = "Title";	
			public const string TitleRemainder = "TitleRemainder";	
			public const string PartNumber = "PartNumber";	
			public const string PartName = "PartName";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
