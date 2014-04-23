
// Generated 3/27/2014 11:56:11 AM
// Do not modify the contents of this code file.
// This abstract class __TitleAuthor is based upon TitleAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleAuthor : __TitleAuthor
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
	public abstract class __TitleAuthor : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleAuthor()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleAuthorID"></param>
		/// <param name="titleID"></param>
		/// <param name="authorID"></param>
		/// <param name="authorRoleID"></param>
		/// <param name="relationship"></param>
		/// <param name="titleOfWork"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __TitleAuthor(int titleAuthorID, 
			int titleID, 
			int authorID, 
			int? authorRoleID, 
			string relationship, 
			string titleOfWork, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_TitleAuthorID = titleAuthorID;
			TitleID = titleID;
			AuthorID = authorID;
			AuthorRoleID = authorRoleID;
			Relationship = relationship;
			TitleOfWork = titleOfWork;
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
		~__TitleAuthor()
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
					case "TitleAuthorID" :
					{
						_TitleAuthorID = (int)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int)column.Value;
						break;
					}
					case "AuthorID" :
					{
						_AuthorID = (int)column.Value;
						break;
					}
					case "AuthorRoleID" :
					{
						_AuthorRoleID = (int?)column.Value;
						break;
					}
					case "Relationship" :
					{
						_Relationship = (string)column.Value;
						break;
					}
					case "TitleOfWork" :
					{
						_TitleOfWork = (string)column.Value;
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
		
		#region TitleAuthorID
		
		private int _TitleAuthorID = default(int);
		
		/// <summary>
		/// Column: TitleAuthorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleAuthorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TitleAuthorID
		{
			get
			{
				return _TitleAuthorID;
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
		
		#endregion TitleAuthorID
		
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
		
		#region AuthorID
		
		private int _AuthorID = default(int);
		
		/// <summary>
		/// Column: AuthorID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AuthorID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
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
		
		#region AuthorRoleID
		
		private int? _AuthorRoleID = null;
		
		/// <summary>
		/// Column: AuthorRoleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("AuthorRoleID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? AuthorRoleID
		{
			get
			{
				return _AuthorRoleID;
			}
			set
			{
				if (_AuthorRoleID != value)
				{
					_AuthorRoleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AuthorRoleID
		
		#region Relationship
		
		private string _Relationship = string.Empty;
		
		/// <summary>
		/// Column: Relationship;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("Relationship", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=150)]
		public string Relationship
		{
			get
			{
				return _Relationship;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_Relationship != value)
				{
					_Relationship = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Relationship
		
		#region TitleOfWork
		
		private string _TitleOfWork = string.Empty;
		
		/// <summary>
		/// Column: TitleOfWork;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("TitleOfWork", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=500)]
		public string TitleOfWork
		{
			get
			{
				return _TitleOfWork;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_TitleOfWork != value)
				{
					_TitleOfWork = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleOfWork
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__TitleAuthor"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleAuthor"/>, 
		/// returns an instance of <see cref="__TitleAuthor"/>; otherwise returns null.</returns>
		public static new __TitleAuthor FromArray(byte[] byteArray)
		{
			__TitleAuthor o = null;
			
			try
			{
				o = (__TitleAuthor) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleAuthor"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleAuthor"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleAuthor)
			{
				__TitleAuthor o = (__TitleAuthor) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleAuthorID == TitleAuthorID &&
					o.TitleID == TitleID &&
					o.AuthorID == AuthorID &&
					o.AuthorRoleID == AuthorRoleID &&
					GetComparisonString(o.Relationship) == GetComparisonString(Relationship) &&
					GetComparisonString(o.TitleOfWork) == GetComparisonString(TitleOfWork) &&
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
				throw new ArgumentException("Argument is not of type __TitleAuthor");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleAuthor"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleAuthor"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleAuthor a, __TitleAuthor b)
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
		/// <param name="a">The first <see cref="__TitleAuthor"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleAuthor"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleAuthor a, __TitleAuthor b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleAuthor"/> object to compare with the current <see cref="__TitleAuthor"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleAuthor))
			{
				return false;
			}
			
			return this == (__TitleAuthor) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleAuthor.SortColumn.TitleAuthorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleAuthorID = "TitleAuthorID";	
			public const string TitleID = "TitleID";	
			public const string AuthorID = "AuthorID";	
			public const string AuthorRoleID = "AuthorRoleID";	
			public const string Relationship = "Relationship";	
			public const string TitleOfWork = "TitleOfWork";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
