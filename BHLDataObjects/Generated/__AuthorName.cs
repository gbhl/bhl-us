
// Generated 5/29/2012 12:59:27 PM
// Do not modify the contents of this code file.
// This abstract class __AuthorName is based upon AuthorName.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AuthorName : __AuthorName
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
	public abstract class __AuthorName : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AuthorName()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="authorNameID"></param>
		/// <param name="authorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="fullerForm"></param>
		/// <param name="isPreferredName"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __AuthorName(int authorNameID, 
			int authorID, 
			string fullName, 
			string lastName, 
			string firstName, 
			string fullerForm, 
			short isPreferredName, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_AuthorNameID = authorNameID;
			AuthorID = authorID;
			FullName = fullName;
			LastName = lastName;
			FirstName = firstName;
			FullerForm = fullerForm;
			IsPreferredName = isPreferredName;
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
		~__AuthorName()
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
					case "AuthorNameID" :
					{
						_AuthorNameID = (int)column.Value;
						break;
					}
					case "AuthorID" :
					{
						_AuthorID = (int)column.Value;
						break;
					}
					case "FullName" :
					{
						_FullName = (string)column.Value;
						break;
					}
					case "LastName" :
					{
						_LastName = (string)column.Value;
						break;
					}
					case "FirstName" :
					{
						_FirstName = (string)column.Value;
						break;
					}
					case "FullerForm" :
					{
						_FullerForm = (string)column.Value;
						break;
					}
					case "IsPreferredName" :
					{
						_IsPreferredName = (short)column.Value;
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
		
		#region AuthorNameID
		
		private int _AuthorNameID = default(int);
		
		/// <summary>
		/// Column: AuthorNameID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AuthorNameID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int AuthorNameID
		{
			get
			{
				return _AuthorNameID;
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
		
		#endregion AuthorNameID
		
		#region AuthorID
		
		private int _AuthorID = default(int);
		
		/// <summary>
		/// Column: AuthorID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AuthorID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int AuthorID
		{
			get
			{
				return _AuthorID;
			}
			set
			{
				if (_AuthorID != value)
				{
					_AuthorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AuthorID
		
		#region FullName
		
		private string _FullName = string.Empty;
		
		/// <summary>
		/// Column: FullName;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("FullName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=300)]
		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_FullName != value)
				{
					_FullName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FullName
		
		#region LastName
		
		private string _LastName = string.Empty;
		
		/// <summary>
		/// Column: LastName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("LastName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=150)]
		public string LastName
		{
			get
			{
				return _LastName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_LastName != value)
				{
					_LastName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastName
		
		#region FirstName
		
		private string _FirstName = string.Empty;
		
		/// <summary>
		/// Column: FirstName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("FirstName", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=150)]
		public string FirstName
		{
			get
			{
				return _FirstName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_FirstName != value)
				{
					_FirstName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FirstName
		
		#region FullerForm
		
		private string _FullerForm = string.Empty;
		
		/// <summary>
		/// Column: FullerForm;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("FullerForm", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=150)]
		public string FullerForm
		{
			get
			{
				return _FullerForm;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_FullerForm != value)
				{
					_FullerForm = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FullerForm
		
		#region IsPreferredName
		
		private short _IsPreferredName = default(short);
		
		/// <summary>
		/// Column: IsPreferredName;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("IsPreferredName", DbTargetType=SqlDbType.SmallInt, Ordinal=7, NumericPrecision=5)]
		public short IsPreferredName
		{
			get
			{
				return _IsPreferredName;
			}
			set
			{
				if (_IsPreferredName != value)
				{
					_IsPreferredName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsPreferredName
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=8, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=9, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AuthorName"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AuthorName"/>, 
		/// returns an instance of <see cref="__AuthorName"/>; otherwise returns null.</returns>
		public static new __AuthorName FromArray(byte[] byteArray)
		{
			__AuthorName o = null;
			
			try
			{
				o = (__AuthorName) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AuthorName"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AuthorName"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AuthorName)
			{
				__AuthorName o = (__AuthorName) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AuthorNameID == AuthorNameID &&
					o.AuthorID == AuthorID &&
					GetComparisonString(o.FullName) == GetComparisonString(FullName) &&
					GetComparisonString(o.LastName) == GetComparisonString(LastName) &&
					GetComparisonString(o.FirstName) == GetComparisonString(FirstName) &&
					GetComparisonString(o.FullerForm) == GetComparisonString(FullerForm) &&
					o.IsPreferredName == IsPreferredName &&
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
				throw new ArgumentException("Argument is not of type __AuthorName");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AuthorName"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AuthorName"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AuthorName a, __AuthorName b)
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
		/// <param name="a">The first <see cref="__AuthorName"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AuthorName"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AuthorName a, __AuthorName b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AuthorName"/> object to compare with the current <see cref="__AuthorName"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AuthorName))
			{
				return false;
			}
			
			return this == (__AuthorName) obj;
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
		/// list.Sort(SortOrder.Ascending, __AuthorName.SortColumn.AuthorNameID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AuthorNameID = "AuthorNameID";	
			public const string AuthorID = "AuthorID";	
			public const string FullName = "FullName";	
			public const string LastName = "LastName";	
			public const string FirstName = "FirstName";	
			public const string FullerForm = "FullerForm";	
			public const string IsPreferredName = "IsPreferredName";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
