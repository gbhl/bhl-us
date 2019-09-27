
// Generated 9/27/2019 3:50:04 PM
// Do not modify the contents of this code file.
// This abstract class __MarcSubField is based upon dbo.MarcSubField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class MarcSubField : __MarcSubField
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
	public abstract class __MarcSubField : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __MarcSubField()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcSubFieldID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __MarcSubField(int marcSubFieldID, 
			int marcDataFieldID, 
			string code, 
			string value, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_MarcSubFieldID = marcSubFieldID;
			MarcDataFieldID = marcDataFieldID;
			Code = code;
			Value = value;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__MarcSubField()
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
					case "MarcSubFieldID" :
					{
						_MarcSubFieldID = (int)column.Value;
						break;
					}
					case "MarcDataFieldID" :
					{
						_MarcDataFieldID = (int)column.Value;
						break;
					}
					case "Code" :
					{
						_Code = (string)column.Value;
						break;
					}
					case "Value" :
					{
						_Value = (string)column.Value;
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
		
		#region MarcSubFieldID
		
		private int _MarcSubFieldID = default(int);
		
		/// <summary>
		/// Column: MarcSubFieldID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MarcSubFieldID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int MarcSubFieldID
		{
			get
			{
				return _MarcSubFieldID;
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
		
		#endregion MarcSubFieldID
		
		#region MarcDataFieldID
		
		private int _MarcDataFieldID = default(int);
		
		/// <summary>
		/// Column: MarcDataFieldID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcDataFieldID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int MarcDataFieldID
		{
			get
			{
				return _MarcDataFieldID;
			}
			set
			{
				if (_MarcDataFieldID != value)
				{
					_MarcDataFieldID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcDataFieldID
		
		#region Code
		
		private string _Code = string.Empty;
		
		/// <summary>
		/// Column: Code;
		/// DBMS data type: nchar(1);
		/// </summary>
		[ColumnDefinition("Code", DbTargetType=SqlDbType.NChar, Ordinal=3, CharacterMaxLength=1)]
		public string Code
		{
			get
			{
				return _Code;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1);
				if (_Code != value)
				{
					_Code = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Code
		
		#region Value
		
		private string _Value = string.Empty;
		
		/// <summary>
		/// Column: Value;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Value", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1073741823)]
		public string Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Value != value)
				{
					_Value = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Value
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__MarcSubField"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__MarcSubField"/>, 
		/// returns an instance of <see cref="__MarcSubField"/>; otherwise returns null.</returns>
		public static new __MarcSubField FromArray(byte[] byteArray)
		{
			__MarcSubField o = null;
			
			try
			{
				o = (__MarcSubField) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__MarcSubField"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__MarcSubField"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __MarcSubField)
			{
				__MarcSubField o = (__MarcSubField) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcSubFieldID == MarcSubFieldID &&
					o.MarcDataFieldID == MarcDataFieldID &&
					GetComparisonString(o.Code) == GetComparisonString(Code) &&
					GetComparisonString(o.Value) == GetComparisonString(Value) &&
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
				throw new ArgumentException("Argument is not of type __MarcSubField");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__MarcSubField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MarcSubField"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__MarcSubField a, __MarcSubField b)
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
		/// <param name="a">The first <see cref="__MarcSubField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__MarcSubField"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__MarcSubField a, __MarcSubField b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__MarcSubField"/> object to compare with the current <see cref="__MarcSubField"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __MarcSubField))
			{
				return false;
			}
			
			return this == (__MarcSubField) obj;
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
		/// list.Sort(SortOrder.Ascending, __MarcSubField.SortColumn.MarcSubFieldID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcSubFieldID = "MarcSubFieldID";	
			public const string MarcDataFieldID = "MarcDataFieldID";	
			public const string Code = "Code";	
			public const string Value = "Value";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

