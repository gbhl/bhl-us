
// Generated 6/24/2022 2:30:17 PM
// Do not modify the contents of this code file.
// This abstract class __OAIRecordCreatorIdentifier is based upon dbo.OAIRecordCreatorIdentifier.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class OAIRecordCreatorIdentifier : __OAIRecordCreatorIdentifier
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
	public abstract class __OAIRecordCreatorIdentifier : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __OAIRecordCreatorIdentifier()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="oAIRecordCreatorIdentifierID"></param>
		/// <param name="oAIRecordCreatorID"></param>
		/// <param name="identifierType"></param>
		/// <param name="identifierValue"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __OAIRecordCreatorIdentifier(int oAIRecordCreatorIdentifierID, 
			int oAIRecordCreatorID, 
			string identifierType, 
			string identifierValue, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_OAIRecordCreatorIdentifierID = oAIRecordCreatorIdentifierID;
			OAIRecordCreatorID = oAIRecordCreatorID;
			IdentifierType = identifierType;
			IdentifierValue = identifierValue;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__OAIRecordCreatorIdentifier()
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
					case "OAIRecordCreatorIdentifierID" :
					{
						_OAIRecordCreatorIdentifierID = (int)column.Value;
						break;
					}
					case "OAIRecordCreatorID" :
					{
						_OAIRecordCreatorID = (int)column.Value;
						break;
					}
					case "IdentifierType" :
					{
						_IdentifierType = (string)column.Value;
						break;
					}
					case "IdentifierValue" :
					{
						_IdentifierValue = (string)column.Value;
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
		
		#region OAIRecordCreatorIdentifierID
		
		private int _OAIRecordCreatorIdentifierID = default(int);
		
		/// <summary>
		/// Column: OAIRecordCreatorIdentifierID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("OAIRecordCreatorIdentifierID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int OAIRecordCreatorIdentifierID
		{
			get
			{
				return _OAIRecordCreatorIdentifierID;
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
		
		#endregion OAIRecordCreatorIdentifierID
		
		#region OAIRecordCreatorID
		
		private int _OAIRecordCreatorID = default(int);
		
		/// <summary>
		/// Column: OAIRecordCreatorID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("OAIRecordCreatorID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int OAIRecordCreatorID
		{
			get
			{
				return _OAIRecordCreatorID;
			}
			set
			{
				if (_OAIRecordCreatorID != value)
				{
					_OAIRecordCreatorID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OAIRecordCreatorID
		
		#region IdentifierType
		
		private string _IdentifierType = string.Empty;
		
		/// <summary>
		/// Column: IdentifierType;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("IdentifierType", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40)]
		public string IdentifierType
		{
			get
			{
				return _IdentifierType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_IdentifierType != value)
				{
					_IdentifierType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IdentifierType
		
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
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__OAIRecordCreatorIdentifier"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__OAIRecordCreatorIdentifier"/>, 
		/// returns an instance of <see cref="__OAIRecordCreatorIdentifier"/>; otherwise returns null.</returns>
		public static new __OAIRecordCreatorIdentifier FromArray(byte[] byteArray)
		{
			__OAIRecordCreatorIdentifier o = null;
			
			try
			{
				o = (__OAIRecordCreatorIdentifier) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__OAIRecordCreatorIdentifier"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__OAIRecordCreatorIdentifier"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __OAIRecordCreatorIdentifier)
			{
				__OAIRecordCreatorIdentifier o = (__OAIRecordCreatorIdentifier) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.OAIRecordCreatorIdentifierID == OAIRecordCreatorIdentifierID &&
					o.OAIRecordCreatorID == OAIRecordCreatorID &&
					GetComparisonString(o.IdentifierType) == GetComparisonString(IdentifierType) &&
					GetComparisonString(o.IdentifierValue) == GetComparisonString(IdentifierValue) &&
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
				throw new ArgumentException("Argument is not of type __OAIRecordCreatorIdentifier");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__OAIRecordCreatorIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordCreatorIdentifier"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__OAIRecordCreatorIdentifier a, __OAIRecordCreatorIdentifier b)
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
		/// <param name="a">The first <see cref="__OAIRecordCreatorIdentifier"/> object to compare.</param>
		/// <param name="b">The second <see cref="__OAIRecordCreatorIdentifier"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__OAIRecordCreatorIdentifier a, __OAIRecordCreatorIdentifier b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__OAIRecordCreatorIdentifier"/> object to compare with the current <see cref="__OAIRecordCreatorIdentifier"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __OAIRecordCreatorIdentifier))
			{
				return false;
			}
			
			return this == (__OAIRecordCreatorIdentifier) obj;
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
		/// list.Sort(SortOrder.Ascending, __OAIRecordCreatorIdentifier.SortColumn.OAIRecordCreatorIdentifierID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string OAIRecordCreatorIdentifierID = "OAIRecordCreatorIdentifierID";	
			public const string OAIRecordCreatorID = "OAIRecordCreatorID";	
			public const string IdentifierType = "IdentifierType";	
			public const string IdentifierValue = "IdentifierValue";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

