
// Generated 9/18/2012 12:12:30 PM
// Do not modify the contents of this code file.
// This abstract class __SegmentPage is based upon SegmentPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class SegmentPage : __SegmentPage
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
	public abstract class __SegmentPage : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __SegmentPage()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentPageID"></param>
		/// <param name="segmentID"></param>
		/// <param name="pageID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __SegmentPage(int segmentPageID, 
			int segmentID, 
			int pageID, 
			short sequenceOrder, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_SegmentPageID = segmentPageID;
			SegmentID = segmentID;
			PageID = pageID;
			SequenceOrder = sequenceOrder;
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
		~__SegmentPage()
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
					case "SegmentPageID" :
					{
						_SegmentPageID = (int)column.Value;
						break;
					}
					case "SegmentID" :
					{
						_SegmentID = (int)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
					case "SequenceOrder" :
					{
						_SequenceOrder = (short)column.Value;
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
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region SegmentPageID
		
		private int _SegmentPageID = default(int);
		
		/// <summary>
		/// Column: SegmentPageID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SegmentPageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int SegmentPageID
		{
			get
			{
				return _SegmentPageID;
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
		
		#endregion SegmentPageID
		
		#region SegmentID
		
		private int _SegmentID = default(int);
		
		/// <summary>
		/// Column: SegmentID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int SegmentID
		{
			get
			{
				return _SegmentID;
			}
			set
			{
				if (_SegmentID != value)
				{
					_SegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentID
		
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
		
		#region SequenceOrder
		
		private short _SequenceOrder = default(short);
		
		/// <summary>
		/// Column: SequenceOrder;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.SmallInt, Ordinal=4, NumericPrecision=5)]
		public short SequenceOrder
		{
			get
			{
				return _SequenceOrder;
			}
			set
			{
				if (_SequenceOrder != value)
				{
					_SequenceOrder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SequenceOrder
		
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
		
		#region LastModifiedDate
		
		private DateTime _LastModifiedDate;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
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
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
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
		/// Deserializes the byte array and returns an instance of <see cref="__SegmentPage"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__SegmentPage"/>, 
		/// returns an instance of <see cref="__SegmentPage"/>; otherwise returns null.</returns>
		public static new __SegmentPage FromArray(byte[] byteArray)
		{
			__SegmentPage o = null;
			
			try
			{
				o = (__SegmentPage) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__SegmentPage"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__SegmentPage"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __SegmentPage)
			{
				__SegmentPage o = (__SegmentPage) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentPageID == SegmentPageID &&
					o.SegmentID == SegmentID &&
					o.PageID == PageID &&
					o.SequenceOrder == SequenceOrder &&
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
				throw new ArgumentException("Argument is not of type __SegmentPage");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__SegmentPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentPage"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__SegmentPage a, __SegmentPage b)
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
		/// <param name="a">The first <see cref="__SegmentPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__SegmentPage"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__SegmentPage a, __SegmentPage b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__SegmentPage"/> object to compare with the current <see cref="__SegmentPage"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __SegmentPage))
			{
				return false;
			}
			
			return this == (__SegmentPage) obj;
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
		/// list.Sort(SortOrder.Ascending, __SegmentPage.SortColumn.SegmentPageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentPageID = "SegmentPageID";	
			public const string SegmentID = "SegmentID";	
			public const string PageID = "PageID";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
