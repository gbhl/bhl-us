
// Generated 6/23/2020 4:38:51 PM
// Do not modify the contents of this code file.
// This abstract class __ApplicationCache is based upon dbo.ApplicationCache.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ApplicationCache : __ApplicationCache
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
	public abstract class __ApplicationCache : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ApplicationCache()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="cacheData"></param>
		/// <param name="absoluteExpirationDate"></param>
		/// <param name="slidingExpirationDuration"></param>
		/// <param name="lastAccessDate"></param>
		/// <param name="creationDate"></param>
		public __ApplicationCache(string cacheKey, 
			string cacheData, 
			DateTime? absoluteExpirationDate, 
			int? slidingExpirationDuration, 
			DateTime lastAccessDate, 
			DateTime creationDate) : this()
		{
			CacheKey = cacheKey;
			CacheData = cacheData;
			AbsoluteExpirationDate = absoluteExpirationDate;
			SlidingExpirationDuration = slidingExpirationDuration;
			LastAccessDate = lastAccessDate;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ApplicationCache()
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
					case "CacheKey" :
					{
						_CacheKey = (string)column.Value;
						break;
					}
					case "CacheData" :
					{
						_CacheData = (string)column.Value;
						break;
					}
					case "AbsoluteExpirationDate" :
					{
						_AbsoluteExpirationDate = (DateTime?)column.Value;
						break;
					}
					case "SlidingExpirationDuration" :
					{
						_SlidingExpirationDuration = (int?)column.Value;
						break;
					}
					case "LastAccessDate" :
					{
						_LastAccessDate = (DateTime)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region CacheKey
		
		private string _CacheKey = string.Empty;
		
		/// <summary>
		/// Column: CacheKey;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("CacheKey", DbTargetType=SqlDbType.NVarChar, Ordinal=1, CharacterMaxLength=100, IsInPrimaryKey=true)]
		public string CacheKey
		{
			get
			{
				return _CacheKey;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_CacheKey != value)
				{
					_CacheKey = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CacheKey
		
		#region CacheData
		
		private string _CacheData = string.Empty;
		
		/// <summary>
		/// Column: CacheData;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CacheData", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=1073741823)]
		public string CacheData
		{
			get
			{
				return _CacheData;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CacheData != value)
				{
					_CacheData = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CacheData
		
		#region AbsoluteExpirationDate
		
		private DateTime? _AbsoluteExpirationDate = null;
		
		/// <summary>
		/// Column: AbsoluteExpirationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("AbsoluteExpirationDate", DbTargetType=SqlDbType.DateTime, Ordinal=3, IsNullable=true)]
		public DateTime? AbsoluteExpirationDate
		{
			get
			{
				return _AbsoluteExpirationDate;
			}
			set
			{
				if (_AbsoluteExpirationDate != value)
				{
					_AbsoluteExpirationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AbsoluteExpirationDate
		
		#region SlidingExpirationDuration
		
		private int? _SlidingExpirationDuration = null;
		
		/// <summary>
		/// Column: SlidingExpirationDuration;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("SlidingExpirationDuration", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? SlidingExpirationDuration
		{
			get
			{
				return _SlidingExpirationDuration;
			}
			set
			{
				if (_SlidingExpirationDuration != value)
				{
					_SlidingExpirationDuration = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SlidingExpirationDuration
		
		#region LastAccessDate
		
		private DateTime _LastAccessDate;
		
		/// <summary>
		/// Column: LastAccessDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("LastAccessDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
		public DateTime LastAccessDate
		{
			get
			{
				return _LastAccessDate;
			}
			set
			{
				if (_LastAccessDate != value)
				{
					_LastAccessDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastAccessDate
		
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ApplicationCache"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ApplicationCache"/>, 
		/// returns an instance of <see cref="__ApplicationCache"/>; otherwise returns null.</returns>
		public static new __ApplicationCache FromArray(byte[] byteArray)
		{
			__ApplicationCache o = null;
			
			try
			{
				o = (__ApplicationCache) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ApplicationCache"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ApplicationCache"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ApplicationCache)
			{
				__ApplicationCache o = (__ApplicationCache) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					GetComparisonString(o.CacheKey) == GetComparisonString(CacheKey) &&
					GetComparisonString(o.CacheData) == GetComparisonString(CacheData) &&
					o.AbsoluteExpirationDate == AbsoluteExpirationDate &&
					o.SlidingExpirationDuration == SlidingExpirationDuration &&
					o.LastAccessDate == LastAccessDate &&
					o.CreationDate == CreationDate 
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
				throw new ArgumentException("Argument is not of type __ApplicationCache");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ApplicationCache"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ApplicationCache"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ApplicationCache a, __ApplicationCache b)
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
		/// <param name="a">The first <see cref="__ApplicationCache"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ApplicationCache"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ApplicationCache a, __ApplicationCache b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ApplicationCache"/> object to compare with the current <see cref="__ApplicationCache"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ApplicationCache))
			{
				return false;
			}
			
			return this == (__ApplicationCache) obj;
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
		/// list.Sort(SortOrder.Ascending, __ApplicationCache.SortColumn.CacheKey);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string CacheKey = "CacheKey";	
			public const string CacheData = "CacheData";	
			public const string AbsoluteExpirationDate = "AbsoluteExpirationDate";	
			public const string SlidingExpirationDuration = "SlidingExpirationDuration";	
			public const string LastAccessDate = "LastAccessDate";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

