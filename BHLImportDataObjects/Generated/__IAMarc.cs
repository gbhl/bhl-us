
// Generated 1/15/2008 11:27:51 AM
// Do not modify the contents of this code file.
// This abstract class __IAMarc is based upon IAMarc.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAMarc : __IAMarc
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
	public abstract class __IAMarc : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAMarc()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcID"></param>
		/// <param name="itemID"></param>
		/// <param name="leader"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAMarc(int marcID, 
			int itemID, 
			string leader, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_MarcID = marcID;
			ItemID = itemID;
			Leader = leader;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAMarc()
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
					case "MarcID" :
					{
						_MarcID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "Leader" :
					{
						_Leader = (string)column.Value;
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
		
		#region MarcID
		
		private int _MarcID = default(int);
		
		/// <summary>
		/// Column: MarcID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MarcID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int MarcID
		{
			get
			{
				return _MarcID;
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
		
		#endregion MarcID
		
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
		
		#region Leader
		
		private string _Leader = string.Empty;
		
		/// <summary>
		/// Column: Leader;
		/// DBMS data type: varchar(200);
		/// </summary>
		[ColumnDefinition("Leader", DbTargetType=SqlDbType.VarChar, Ordinal=3, CharacterMaxLength=200)]
		public string Leader
		{
			get
			{
				return _Leader;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Leader != value)
				{
					_Leader = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Leader
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAMarc"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAMarc"/>, 
		/// returns an instance of <see cref="__IAMarc"/>; otherwise returns null.</returns>
		public static new __IAMarc FromArray(byte[] byteArray)
		{
			__IAMarc o = null;
			
			try
			{
				o = (__IAMarc) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAMarc"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAMarc"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAMarc)
			{
				__IAMarc o = (__IAMarc) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcID == MarcID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.Leader) == GetComparisonString(Leader) &&
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
				throw new ArgumentException("Argument is not of type __IAMarc");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAMarc"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarc"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAMarc a, __IAMarc b)
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
		/// <param name="a">The first <see cref="__IAMarc"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarc"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAMarc a, __IAMarc b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAMarc"/> object to compare with the current <see cref="__IAMarc"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAMarc))
			{
				return false;
			}
			
			return this == (__IAMarc) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAMarc.SortColumn.MarcID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcID = "MarcID";	
			public const string ItemID = "ItemID";	
			public const string Leader = "Leader";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
