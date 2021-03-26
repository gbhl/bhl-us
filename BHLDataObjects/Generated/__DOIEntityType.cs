
// Generated 2/4/2021 1:41:31 PM
// Do not modify the contents of this code file.
// This abstract class __DOIEntityType is based upon dbo.DOIEntityType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class DOIEntityType : __DOIEntityType
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
	public abstract class __DOIEntityType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __DOIEntityType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="dOIEntityTypeID"></param>
		/// <param name="dOIEntityTypeName"></param>
		public __DOIEntityType(int dOIEntityTypeID, 
			string dOIEntityTypeName) : this()
		{
			DOIEntityTypeID = dOIEntityTypeID;
			DOIEntityTypeName = dOIEntityTypeName;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__DOIEntityType()
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
					case "DOIEntityTypeID" :
					{
						_DOIEntityTypeID = (int)column.Value;
						break;
					}
					case "DOIEntityTypeName" :
					{
						_DOIEntityTypeName = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region DOIEntityTypeID
		
		private int _DOIEntityTypeID = default(int);
		
		/// <summary>
		/// Column: DOIEntityTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("DOIEntityTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
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
		
		#region DOIEntityTypeName
		
		private string _DOIEntityTypeName = string.Empty;
		
		/// <summary>
		/// Column: DOIEntityTypeName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("DOIEntityTypeName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string DOIEntityTypeName
		{
			get
			{
				return _DOIEntityTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_DOIEntityTypeName != value)
				{
					_DOIEntityTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOIEntityTypeName
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__DOIEntityType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__DOIEntityType"/>, 
		/// returns an instance of <see cref="__DOIEntityType"/>; otherwise returns null.</returns>
		public static new __DOIEntityType FromArray(byte[] byteArray)
		{
			__DOIEntityType o = null;
			
			try
			{
				o = (__DOIEntityType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__DOIEntityType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__DOIEntityType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __DOIEntityType)
			{
				__DOIEntityType o = (__DOIEntityType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.DOIEntityTypeID == DOIEntityTypeID &&
					GetComparisonString(o.DOIEntityTypeName) == GetComparisonString(DOIEntityTypeName) 
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
				throw new ArgumentException("Argument is not of type __DOIEntityType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__DOIEntityType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DOIEntityType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__DOIEntityType a, __DOIEntityType b)
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
		/// <param name="a">The first <see cref="__DOIEntityType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DOIEntityType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__DOIEntityType a, __DOIEntityType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__DOIEntityType"/> object to compare with the current <see cref="__DOIEntityType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __DOIEntityType))
			{
				return false;
			}
			
			return this == (__DOIEntityType) obj;
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
		/// list.Sort(SortOrder.Ascending, __DOIEntityType.SortColumn.DOIEntityTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string DOIEntityTypeID = "DOIEntityTypeID";	
			public const string DOIEntityTypeName = "DOIEntityTypeName";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

