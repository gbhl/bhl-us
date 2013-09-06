
// Generated 1/24/2008 10:03:58 AM
// Do not modify the contents of this code file.
// This abstract class __Vault is based upon Vault.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Vault : __Vault
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
	public abstract class __Vault : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Vault()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="vaultID"></param>
		/// <param name="server"></param>
		/// <param name="folderShare"></param>
		/// <param name="webVirtualDirectory"></param>
		/// <param name="oCRFolderShare"></param>
		public __Vault(int vaultID, 
			string server, 
			string folderShare, 
			string webVirtualDirectory, 
			string oCRFolderShare) : this()
		{
			VaultID = vaultID;
			Server = server;
			FolderShare = folderShare;
			WebVirtualDirectory = webVirtualDirectory;
			OCRFolderShare = oCRFolderShare;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__Vault()
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
					case "VaultID" :
					{
						_VaultID = (int)column.Value;
						break;
					}
					case "Server" :
					{
						_Server = (string)column.Value;
						break;
					}
					case "FolderShare" :
					{
						_FolderShare = (string)column.Value;
						break;
					}
					case "WebVirtualDirectory" :
					{
						_WebVirtualDirectory = (string)column.Value;
						break;
					}
					case "OCRFolderShare" :
					{
						_OCRFolderShare = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region VaultID
		
		private int _VaultID = default(int);
		
		/// <summary>
		/// Column: VaultID;
		/// DBMS data type: int;
		/// Description: Unique identifier for each Vault entry.
		/// </summary>
		[ColumnDefinition("VaultID", DbTargetType=SqlDbType.Int, Ordinal=1, Description="Unique identifier for each Vault entry.", NumericPrecision=10, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int VaultID
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
		
		#region Server
		
		private string _Server = null;
		
		/// <summary>
		/// Column: Server;
		/// DBMS data type: nvarchar(30); Nullable;
		/// Description: Name of server for this Vault entry.
		/// </summary>
		[ColumnDefinition("Server", DbTargetType=SqlDbType.NVarChar, Ordinal=2, Description="Name of server for this Vault entry.", CharacterMaxLength=30, IsNullable=true)]
		public string Server
		{
			get
			{
				return _Server;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_Server != value)
				{
					_Server = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Server
		
		#region FolderShare
		
		private string _FolderShare = null;
		
		/// <summary>
		/// Column: FolderShare;
		/// DBMS data type: nvarchar(30); Nullable;
		/// Description: Name for the folder share for this Vault entry.
		/// </summary>
		[ColumnDefinition("FolderShare", DbTargetType=SqlDbType.NVarChar, Ordinal=3, Description="Name for the folder share for this Vault entry.", CharacterMaxLength=30, IsNullable=true)]
		public string FolderShare
		{
			get
			{
				return _FolderShare;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_FolderShare != value)
				{
					_FolderShare = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FolderShare
		
		#region WebVirtualDirectory
		
		private string _WebVirtualDirectory = null;
		
		/// <summary>
		/// Column: WebVirtualDirectory;
		/// DBMS data type: nvarchar(30); Nullable;
		/// Description: Name for the Web Virtual Directory for this Vault entry.
		/// </summary>
		[ColumnDefinition("WebVirtualDirectory", DbTargetType=SqlDbType.NVarChar, Ordinal=4, Description="Name for the Web Virtual Directory for this Vault entry.", CharacterMaxLength=30, IsNullable=true)]
		public string WebVirtualDirectory
		{
			get
			{
				return _WebVirtualDirectory;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_WebVirtualDirectory != value)
				{
					_WebVirtualDirectory = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion WebVirtualDirectory
		
		#region OCRFolderShare
		
		private string _OCRFolderShare = null;
		
		/// <summary>
		/// Column: OCRFolderShare;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("OCRFolderShare", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100, IsNullable=true)]
		public string OCRFolderShare
		{
			get
			{
				return _OCRFolderShare;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_OCRFolderShare != value)
				{
					_OCRFolderShare = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OCRFolderShare
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__Vault"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Vault"/>, 
		/// returns an instance of <see cref="__Vault"/>; otherwise returns null.</returns>
		public static new __Vault FromArray(byte[] byteArray)
		{
			__Vault o = null;
			
			try
			{
				o = (__Vault) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Vault"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Vault"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Vault)
			{
				__Vault o = (__Vault) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.VaultID == VaultID &&
					GetComparisonString(o.Server) == GetComparisonString(Server) &&
					GetComparisonString(o.FolderShare) == GetComparisonString(FolderShare) &&
					GetComparisonString(o.WebVirtualDirectory) == GetComparisonString(WebVirtualDirectory) &&
					GetComparisonString(o.OCRFolderShare) == GetComparisonString(OCRFolderShare) 
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
				throw new ArgumentException("Argument is not of type __Vault");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Vault"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Vault"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Vault a, __Vault b)
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
		/// <param name="a">The first <see cref="__Vault"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Vault"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Vault a, __Vault b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Vault"/> object to compare with the current <see cref="__Vault"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Vault))
			{
				return false;
			}
			
			return this == (__Vault) obj;
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
		/// list.Sort(SortOrder.Ascending, __Vault.SortColumn.VaultID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string VaultID = "VaultID";	
			public const string Server = "Server";	
			public const string FolderShare = "FolderShare";	
			public const string WebVirtualDirectory = "WebVirtualDirectory";	
			public const string OCRFolderShare = "OCRFolderShare";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
