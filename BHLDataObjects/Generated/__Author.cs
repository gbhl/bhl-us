
// Generated 5/29/2012 12:59:27 PM
// Do not modify the contents of this code file.
// This abstract class __Author is based upon Author.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Author : __Author
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
	public abstract class __Author : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Author()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="authorID"></param>
		/// <param name="authorTypeID"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="numeration"></param>
		/// <param name="title"></param>
		/// <param name="unit"></param>
		/// <param name="location"></param>
		/// <param name="isActive"></param>
		/// <param name="redirectAuthorID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __Author(int authorID, 
			int? authorTypeID, 
			string startDate, 
			string endDate, 
			string numeration, 
			string title, 
			string unit, 
			string location, 
			short isActive, 
			int? redirectAuthorID, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_AuthorID = authorID;
			AuthorTypeID = authorTypeID;
			StartDate = startDate;
			EndDate = endDate;
			Numeration = numeration;
			Title = title;
			Unit = unit;
			Location = location;
			IsActive = isActive;
			RedirectAuthorID = redirectAuthorID;
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
		~__Author()
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
					case "AuthorID" :
					{
						_AuthorID = (int)column.Value;
						break;
					}
					case "AuthorTypeID" :
					{
						_AuthorTypeID = (int?)column.Value;
						break;
					}
					case "StartDate" :
					{
						_StartDate = (string)column.Value;
						break;
					}
					case "EndDate" :
					{
						_EndDate = (string)column.Value;
						break;
					}
					case "Numeration" :
					{
						_Numeration = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "Unit" :
					{
						_Unit = (string)column.Value;
						break;
					}
					case "Location" :
					{
						_Location = (string)column.Value;
						break;
					}
					case "IsActive" :
					{
						_IsActive = (short)column.Value;
						break;
					}
					case "RedirectAuthorID" :
					{
						_RedirectAuthorID = (int?)column.Value;
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
		
		#region AuthorID
		
		private int _AuthorID = default(int);
		
		/// <summary>
		/// Column: AuthorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AuthorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AuthorID
		{
			get
			{
				return _AuthorID;
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
		
		#endregion AuthorID
		
		#region AuthorTypeID
		
		private int? _AuthorTypeID = null;
		
		/// <summary>
		/// Column: AuthorTypeID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("AuthorTypeID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? AuthorTypeID
		{
			get
			{
				return _AuthorTypeID;
			}
			set
			{
				if (_AuthorTypeID != value)
				{
					_AuthorTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AuthorTypeID
		
		#region StartDate
		
		private string _StartDate = string.Empty;
		
		/// <summary>
		/// Column: StartDate;
		/// DBMS data type: nvarchar(25);
		/// </summary>
		[ColumnDefinition("StartDate", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=25)]
		public string StartDate
		{
			get
			{
				return _StartDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 25);
				if (_StartDate != value)
				{
					_StartDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartDate
		
		#region EndDate
		
		private string _EndDate = string.Empty;
		
		/// <summary>
		/// Column: EndDate;
		/// DBMS data type: nvarchar(25);
		/// </summary>
		[ColumnDefinition("EndDate", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=25)]
		public string EndDate
		{
			get
			{
				return _EndDate;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 25);
				if (_EndDate != value)
				{
					_EndDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndDate
		
		#region Numeration
		
		private string _Numeration = string.Empty;
		
		/// <summary>
		/// Column: Numeration;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("Numeration", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=300)]
		public string Numeration
		{
			get
			{
				return _Numeration;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_Numeration != value)
				{
					_Numeration = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Numeration
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=200)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region Unit
		
		private string _Unit = string.Empty;
		
		/// <summary>
		/// Column: Unit;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("Unit", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=300)]
		public string Unit
		{
			get
			{
				return _Unit;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_Unit != value)
				{
					_Unit = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Unit
		
		#region Location
		
		private string _Location = string.Empty;
		
		/// <summary>
		/// Column: Location;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Location", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=200)]
		public string Location
		{
			get
			{
				return _Location;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Location != value)
				{
					_Location = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Location
		
		#region IsActive
		
		private short _IsActive = default(short);
		
		/// <summary>
		/// Column: IsActive;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("IsActive", DbTargetType=SqlDbType.SmallInt, Ordinal=9, NumericPrecision=5)]
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
		
		#region RedirectAuthorID
		
		private int? _RedirectAuthorID = null;
		
		/// <summary>
		/// Column: RedirectAuthorID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectAuthorID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? RedirectAuthorID
		{
			get
			{
				return _RedirectAuthorID;
			}
			set
			{
				if (_RedirectAuthorID != value)
				{
					_RedirectAuthorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectAuthorID
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=11, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=12, IsNullable=true)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=14, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Author"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Author"/>, 
		/// returns an instance of <see cref="__Author"/>; otherwise returns null.</returns>
		public static new __Author FromArray(byte[] byteArray)
		{
			__Author o = null;
			
			try
			{
				o = (__Author) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Author"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Author"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Author)
			{
				__Author o = (__Author) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AuthorID == AuthorID &&
					o.AuthorTypeID == AuthorTypeID &&
					GetComparisonString(o.StartDate) == GetComparisonString(StartDate) &&
					GetComparisonString(o.EndDate) == GetComparisonString(EndDate) &&
					GetComparisonString(o.Numeration) == GetComparisonString(Numeration) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.Unit) == GetComparisonString(Unit) &&
					GetComparisonString(o.Location) == GetComparisonString(Location) &&
					o.IsActive == IsActive &&
					o.RedirectAuthorID == RedirectAuthorID &&
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
				throw new ArgumentException("Argument is not of type __Author");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Author"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Author"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Author a, __Author b)
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
		/// <param name="a">The first <see cref="__Author"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Author"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Author a, __Author b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Author"/> object to compare with the current <see cref="__Author"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Author))
			{
				return false;
			}
			
			return this == (__Author) obj;
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
		/// list.Sort(SortOrder.Ascending, __Author.SortColumn.AuthorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AuthorID = "AuthorID";	
			public const string AuthorTypeID = "AuthorTypeID";	
			public const string StartDate = "StartDate";	
			public const string EndDate = "EndDate";	
			public const string Numeration = "Numeration";	
			public const string Title = "Title";	
			public const string Unit = "Unit";	
			public const string Location = "Location";	
			public const string IsActive = "IsActive";	
			public const string RedirectAuthorID = "RedirectAuthorID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
