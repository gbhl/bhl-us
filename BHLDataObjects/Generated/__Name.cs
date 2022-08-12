
// Generated 1/5/2021 3:26:17 PM
// Do not modify the contents of this code file.
// This abstract class __Name is based upon dbo.Name.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Name : __Name
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
	public abstract class __Name : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Name()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="nameID"></param>
		/// <param name="nameSourceID"></param>
		/// <param name="nameString"></param>
		/// <param name="isActive"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		/// <param name="nameResolvedID"></param>
		public __Name(int nameID, 
			int nameSourceID, 
			string nameString, 
			short isActive, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID, 
			int? nameResolvedID) : this()
		{
			_NameID = nameID;
			NameSourceID = nameSourceID;
			NameString = nameString;
			IsActive = isActive;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
			NameResolvedID = nameResolvedID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Name()
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
					case "NameID" :
					{
						_NameID = (int)column.Value;
						break;
					}
					case "NameSourceID" :
					{
						_NameSourceID = (int)column.Value;
						break;
					}
					case "NameString" :
					{
						_NameString = (string)column.Value;
						break;
					}
					case "IsActive" :
					{
						_IsActive = (short)column.Value;
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
					case "NameResolvedID" :
					{
						_NameResolvedID = (int?)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region NameID
		
		private int _NameID = default(int);
		
		/// <summary>
		/// Column: NameID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("NameID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int NameID
		{
			get
			{
				return _NameID;
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
		
		#endregion NameID
		
		#region NameSourceID
		
		private int _NameSourceID = default(int);
		
		/// <summary>
		/// Column: NameSourceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("NameSourceID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int NameSourceID
		{
			get
			{
				return _NameSourceID;
			}
			set
			{
				if (_NameSourceID != value)
				{
					_NameSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NameSourceID
		
		#region NameString
		
		private string _NameString = string.Empty;
		
		/// <summary>
		/// Column: NameString;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("NameString", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string NameString
		{
			get
			{
				return _NameString;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_NameString != value)
				{
					_NameString = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NameString
		
		#region IsActive
		
		private short _IsActive = default(short);
		
		/// <summary>
		/// Column: IsActive;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("IsActive", DbTargetType=SqlDbType.SmallInt, Ordinal=4, NumericPrecision=5)]
		public short IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				if (_IsActive != value)
				{
					_IsActive = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsActive
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		
		#region NameResolvedID
		
		private int? _NameResolvedID = null;
		
		/// <summary>
		/// Column: NameResolvedID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("NameResolvedID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? NameResolvedID
		{
			get
			{
				return _NameResolvedID;
			}
			set
			{
				if (_NameResolvedID != value)
				{
					_NameResolvedID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NameResolvedID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Name"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Name"/>, 
		/// returns an instance of <see cref="__Name"/>; otherwise returns null.</returns>
		public static new __Name FromArray(byte[] byteArray)
		{
			__Name o = null;
			
			try
			{
				o = (__Name) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Name"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Name"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Name)
			{
				__Name o = (__Name) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.NameID == NameID &&
					o.NameSourceID == NameSourceID &&
					GetComparisonString(o.NameString) == GetComparisonString(NameString) &&
					o.IsActive == IsActive &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID &&
					o.NameResolvedID == NameResolvedID 
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
				throw new ArgumentException("Argument is not of type __Name");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Name"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Name"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Name a, __Name b)
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
		/// <param name="a">The first <see cref="__Name"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Name"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Name a, __Name b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Name"/> object to compare with the current <see cref="__Name"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Name))
			{
				return false;
			}
			
			return this == (__Name) obj;
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
		/// list.Sort(SortOrder.Ascending, __Name.SortColumn.NameID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string NameID = "NameID";	
			public const string NameSourceID = "NameSourceID";	
			public const string NameString = "NameString";	
			public const string IsActive = "IsActive";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";	
			public const string NameResolvedID = "NameResolvedID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

