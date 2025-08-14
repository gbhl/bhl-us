
// Generated 8/14/2025 10:07:46 AM
// Do not modify the contents of this code file.
// This abstract class __WDEntityIdentifier is based upon dbo.WDEntityIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class WDEntityIdentifier : __WDEntityIdentifier
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
	public abstract class __WDEntityIdentifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __WDEntityIdentifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="bHLEntityType"></param>
		/// <param name="bHLEntityID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierValue"></param>
		/// <param name="harvestDate"></param>
		public __WDEntityIdentifier(string bHLEntityType, 
			int bHLEntityID, 
			string identifierType, 
			string identifierValue, 
			DateTime harvestDate) : this()
		{
			BHLEntityType = bHLEntityType;
			BHLEntityID = bHLEntityID;
			IdentifierType = identifierType;
			IdentifierValue = identifierValue;
			HarvestDate = harvestDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__WDEntityIdentifier()
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
					case "BHLEntityType" :
					{
						_BHLEntityType = (string)column.Value;
						break;
					}
					case "BHLEntityID" :
					{
						_BHLEntityID = (int)column.Value;
						break;
					}
					case "IdentifierType" :
					{
						_IdentifierType = (string)column.Value;
						break;
					}
					case "IdentifierValue" :
					{
						_IdentifierValue = (string)column.Value;
						break;
					}
					case "HarvestDate" :
					{
						_HarvestDate = (DateTime)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region BHLEntityType
		
		private string _BHLEntityType = string.Empty;
		
		/// <summary>
		/// Column: BHLEntityType;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("BHLEntityType", DbTargetType=SqlDbType.NVarChar, Ordinal=1, CharacterMaxLength=20, IsInPrimaryKey=true)]
		public string BHLEntityType
		{
			get
			{
				return _BHLEntityType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_BHLEntityType != value)
				{
					_BHLEntityType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLEntityType
		
		#region BHLEntityID
		
		private int _BHLEntityID = default(int);
		
		/// <summary>
		/// Column: BHLEntityID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("BHLEntityID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInPrimaryKey=true)]
		public int BHLEntityID
		{
			get
			{
				return _BHLEntityID;
			}
			set
			{
				if (_BHLEntityID != value)
				{
					_BHLEntityID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLEntityID
		
		#region IdentifierType
		
		private string _IdentifierType = string.Empty;
		
		/// <summary>
		/// Column: IdentifierType;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("IdentifierType", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40, IsInPrimaryKey=true)]
		public string IdentifierType
		{
			get
			{
				return _IdentifierType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_IdentifierType != value)
				{
					_IdentifierType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierType
		
		#region IdentifierValue
		
		private string _IdentifierValue = string.Empty;
		
		/// <summary>
		/// Column: IdentifierValue;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("IdentifierValue", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=125, IsInPrimaryKey=true)]
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
		
		#region HarvestDate
		
		private DateTime _HarvestDate;
		
		/// <summary>
		/// Column: HarvestDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("HarvestDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
		public DateTime HarvestDate
		{
			get
			{
				return _HarvestDate;
			}
			set
			{
				if (_HarvestDate != value)
				{
					_HarvestDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion HarvestDate
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__WDEntityIdentifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__WDEntityIdentifier"/>, 
		/// returns an instance of <see cref="__WDEntityIdentifier"/>; otherwise returns null.</returns>
		public static new __WDEntityIdentifier FromArray(byte[] byteArray)
		{
			__WDEntityIdentifier o = null;
			
			try
			{
				o = (__WDEntityIdentifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__WDEntityIdentifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__WDEntityIdentifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __WDEntityIdentifier)
			{
				__WDEntityIdentifier o = (__WDEntityIdentifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					GetComparisonString(o.BHLEntityType) == GetComparisonString(BHLEntityType) &&
					o.BHLEntityID == BHLEntityID &&
					GetComparisonString(o.IdentifierType) == GetComparisonString(IdentifierType) &&
					GetComparisonString(o.IdentifierValue) == GetComparisonString(IdentifierValue) &&
					o.HarvestDate == HarvestDate 
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
				throw new ArgumentException("Argument is not of type __WDEntityIdentifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__WDEntityIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__WDEntityIdentifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__WDEntityIdentifier a, __WDEntityIdentifier b)
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
		/// <param name="a">The first <see cref="__WDEntityIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__WDEntityIdentifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__WDEntityIdentifier a, __WDEntityIdentifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__WDEntityIdentifier"/> object to compare with the current <see cref="__WDEntityIdentifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __WDEntityIdentifier))
			{
				return false;
			}
			
			return this == (__WDEntityIdentifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __WDEntityIdentifier.SortColumn.BHLEntityType);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string BHLEntityType = "BHLEntityType";	
			public const string BHLEntityID = "BHLEntityID";	
			public const string IdentifierType = "IdentifierType";	
			public const string IdentifierValue = "IdentifierValue";	
			public const string HarvestDate = "HarvestDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

