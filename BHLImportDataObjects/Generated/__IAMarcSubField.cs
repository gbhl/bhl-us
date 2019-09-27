
// Generated 9/27/2019 3:36:32 PM
// Do not modify the contents of this code file.
// This abstract class __IAMarcSubField is based upon dbo.IAMarcSubField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAMarcSubField : __IAMarcSubField
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
	public abstract class __IAMarcSubField : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAMarcSubField()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcSubFieldID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAMarcSubField(int marcSubFieldID, 
			int marcDataFieldID, 
			string code, 
			string value, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_MarcSubFieldID = marcSubFieldID;
			MarcDataFieldID = marcDataFieldID;
			Code = code;
			Value = value;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAMarcSubField()
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAMarcSubField"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAMarcSubField"/>, 
		/// returns an instance of <see cref="__IAMarcSubField"/>; otherwise returns null.</returns>
		public static new __IAMarcSubField FromArray(byte[] byteArray)
		{
			__IAMarcSubField o = null;
			
			try
			{
				o = (__IAMarcSubField) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAMarcSubField"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAMarcSubField"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAMarcSubField)
			{
				__IAMarcSubField o = (__IAMarcSubField) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcSubFieldID == MarcSubFieldID &&
					o.MarcDataFieldID == MarcDataFieldID &&
					GetComparisonString(o.Code) == GetComparisonString(Code) &&
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
				throw new ArgumentException("Argument is not of type __IAMarcSubField");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAMarcSubField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarcSubField"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAMarcSubField a, __IAMarcSubField b)
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
		/// <param name="a">The first <see cref="__IAMarcSubField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarcSubField"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAMarcSubField a, __IAMarcSubField b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAMarcSubField"/> object to compare with the current <see cref="__IAMarcSubField"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAMarcSubField))
			{
				return false;
			}
			
			return this == (__IAMarcSubField) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAMarcSubField.SortColumn.MarcSubFieldID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcSubFieldID = "MarcSubFieldID";	
			public const string MarcDataFieldID = "MarcDataFieldID";	
			public const string Code = "Code";	
			public const string Value = "Value";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

