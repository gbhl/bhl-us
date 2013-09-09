
// Generated 10/14/2011 12:13:11 PM
// Do not modify the contents of this code file.
// This abstract class __IAItemStatus is based upon IAItemStatus.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAItemStatus : __IAItemStatus
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
	public abstract class __IAItemStatus : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAItemStatus()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemStatusID"></param>
		/// <param name="status"></param>
		/// <param name="description"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAItemStatus(int itemStatusID, 
			string status, 
			string description, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			ItemStatusID = itemStatusID;
			Status = status;
			Description = description;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAItemStatus()
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
					case "ItemStatusID" :
					{
						_ItemStatusID = (int)column.Value;
						break;
					}
					case "Status" :
					{
						_Status = (string)column.Value;
						break;
					}
					case "Description" :
					{
						_Description = (string)column.Value;
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
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ItemStatusID
		{
			get
			{
				return _ItemStatusID;
			}
			set
			{
				if (_ItemStatusID != value)
				{
					_ItemStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemStatusID
		
		#region Status
		
		private string _Status = string.Empty;
		
		/// <summary>
		/// Column: Status;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("Status", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string Status
		{
			get
			{
				return _Status;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_Status != value)
				{
					_Status = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Status
		
		#region Description
		
		private string _Description = string.Empty;
		
		/// <summary>
		/// Column: Description;
		/// DBMS data type: nvarchar(4000);
		/// </summary>
		[ColumnDefinition("Description", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=4000)]
		public string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 4000);
				if (_Description != value)
				{
					_Description = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Description
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAItemStatus"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAItemStatus"/>, 
		/// returns an instance of <see cref="__IAItemStatus"/>; otherwise returns null.</returns>
		public static new __IAItemStatus FromArray(byte[] byteArray)
		{
			__IAItemStatus o = null;
			
			try
			{
				o = (__IAItemStatus) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAItemStatus"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAItemStatus"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAItemStatus)
			{
				__IAItemStatus o = (__IAItemStatus) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemStatusID == ItemStatusID &&
					GetComparisonString(o.Status) == GetComparisonString(Status) &&
					GetComparisonString(o.Description) == GetComparisonString(Description) &&
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
				throw new ArgumentException("Argument is not of type __IAItemStatus");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAItemStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAItemStatus"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAItemStatus a, __IAItemStatus b)
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
		/// <param name="a">The first <see cref="__IAItemStatus"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAItemStatus"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAItemStatus a, __IAItemStatus b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAItemStatus"/> object to compare with the current <see cref="__IAItemStatus"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAItemStatus))
			{
				return false;
			}
			
			return this == (__IAItemStatus) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAItemStatus.SortColumn.ItemStatusID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemStatusID = "ItemStatusID";	
			public const string Status = "Status";	
			public const string Description = "Description";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
