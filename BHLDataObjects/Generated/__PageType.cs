
// Generated 12/14/2020 4:22:57 PM
// Do not modify the contents of this code file.
// This abstract class __PageType is based upon dbo.PageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PageType : __PageType
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
	public abstract class __PageType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageTypeID"></param>
		/// <param name="pageTypeName"></param>
		/// <param name="pageTypeDescription"></param>
		/// <param name="active"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __PageType(int pageTypeID, 
			string pageTypeName, 
			string pageTypeDescription, 
			byte active, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_PageTypeID = pageTypeID;
			PageTypeName = pageTypeName;
			PageTypeDescription = pageTypeDescription;
			Active = active;
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
		~__PageType()
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
					case "PageTypeID" :
					{
						_PageTypeID = (int)column.Value;
						break;
					}
					case "PageTypeName" :
					{
						_PageTypeName = (string)column.Value;
						break;
					}
					case "PageTypeDescription" :
					{
						_PageTypeDescription = (string)column.Value;
						break;
					}
					case "Active" :
					{
						_Active = (byte)column.Value;
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
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region PageTypeID
		
		private int _PageTypeID = default(int);
		
		/// <summary>
		/// Column: PageTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int PageTypeID
		{
			get
			{
				return _PageTypeID;
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
		
		#endregion PageTypeID
		
		#region PageTypeName
		
		private string _PageTypeName = string.Empty;
		
		/// <summary>
		/// Column: PageTypeName;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("PageTypeName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string PageTypeName
		{
			get
			{
				return _PageTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_PageTypeName != value)
				{
					_PageTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageTypeName
		
		#region PageTypeDescription
		
		private string _PageTypeDescription = null;
		
		/// <summary>
		/// Column: PageTypeDescription;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PageTypeDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=255, IsNullable=true)]
		public string PageTypeDescription
		{
			get
			{
				return _PageTypeDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PageTypeDescription != value)
				{
					_PageTypeDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageTypeDescription
		
		#region Active
		
		private byte _Active = default(byte);
		
		/// <summary>
		/// Column: Active;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("Active", DbTargetType=SqlDbType.TinyInt, Ordinal=4, NumericPrecision=3)]
		public byte Active
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
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=5, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6, IsNullable=true)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__PageType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageType"/>, 
		/// returns an instance of <see cref="__PageType"/>; otherwise returns null.</returns>
		public static new __PageType FromArray(byte[] byteArray)
		{
			__PageType o = null;
			
			try
			{
				o = (__PageType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageType)
			{
				__PageType o = (__PageType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageTypeID == PageTypeID &&
					GetComparisonString(o.PageTypeName) == GetComparisonString(PageTypeName) &&
					GetComparisonString(o.PageTypeDescription) == GetComparisonString(PageTypeDescription) &&
					o.Active == Active &&
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
				throw new ArgumentException("Argument is not of type __PageType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageType a, __PageType b)
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
		/// <param name="a">The first <see cref="__PageType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageType a, __PageType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageType"/> object to compare with the current <see cref="__PageType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageType))
			{
				return false;
			}
			
			return this == (__PageType) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageType.SortColumn.PageTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageTypeID = "PageTypeID";	
			public const string PageTypeName = "PageTypeName";	
			public const string PageTypeDescription = "PageTypeDescription";	
			public const string Active = "Active";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

