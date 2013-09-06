
// Generated 12/6/2011 12:20:56 PM
// Do not modify the contents of this code file.
// This abstract class __DOIStatus is based upon DOIStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class DOIStatus : __DOIStatus
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
	public abstract class __DOIStatus : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __DOIStatus()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="dOIStatusID"></param>
		/// <param name="dOIStatusName"></param>
		/// <param name="dOIStatusDescription"></param>
		public __DOIStatus(int dOIStatusID, 
			string dOIStatusName, 
			string dOIStatusDescription) : this()
		{
			DOIStatusID = dOIStatusID;
			DOIStatusName = dOIStatusName;
			DOIStatusDescription = dOIStatusDescription;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__DOIStatus()
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
					case "DOIStatusID" :
					{
						_DOIStatusID = (int)column.Value;
						break;
					}
					case "DOIStatusName" :
					{
						_DOIStatusName = (string)column.Value;
						break;
					}
					case "DOIStatusDescription" :
					{
						_DOIStatusDescription = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region DOIStatusID
		
		private int _DOIStatusID = default(int);
		
		/// <summary>
		/// Column: DOIStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("DOIStatusID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
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
		
		#region DOIStatusName
		
		private string _DOIStatusName = string.Empty;
		
		/// <summary>
		/// Column: DOIStatusName;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("DOIStatusName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string DOIStatusName
		{
			get
			{
				return _DOIStatusName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_DOIStatusName != value)
				{
					_DOIStatusName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIStatusName
		
		#region DOIStatusDescription
		
		private string _DOIStatusDescription = string.Empty;
		
		/// <summary>
		/// Column: DOIStatusDescription;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("DOIStatusDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=2000)]
		public string DOIStatusDescription
		{
			get
			{
				return _DOIStatusDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_DOIStatusDescription != value)
				{
					_DOIStatusDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIStatusDescription
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__DOIStatus"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__DOIStatus"/>, 
		/// returns an instance of <see cref="__DOIStatus"/>; otherwise returns null.</returns>
		public static new __DOIStatus FromArray(byte[] byteArray)
		{
			__DOIStatus o = null;
			
			try
			{
				o = (__DOIStatus) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__DOIStatus"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__DOIStatus"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __DOIStatus)
			{
				__DOIStatus o = (__DOIStatus) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.DOIStatusID == DOIStatusID &&
					GetComparisonString(o.DOIStatusName) == GetComparisonString(DOIStatusName) &&
					GetComparisonString(o.DOIStatusDescription) == GetComparisonString(DOIStatusDescription) 
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
				throw new ArgumentException("Argument is not of type __DOIStatus");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__DOIStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DOIStatus"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__DOIStatus a, __DOIStatus b)
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
		/// <param name="a">The first <see cref="__DOIStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DOIStatus"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__DOIStatus a, __DOIStatus b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__DOIStatus"/> object to compare with the current <see cref="__DOIStatus"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __DOIStatus))
			{
				return false;
			}
			
			return this == (__DOIStatus) obj;
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
		/// list.Sort(SortOrder.Ascending, __DOIStatus.SortColumn.DOIStatusID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string DOIStatusID = "DOIStatusID";	
			public const string DOIStatusName = "DOIStatusName";	
			public const string DOIStatusDescription = "DOIStatusDescription";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
