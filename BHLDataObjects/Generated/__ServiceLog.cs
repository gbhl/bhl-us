
// Generated 10/16/2024 4:27:16 PM
// Do not modify the contents of this code file.
// This abstract class __ServiceLog is based upon servlog.ServiceLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ServiceLog : __ServiceLog
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
	public abstract class __ServiceLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ServiceLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="serviceLogID"></param>
		/// <param name="serviceID"></param>
		/// <param name="severityID"></param>
		/// <param name="errorNumber"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <param name="creationDate"></param>
		public __ServiceLog(int serviceLogID, 
			int serviceID, 
			int severityID, 
			int? errorNumber, 
			string procedure, 
			int? line, 
			string message, 
			string stackTrace, 
			DateTime creationDate) : this()
		{
			_ServiceLogID = serviceLogID;
			ServiceID = serviceID;
			SeverityID = severityID;
			ErrorNumber = errorNumber;
			Procedure = procedure;
			Line = line;
			Message = message;
			StackTrace = stackTrace;
			CreationDate = creationDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ServiceLog()
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
					case "ServiceLogID" :
					{
						_ServiceLogID = (int)column.Value;
						break;
					}
					case "ServiceID" :
					{
						_ServiceID = (int)column.Value;
						break;
					}
					case "SeverityID" :
					{
						_SeverityID = (int)column.Value;
						break;
					}
					case "ErrorNumber" :
					{
						_ErrorNumber = (int?)column.Value;
						break;
					}
					case "Procedure" :
					{
						_Procedure = (string)column.Value;
						break;
					}
					case "Line" :
					{
						_Line = (int?)column.Value;
						break;
					}
					case "Message" :
					{
						_Message = (string)column.Value;
						break;
					}
					case "StackTrace" :
					{
						_StackTrace = (string)column.Value;
						break;
					}
					case "CreationDate" :
					{
						_CreationDate = (DateTime)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ServiceLogID
		
		private int _ServiceLogID = default(int);
		
		/// <summary>
		/// Column: ServiceLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ServiceLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ServiceLogID
		{
			get
			{
				return _ServiceLogID;
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
		
		#endregion ServiceLogID
		
		#region ServiceID
		
		private int _ServiceID = default(int);
		
		/// <summary>
		/// Column: ServiceID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ServiceID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
		public int ServiceID
		{
			get
			{
				return _ServiceID;
			}
			set
			{
				if (_ServiceID != value)
				{
					_ServiceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ServiceID
		
		#region SeverityID
		
		private int _SeverityID = default(int);
		
		/// <summary>
		/// Column: SeverityID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SeverityID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int SeverityID
		{
			get
			{
				return _SeverityID;
			}
			set
			{
				if (_SeverityID != value)
				{
					_SeverityID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SeverityID
		
		#region ErrorNumber
		
		private int? _ErrorNumber = null;
		
		/// <summary>
		/// Column: ErrorNumber;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ErrorNumber", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsNullable=true)]
		public int? ErrorNumber
		{
			get
			{
				return _ErrorNumber;
			}
			set
			{
				if (_ErrorNumber != value)
				{
					_ErrorNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ErrorNumber
		
		#region Procedure
		
		private string _Procedure = string.Empty;
		
		/// <summary>
		/// Column: Procedure;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("Procedure", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=500)]
		public string Procedure
		{
			get
			{
				return _Procedure;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_Procedure != value)
				{
					_Procedure = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Procedure
		
		#region Line
		
		private int? _Line = null;
		
		/// <summary>
		/// Column: Line;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("Line", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? Line
		{
			get
			{
				return _Line;
			}
			set
			{
				if (_Line != value)
				{
					_Line = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Line
		
		#region Message
		
		private string _Message = string.Empty;
		
		/// <summary>
		/// Column: Message;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Message", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=1073741823)]
		public string Message
		{
			get
			{
				return _Message;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Message != value)
				{
					_Message = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Message
		
		#region StackTrace
		
		private string _StackTrace = string.Empty;
		
		/// <summary>
		/// Column: StackTrace;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("StackTrace", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=1073741823)]
		public string StackTrace
		{
			get
			{
				return _StackTrace;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_StackTrace != value)
				{
					_StackTrace = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StackTrace
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=9)]
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
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ServiceLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ServiceLog"/>, 
		/// returns an instance of <see cref="__ServiceLog"/>; otherwise returns null.</returns>
		public static new __ServiceLog FromArray(byte[] byteArray)
		{
			__ServiceLog o = null;
			
			try
			{
				o = (__ServiceLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ServiceLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ServiceLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ServiceLog)
			{
				__ServiceLog o = (__ServiceLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ServiceLogID == ServiceLogID &&
					o.ServiceID == ServiceID &&
					o.SeverityID == SeverityID &&
					o.ErrorNumber == ErrorNumber &&
					GetComparisonString(o.Procedure) == GetComparisonString(Procedure) &&
					o.Line == Line &&
					GetComparisonString(o.Message) == GetComparisonString(Message) &&
					GetComparisonString(o.StackTrace) == GetComparisonString(StackTrace) &&
					o.CreationDate == CreationDate 
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
				throw new ArgumentException("Argument is not of type __ServiceLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ServiceLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ServiceLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ServiceLog a, __ServiceLog b)
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
		/// <param name="a">The first <see cref="__ServiceLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ServiceLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ServiceLog a, __ServiceLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ServiceLog"/> object to compare with the current <see cref="__ServiceLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ServiceLog))
			{
				return false;
			}
			
			return this == (__ServiceLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __ServiceLog.SortColumn.ServiceLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ServiceLogID = "ServiceLogID";	
			public const string ServiceID = "ServiceID";	
			public const string SeverityID = "SeverityID";	
			public const string ErrorNumber = "ErrorNumber";	
			public const string Procedure = "Procedure";	
			public const string Line = "Line";	
			public const string Message = "Message";	
			public const string StackTrace = "StackTrace";	
			public const string CreationDate = "CreationDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

