
// Generated 1/24/2020 4:10:38 PM
// Do not modify the contents of this code file.
// This abstract class __IASegmentAuthor is based upon dbo.IASegmentAuthor.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IASegmentAuthor : __IASegmentAuthor
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
	public abstract class __IASegmentAuthor : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IASegmentAuthor()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentAuthorID"></param>
		/// <param name="segmentID"></param>
		/// <param name="sequence"></param>
		/// <param name="bHLAuthorID"></param>
		/// <param name="fullName"></param>
		/// <param name="lastName"></param>
		/// <param name="firstName"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="identifierValue"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IASegmentAuthor(int segmentAuthorID, 
			int segmentID, 
			int sequence, 
			int? bHLAuthorID, 
			string fullName, 
			string lastName, 
			string firstName, 
			string startDate, 
			string endDate, 
			int? bHLIdentifierID, 
			string identifierValue, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_SegmentAuthorID = segmentAuthorID;
			SegmentID = segmentID;
			Sequence = sequence;
			BHLAuthorID = bHLAuthorID;
			FullName = fullName;
			LastName = lastName;
			FirstName = firstName;
			StartDate = startDate;
			EndDate = endDate;
			BHLIdentifierID = bHLIdentifierID;
			IdentifierValue = identifierValue;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IASegmentAuthor()
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
					case "SegmentAuthorID" :
					{
						_SegmentAuthorID = (int)column.Value;
						break;
					}
					case "SegmentID" :
					{
						_SegmentID = (int)column.Value;
						break;
					}
					case "Sequence" :
					{
						_Sequence = (int)column.Value;
						break;
					}
					case "BHLAuthorID" :
					{
						_BHLAuthorID = (int?)column.Value;
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
					case "BHLIdentifierID" :
					{
						_BHLIdentifierID = (int?)column.Value;
						break;
					}
					case "IdentifierValue" :
					{
						_IdentifierValue = (string)column.Value;
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
		
		#region SegmentAuthorID
		
		private int _SegmentAuthorID = default(int);
		
		/// <summary>
		/// Column: SegmentAuthorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SegmentAuthorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int SegmentAuthorID
		{
			get
			{
				return _SegmentAuthorID;
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
		
		#endregion SegmentAuthorID
		
		#region SegmentID
		
		private int _SegmentID = default(int);
		
		/// <summary>
		/// Column: SegmentID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int SegmentID
		{
			get
			{
				return _SegmentID;
			}
			set
			{
				if (_SegmentID != value)
				{
					_SegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentID
		
		#region Sequence
		
		private int _Sequence = default(int);
		
		/// <summary>
		/// Column: Sequence;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Sequence", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int Sequence
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
		
		#region BHLAuthorID
		
		private int? _BHLAuthorID = null;
		
		/// <summary>
		/// Column: BHLAuthorID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("BHLAuthorID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? BHLAuthorID
		{
			get
			{
				return _BHLAuthorID;
			}
			set
			{
				if (_BHLAuthorID != value)
				{
					_BHLAuthorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLAuthorID
		
		#region FullName
		
		private string _FullName = string.Empty;
		
		/// <summary>
		/// Column: FullName;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("FullName", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=300)]
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
		[ColumnDefinition("LastName", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=150)]
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
		[ColumnDefinition("FirstName", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=150)]
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
		
		#region StartDate
		
		private string _StartDate = string.Empty;
		
		/// <summary>
		/// Column: StartDate;
		/// DBMS data type: nvarchar(25);
		/// </summary>
		[ColumnDefinition("StartDate", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=25)]
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
		[ColumnDefinition("EndDate", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=25)]
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
		
		#region BHLIdentifierID
		
		private int? _BHLIdentifierID = null;
		
		/// <summary>
		/// Column: BHLIdentifierID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("BHLIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? BHLIdentifierID
		{
			get
			{
				return _BHLIdentifierID;
			}
			set
			{
				if (_BHLIdentifierID != value)
				{
					_BHLIdentifierID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLIdentifierID
		
		#region IdentifierValue
		
		private string _IdentifierValue = string.Empty;
		
		/// <summary>
		/// Column: IdentifierValue;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("IdentifierValue", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=125)]
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
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=12)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=13)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IASegmentAuthor"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IASegmentAuthor"/>, 
		/// returns an instance of <see cref="__IASegmentAuthor"/>; otherwise returns null.</returns>
		public static new __IASegmentAuthor FromArray(byte[] byteArray)
		{
			__IASegmentAuthor o = null;
			
			try
			{
				o = (__IASegmentAuthor) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IASegmentAuthor"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IASegmentAuthor"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IASegmentAuthor)
			{
				__IASegmentAuthor o = (__IASegmentAuthor) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentAuthorID == SegmentAuthorID &&
					o.SegmentID == SegmentID &&
					o.Sequence == Sequence &&
					o.BHLAuthorID == BHLAuthorID &&
					GetComparisonString(o.FullName) == GetComparisonString(FullName) &&
					GetComparisonString(o.LastName) == GetComparisonString(LastName) &&
					GetComparisonString(o.FirstName) == GetComparisonString(FirstName) &&
					GetComparisonString(o.StartDate) == GetComparisonString(StartDate) &&
					GetComparisonString(o.EndDate) == GetComparisonString(EndDate) &&
					o.BHLIdentifierID == BHLIdentifierID &&
					GetComparisonString(o.IdentifierValue) == GetComparisonString(IdentifierValue) &&
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
				throw new ArgumentException("Argument is not of type __IASegmentAuthor");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IASegmentAuthor"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IASegmentAuthor"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IASegmentAuthor a, __IASegmentAuthor b)
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
		/// <param name="a">The first <see cref="__IASegmentAuthor"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IASegmentAuthor"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IASegmentAuthor a, __IASegmentAuthor b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IASegmentAuthor"/> object to compare with the current <see cref="__IASegmentAuthor"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IASegmentAuthor))
			{
				return false;
			}
			
			return this == (__IASegmentAuthor) obj;
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
		/// list.Sort(SortOrder.Ascending, __IASegmentAuthor.SortColumn.SegmentAuthorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentAuthorID = "SegmentAuthorID";	
			public const string SegmentID = "SegmentID";	
			public const string Sequence = "Sequence";	
			public const string BHLAuthorID = "BHLAuthorID";	
			public const string FullName = "FullName";	
			public const string LastName = "LastName";	
			public const string FirstName = "FirstName";	
			public const string StartDate = "StartDate";	
			public const string EndDate = "EndDate";	
			public const string BHLIdentifierID = "BHLIdentifierID";	
			public const string IdentifierValue = "IdentifierValue";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

