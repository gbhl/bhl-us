
// Generated 11/4/2015 10:49:28 AM
// Do not modify the contents of this code file.
// This abstract class __DisqusCache is based upon DisqusCache.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class DisqusCache : __DisqusCache
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
	public abstract class __DisqusCache : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __DisqusCache()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="disqusCacheID"></param>
		/// <param name="itemID"></param>
		/// <param name="pageID"></param>
		/// <param name="creationDate"></param>
		public __DisqusCache(int disqusCacheID, 
			int itemID, 
			int pageID,
			int count, 
			DateTime creationDate) : this()
		{
			_DisqusCacheID = disqusCacheID;
			ItemID = itemID;
			PageID = pageID;
			Count = count;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__DisqusCache()
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
					case "DisqusCacheID" :
					{
						_DisqusCacheID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
					case "Count" :
					{
						_Count = (int)column.Value;
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
		
		#region DisqusCacheID
		
		private int _DisqusCacheID = default(int);
		
		/// <summary>
		/// Column: DisqusCacheID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("DisqusCacheID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int DisqusCacheID
		{
			get
			{
				return _DisqusCacheID;
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
		
		#endregion DisqusCacheID
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
			}
			set
			{
				if (_ItemID != value)
				{
					_ItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemID
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int PageID
		{
			get
			{
				return _PageID;
			}
			set
			{
				if (_PageID != value)
				{
					_PageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageID

		#region Count
		
		private int _Count = default(int);

		/// <summary>
		/// Column: Count;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Count", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10)]
		public int Count
		{
			get
			{
				return _Count;
			}
			set
			{
				if (_Count != value)
				{
					_Count = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Count
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__DisqusCache"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__DisqusCache"/>, 
		/// returns an instance of <see cref="__DisqusCache"/>; otherwise returns null.</returns>
		public static new __DisqusCache FromArray(byte[] byteArray)
		{
			__DisqusCache o = null;
			
			try
			{
				o = (__DisqusCache) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__DisqusCache"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__DisqusCache"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __DisqusCache)
			{
				__DisqusCache o = (__DisqusCache) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.DisqusCacheID == DisqusCacheID &&
					o.ItemID == ItemID &&
					o.PageID == PageID &&
					o.Count == Count &&
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
				throw new ArgumentException("Argument is not of type __DisqusCache");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__DisqusCache"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DisqusCache"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__DisqusCache a, __DisqusCache b)
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
		/// <param name="a">The first <see cref="__DisqusCache"/> object to compare.</param>
		/// <param name="b">The second <see cref="__DisqusCache"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__DisqusCache a, __DisqusCache b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__DisqusCache"/> object to compare with the current <see cref="__DisqusCache"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __DisqusCache))
			{
				return false;
			}
			
			return this == (__DisqusCache) obj;
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
		/// list.Sort(SortOrder.Ascending, __DisqusCache.SortColumn.DisqusCacheID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string DisqusCacheID = "DisqusCacheID";	
			public const string ItemID = "ItemID";	
			public const string PageID = "PageID";	
			public const string Count = "Count";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
