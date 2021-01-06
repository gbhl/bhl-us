
// Generated 1/5/2021 2:16:16 PM
// Do not modify the contents of this code file.
// This abstract class __ImportError is based upon dbo.ImportError.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class ImportError : __ImportError
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
	public abstract class __ImportError : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __ImportError()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="importErrorID"></param>
		/// <param name="keyType"></param>
		/// <param name="keyValue"></param>
		/// <param name="errorDate"></param>
		/// <param name="number"></param>
		/// <param name="severity"></param>
		/// <param name="state"></param>
		/// <param name="procedure"></param>
		/// <param name="line"></param>
		/// <param name="message"></param>
		public __ImportError(int importErrorID, 
			string keyType, 
			string keyValue, 
			DateTime errorDate, 
			int? number, 
			int? severity, 
			int? state, 
			string procedure, 
			int? line, 
			string message) : this()
		{
			_ImportErrorID = importErrorID;
			KeyType = keyType;
			KeyValue = keyValue;
			ErrorDate = errorDate;
			Number = number;
			Severity = severity;
			State = state;
			Procedure = procedure;
			Line = line;
			Message = message;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__ImportError()
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
					case "ImportErrorID" :
					{
						_ImportErrorID = (int)column.Value;
						break;
					}
					case "KeyType" :
					{
						_KeyType = (string)column.Value;
						break;
					}
					case "KeyValue" :
					{
						_KeyValue = (string)column.Value;
						break;
					}
					case "ErrorDate" :
					{
						_ErrorDate = (DateTime)column.Value;
						break;
					}
					case "Number" :
					{
						_Number = (int?)column.Value;
						break;
					}
					case "Severity" :
					{
						_Severity = (int?)column.Value;
						break;
					}
					case "State" :
					{
						_State = (int?)column.Value;
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
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ImportErrorID
		
		private int _ImportErrorID = default(int);
		
		/// <summary>
		/// Column: ImportErrorID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ImportErrorID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ImportErrorID
		{
			get
			{
				return _ImportErrorID;
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
		
		#endregion ImportErrorID
		
		#region KeyType
		
		private string _KeyType = null;
		
		/// <summary>
		/// Column: KeyType;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("KeyType", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50, IsNullable=true)]
		public string KeyType
		{
			get
			{
				return _KeyType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_KeyType != value)
				{
					_KeyType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion KeyType
		
		#region KeyValue
		
		private string _KeyValue = null;
		
		/// <summary>
		/// Column: KeyValue;
		/// DBMS data type: nvarchar(40); Nullable;
		/// </summary>
		[ColumnDefinition("KeyValue", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=40, IsNullable=true)]
		public string KeyValue
		{
			get
			{
				return _KeyValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_KeyValue != value)
				{
					_KeyValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion KeyValue
		
		#region ErrorDate
		
		private DateTime _ErrorDate;
		
		/// <summary>
		/// Column: ErrorDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("ErrorDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		
		#region Number
		
		private int? _Number = null;
		
		/// <summary>
		/// Column: Number;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("Number", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
		public int? Number
		{
			get
			{
				return _Number;
			}
			set
			{
				if (_Number != value)
				{
					_Number = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Number
		
		#region Severity
		
		private int? _Severity = null;
		
		/// <summary>
		/// Column: Severity;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("Severity", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? Severity
		{
			get
			{
				return _Severity;
			}
			set
			{
				if (_Severity != value)
				{
					_Severity = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Severity
		
		#region State
		
		private int? _State = null;
		
		/// <summary>
		/// Column: State;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("State", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10, IsNullable=true)]
		public int? State
		{
			get
			{
				return _State;
			}
			set
			{
				if (_State != value)
				{
					_State = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion State
		
		#region Procedure
		
		private string _Procedure = null;
		
		/// <summary>
		/// Column: Procedure;
		/// DBMS data type: nvarchar(126); Nullable;
		/// </summary>
		[ColumnDefinition("Procedure", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=126, IsNullable=true)]
		public string Procedure
		{
			get
			{
				return _Procedure;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 126);
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
		[ColumnDefinition("Line", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
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
		
		private string _Message = null;
		
		/// <summary>
		/// Column: Message;
		/// DBMS data type: nvarchar(4000); Nullable;
		/// </summary>
		[ColumnDefinition("Message", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=4000, IsNullable=true)]
		public string Message
		{
			get
			{
				return _Message;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 4000);
				if (_Message != value)
				{
					_Message = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Message
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__ImportError"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__ImportError"/>, 
		/// returns an instance of <see cref="__ImportError"/>; otherwise returns null.</returns>
		public static new __ImportError FromArray(byte[] byteArray)
		{
			__ImportError o = null;
			
			try
			{
				o = (__ImportError) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__ImportError"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__ImportError"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __ImportError)
			{
				__ImportError o = (__ImportError) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ImportErrorID == ImportErrorID &&
					GetComparisonString(o.KeyType) == GetComparisonString(KeyType) &&
					GetComparisonString(o.KeyValue) == GetComparisonString(KeyValue) &&
					o.ErrorDate == ErrorDate &&
					o.Number == Number &&
					o.Severity == Severity &&
					o.State == State &&
					GetComparisonString(o.Procedure) == GetComparisonString(Procedure) &&
					o.Line == Line &&
					GetComparisonString(o.Message) == GetComparisonString(Message) 
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
				throw new ArgumentException("Argument is not of type __ImportError");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__ImportError"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportError"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__ImportError a, __ImportError b)
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
		/// <param name="a">The first <see cref="__ImportError"/> object to compare.</param>
		/// <param name="b">The second <see cref="__ImportError"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__ImportError a, __ImportError b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__ImportError"/> object to compare with the current <see cref="__ImportError"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __ImportError))
			{
				return false;
			}
			
			return this == (__ImportError) obj;
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
		/// list.Sort(SortOrder.Ascending, __ImportError.SortColumn.ImportErrorID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ImportErrorID = "ImportErrorID";	
			public const string KeyType = "KeyType";	
			public const string KeyValue = "KeyValue";	
			public const string ErrorDate = "ErrorDate";	
			public const string Number = "Number";	
			public const string Severity = "Severity";	
			public const string State = "State";	
			public const string Procedure = "Procedure";	
			public const string Line = "Line";	
			public const string Message = "Message";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

