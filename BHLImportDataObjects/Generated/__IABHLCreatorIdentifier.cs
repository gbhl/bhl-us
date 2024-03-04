
// Generated 3/4/2024 11:54:37 AM
// Do not modify the contents of this code file.
// This abstract class __IABHLCreatorIdentifier is based upon dbo.IABHLCreatorIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IABHLCreatorIdentifier : __IABHLCreatorIdentifier
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
	public abstract class __IABHLCreatorIdentifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IABHLCreatorIdentifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="bHLCreatorIdentifierID"></param>
		/// <param name="bHLCreatorID"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IABHLCreatorIdentifier(int bHLCreatorIdentifierID, 
			int bHLCreatorID, 
			string type, 
			string value, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_BHLCreatorIdentifierID = bHLCreatorIdentifierID;
			BHLCreatorID = bHLCreatorID;
			Type = type;
			Value = value;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IABHLCreatorIdentifier()
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
					case "BHLCreatorIdentifierID" :
					{
						_BHLCreatorIdentifierID = (int)column.Value;
						break;
					}
					case "BHLCreatorID" :
					{
						_BHLCreatorID = (int)column.Value;
						break;
					}
					case "Type" :
					{
						_Type = (string)column.Value;
						break;
					}
					case "Value" :
					{
						_Value = (string)column.Value;
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
		
		#region BHLCreatorIdentifierID
		
		private int _BHLCreatorIdentifierID = default(int);
		
		/// <summary>
		/// Column: BHLCreatorIdentifierID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("BHLCreatorIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int BHLCreatorIdentifierID
		{
			get
			{
				return _BHLCreatorIdentifierID;
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
		
		#endregion BHLCreatorIdentifierID
		
		#region BHLCreatorID
		
		private int _BHLCreatorID = default(int);
		
		/// <summary>
		/// Column: BHLCreatorID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("BHLCreatorID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int BHLCreatorID
		{
			get
			{
				return _BHLCreatorID;
			}
			set
			{
				if (_BHLCreatorID != value)
				{
					_BHLCreatorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLCreatorID
		
		#region Type
		
		private string _Type = string.Empty;
		
		/// <summary>
		/// Column: Type;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("Type", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40)]
		public string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_Type != value)
				{
					_Type = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Type
		
		#region Value
		
		private string _Value = string.Empty;
		
		/// <summary>
		/// Column: Value;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("Value", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=125)]
		public string Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_Value != value)
				{
					_Value = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Value
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__IABHLCreatorIdentifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IABHLCreatorIdentifier"/>, 
		/// returns an instance of <see cref="__IABHLCreatorIdentifier"/>; otherwise returns null.</returns>
		public static new __IABHLCreatorIdentifier FromArray(byte[] byteArray)
		{
			__IABHLCreatorIdentifier o = null;
			
			try
			{
				o = (__IABHLCreatorIdentifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IABHLCreatorIdentifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IABHLCreatorIdentifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IABHLCreatorIdentifier)
			{
				__IABHLCreatorIdentifier o = (__IABHLCreatorIdentifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.BHLCreatorIdentifierID == BHLCreatorIdentifierID &&
					o.BHLCreatorID == BHLCreatorID &&
					GetComparisonString(o.Type) == GetComparisonString(Type) &&
					GetComparisonString(o.Value) == GetComparisonString(Value) &&
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
				throw new ArgumentException("Argument is not of type __IABHLCreatorIdentifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IABHLCreatorIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IABHLCreatorIdentifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IABHLCreatorIdentifier a, __IABHLCreatorIdentifier b)
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
		/// <param name="a">The first <see cref="__IABHLCreatorIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IABHLCreatorIdentifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IABHLCreatorIdentifier a, __IABHLCreatorIdentifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IABHLCreatorIdentifier"/> object to compare with the current <see cref="__IABHLCreatorIdentifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IABHLCreatorIdentifier))
			{
				return false;
			}
			
			return this == (__IABHLCreatorIdentifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __IABHLCreatorIdentifier.SortColumn.BHLCreatorIdentifierID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string BHLCreatorIdentifierID = "BHLCreatorIdentifierID";	
			public const string BHLCreatorID = "BHLCreatorID";	
			public const string Type = "Type";	
			public const string Value = "Value";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

