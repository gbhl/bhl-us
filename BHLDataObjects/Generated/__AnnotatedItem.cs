
// Generated 7/14/2010 1:25:28 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotatedItem is based upon AnnotatedItem.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotatedItem : __AnnotatedItem
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
	public abstract class __AnnotatedItem : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotatedItem()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotatedItemID"></param>
		/// <param name="annotatedTitleID"></param>
		/// <param name="itemID"></param>
		/// <param name="externalIdentifier"></param>
		/// <param name="volume"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotatedItem(int annotatedItemID, 
			int annotatedTitleID, 
			int? itemID, 
			string externalIdentifier, 
			string volume, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotatedItemID = annotatedItemID;
			AnnotatedTitleID = annotatedTitleID;
			ItemID = itemID;
			ExternalIdentifier = externalIdentifier;
			Volume = volume;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotatedItem()
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
					case "AnnotatedItemID" :
					{
						_AnnotatedItemID = (int)column.Value;
						break;
					}
					case "AnnotatedTitleID" :
					{
						_AnnotatedTitleID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int?)column.Value;
						break;
					}
					case "ExternalIdentifier" :
					{
						_ExternalIdentifier = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
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
		
		#region AnnotatedItemID
		
		private int _AnnotatedItemID = default(int);
		
		/// <summary>
		/// Column: AnnotatedItemID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotatedItemID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotatedItemID
		{
			get
			{
				return _AnnotatedItemID;
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
		
		#endregion AnnotatedItemID
		
		#region AnnotatedTitleID
		
		private int _AnnotatedTitleID = default(int);
		
		/// <summary>
		/// Column: AnnotatedTitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("AnnotatedTitleID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int AnnotatedTitleID
		{
			get
			{
				return _AnnotatedTitleID;
			}
			set
			{
				if (_AnnotatedTitleID != value)
				{
					_AnnotatedTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedTitleID
		
		#region ItemID
		
		private int? _ItemID = null;
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsNullable=true)]
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
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=10)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotatedItem"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotatedItem"/>, 
		/// returns an instance of <see cref="__AnnotatedItem"/>; otherwise returns null.</returns>
		public static new __AnnotatedItem FromArray(byte[] byteArray)
		{
			__AnnotatedItem o = null;
			
			try
			{
				o = (__AnnotatedItem) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotatedItem"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotatedItem"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotatedItem)
			{
				__AnnotatedItem o = (__AnnotatedItem) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotatedItemID == AnnotatedItemID &&
					o.AnnotatedTitleID == AnnotatedTitleID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.ExternalIdentifier) == GetComparisonString(ExternalIdentifier) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
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
				throw new ArgumentException("Argument is not of type __AnnotatedItem");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotatedItem"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedItem"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotatedItem a, __AnnotatedItem b)
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
		/// <param name="a">The first <see cref="__AnnotatedItem"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedItem"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotatedItem a, __AnnotatedItem b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotatedItem"/> object to compare with the current <see cref="__AnnotatedItem"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotatedItem))
			{
				return false;
			}
			
			return this == (__AnnotatedItem) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotatedItem.SortColumn.AnnotatedItemID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotatedItemID = "AnnotatedItemID";	
			public const string AnnotatedTitleID = "AnnotatedTitleID";	
			public const string ItemID = "ItemID";	
			public const string ExternalIdentifier = "ExternalIdentifier";	
			public const string Volume = "Volume";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
