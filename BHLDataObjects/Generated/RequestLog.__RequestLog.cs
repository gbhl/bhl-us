
// Generated 10/4/2009 6:06:26 PM
// Do not modify the contents of this code file.
// This abstract class __RequestLog is based upon RequestLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.RequestLog.DataObjects
// {
//		[Serializable]
// 		public class RequestLog : __RequestLog
//		{
//		}
// }

#endregion How To Implement

using System;
using System.Data;
using CustomDataAccess;

namespace MOBOT.BHL.RequestLog.DataObjects
{	
	[Serializable]
	public abstract class __RequestLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __RequestLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="requestLogID"></param>
		/// <param name="applicationID"></param>
		/// <param name="iPAddress"></param>
		/// <param name="userID"></param>
		/// <param name="creationDate"></param>
		/// <param name="requestTypeID"></param>
		/// <param name="detail"></param>
		public __RequestLog(int requestLogID, 
			int applicationID, 
			string iPAddress, 
			int? userID, 
			DateTime creationDate, 
			int requestTypeID, 
			string detail) : this()
		{
			_RequestLogID = requestLogID;
			ApplicationID = applicationID;
			IPAddress = iPAddress;
			UserID = userID;
			CreationDate = creationDate;
			RequestTypeID = requestTypeID;
			Detail = detail;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__RequestLog()
		{
		}
		
		#endregion Destructor
		
		#region Set Values
		
		/// <summary>
		/// Set the property values of this instance from the specified <see cref="DataItemRow"/>.
		/// </summary>
		public virtual void SetValues(CustomDataRow row)
		{
			foreach (CustomDataColumn column in row)
			{
				switch (column.Name)
				{
					case "RequestLogID" :
					{
						_RequestLogID = (int)column.Value;
						break;
					}
					case "ApplicationID" :
					{
						_ApplicationID = (int)column.Value;
						break;
					}
					case "IPAddress" :
					{
						_IPAddress = (string)column.Value;
						break;
					}
					case "UserID" :
					{
						_UserID = (int?)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
					case "RequestTypeID" :
					{
						_RequestTypeID = (int)column.Value;
						break;
					}
					case "Detail" :
					{
						_Detail = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region RequestLogID
		
		private int _RequestLogID = default(int);
		
		/// <summary>
		/// Column: RequestLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("RequestLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int RequestLogID
		{
			get
			{
				return _RequestLogID;
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
		
		#endregion RequestLogID
		
		#region ApplicationID
		
		private int _ApplicationID = default(int);
		
		/// <summary>
		/// Column: ApplicationID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ApplicationID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ApplicationID
		{
			get
			{
				return _ApplicationID;
			}
			set
			{
				if (_ApplicationID != value)
				{
					_ApplicationID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ApplicationID
		
		#region IPAddress
		
		private string _IPAddress = null;
		
		/// <summary>
		/// Column: IPAddress;
		/// DBMS data type: varchar(15); Nullable;
		/// </summary>
		[ColumnDefinition("IPAddress", DbTargetType=SqlDbType.VarChar, Ordinal=3, CharacterMaxLength=15, IsNullable=true)]
		public string IPAddress
		{
			get
			{
				return _IPAddress;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 15);
				if (_IPAddress != value)
				{
					_IPAddress = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IPAddress
		
		#region UserID
		
		private int? _UserID = null;
		
		/// <summary>
		/// Column: UserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("UserID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? UserID
		{
			get
			{
				return _UserID;
			}
			set
			{
				if (_UserID != value)
				{
					_UserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion UserID
		
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
		
		#region RequestTypeID
		
		private int _RequestTypeID = default(int);
		
		/// <summary>
		/// Column: RequestTypeID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("RequestTypeID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsInForeignKey=true)]
		public int RequestTypeID
		{
			get
			{
				return _RequestTypeID;
			}
			set
			{
				if (_RequestTypeID != value)
				{
					_RequestTypeID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RequestTypeID
		
		#region Detail
		
		private string _Detail = null;
		
		/// <summary>
		/// Column: Detail;
		/// DBMS data type: varchar(2000); Nullable;
		/// </summary>
		[ColumnDefinition("Detail", DbTargetType=SqlDbType.VarChar, Ordinal=7, CharacterMaxLength=2000, IsNullable=true)]
		public string Detail
		{
			get
			{
				return _Detail;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_Detail != value)
				{
					_Detail = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Detail
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__RequestLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__RequestLog"/>, 
		/// returns an instance of <see cref="__RequestLog"/>; otherwise returns null.</returns>
		public static new __RequestLog FromArray(byte[] byteArray)
		{
			__RequestLog o = null;
			
			try
			{
				o = (__RequestLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__RequestLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__RequestLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __RequestLog)
			{
				__RequestLog o = (__RequestLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.RequestLogID == RequestLogID &&
					o.ApplicationID == ApplicationID &&
					GetComparisonString(o.IPAddress) == GetComparisonString(IPAddress) &&
					o.UserID == UserID &&
					o.CreationDate == CreationDate &&
					o.RequestTypeID == RequestTypeID &&
					GetComparisonString(o.Detail) == GetComparisonString(Detail) 
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
				throw new ArgumentException("Argument is not of type __RequestLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__RequestLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__RequestLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__RequestLog a, __RequestLog b)
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
		/// <param name="a">The first <see cref="__RequestLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__RequestLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__RequestLog a, __RequestLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__RequestLog"/> object to compare with the current <see cref="__RequestLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __RequestLog))
			{
				return false;
			}
			
			return this == (__RequestLog) obj;
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
		/// For example where list is a instance of <see cref="DataCollection">, 
		/// list.Sort(SortOrder.Ascending, __RequestLog.SortColumn.RequestLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string RequestLogID = "RequestLogID";	
			public const string ApplicationID = "ApplicationID";	
			public const string IPAddress = "IPAddress";	
			public const string UserID = "UserID";	
			public const string CreationDate = "CreationDate";	
			public const string RequestTypeID = "RequestTypeID";	
			public const string Detail = "Detail";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
