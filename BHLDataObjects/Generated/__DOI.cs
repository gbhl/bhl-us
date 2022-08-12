
// Generated 1/20/2021 12:05:35 PM
// Do not modify the contents of this code file.
// This abstract class __DOI is based upon dbo.DOI.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class DOI : __DOI
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
	public abstract class __DOI : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __DOI()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="dOIID"></param>
		/// <param name="dOIEntityTypeID"></param>
		/// <param name="entityID"></param>
		/// <param name="dOIStatusID"></param>
		/// <param name="dOIBatchID"></param>
		/// <param name="dOIName"></param>
		/// <param name="statusDate"></param>
		/// <param name="statusMessage"></param>
		/// <param name="isValid"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __DOI(int dOIID, 
			int dOIEntityTypeID, 
			int entityID, 
			int dOIStatusID, 
			string dOIBatchID, 
			string dOIName, 
			DateTime statusDate, 
			string statusMessage, 
			short isValid, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_DOIID = dOIID;
			DOIEntityTypeID = dOIEntityTypeID;
			EntityID = entityID;
			DOIStatusID = dOIStatusID;
			DOIBatchID = dOIBatchID;
			DOIName = dOIName;
			StatusDate = statusDate;
			StatusMessage = statusMessage;
			IsValid = isValid;
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
		~__DOI()
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
					case "DOIID" :
					{
						_DOIID = (int)column.Value;
						break;
					}
					case "DOIEntityTypeID" :
					{
						_DOIEntityTypeID = (int)column.Value;
						break;
					}
					case "EntityID" :
					{
						_EntityID = (int)column.Value;
						break;
					}
					case "DOIStatusID" :
					{
						_DOIStatusID = (int)column.Value;
						break;
					}
					case "DOIBatchID" :
					{
						_DOIBatchID = (string)column.Value;
						break;
					}
					case "DOIName" :
					{
						_DOIName = (string)column.Value;
						break;
					}
					case "StatusDate" :
					{
						_StatusDate = (DateTime)column.Value;
						break;
					}
					case "StatusMessage" :
					{
						_StatusMessage = (string)column.Value;
						break;
					}
					case "IsValid" :
					{
						_IsValid = (short)column.Value;
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
		
		#region DOIID
		
		private int _DOIID = default(int);
		
		/// <summary>
		/// Column: DOIID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("DOIID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int DOIID
		{
			get
			{
				return _DOIID;
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
		
		#endregion DOIID
		
		#region DOIEntityTypeID
		
		private int _DOIEntityTypeID = default(int);
		
		/// <summary>
		/// Column: DOIEntityTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("DOIEntityTypeID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int DOIEntityTypeID
		{
			get
			{
				return _DOIEntityTypeID;
			}
			set
			{
				if (_DOIEntityTypeID != value)
				{
					_DOIEntityTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIEntityTypeID
		
		#region EntityID
		
		private int _EntityID = default(int);
		
		/// <summary>
		/// Column: EntityID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("EntityID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int EntityID
		{
			get
			{
				return _EntityID;
			}
			set
			{
				if (_EntityID != value)
				{
					_EntityID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EntityID
		
		#region DOIStatusID
		
		private int _DOIStatusID = default(int);
		
		/// <summary>
		/// Column: DOIStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("DOIStatusID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true)]
		public int DOIStatusID
		{
			get
			{
				return _DOIStatusID;
			}
			set
			{
				if (_DOIStatusID != value)
				{
					_DOIStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIStatusID
		
		#region DOIBatchID
		
		private string _DOIBatchID = string.Empty;
		
		/// <summary>
		/// Column: DOIBatchID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("DOIBatchID", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
		public string DOIBatchID
		{
			get
			{
				return _DOIBatchID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_DOIBatchID != value)
				{
					_DOIBatchID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIBatchID
		
		#region DOIName
		
		private string _DOIName = string.Empty;
		
		/// <summary>
		/// Column: DOIName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("DOIName", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=50)]
		public string DOIName
		{
			get
			{
				return _DOIName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_DOIName != value)
				{
					_DOIName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIName
		
		#region StatusDate
		
		private DateTime _StatusDate;
		
		/// <summary>
		/// Column: StatusDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("StatusDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
		public DateTime StatusDate
		{
			get
			{
				return _StatusDate;
			}
			set
			{
				if (_StatusDate != value)
				{
					_StatusDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatusDate
		
		#region StatusMessage
		
		private string _StatusMessage = string.Empty;
		
		/// <summary>
		/// Column: StatusMessage;
		/// DBMS data type: nvarchar(1000);
		/// </summary>
		[ColumnDefinition("StatusMessage", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=1000)]
		public string StatusMessage
		{
			get
			{
				return _StatusMessage;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1000);
				if (_StatusMessage != value)
				{
					_StatusMessage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatusMessage
		
		#region IsValid
		
		private short _IsValid = default(short);
		
		/// <summary>
		/// Column: IsValid;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("IsValid", DbTargetType=SqlDbType.SmallInt, Ordinal=9, NumericPrecision=5)]
		public short IsValid
		{
			get
			{
				return _IsValid;
			}
			set
			{
				if (_IsValid != value)
				{
					_IsValid = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsValid
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__DOI"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__DOI"/>, 
		/// returns an instance of <see cref="__DOI"/>; otherwise returns null.</returns>
		public static new __DOI FromArray(byte[] byteArray)
		{
			__DOI o = null;
			
			try
			{
				o = (__DOI) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__DOI"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__DOI"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __DOI)
			{
				__DOI o = (__DOI) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.DOIID == DOIID &&
					o.DOIEntityTypeID == DOIEntityTypeID &&
					o.EntityID == EntityID &&
					o.DOIStatusID == DOIStatusID &&
					GetComparisonString(o.DOIBatchID) == GetComparisonString(DOIBatchID) &&
					GetComparisonString(o.DOIName) == GetComparisonString(DOIName) &&
					o.StatusDate == StatusDate &&
					GetComparisonString(o.StatusMessage) == GetComparisonString(StatusMessage) &&
					o.IsValid == IsValid &&
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
				throw new ArgumentException("Argument is not of type __DOI");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__DOI"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DOI"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__DOI a, __DOI b)
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
		/// <param name="a">The first <see cref="__DOI"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DOI"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__DOI a, __DOI b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__DOI"/> object to compare with the current <see cref="__DOI"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __DOI))
			{
				return false;
			}
			
			return this == (__DOI) obj;
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
		/// list.Sort(SortOrder.Ascending, __DOI.SortColumn.DOIID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string DOIID = "DOIID";	
			public const string DOIEntityTypeID = "DOIEntityTypeID";	
			public const string EntityID = "EntityID";	
			public const string DOIStatusID = "DOIStatusID";	
			public const string DOIBatchID = "DOIBatchID";	
			public const string DOIName = "DOIName";	
			public const string StatusDate = "StatusDate";	
			public const string StatusMessage = "StatusMessage";	
			public const string IsValid = "IsValid";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

