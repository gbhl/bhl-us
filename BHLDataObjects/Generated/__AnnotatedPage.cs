
// Generated 1/5/2021 3:36:20 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotatedPage is based upon annotation.AnnotatedPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotatedPage : __AnnotatedPage
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
	public abstract class __AnnotatedPage : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotatedPage()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotatedPageID"></param>
		/// <param name="annotatedItemID"></param>
		/// <param name="pageID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="pageNumber"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotatedPage(int annotatedPageID, 
			int annotatedItemID, 
			int? pageID, 
			string externalIdentifier, 
			int annotatedPageTypeID, 
			string pageNumber, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotatedPageID = annotatedPageID;
			AnnotatedItemID = annotatedItemID;
			PageID = pageID;
			ExternalIdentifier = externalIdentifier;
			AnnotatedPageTypeID = annotatedPageTypeID;
			PageNumber = pageNumber;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotatedPage()
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
					case "AnnotatedPageID" :
					{
						_AnnotatedPageID = (int)column.Value;
						break;
					}
					case "AnnotatedItemID" :
					{
						_AnnotatedItemID = (int)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int?)column.Value;
						break;
					}
					case "ExternalIdentifier" :
					{
						_ExternalIdentifier = (string)column.Value;
						break;
					}
					case "AnnotatedPageTypeID" :
					{
						_AnnotatedPageTypeID = (int)column.Value;
						break;
					}
					case "PageNumber" :
					{
						_PageNumber = (string)column.Value;
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
		
		#region AnnotatedPageID
		
		private int _AnnotatedPageID = default(int);
		
		/// <summary>
		/// Column: AnnotatedPageID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotatedPageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotatedPageID
		{
			get
			{
				return _AnnotatedPageID;
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
		
		#endregion AnnotatedPageID
		
		#region AnnotatedItemID
		
		private int _AnnotatedItemID = default(int);
		
		/// <summary>
		/// Column: AnnotatedItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotatedItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotatedItemID
		{
			get
			{
				return _AnnotatedItemID;
			}
			set
			{
				if (_AnnotatedItemID != value)
				{
					_AnnotatedItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedItemID
		
		#region PageID
		
		private int? _PageID = null;
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsNullable=true)]
		public int? PageID
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
		
		#region ExternalIdentifier
		
		private string _ExternalIdentifier = string.Empty;
		
		/// <summary>
		/// Column: ExternalIdentifier;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("ExternalIdentifier", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
		public string ExternalIdentifier
		{
			get
			{
				return _ExternalIdentifier;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_ExternalIdentifier != value)
				{
					_ExternalIdentifier = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalIdentifier
		
		#region AnnotatedPageTypeID
		
		private int _AnnotatedPageTypeID = default(int);
		
		/// <summary>
		/// Column: AnnotatedPageTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotatedPageTypeID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotatedPageTypeID
		{
			get
			{
				return _AnnotatedPageTypeID;
			}
			set
			{
				if (_AnnotatedPageTypeID != value)
				{
					_AnnotatedPageTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedPageTypeID
		
		#region PageNumber
		
		private string _PageNumber = string.Empty;
		
		/// <summary>
		/// Column: PageNumber;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("PageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=20)]
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
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotatedPage"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotatedPage"/>, 
		/// returns an instance of <see cref="__AnnotatedPage"/>; otherwise returns null.</returns>
		public static new __AnnotatedPage FromArray(byte[] byteArray)
		{
			__AnnotatedPage o = null;
			
			try
			{
				o = (__AnnotatedPage) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotatedPage"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotatedPage"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotatedPage)
			{
				__AnnotatedPage o = (__AnnotatedPage) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotatedPageID == AnnotatedPageID &&
					o.AnnotatedItemID == AnnotatedItemID &&
					o.PageID == PageID &&
					GetComparisonString(o.ExternalIdentifier) == GetComparisonString(ExternalIdentifier) &&
					o.AnnotatedPageTypeID == AnnotatedPageTypeID &&
					GetComparisonString(o.PageNumber) == GetComparisonString(PageNumber) &&
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
				throw new ArgumentException("Argument is not of type __AnnotatedPage");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotatedPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedPage"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotatedPage a, __AnnotatedPage b)
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
		/// <param name="a">The first <see cref="__AnnotatedPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedPage"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotatedPage a, __AnnotatedPage b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotatedPage"/> object to compare with the current <see cref="__AnnotatedPage"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotatedPage))
			{
				return false;
			}
			
			return this == (__AnnotatedPage) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotatedPage.SortColumn.AnnotatedPageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotatedPageID = "AnnotatedPageID";	
			public const string AnnotatedItemID = "AnnotatedItemID";	
			public const string PageID = "PageID";	
			public const string ExternalIdentifier = "ExternalIdentifier";	
			public const string AnnotatedPageTypeID = "AnnotatedPageTypeID";	
			public const string PageNumber = "PageNumber";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

