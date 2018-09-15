
// Generated 9/14/2018 11:09:07 AM
// Do not modify the contents of this code file.
// This abstract class __TextImportBatchFile is based upon txtimport.TextImportBatchFile.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TextImportBatchFile : __TextImportBatchFile
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
	public abstract class __TextImportBatchFile : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TextImportBatchFile()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="textImportBatchFileID"></param>
		/// <param name="textImportBatchID"></param>
		/// <param name="textImportBatchFileStatusID"></param>
		/// <param name="itemID"></param>
		/// <param name="filename"></param>
		/// <param name="fileFormat"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __TextImportBatchFile(int textImportBatchFileID, 
			int textImportBatchID, 
			int textImportBatchFileStatusID, 
			int? itemID, 
			string filename, 
			string fileFormat, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_TextImportBatchFileID = textImportBatchFileID;
			TextImportBatchID = textImportBatchID;
			TextImportBatchFileStatusID = textImportBatchFileStatusID;
			ItemID = itemID;
			Filename = filename;
			FileFormat = fileFormat;
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
		~__TextImportBatchFile()
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
					case "TextImportBatchFileID" :
					{
						_TextImportBatchFileID = (int)column.Value;
						break;
					}
					case "TextImportBatchID" :
					{
						_TextImportBatchID = (int)column.Value;
						break;
					}
					case "TextImportBatchFileStatusID" :
					{
						_TextImportBatchFileStatusID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int?)column.Value;
						break;
					}
					case "Filename" :
					{
						_Filename = (string)column.Value;
						break;
					}
					case "FileFormat" :
					{
						_FileFormat = (string)column.Value;
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
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region TextImportBatchFileID
		
		private int _TextImportBatchFileID = default(int);
		
		/// <summary>
		/// Column: TextImportBatchFileID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TextImportBatchFileID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int TextImportBatchFileID
		{
			get
			{
				return _TextImportBatchFileID;
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
		
		#endregion TextImportBatchFileID
		
		#region TextImportBatchID
		
		private int _TextImportBatchID = default(int);
		
		/// <summary>
		/// Column: TextImportBatchID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TextImportBatchID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int TextImportBatchID
		{
			get
			{
				return _TextImportBatchID;
			}
			set
			{
				if (_TextImportBatchID != value)
				{
					_TextImportBatchID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TextImportBatchID
		
		#region TextImportBatchFileStatusID
		
		private int _TextImportBatchFileStatusID = default(int);
		
		/// <summary>
		/// Column: TextImportBatchFileStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TextImportBatchFileStatusID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
		public int TextImportBatchFileStatusID
		{
			get
			{
				return _TextImportBatchFileStatusID;
			}
			set
			{
				if (_TextImportBatchFileStatusID != value)
				{
					_TextImportBatchFileStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TextImportBatchFileStatusID
		
		#region ItemID
		
		private int? _ItemID = null;
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? ItemID
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
		
		#region Filename
		
		private string _Filename = string.Empty;
		
		/// <summary>
		/// Column: Filename;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Filename", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=500)]
		public string Filename
		{
			get
			{
				return _Filename;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Filename != value)
				{
					_Filename = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Filename
		
		#region FileFormat
		
		private string _FileFormat = string.Empty;
		
		/// <summary>
		/// Column: FileFormat;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("FileFormat", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100)]
		public string FileFormat
		{
			get
			{
				return _FileFormat;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_FileFormat != value)
				{
					_FileFormat = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileFormat
		
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
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
		public int CreationUserID
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
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
		public int LastModifiedUserID
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
		/// Deserializes the byte array and returns an instance of <see cref="__TextImportBatchFile"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TextImportBatchFile"/>, 
		/// returns an instance of <see cref="__TextImportBatchFile"/>; otherwise returns null.</returns>
		public static new __TextImportBatchFile FromArray(byte[] byteArray)
		{
			__TextImportBatchFile o = null;
			
			try
			{
				o = (__TextImportBatchFile) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TextImportBatchFile"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TextImportBatchFile"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TextImportBatchFile)
			{
				__TextImportBatchFile o = (__TextImportBatchFile) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TextImportBatchFileID == TextImportBatchFileID &&
					o.TextImportBatchID == TextImportBatchID &&
					o.TextImportBatchFileStatusID == TextImportBatchFileStatusID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.Filename) == GetComparisonString(Filename) &&
					GetComparisonString(o.FileFormat) == GetComparisonString(FileFormat) &&
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
				throw new ArgumentException("Argument is not of type __TextImportBatchFile");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TextImportBatchFile"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TextImportBatchFile"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TextImportBatchFile a, __TextImportBatchFile b)
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
		/// <param name="a">The first <see cref="__TextImportBatchFile"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TextImportBatchFile"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TextImportBatchFile a, __TextImportBatchFile b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TextImportBatchFile"/> object to compare with the current <see cref="__TextImportBatchFile"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TextImportBatchFile))
			{
				return false;
			}
			
			return this == (__TextImportBatchFile) obj;
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
		/// list.Sort(SortOrder.Ascending, __TextImportBatchFile.SortColumn.TextImportBatchFileID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TextImportBatchFileID = "TextImportBatchFileID";	
			public const string TextImportBatchID = "TextImportBatchID";	
			public const string TextImportBatchFileStatusID = "TextImportBatchFileStatusID";	
			public const string ItemID = "ItemID";	
			public const string Filename = "Filename";	
			public const string FileFormat = "FileFormat";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

