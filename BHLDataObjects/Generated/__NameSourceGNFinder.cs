
// Generated 1/5/2021 3:26:27 PM
// Do not modify the contents of this code file.
// This abstract class __NameSourceGNFinder is based upon dbo.NameSourceGNFinder.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class NameSourceGNFinder : __NameSourceGNFinder
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
	public abstract class __NameSourceGNFinder : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __NameSourceGNFinder()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="dataSourceID"></param>
		/// <param name="gNDataSourceName"></param>
		/// <param name="bHLIdentifierID"></param>
		/// <param name="gNDataSourceLabel"></param>
		/// <param name="gNDataSourceIcon"></param>
		/// <param name="gNDataSourceURLFormat"></param>
		public __NameSourceGNFinder(int dataSourceID, 
			string gNDataSourceName, 
			int bHLIdentifierID, 
			string gNDataSourceLabel, 
			string gNDataSourceIcon, 
			string gNDataSourceURLFormat) : this()
		{
			DataSourceID = dataSourceID;
			GNDataSourceName = gNDataSourceName;
			BHLIdentifierID = bHLIdentifierID;
			GNDataSourceLabel = gNDataSourceLabel;
			GNDataSourceIcon = gNDataSourceIcon;
			GNDataSourceURLFormat = gNDataSourceURLFormat;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__NameSourceGNFinder()
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
					case "DataSourceID" :
					{
						_DataSourceID = (int)column.Value;
						break;
					}
					case "GNDataSourceName" :
					{
						_GNDataSourceName = (string)column.Value;
						break;
					}
					case "BHLIdentifierID" :
					{
						_BHLIdentifierID = (int)column.Value;
						break;
					}
					case "GNDataSourceLabel" :
					{
						_GNDataSourceLabel = (string)column.Value;
						break;
					}
					case "GNDataSourceIcon" :
					{
						_GNDataSourceIcon = (string)column.Value;
						break;
					}
					case "GNDataSourceURLFormat" :
					{
						_GNDataSourceURLFormat = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region DataSourceID
		
		private int _DataSourceID = default(int);
		
		/// <summary>
		/// Column: DataSourceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("DataSourceID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInPrimaryKey=true)]
		public int DataSourceID
		{
			get
			{
				return _DataSourceID;
			}
			set
			{
				if (_DataSourceID != value)
				{
					_DataSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DataSourceID
		
		#region GNDataSourceName
		
		private string _GNDataSourceName = string.Empty;
		
		/// <summary>
		/// Column: GNDataSourceName;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("GNDataSourceName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=200)]
		public string GNDataSourceName
		{
			get
			{
				return _GNDataSourceName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_GNDataSourceName != value)
				{
					_GNDataSourceName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GNDataSourceName
		
		#region BHLIdentifierID
		
		private int _BHLIdentifierID = default(int);
		
		/// <summary>
		/// Column: BHLIdentifierID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("BHLIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int BHLIdentifierID
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
		
		#region GNDataSourceLabel
		
		private string _GNDataSourceLabel = string.Empty;
		
		/// <summary>
		/// Column: GNDataSourceLabel;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("GNDataSourceLabel", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=200)]
		public string GNDataSourceLabel
		{
			get
			{
				return _GNDataSourceLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_GNDataSourceLabel != value)
				{
					_GNDataSourceLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GNDataSourceLabel
		
		#region GNDataSourceIcon
		
		private string _GNDataSourceIcon = string.Empty;
		
		/// <summary>
		/// Column: GNDataSourceIcon;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("GNDataSourceIcon", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=300)]
		public string GNDataSourceIcon
		{
			get
			{
				return _GNDataSourceIcon;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_GNDataSourceIcon != value)
				{
					_GNDataSourceIcon = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GNDataSourceIcon
		
		#region GNDataSourceURLFormat
		
		private string _GNDataSourceURLFormat = string.Empty;
		
		/// <summary>
		/// Column: GNDataSourceURLFormat;
		/// DBMS data type: nvarchar(300);
		/// </summary>
		[ColumnDefinition("GNDataSourceURLFormat", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=300)]
		public string GNDataSourceURLFormat
		{
			get
			{
				return _GNDataSourceURLFormat;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 300);
				if (_GNDataSourceURLFormat != value)
				{
					_GNDataSourceURLFormat = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion GNDataSourceURLFormat
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__NameSourceGNFinder"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__NameSourceGNFinder"/>, 
		/// returns an instance of <see cref="__NameSourceGNFinder"/>; otherwise returns null.</returns>
		public static new __NameSourceGNFinder FromArray(byte[] byteArray)
		{
			__NameSourceGNFinder o = null;
			
			try
			{
				o = (__NameSourceGNFinder) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__NameSourceGNFinder"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__NameSourceGNFinder"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __NameSourceGNFinder)
			{
				__NameSourceGNFinder o = (__NameSourceGNFinder) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.DataSourceID == DataSourceID &&
					GetComparisonString(o.GNDataSourceName) == GetComparisonString(GNDataSourceName) &&
					o.BHLIdentifierID == BHLIdentifierID &&
					GetComparisonString(o.GNDataSourceLabel) == GetComparisonString(GNDataSourceLabel) &&
					GetComparisonString(o.GNDataSourceIcon) == GetComparisonString(GNDataSourceIcon) &&
					GetComparisonString(o.GNDataSourceURLFormat) == GetComparisonString(GNDataSourceURLFormat) 
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
				throw new ArgumentException("Argument is not of type __NameSourceGNFinder");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__NameSourceGNFinder"/> object to compare.</param>
		/// <param name="b">The second <see cref="__NameSourceGNFinder"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__NameSourceGNFinder a, __NameSourceGNFinder b)
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
		/// <param name="a">The first <see cref="__NameSourceGNFinder"/> object to compare.</param>
		/// <param name="b">The second <see cref="__NameSourceGNFinder"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__NameSourceGNFinder a, __NameSourceGNFinder b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__NameSourceGNFinder"/> object to compare with the current <see cref="__NameSourceGNFinder"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __NameSourceGNFinder))
			{
				return false;
			}
			
			return this == (__NameSourceGNFinder) obj;
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
		/// list.Sort(SortOrder.Ascending, __NameSourceGNFinder.SortColumn.DataSourceID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string DataSourceID = "DataSourceID";	
			public const string GNDataSourceName = "GNDataSourceName";	
			public const string BHLIdentifierID = "BHLIdentifierID";	
			public const string GNDataSourceLabel = "GNDataSourceLabel";	
			public const string GNDataSourceIcon = "GNDataSourceIcon";	
			public const string GNDataSourceURLFormat = "GNDataSourceURLFormat";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

