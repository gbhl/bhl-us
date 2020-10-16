
// Generated 10/19/2020 1:12:47 PM
// Do not modify the contents of this code file.
// This abstract class __Item is based upon dbo.Item.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Item : __Item
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
	public abstract class __Item : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Item()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="itemTypeID"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemSourceID"></param>
		/// <param name="vaultID"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="itemDescription"></param>
		/// <param name="note"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __Item(int itemID, 
			int? itemTypeID, 
			int itemStatusID, 
			int? itemSourceID, 
			int? vaultID, 
			string fileRootFolder, 
			string itemDescription, 
			string note, 
			DateTime? creationDate, 
			DateTime? lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_ItemID = itemID;
			ItemTypeID = itemTypeID;
			ItemStatusID = itemStatusID;
			ItemSourceID = itemSourceID;
			VaultID = vaultID;
			FileRootFolder = fileRootFolder;
			ItemDescription = itemDescription;
			Note = note;
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
		~__Item()
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
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "ItemTypeID" :
					{
						_ItemTypeID = (int?)column.Value;
						break;
					}
					case "ItemStatusID" :
					{
						_ItemStatusID = (int)column.Value;
						break;
					}
					case "ItemSourceID" :
					{
						_ItemSourceID = (int?)column.Value;
						break;
					}
					case "VaultID" :
					{
						_VaultID = (int?)column.Value;
						break;
					}
					case "FileRootFolder" :
					{
						_FileRootFolder = (string)column.Value;
						break;
					}
					case "ItemDescription" :
					{
						_ItemDescription = (string)column.Value;
						break;
					}
					case "Note" :
					{
						_Note = (string)column.Value;
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
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
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
		
		#endregion ItemID
		
		#region ItemTypeID
		
		private int? _ItemTypeID = null;
		
		/// <summary>
		/// Column: ItemTypeID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ItemTypeID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? ItemTypeID
		{
			get
			{
				return _ItemTypeID;
			}
			set
			{
				if (_ItemTypeID != value)
				{
					_ItemTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemTypeID
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true)]
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
		
		#region ItemSourceID
		
		private int? _ItemSourceID = null;
		
		/// <summary>
		/// Column: ItemSourceID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ItemSourceID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? ItemSourceID
		{
			get
			{
				return _ItemSourceID;
			}
			set
			{
				if (_ItemSourceID != value)
				{
					_ItemSourceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemSourceID
		
		#region VaultID
		
		private int? _VaultID = null;
		
		/// <summary>
		/// Column: VaultID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("VaultID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? VaultID
		{
			get
			{
				return _VaultID;
			}
			set
			{
				if (_VaultID != value)
				{
					_VaultID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion VaultID
		
		#region FileRootFolder
		
		private string _FileRootFolder = null;
		
		/// <summary>
		/// Column: FileRootFolder;
		/// DBMS data type: nvarchar(250); Nullable;
		/// </summary>
		[ColumnDefinition("FileRootFolder", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=250, IsNullable=true)]
		public string FileRootFolder
		{
			get
			{
				return _FileRootFolder;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 250);
				if (_FileRootFolder != value)
				{
					_FileRootFolder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileRootFolder
		
		#region ItemDescription
		
		private string _ItemDescription = null;
		
		/// <summary>
		/// Column: ItemDescription;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("ItemDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=1073741823, IsNullable=true)]
		public string ItemDescription
		{
			get
			{
				return _ItemDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ItemDescription != value)
				{
					_ItemDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemDescription
		
		#region Note
		
		private string _Note = null;
		
		/// <summary>
		/// Column: Note;
		/// DBMS data type: nvarchar(MAX); Nullable;
		/// </summary>
		[ColumnDefinition("Note", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=1073741823, IsNullable=true)]
		public string Note
		{
			get
			{
				return _Note;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Note != value)
				{
					_Note = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Note
		
		#region CreationDate
		
		private DateTime? _CreationDate = null;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=9, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=10, IsNullable=true)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Item"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Item"/>, 
		/// returns an instance of <see cref="__Item"/>; otherwise returns null.</returns>
		public static new __Item FromArray(byte[] byteArray)
		{
			__Item o = null;
			
			try
			{
				o = (__Item) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Item"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Item"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Item)
			{
				__Item o = (__Item) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemID == ItemID &&
					o.ItemTypeID == ItemTypeID &&
					o.ItemStatusID == ItemStatusID &&
					o.ItemSourceID == ItemSourceID &&
					o.VaultID == VaultID &&
					GetComparisonString(o.FileRootFolder) == GetComparisonString(FileRootFolder) &&
					GetComparisonString(o.ItemDescription) == GetComparisonString(ItemDescription) &&
					GetComparisonString(o.Note) == GetComparisonString(Note) &&
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
				throw new ArgumentException("Argument is not of type __Item");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Item"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Item"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Item a, __Item b)
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
		/// <param name="a">The first <see cref="__Item"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Item"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Item a, __Item b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Item"/> object to compare with the current <see cref="__Item"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Item))
			{
				return false;
			}
			
			return this == (__Item) obj;
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
		/// list.Sort(SortOrder.Ascending, __Item.SortColumn.ItemID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemID = "ItemID";	
			public const string ItemTypeID = "ItemTypeID";	
			public const string ItemStatusID = "ItemStatusID";	
			public const string ItemSourceID = "ItemSourceID";	
			public const string VaultID = "VaultID";	
			public const string FileRootFolder = "FileRootFolder";	
			public const string ItemDescription = "ItemDescription";	
			public const string Note = "Note";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

