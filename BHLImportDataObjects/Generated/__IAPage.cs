
// Generated 1/15/2008 11:27:51 AM
// Do not modify the contents of this code file.
// This abstract class __IAPage is based upon IAPage.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAPage : __IAPage
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
	public abstract class __IAPage : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAPage()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="pageID"></param>
		/// <param name="itemID"></param>
		/// <param name="localFileName"></param>
		/// <param name="sequence"></param>
		/// <param name="externalUrl"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAPage(int pageID, 
			int itemID, 
			string localFileName, 
			int? sequence, 
			string externalUrl, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_PageID = pageID;
			ItemID = itemID;
			LocalFileName = localFileName;
			Sequence = sequence;
			ExternalUrl = externalUrl;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAPage()
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
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "LocalFileName" :
					{
						_LocalFileName = (string)column.Value;
						break;
					}
					case "Sequence" :
					{
						_Sequence = (int?)column.Value;
						break;
					}
					case "ExternalUrl" :
					{
						_ExternalUrl = (string)column.Value;
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
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int PageID
		{
			get
			{
				return _PageID;
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
		
		#endregion PageID
		
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
		
		#region LocalFileName
		
		private string _LocalFileName = string.Empty;
		
		/// <summary>
		/// Column: LocalFileName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("LocalFileName", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string LocalFileName
		{
			get
			{
				return _LocalFileName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_LocalFileName != value)
				{
					_LocalFileName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LocalFileName
		
		#region Sequence
		
		private int? _Sequence = null;
		
		/// <summary>
		/// Column: Sequence;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("Sequence", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? Sequence
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
		
		#region ExternalUrl
		
		private string _ExternalUrl = null;
		
		/// <summary>
		/// Column: ExternalUrl;
		/// DBMS data type: nvarchar(500); Nullable;
		/// </summary>
		[ColumnDefinition("ExternalUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=500, IsNullable=true)]
		public string ExternalUrl
		{
			get
			{
				return _ExternalUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_ExternalUrl != value)
				{
					_ExternalUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalUrl
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAPage"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAPage"/>, 
		/// returns an instance of <see cref="__IAPage"/>; otherwise returns null.</returns>
		public static new __IAPage FromArray(byte[] byteArray)
		{
			__IAPage o = null;
			
			try
			{
				o = (__IAPage) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAPage"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAPage"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAPage)
			{
				__IAPage o = (__IAPage) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.PageID == PageID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.LocalFileName) == GetComparisonString(LocalFileName) &&
					o.Sequence == Sequence &&
					GetComparisonString(o.ExternalUrl) == GetComparisonString(ExternalUrl) &&
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
				throw new ArgumentException("Argument is not of type __IAPage");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAPage"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAPage a, __IAPage b)
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
		/// <param name="a">The first <see cref="__IAPage"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAPage"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAPage a, __IAPage b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAPage"/> object to compare with the current <see cref="__IAPage"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAPage))
			{
				return false;
			}
			
			return this == (__IAPage) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAPage.SortColumn.PageID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string PageID = "PageID";	
			public const string ItemID = "ItemID";	
			public const string LocalFileName = "LocalFileName";	
			public const string Sequence = "Sequence";	
			public const string ExternalUrl = "ExternalUrl";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
