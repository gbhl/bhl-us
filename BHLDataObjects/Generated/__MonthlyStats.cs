
// Generated 1/5/2021 3:26:14 PM
// Do not modify the contents of this code file.
// This abstract class __MonthlyStats is based upon dbo.MonthlyStats.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class MonthlyStats : __MonthlyStats
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
	public abstract class __MonthlyStats : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __MonthlyStats()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="monthlyStatID"></param>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionCode"></param>
		/// <param name="statType"></param>
		/// <param name="statLevel"></param>
		/// <param name="statValue"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __MonthlyStats(int monthlyStatID, 
			int year, 
			int month, 
			string institutionCode, 
			string statType, 
			string statLevel, 
			int statValue, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_MonthlyStatID = monthlyStatID;
			Year = year;
			Month = month;
			InstitutionCode = institutionCode;
			StatType = statType;
			StatLevel = statLevel;
			StatValue = statValue;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__MonthlyStats()
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
					case "MonthlyStatID" :
					{
						_MonthlyStatID = (int)column.Value;
						break;
					}
					case "Year" :
					{
						_Year = (int)column.Value;
						break;
					}
					case "Month" :
					{
						_Month = (int)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "StatType" :
					{
						_StatType = (string)column.Value;
						break;
					}
					case "StatLevel" :
					{
						_StatLevel = (string)column.Value;
						break;
					}
					case "StatValue" :
					{
						_StatValue = (int)column.Value;
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
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region MonthlyStatID
		
		private int _MonthlyStatID = default(int);
		
		/// <summary>
		/// Column: MonthlyStatID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MonthlyStatID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int MonthlyStatID
		{
			get
			{
				return _MonthlyStatID;
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
		
		#endregion MonthlyStatID
		
		#region Year
		
		private int _Year = default(int);
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
		public int Year
		{
			get
			{
				return _Year;
			}
			set
			{
				if (_Year != value)
				{
					_Year = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Year
		
		#region Month
		
		private int _Month = default(int);
		
		/// <summary>
		/// Column: Month;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Month", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int Month
		{
			get
			{
				return _Month;
			}
			set
			{
				if (_Month != value)
				{
					_Month = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Month
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=10, IsNullable=true)]
		public string InstitutionCode
		{
			get
			{
				return _InstitutionCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_InstitutionCode != value)
				{
					_InstitutionCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionCode
		
		#region StatType
		
		private string _StatType = string.Empty;
		
		/// <summary>
		/// Column: StatType;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("StatType", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
		public string StatType
		{
			get
			{
				return _StatType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_StatType != value)
				{
					_StatType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatType
		
		#region StatLevel
		
		private string _StatLevel = string.Empty;
		
		/// <summary>
		/// Column: StatLevel;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("StatLevel", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100)]
		public string StatLevel
		{
			get
			{
				return _StatLevel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_StatLevel != value)
				{
					_StatLevel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatLevel
		
		#region StatValue
		
		private int _StatValue = default(int);
		
		/// <summary>
		/// Column: StatValue;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("StatValue", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
		public int StatValue
		{
			get
			{
				return _StatValue;
			}
			set
			{
				if (_StatValue != value)
				{
					_StatValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StatValue
		
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__MonthlyStats"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__MonthlyStats"/>, 
		/// returns an instance of <see cref="__MonthlyStats"/>; otherwise returns null.</returns>
		public static new __MonthlyStats FromArray(byte[] byteArray)
		{
			__MonthlyStats o = null;
			
			try
			{
				o = (__MonthlyStats) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__MonthlyStats"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__MonthlyStats"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __MonthlyStats)
			{
				__MonthlyStats o = (__MonthlyStats) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MonthlyStatID == MonthlyStatID &&
					o.Year == Year &&
					o.Month == Month &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					GetComparisonString(o.StatType) == GetComparisonString(StatType) &&
					GetComparisonString(o.StatLevel) == GetComparisonString(StatLevel) &&
					o.StatValue == StatValue &&
					o.CreationDate == CreationDate &&
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
				throw new ArgumentException("Argument is not of type __MonthlyStats");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__MonthlyStats"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MonthlyStats"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__MonthlyStats a, __MonthlyStats b)
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
		/// <param name="a">The first <see cref="__MonthlyStats"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MonthlyStats"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__MonthlyStats a, __MonthlyStats b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__MonthlyStats"/> object to compare with the current <see cref="__MonthlyStats"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __MonthlyStats))
			{
				return false;
			}
			
			return this == (__MonthlyStats) obj;
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
		/// list.Sort(SortOrder.Ascending, __MonthlyStats.SortColumn.MonthlyStatID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MonthlyStatID = "MonthlyStatID";	
			public const string Year = "Year";	
			public const string Month = "Month";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string StatType = "StatType";	
			public const string StatLevel = "StatLevel";	
			public const string StatValue = "StatValue";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

