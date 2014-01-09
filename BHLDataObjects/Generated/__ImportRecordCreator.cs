
// Generated 1/10/2014 11:05:49 AM
// Do not modify the contents of this code file.
// This abstract class __ImportRecordCreator is based upon ImportRecordCreator.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ImportRecordCreator : __ImportRecordCreator
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
	public abstract class __ImportRecordCreator : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ImportRecordCreator()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="importRecordCreatorID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="fullName"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="startYear"></param>
		/// <param name="endYear"></param>
		/// <param name="authorType"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __ImportRecordCreator(int importRecordCreatorID, 
			int importRecordID, 
			string fullName, 
			string firstName, 
			string lastName, 
			string startYear, 
			string endYear, 
			string authorType, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_ImportRecordCreatorID = importRecordCreatorID;
			ImportRecordID = importRecordID;
			FullName = fullName;
			FirstName = firstName;
			LastName = lastName;
			StartYear = startYear;
			EndYear = endYear;
			AuthorType = authorType;
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
		~__ImportRecordCreator()
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
					case "ImportRecordCreatorID" :
					{
						_ImportRecordCreatorID = (int)column.Value;
						break;
					}
					case "ImportRecordID" :
					{
						_ImportRecordID = (int)column.Value;
						break;
					}
					case "FullName" :
					{
						_FullName = (string)column.Value;
						break;
					}
					case "FirstName" :
					{
						_FirstName = (string)column.Value;
						break;
					}
					case "LastName" :
					{
						_LastName = (string)column.Value;
						break;
					}
					case "StartYear" :
					{
						_StartYear = (string)column.Value;
						break;
					}
					case "EndYear" :
					{
						_EndYear = (string)column.Value;
						break;
					}
					case "AuthorType" :
					{
						_AuthorType = (string)column.Value;
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
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region ImportRecordCreatorID
		
		private int _ImportRecordCreatorID = default(int);
		
		/// <summary>
		/// Column: ImportRecordCreatorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ImportRecordCreatorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ImportRecordCreatorID
		{
			get
			{
				return _ImportRecordCreatorID;
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
		
		#endregion ImportRecordCreatorID
		
		#region ImportRecordID
		
		private int _ImportRecordID = default(int);
		
		/// <summary>
		/// Column: ImportRecordID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportRecordID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
		public int ImportRecordID
		{
			get
			{
				return _ImportRecordID;
			}
			set
			{
				if (_ImportRecordID != value)
				{
					_ImportRecordID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportRecordID
		
		#region FullName
		
		private string _FullName = string.Empty;
		
		/// <summary>
		/// Column: FullName;
		/// DBMS data type: nvarchar(30);
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
		
		#region FirstName
		
		private string _FirstName = string.Empty;
		
		/// <summary>
		/// Column: FirstName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("FirstName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=150)]
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
		
		#region LastName
		
		private string _LastName = string.Empty;
		
		/// <summary>
		/// Column: LastName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("LastName", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=150)]
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
		
		#region StartYear
		
		private string _StartYear = string.Empty;
		
		/// <summary>
		/// Column: StartYear;
		/// DBMS data type: nvarchar(25);
		/// </summary>
		[ColumnDefinition("StartYear", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=25)]
		public string StartYear
		{
			get
			{
				return _StartYear;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 25);
				if (_StartYear != value)
				{
					_StartYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartYear
		
		#region EndYear
		
		private string _EndYear = string.Empty;
		
		/// <summary>
		/// Column: EndYear;
		/// DBMS data type: nvarchar(25);
		/// </summary>
		[ColumnDefinition("EndYear", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=25)]
		public string EndYear
		{
			get
			{
				return _EndYear;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 25);
				if (_EndYear != value)
				{
					_EndYear = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndYear
		
		#region AuthorType
		
		private string _AuthorType = string.Empty;
		
		/// <summary>
		/// Column: AuthorType;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("AuthorType", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=50)]
		public string AuthorType
		{
			get
			{
				return _AuthorType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_AuthorType != value)
				{
					_AuthorType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AuthorType
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10)]
		public int CreationUserID
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
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10)]
		public int LastModifiedUserID
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
		/// Deserializes the byte array and returns an instance of <see cref="__ImportRecordCreator"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ImportRecordCreator"/>, 
		/// returns an instance of <see cref="__ImportRecordCreator"/>; otherwise returns null.</returns>
		public static new __ImportRecordCreator FromArray(byte[] byteArray)
		{
			__ImportRecordCreator o = null;
			
			try
			{
				o = (__ImportRecordCreator) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ImportRecordCreator"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ImportRecordCreator"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ImportRecordCreator)
			{
				__ImportRecordCreator o = (__ImportRecordCreator) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ImportRecordCreatorID == ImportRecordCreatorID &&
					o.ImportRecordID == ImportRecordID &&
					GetComparisonString(o.FullName) == GetComparisonString(FullName) &&
					GetComparisonString(o.FirstName) == GetComparisonString(FirstName) &&
					GetComparisonString(o.LastName) == GetComparisonString(LastName) &&
					GetComparisonString(o.StartYear) == GetComparisonString(StartYear) &&
					GetComparisonString(o.EndYear) == GetComparisonString(EndYear) &&
					GetComparisonString(o.AuthorType) == GetComparisonString(AuthorType) &&
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
				throw new ArgumentException("Argument is not of type __ImportRecordCreator");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportRecordCreator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportRecordCreator"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ImportRecordCreator a, __ImportRecordCreator b)
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
		/// <param name="a">The first <see cref="__ImportRecordCreator"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportRecordCreator"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ImportRecordCreator a, __ImportRecordCreator b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ImportRecordCreator"/> object to compare with the current <see cref="__ImportRecordCreator"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ImportRecordCreator))
			{
				return false;
			}
			
			return this == (__ImportRecordCreator) obj;
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
		/// list.Sort(SortOrder.Ascending, __ImportRecordCreator.SortColumn.ImportRecordCreatorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ImportRecordCreatorID = "ImportRecordCreatorID";	
			public const string ImportRecordID = "ImportRecordID";	
			public const string FullName = "FullName";	
			public const string FirstName = "FirstName";	
			public const string LastName = "LastName";	
			public const string StartYear = "StartYear";	
			public const string EndYear = "EndYear";	
			public const string AuthorType = "AuthorType";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
