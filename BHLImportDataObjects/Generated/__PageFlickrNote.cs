
// Generated 5/5/2017 4:51:32 PM
// Do not modify the contents of this code file.
// This abstract class __PageFlickrNote is based upon dbo.PageFlickrNote.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class PageFlickrNote : __PageFlickrNote
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
	public abstract class __PageFlickrNote : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageFlickrNote()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageFlickrNoteID"></param>
		/// <param name="pageID"></param>
		/// <param name="photoID"></param>
		/// <param name="flickrNoteID"></param>
		/// <param name="flickrAuthorID"></param>
		/// <param name="flickrAuthorName"></param>
		/// <param name="flickrAuthorRealName"></param>
		/// <param name="authorIsPro"></param>
		/// <param name="xCoord"></param>
		/// <param name="yCoord"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="noteValue"></param>
		/// <param name="isActive"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="deleteDate"></param>
		public __PageFlickrNote(int pageFlickrNoteID, 
			int pageID, 
			string photoID, 
			string flickrNoteID, 
			string flickrAuthorID, 
			string flickrAuthorName, 
			string flickrAuthorRealName, 
			short authorIsPro, 
			int? xCoord, 
			int? yCoord, 
			int? width, 
			int? height, 
			string noteValue, 
			byte isActive, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			DateTime? deleteDate) : this()
		{
			_PageFlickrNoteID = pageFlickrNoteID;
			PageID = pageID;
			PhotoID = photoID;
			FlickrNoteID = flickrNoteID;
			FlickrAuthorID = flickrAuthorID;
			FlickrAuthorName = flickrAuthorName;
			FlickrAuthorRealName = flickrAuthorRealName;
			AuthorIsPro = authorIsPro;
			XCoord = xCoord;
			YCoord = yCoord;
			Width = width;
			Height = height;
			NoteValue = noteValue;
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
		~__PageFlickrNote()
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
					case "PageFlickrNoteID" :
					{
						_PageFlickrNoteID = (int)column.Value;
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
					case "FlickrNoteID" :
					{
						_FlickrNoteID = (string)column.Value;
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
					case "FlickrAuthorRealName" :
					{
						_FlickrAuthorRealName = (string)column.Value;
						break;
					}
					case "AuthorIsPro" :
					{
						_AuthorIsPro = (short)column.Value;
						break;
					}
					case "XCoord" :
					{
						_XCoord = (int?)column.Value;
						break;
					}
					case "YCoord" :
					{
						_YCoord = (int?)column.Value;
						break;
					}
					case "Width" :
					{
						_Width = (int?)column.Value;
						break;
					}
					case "Height" :
					{
						_Height = (int?)column.Value;
						break;
					}
					case "NoteValue" :
					{
						_NoteValue = (string)column.Value;
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
		
		#region PageFlickrNoteID
		
		private int _PageFlickrNoteID = default(int);
		
		/// <summary>
		/// Column: PageFlickrNoteID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageFlickrNoteID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PageFlickrNoteID
		{
			get
			{
				return _PageFlickrNoteID;
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
		
		#endregion PageFlickrNoteID
		
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
		
		#region FlickrNoteID
		
		private string _FlickrNoteID = string.Empty;
		
		/// <summary>
		/// Column: FlickrNoteID;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("FlickrNoteID", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=100)]
		public string FlickrNoteID
		{
			get
			{
				return _FlickrNoteID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_FlickrNoteID != value)
				{
					_FlickrNoteID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FlickrNoteID
		
		#region FlickrAuthorID
		
		private string _FlickrAuthorID = string.Empty;
		
		/// <summary>
		/// Column: FlickrAuthorID;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("FlickrAuthorID", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
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
		[ColumnDefinition("FlickrAuthorName", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=150)]
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
		
		#region FlickrAuthorRealName
		
		private string _FlickrAuthorRealName = string.Empty;
		
		/// <summary>
		/// Column: FlickrAuthorRealName;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("FlickrAuthorRealName", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=150)]
		public string FlickrAuthorRealName
		{
			get
			{
				return _FlickrAuthorRealName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_FlickrAuthorRealName != value)
				{
					_FlickrAuthorRealName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FlickrAuthorRealName
		
		#region AuthorIsPro
		
		private short _AuthorIsPro = default(short);
		
		/// <summary>
		/// Column: AuthorIsPro;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("AuthorIsPro", DbTargetType=SqlDbType.SmallInt, Ordinal=8, NumericPrecision=5)]
		public short AuthorIsPro
		{
			get
			{
				return _AuthorIsPro;
			}
			set
			{
				if (_AuthorIsPro != value)
				{
					_AuthorIsPro = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AuthorIsPro
		
		#region XCoord
		
		private int? _XCoord = null;
		
		/// <summary>
		/// Column: XCoord;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("XCoord", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
		public int? XCoord
		{
			get
			{
				return _XCoord;
			}
			set
			{
				if (_XCoord != value)
				{
					_XCoord = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion XCoord
		
		#region YCoord
		
		private int? _YCoord = null;
		
		/// <summary>
		/// Column: YCoord;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("YCoord", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10, IsNullable=true)]
		public int? YCoord
		{
			get
			{
				return _YCoord;
			}
			set
			{
				if (_YCoord != value)
				{
					_YCoord = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion YCoord
		
		#region Width
		
		private int? _Width = null;
		
		/// <summary>
		/// Column: Width;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("Width", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10, IsNullable=true)]
		public int? Width
		{
			get
			{
				return _Width;
			}
			set
			{
				if (_Width != value)
				{
					_Width = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Width
		
		#region Height
		
		private int? _Height = null;
		
		/// <summary>
		/// Column: Height;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("Height", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10, IsNullable=true)]
		public int? Height
		{
			get
			{
				return _Height;
			}
			set
			{
				if (_Height != value)
				{
					_Height = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Height
		
		#region NoteValue
		
		private string _NoteValue = string.Empty;
		
		/// <summary>
		/// Column: NoteValue;
		/// DBMS data type: nvarchar(1073741823);
		/// </summary>
		[ColumnDefinition("NoteValue", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=1073741823)]
		public string NoteValue
		{
			get
			{
				return _NoteValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_NoteValue != value)
				{
					_NoteValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteValue
		
		#region IsActive
		
		private byte _IsActive = default(byte);
		
		/// <summary>
		/// Column: IsActive;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("IsActive", DbTargetType=SqlDbType.TinyInt, Ordinal=14, NumericPrecision=3)]
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
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=15)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=16)]
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
		[ColumnDefinition("DeleteDate", DbTargetType=SqlDbType.DateTime, Ordinal=17, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__PageFlickrNote"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageFlickrNote"/>, 
		/// returns an instance of <see cref="__PageFlickrNote"/>; otherwise returns null.</returns>
		public static new __PageFlickrNote FromArray(byte[] byteArray)
		{
			__PageFlickrNote o = null;
			
			try
			{
				o = (__PageFlickrNote) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageFlickrNote"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageFlickrNote"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageFlickrNote)
			{
				__PageFlickrNote o = (__PageFlickrNote) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageFlickrNoteID == PageFlickrNoteID &&
					o.PageID == PageID &&
					GetComparisonString(o.PhotoID) == GetComparisonString(PhotoID) &&
					GetComparisonString(o.FlickrNoteID) == GetComparisonString(FlickrNoteID) &&
					GetComparisonString(o.FlickrAuthorID) == GetComparisonString(FlickrAuthorID) &&
					GetComparisonString(o.FlickrAuthorName) == GetComparisonString(FlickrAuthorName) &&
					GetComparisonString(o.FlickrAuthorRealName) == GetComparisonString(FlickrAuthorRealName) &&
					o.AuthorIsPro == AuthorIsPro &&
					o.XCoord == XCoord &&
					o.YCoord == YCoord &&
					o.Width == Width &&
					o.Height == Height &&
					GetComparisonString(o.NoteValue) == GetComparisonString(NoteValue) &&
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
				throw new ArgumentException("Argument is not of type __PageFlickrNote");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageFlickrNote"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageFlickrNote"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageFlickrNote a, __PageFlickrNote b)
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
		/// <param name="a">The first <see cref="__PageFlickrNote"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageFlickrNote"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageFlickrNote a, __PageFlickrNote b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageFlickrNote"/> object to compare with the current <see cref="__PageFlickrNote"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageFlickrNote))
			{
				return false;
			}
			
			return this == (__PageFlickrNote) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageFlickrNote.SortColumn.PageFlickrNoteID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageFlickrNoteID = "PageFlickrNoteID";	
			public const string PageID = "PageID";	
			public const string PhotoID = "PhotoID";	
			public const string FlickrNoteID = "FlickrNoteID";	
			public const string FlickrAuthorID = "FlickrAuthorID";	
			public const string FlickrAuthorName = "FlickrAuthorName";	
			public const string FlickrAuthorRealName = "FlickrAuthorRealName";	
			public const string AuthorIsPro = "AuthorIsPro";	
			public const string XCoord = "XCoord";	
			public const string YCoord = "YCoord";	
			public const string Width = "Width";	
			public const string Height = "Height";	
			public const string NoteValue = "NoteValue";	
			public const string IsActive = "IsActive";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string DeleteDate = "DeleteDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

