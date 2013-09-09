
// Generated 8/23/2010 3:08:23 PM
// Do not modify the contents of this code file.
// This abstract class __IAFile is based upon IAFile.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAFile : __IAFile
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
	public abstract class __IAFile : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAFile()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="fileID"></param>
		/// <param name="itemID"></param>
		/// <param name="remoteFileName"></param>
		/// <param name="localFileName"></param>
		/// <param name="source"></param>
		/// <param name="format"></param>
		/// <param name="original"></param>
		/// <param name="remoteFileLastModifiedDate"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAFile(int fileID, 
			int itemID, 
			string remoteFileName, 
			string localFileName, 
			string source, 
			string format, 
			string original, 
			DateTime? remoteFileLastModifiedDate, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_FileID = fileID;
			ItemID = itemID;
			RemoteFileName = remoteFileName;
			LocalFileName = localFileName;
			Source = source;
			Format = format;
			Original = original;
			RemoteFileLastModifiedDate = remoteFileLastModifiedDate;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAFile()
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
					case "FileID" :
					{
						_FileID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "RemoteFileName" :
					{
						_RemoteFileName = (string)column.Value;
						break;
					}
					case "LocalFileName" :
					{
						_LocalFileName = (string)column.Value;
						break;
					}
					case "Source" :
					{
						_Source = (string)column.Value;
						break;
					}
					case "Format" :
					{
						_Format = (string)column.Value;
						break;
					}
					case "Original" :
					{
						_Original = (string)column.Value;
						break;
					}
					case "RemoteFileLastModifiedDate" :
					{
						_RemoteFileLastModifiedDate = (DateTime?)column.Value;
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
		
		#region FileID
		
		private int _FileID = default(int);
		
		/// <summary>
		/// Column: FileID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("FileID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int FileID
		{
			get
			{
				return _FileID;
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
		
		#endregion FileID
		
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
		
		#region RemoteFileName
		
		private string _RemoteFileName = string.Empty;
		
		/// <summary>
		/// Column: RemoteFileName;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("RemoteFileName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string RemoteFileName
		{
			get
			{
				return _RemoteFileName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_RemoteFileName != value)
				{
					_RemoteFileName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RemoteFileName
		
		#region LocalFileName
		
		private string _LocalFileName = string.Empty;
		
		/// <summary>
		/// Column: LocalFileName;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("LocalFileName", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=100)]
		public string LocalFileName
		{
			get
			{
				return _LocalFileName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_LocalFileName != value)
				{
					_LocalFileName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LocalFileName
		
		#region Source
		
		private string _Source = string.Empty;
		
		/// <summary>
		/// Column: Source;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Source", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=20)]
		public string Source
		{
			get
			{
				return _Source;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Source != value)
				{
					_Source = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Source
		
		#region Format
		
		private string _Format = string.Empty;
		
		/// <summary>
		/// Column: Format;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Format", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=50)]
		public string Format
		{
			get
			{
				return _Format;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Format != value)
				{
					_Format = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Format
		
		#region Original
		
		private string _Original = string.Empty;
		
		/// <summary>
		/// Column: Original;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Original", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=50)]
		public string Original
		{
			get
			{
				return _Original;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Original != value)
				{
					_Original = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Original
		
		#region RemoteFileLastModifiedDate
		
		private DateTime? _RemoteFileLastModifiedDate = null;
		
		/// <summary>
		/// Column: RemoteFileLastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("RemoteFileLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8, IsNullable=true)]
		public DateTime? RemoteFileLastModifiedDate
		{
			get
			{
				return _RemoteFileLastModifiedDate;
			}
			set
			{
				if (_RemoteFileLastModifiedDate != value)
				{
					_RemoteFileLastModifiedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RemoteFileLastModifiedDate
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__IAFile"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAFile"/>, 
		/// returns an instance of <see cref="__IAFile"/>; otherwise returns null.</returns>
		public static new __IAFile FromArray(byte[] byteArray)
		{
			__IAFile o = null;
			
			try
			{
				o = (__IAFile) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAFile"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAFile"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAFile)
			{
				__IAFile o = (__IAFile) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.FileID == FileID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.RemoteFileName) == GetComparisonString(RemoteFileName) &&
					GetComparisonString(o.LocalFileName) == GetComparisonString(LocalFileName) &&
					GetComparisonString(o.Source) == GetComparisonString(Source) &&
					GetComparisonString(o.Format) == GetComparisonString(Format) &&
					GetComparisonString(o.Original) == GetComparisonString(Original) &&
					o.RemoteFileLastModifiedDate == RemoteFileLastModifiedDate &&
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
				throw new ArgumentException("Argument is not of type __IAFile");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAFile"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAFile"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAFile a, __IAFile b)
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
		/// <param name="a">The first <see cref="__IAFile"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAFile"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAFile a, __IAFile b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAFile"/> object to compare with the current <see cref="__IAFile"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAFile))
			{
				return false;
			}
			
			return this == (__IAFile) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAFile.SortColumn.FileID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string FileID = "FileID";	
			public const string ItemID = "ItemID";	
			public const string RemoteFileName = "RemoteFileName";	
			public const string LocalFileName = "LocalFileName";	
			public const string Source = "Source";	
			public const string Format = "Format";	
			public const string Original = "Original";	
			public const string RemoteFileLastModifiedDate = "RemoteFileLastModifiedDate";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
