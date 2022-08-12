
// Generated 1/5/2021 3:18:36 PM
// Do not modify the contents of this code file.
// This abstract class __APIKey is based upon dbo.APIKey.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class APIKey : __APIKey
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
	public abstract class __APIKey : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __APIKey()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="apiKeyID"></param>
		/// <param name="contactName"></param>
		/// <param name="emailAddress"></param>
		/// <param name="apiKeyValue"></param>
		/// <param name="isActive"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __APIKey(int apiKeyID, 
			string contactName, 
			string emailAddress, 
			Guid apiKeyValue, 
			byte isActive, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_ApiKeyID = apiKeyID;
			ContactName = contactName;
			EmailAddress = emailAddress;
			ApiKeyValue = apiKeyValue;
			IsActive = isActive;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__APIKey()
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
					case "ApiKeyID" :
					{
						_ApiKeyID = (int)column.Value;
						break;
					}
					case "ContactName" :
					{
						_ContactName = (string)column.Value;
						break;
					}
					case "EmailAddress" :
					{
						_EmailAddress = (string)column.Value;
						break;
					}
					case "ApiKeyValue" :
					{
						_ApiKeyValue = (Guid)column.Value;
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
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ApiKeyID
		
		private int _ApiKeyID = default(int);
		
		/// <summary>
		/// Column: ApiKeyID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ApiKeyID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ApiKeyID
		{
			get
			{
				return _ApiKeyID;
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
		
		#endregion ApiKeyID
		
		#region ContactName
		
		private string _ContactName = string.Empty;
		
		/// <summary>
		/// Column: ContactName;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("ContactName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=200)]
		public string ContactName
		{
			get
			{
				return _ContactName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_ContactName != value)
				{
					_ContactName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContactName
		
		#region EmailAddress
		
		private string _EmailAddress = string.Empty;
		
		/// <summary>
		/// Column: EmailAddress;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("EmailAddress", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=200)]
		public string EmailAddress
		{
			get
			{
				return _EmailAddress;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_EmailAddress != value)
				{
					_EmailAddress = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EmailAddress
		
		#region ApiKeyValue
		
		private Guid _ApiKeyValue = Guid.Empty;
		
		/// <summary>
		/// Column: ApiKeyValue;
		/// DBMS data type: uniqueidentifier;
		/// </summary>
		[ColumnDefinition("ApiKeyValue", DbTargetType=SqlDbType.UniqueIdentifier, Ordinal=4)]
		public Guid ApiKeyValue
		{
			get
			{
				return _ApiKeyValue;
			}
			set
			{
				if (_ApiKeyValue != value)
				{
					_ApiKeyValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ApiKeyValue
		
		#region IsActive
		
		private byte _IsActive = default(byte);
		
		/// <summary>
		/// Column: IsActive;
		/// DBMS data type: tinyint;
		/// </summary>
		[ColumnDefinition("IsActive", DbTargetType=SqlDbType.TinyInt, Ordinal=5, NumericPrecision=3)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__APIKey"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__APIKey"/>, 
		/// returns an instance of <see cref="__APIKey"/>; otherwise returns null.</returns>
		public static new __APIKey FromArray(byte[] byteArray)
		{
			__APIKey o = null;
			
			try
			{
				o = (__APIKey) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__APIKey"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__APIKey"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __APIKey)
			{
				__APIKey o = (__APIKey) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ApiKeyID == ApiKeyID &&
					GetComparisonString(o.ContactName) == GetComparisonString(ContactName) &&
					GetComparisonString(o.EmailAddress) == GetComparisonString(EmailAddress) &&
					o.ApiKeyValue == ApiKeyValue &&
					o.IsActive == IsActive &&
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
				throw new ArgumentException("Argument is not of type __APIKey");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__APIKey"/> object to compare.</param>
		/// <param name="b">The second <see cref="__APIKey"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__APIKey a, __APIKey b)
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
		/// <param name="a">The first <see cref="__APIKey"/> object to compare.</param>
		/// <param name="b">The second <see cref="__APIKey"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__APIKey a, __APIKey b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__APIKey"/> object to compare with the current <see cref="__APIKey"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __APIKey))
			{
				return false;
			}
			
			return this == (__APIKey) obj;
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
		/// list.Sort(SortOrder.Ascending, __APIKey.SortColumn.ApiKeyID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ApiKeyID = "ApiKeyID";	
			public const string ContactName = "ContactName";	
			public const string EmailAddress = "EmailAddress";	
			public const string ApiKeyValue = "ApiKeyValue";	
			public const string IsActive = "IsActive";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

