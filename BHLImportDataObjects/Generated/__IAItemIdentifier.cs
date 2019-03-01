
// Generated 2/28/2019 2:07:31 PM
// Do not modify the contents of this code file.
// This abstract class __IAItemIdentifier is based upon dbo.IAItemIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAItemIdentifier : __IAItemIdentifier
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
	public abstract class __IAItemIdentifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAItemIdentifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="itemIdentifierID"></param>
		/// <param name="itemID"></param>
		/// <param name="identifierDescription"></param>
		/// <param name="identifierValue"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAItemIdentifier(int itemIdentifierID, 
			int itemID, 
			string identifierDescription, 
			string identifierValue, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_ItemIdentifierID = itemIdentifierID;
			ItemID = itemID;
			IdentifierDescription = identifierDescription;
			IdentifierValue = identifierValue;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAItemIdentifier()
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
					case "ItemIdentifierID" :
					{
						_ItemIdentifierID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "IdentifierDescription" :
					{
						_IdentifierDescription = (string)column.Value;
						break;
					}
					case "IdentifierValue" :
					{
						_IdentifierValue = (string)column.Value;
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
		
		#region ItemIdentifierID
		
		private int _ItemIdentifierID = default(int);
		
		/// <summary>
		/// Column: ItemIdentifierID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ItemIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ItemIdentifierID
		{
			get
			{
				return _ItemIdentifierID;
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
		
		#endregion ItemIdentifierID
		
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
		
		#region IdentifierDescription
		
		private string _IdentifierDescription = string.Empty;
		
		/// <summary>
		/// Column: IdentifierDescription;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("IdentifierDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string IdentifierDescription
		{
			get
			{
				return _IdentifierDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_IdentifierDescription != value)
				{
					_IdentifierDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierDescription
		
		#region IdentifierValue
		
		private string _IdentifierValue = string.Empty;
		
		/// <summary>
		/// Column: IdentifierValue;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("IdentifierValue", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=125)]
		public string IdentifierValue
		{
			get
			{
				return _IdentifierValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_IdentifierValue != value)
				{
					_IdentifierValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierValue
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAItemIdentifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAItemIdentifier"/>, 
		/// returns an instance of <see cref="__IAItemIdentifier"/>; otherwise returns null.</returns>
		public static new __IAItemIdentifier FromArray(byte[] byteArray)
		{
			__IAItemIdentifier o = null;
			
			try
			{
				o = (__IAItemIdentifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAItemIdentifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAItemIdentifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAItemIdentifier)
			{
				__IAItemIdentifier o = (__IAItemIdentifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ItemIdentifierID == ItemIdentifierID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.IdentifierDescription) == GetComparisonString(IdentifierDescription) &&
					GetComparisonString(o.IdentifierValue) == GetComparisonString(IdentifierValue) &&
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
				throw new ArgumentException("Argument is not of type __IAItemIdentifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAItemIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAItemIdentifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAItemIdentifier a, __IAItemIdentifier b)
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
		/// <param name="a">The first <see cref="__IAItemIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAItemIdentifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAItemIdentifier a, __IAItemIdentifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAItemIdentifier"/> object to compare with the current <see cref="__IAItemIdentifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAItemIdentifier))
			{
				return false;
			}
			
			return this == (__IAItemIdentifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAItemIdentifier.SortColumn.ItemIdentifierID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ItemIdentifierID = "ItemIdentifierID";	
			public const string ItemID = "ItemID";	
			public const string IdentifierDescription = "IdentifierDescription";	
			public const string IdentifierValue = "IdentifierValue";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

