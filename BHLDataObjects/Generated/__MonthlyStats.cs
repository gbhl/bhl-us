
// Generated 10/29/2008 10:12:36 AM
// Do not modify the contents of this code file.
// This abstract class __MonthlyStats is based upon MonthlyStats.

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
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="institutionName"></param>
		/// <param name="statType"></param>
		/// <param name="statValue"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __MonthlyStats(int year, 
			int month, 
			string institutionName, 
			string statType, 
			int statValue, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			Year = year;
			Month = month;
			InstitutionName = institutionName;
			StatType = statType;
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
					case "InstitutionName" :
					{
						_InstitutionName = (string)column.Value;
						break;
					}
					case "StatType" :
					{
						_StatType = (string)column.Value;
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
		
		#region Year
		
		private int _Year = default(int);
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInPrimaryKey=true)]
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
		[ColumnDefinition("Month", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInPrimaryKey=true)]
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
		
		#region InstitutionName
		
		private string _InstitutionName = string.Empty;
		
		/// <summary>
		/// Column: InstitutionName;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("InstitutionName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=255, IsInPrimaryKey=true)]
		public string InstitutionName
		{
			get
			{
				return _InstitutionName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_InstitutionName != value)
				{
					_InstitutionName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionName
		
		#region StatType
		
		private string _StatType = string.Empty;
		
		/// <summary>
		/// Column: StatType;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("StatType", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=100, IsInPrimaryKey=true)]
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
		
		#region StatValue
		
		private int _StatValue = default(int);
		
		/// <summary>
		/// Column: StatValue;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("StatValue", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10)]
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
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
					o.Year == Year &&
					o.Month == Month &&
					GetComparisonString(o.InstitutionName) == GetComparisonString(InstitutionName) &&
					GetComparisonString(o.StatType) == GetComparisonString(StatType) &&
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
		/// For example where list is a instance of <see cref="CustomGenericList">, 
		/// list.Sort(SortOrder.Ascending, __MonthlyStats.SortColumn.Year);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string Year = "Year";	
			public const string Month = "Month";	
			public const string InstitutionName = "InstitutionName";	
			public const string StatType = "StatType";	
			public const string StatValue = "StatValue";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
