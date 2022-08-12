
// Generated 1/5/2021 2:18:05 PM
// Do not modify the contents of this code file.
// This abstract class __PageFlickrTag is based upon dbo.PageFlickrTag.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class PageFlickrTag : __PageFlickrTag
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
	public abstract class __PageFlickrTag : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageFlickrTag()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageFlickrTagID"></param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="isMachineTag"></param>
		/// <param name="tagValue"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="isActive"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="deleteDate"></param>
		public __PageFlickrTag(int pageFlickrTagID, 
			int pageID, 
			string photoID, 
			short isMachineTag, 
			string tagValue, 
			string flickrAuthorID, 
			string flickrAuthorName, 
			byte isActive, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			DateTime? deleteDate) : this()
		{
			_PageFlickrTagID = pageFlickrTagID;
			PageID = pageID;
			PhotoID = photoID;
			IsMachineTag = isMachineTag;
			TagValue = tagValue;
			FlickrAuthorID = flickrAuthorID;
			FlickrAuthorName = flickrAuthorName;
			IsActive = isActive;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			DeleteDate = deleteDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PageFlickrTag()
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
					case "PageFlickrTagID" :
					{
						_PageFlickrTagID = (int)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
					case "PhotoID" :
					{
						_PhotoID = (string)column.Value;
						break;
					}
					case "IsMachineTag" :
					{
						_IsMachineTag = (short)column.Value;
						break;
					}
					case "TagValue" :
					{
						_TagValue = (string)column.Value;
						break;
					}
					case "FlickrAuthorID" :
					{
						_FlickrAuthorID = (string)column.Value;
						break;
					}
					case "FlickrAuthorName" :
					{
						_FlickrAuthorName = (string)column.Value;
						break;
					}
					case "IsActive" :
					{
						_IsActive = (byte)column.Value;
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
					case "DeleteDate" :
					{
						_DeleteDate = (DateTime?)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region PageFlickrTagID
		
		private int _PageFlickrTagID = default(int);
		
		/// <summary>
		/// Column: PageFlickrTagID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageFlickrTagID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PageFlickrTagID
		{
			get
			{
				return _PageFlickrTagID;
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
		
		#endregion PageFlickrTagID
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
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
		
		#region PhotoID
		
		private string _PhotoID = string.Empty;
		
		/// <summary>
		/// Column: PhotoID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("PhotoID", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string PhotoID
		{
			get
			{
				return _PhotoID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_PhotoID != value)
				{
					_PhotoID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PhotoID
		
		#region IsMachineTag
		
		private short _IsMachineTag = default(short);
		
		/// <summary>
		/// Column: IsMachineTag;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("IsMachineTag", DbTargetType=SqlDbType.SmallInt, Ordinal=4, NumericPrecision=5)]
		public short IsMachineTag
		{
			get
			{
				return _IsMachineTag;
			}
			set
			{
				if (_IsMachineTag != value)
				{
					_IsMachineTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsMachineTag
		
		#region TagValue
		
		private string _TagValue = string.Empty;
		
		/// <summary>
		/// Column: TagValue;
		/// DBMS data type: nvarchar(1000);
		/// </summary>
		[ColumnDefinition("TagValue", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1000)]
		public string TagValue
		{
			get
			{
				return _TagValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1000);
				if (_TagValue != value)
				{
					_TagValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TagValue
		
		#region FlickrAuthorID
		
		private string _FlickrAuthorID = string.Empty;
		
		/// <summary>
		/// Column: FlickrAuthorID;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("FlickrAuthorID", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100)]
		public string FlickrAuthorID
		{
			get
			{
				return _FlickrAuthorID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_FlickrAuthorID != value)
				{
					_FlickrAuthorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FlickrAuthorID
		
		#region FlickrAuthorName
		
		private string _FlickrAuthorName = string.Empty;
		
		/// <summary>
		/// Column: FlickrAuthorName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("FlickrAuthorName", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=150)]
		public string FlickrAuthorName
		{
			get
			{
				return _FlickrAuthorName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_FlickrAuthorName != value)
				{
					_FlickrAuthorName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FlickrAuthorName
		
		#region IsActive
		
		private byte _IsActive = default(byte);
		
		/// <summary>
		/// Column: IsActive;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("IsActive", DbTargetType=SqlDbType.TinyInt, Ordinal=8, NumericPrecision=3)]
		public byte IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				if (_IsActive != value)
				{
					_IsActive = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IsActive
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=10)]
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
		
		#region DeleteDate
		
		private DateTime? _DeleteDate = null;
		
		/// <summary>
		/// Column: DeleteDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("DeleteDate", DbTargetType=SqlDbType.DateTime, Ordinal=11, IsNullable=true)]
		public DateTime? DeleteDate
		{
			get
			{
				return _DeleteDate;
			}
			set
			{
				if (_DeleteDate != value)
				{
					_DeleteDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DeleteDate
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PageFlickrTag"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageFlickrTag"/>, 
		/// returns an instance of <see cref="__PageFlickrTag"/>; otherwise returns null.</returns>
		public static new __PageFlickrTag FromArray(byte[] byteArray)
		{
			__PageFlickrTag o = null;
			
			try
			{
				o = (__PageFlickrTag) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageFlickrTag"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageFlickrTag"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageFlickrTag)
			{
				__PageFlickrTag o = (__PageFlickrTag) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageFlickrTagID == PageFlickrTagID &&
					o.PageID == PageID &&
					GetComparisonString(o.PhotoID) == GetComparisonString(PhotoID) &&
					o.IsMachineTag == IsMachineTag &&
					GetComparisonString(o.TagValue) == GetComparisonString(TagValue) &&
					GetComparisonString(o.FlickrAuthorID) == GetComparisonString(FlickrAuthorID) &&
					GetComparisonString(o.FlickrAuthorName) == GetComparisonString(FlickrAuthorName) &&
					o.IsActive == IsActive &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.DeleteDate == DeleteDate 
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
				throw new ArgumentException("Argument is not of type __PageFlickrTag");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageFlickrTag"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageFlickrTag"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageFlickrTag a, __PageFlickrTag b)
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
		/// <param name="a">The first <see cref="__PageFlickrTag"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageFlickrTag"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageFlickrTag a, __PageFlickrTag b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageFlickrTag"/> object to compare with the current <see cref="__PageFlickrTag"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageFlickrTag))
			{
				return false;
			}
			
			return this == (__PageFlickrTag) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageFlickrTag.SortColumn.PageFlickrTagID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageFlickrTagID = "PageFlickrTagID";	
			public const string PageID = "PageID";	
			public const string PhotoID = "PhotoID";	
			public const string IsMachineTag = "IsMachineTag";	
			public const string TagValue = "TagValue";	
			public const string FlickrAuthorID = "FlickrAuthorID";	
			public const string FlickrAuthorName = "FlickrAuthorName";	
			public const string IsActive = "IsActive";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string DeleteDate = "DeleteDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

