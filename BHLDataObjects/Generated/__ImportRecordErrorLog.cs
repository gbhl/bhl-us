
// Generated 1/5/2021 3:42:42 PM
// Do not modify the contents of this code file.
// This abstract class __ImportRecordErrorLog is based upon import.ImportRecordErrorLog.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class ImportRecordErrorLog : __ImportRecordErrorLog
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
	public abstract class __ImportRecordErrorLog : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ImportRecordErrorLog()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="importRecordErrorLogID"></param>
		/// <param name="importRecordID"></param>
		/// <param name="errorDate"></param>
		/// <param name="errorMessage"></param>
		/// <param name="severity"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __ImportRecordErrorLog(int importRecordErrorLogID, 
			int importRecordID, 
			DateTime errorDate, 
			string errorMessage, 
			string severity, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_ImportRecordErrorLogID = importRecordErrorLogID;
			ImportRecordID = importRecordID;
			ErrorDate = errorDate;
			ErrorMessage = errorMessage;
			Severity = severity;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			CreationUserID = creationUserID;
			LastModifiedUserID = lastModifiedUserID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ImportRecordErrorLog()
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
					case "ImportRecordErrorLogID" :
					{
						_ImportRecordErrorLogID = (int)column.Value;
						break;
					}
					case "ImportRecordID" :
					{
						_ImportRecordID = (int)column.Value;
						break;
					}
					case "ErrorDate" :
					{
						_ErrorDate = (DateTime)column.Value;
						break;
					}
					case "ErrorMessage" :
					{
						_ErrorMessage = (string)column.Value;
						break;
					}
					case "Severity" :
					{
						_Severity = (string)column.Value;
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
					case "CreationUserID" :
					{
						_CreationUserID = (int)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ImportRecordErrorLogID
		
		private int _ImportRecordErrorLogID = default(int);
		
		/// <summary>
		/// Column: ImportRecordErrorLogID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ImportRecordErrorLogID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ImportRecordErrorLogID
		{
			get
			{
				return _ImportRecordErrorLogID;
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
		
		#endregion ImportRecordErrorLogID
		
		#region ImportRecordID
		
		private int _ImportRecordID = default(int);
		
		/// <summary>
		/// Column: ImportRecordID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ImportRecordID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ImportRecordID
		{
			get
			{
				return _ImportRecordID;
			}
			set
			{
				if (_ImportRecordID != value)
				{
					_ImportRecordID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImportRecordID
		
		#region ErrorDate
		
		private DateTime _ErrorDate;
		
		/// <summary>
		/// Column: ErrorDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("ErrorDate", DbTargetType=SqlDbType.DateTime, Ordinal=3)]
		public DateTime ErrorDate
		{
			get
			{
				return _ErrorDate;
			}
			set
			{
				if (_ErrorDate != value)
				{
					_ErrorDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ErrorDate
		
		#region ErrorMessage
		
		private string _ErrorMessage = string.Empty;
		
		/// <summary>
		/// Column: ErrorMessage;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("ErrorMessage", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1073741823)]
		public string ErrorMessage
		{
			get
			{
				return _ErrorMessage;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_ErrorMessage != value)
				{
					_ErrorMessage = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ErrorMessage
		
		#region Severity
		
		private string _Severity = string.Empty;
		
		/// <summary>
		/// Column: Severity;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("Severity", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=40)]
		public string Severity
		{
			get
			{
				return _Severity;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_Severity != value)
				{
					_Severity = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Severity
		
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
		
		#region CreationUserID
		
		private int _CreationUserID = default(int);
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10)]
		public int CreationUserID
		{
			get
			{
				return _CreationUserID;
			}
			set
			{
				if (_CreationUserID != value)
				{
					_CreationUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CreationUserID
		
		#region LastModifiedUserID
		
		private int _LastModifiedUserID = default(int);
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10)]
		public int LastModifiedUserID
		{
			get
			{
				return _LastModifiedUserID;
			}
			set
			{
				if (_LastModifiedUserID != value)
				{
					_LastModifiedUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastModifiedUserID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ImportRecordErrorLog"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ImportRecordErrorLog"/>, 
		/// returns an instance of <see cref="__ImportRecordErrorLog"/>; otherwise returns null.</returns>
		public static new __ImportRecordErrorLog FromArray(byte[] byteArray)
		{
			__ImportRecordErrorLog o = null;
			
			try
			{
				o = (__ImportRecordErrorLog) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ImportRecordErrorLog"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ImportRecordErrorLog"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ImportRecordErrorLog)
			{
				__ImportRecordErrorLog o = (__ImportRecordErrorLog) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ImportRecordErrorLogID == ImportRecordErrorLogID &&
					o.ImportRecordID == ImportRecordID &&
					o.ErrorDate == ErrorDate &&
					GetComparisonString(o.ErrorMessage) == GetComparisonString(ErrorMessage) &&
					GetComparisonString(o.Severity) == GetComparisonString(Severity) &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					o.CreationUserID == CreationUserID &&
					o.LastModifiedUserID == LastModifiedUserID 
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
				throw new ArgumentException("Argument is not of type __ImportRecordErrorLog");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportRecordErrorLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportRecordErrorLog"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ImportRecordErrorLog a, __ImportRecordErrorLog b)
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
		/// <param name="a">The first <see cref="__ImportRecordErrorLog"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportRecordErrorLog"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ImportRecordErrorLog a, __ImportRecordErrorLog b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ImportRecordErrorLog"/> object to compare with the current <see cref="__ImportRecordErrorLog"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ImportRecordErrorLog))
			{
				return false;
			}
			
			return this == (__ImportRecordErrorLog) obj;
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
		/// list.Sort(SortOrder.Ascending, __ImportRecordErrorLog.SortColumn.ImportRecordErrorLogID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ImportRecordErrorLogID = "ImportRecordErrorLogID";	
			public const string ImportRecordID = "ImportRecordID";	
			public const string ErrorDate = "ErrorDate";	
			public const string ErrorMessage = "ErrorMessage";	
			public const string Severity = "Severity";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

