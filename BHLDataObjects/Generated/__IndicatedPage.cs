
// Generated 5/17/2010 4:03:17 PM
// Do not modify the contents of this code file.
// This abstract class __IndicatedPage is based upon IndicatedPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class IndicatedPage : __IndicatedPage
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
	public abstract class __IndicatedPage : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IndicatedPage()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __IndicatedPage(int pageID, 
			short sequence, 
			string pagePrefix, 
			string pageNumber, 
			bool implied, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			PageID = pageID;
			Sequence = sequence;
			PagePrefix = pagePrefix;
			PageNumber = pageNumber;
			Implied = implied;
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
		~__IndicatedPage()
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
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
					case "Sequence" :
					{
						_Sequence = (short)column.Value;
						break;
					}
					case "PagePrefix" :
					{
						_PagePrefix = (string)column.Value;
						break;
					}
					case "PageNumber" :
					{
						_PageNumber = (string)column.Value;
						break;
					}
					case "Implied" :
					{
						_Implied = (bool)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime?)column.Value;
						break;
					}
					case "LastModifiedDate" :
					{
						_LastModifiedDate = (DateTime?)column.Value;
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
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
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
		
		#region Sequence
		
		private short _Sequence = default(short);
		
		/// <summary>
		/// Column: Sequence;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("Sequence", DbTargetType=SqlDbType.SmallInt, Ordinal=2, NumericPrecision=5, IsInPrimaryKey=true)]
		public short Sequence
		{
			get
			{
				return _Sequence;
			}
			set
			{
				if (_Sequence != value)
				{
					_Sequence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Sequence
		
		#region PagePrefix
		
		private string _PagePrefix = null;
		
		/// <summary>
		/// Column: PagePrefix;
		/// DBMS data type: nvarchar(40); Nullable;
		/// </summary>
		[ColumnDefinition("PagePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40, IsNullable=true)]
		public string PagePrefix
		{
			get
			{
				return _PagePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_PagePrefix != value)
				{
					_PagePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PagePrefix
		
		#region PageNumber
		
		private string _PageNumber = null;
		
		/// <summary>
		/// Column: PageNumber;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("PageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=20, IsNullable=true)]
		public string PageNumber
		{
			get
			{
				return _PageNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_PageNumber != value)
				{
					_PageNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageNumber
		
		#region Implied
		
		private bool _Implied = false;
		
		/// <summary>
		/// Column: Implied;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("Implied", DbTargetType=SqlDbType.Bit, Ordinal=5)]
		public bool Implied
		{
			get
			{
				return _Implied;
			}
			set
			{
				if (_Implied != value)
				{
					_Implied = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Implied
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=6, IsNullable=true)]
		public DateTime? CreationDate
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
		
		private DateTime? _LastModifiedDate = null;
		
		/// <summary>
		/// Column: LastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=7, IsNullable=true)]
		public DateTime? LastModifiedDate
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IndicatedPage"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IndicatedPage"/>, 
		/// returns an instance of <see cref="__IndicatedPage"/>; otherwise returns null.</returns>
		public static new __IndicatedPage FromArray(byte[] byteArray)
		{
			__IndicatedPage o = null;
			
			try
			{
				o = (__IndicatedPage) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IndicatedPage"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IndicatedPage"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IndicatedPage)
			{
				__IndicatedPage o = (__IndicatedPage) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageID == PageID &&
					o.Sequence == Sequence &&
					GetComparisonString(o.PagePrefix) == GetComparisonString(PagePrefix) &&
					GetComparisonString(o.PageNumber) == GetComparisonString(PageNumber) &&
					o.Implied == Implied &&
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
				throw new ArgumentException("Argument is not of type __IndicatedPage");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IndicatedPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IndicatedPage"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IndicatedPage a, __IndicatedPage b)
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
		/// <param name="a">The first <see cref="__IndicatedPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IndicatedPage"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IndicatedPage a, __IndicatedPage b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IndicatedPage"/> object to compare with the current <see cref="__IndicatedPage"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IndicatedPage))
			{
				return false;
			}
			
			return this == (__IndicatedPage) obj;
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
		/// list.Sort(SortOrder.Ascending, __IndicatedPage.SortColumn.PageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageID = "PageID";	
			public const string Sequence = "Sequence";	
			public const string PagePrefix = "PagePrefix";	
			public const string PageNumber = "PageNumber";	
			public const string Implied = "Implied";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
