
// Generated 4/16/2009 4:40:13 PM
// Do not modify the contents of this code file.
// This abstract class __vwMarcDataField is based upon vwMarcDataField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class vwMarcDataField : __vwMarcDataField
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
	public abstract class __vwMarcDataField : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __vwMarcDataField()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcImportBatchID"></param>
		/// <param name="marcFileLocation"></param>
		/// <param name="institutionCode"></param>
		/// <param name="marcID"></param>
		/// <param name="marcDataFieldID"></param>
		/// <param name="marcSubFieldID"></param>
		/// <param name="dataFieldTag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <param name="code"></param>
		/// <param name="subFieldValue"></param>
		public __vwMarcDataField(int marcImportBatchID, 
			string marcFileLocation, 
			string institutionCode, 
			int marcID, 
			int? marcDataFieldID, 
			int? marcSubFieldID, 
			string dataFieldTag, 
			string indicator1, 
			string indicator2, 
			string code, 
			string subFieldValue) : this()
		{
			MarcImportBatchID = marcImportBatchID;
			MarcFileLocation = marcFileLocation;
			InstitutionCode = institutionCode;
			MarcID = marcID;
			MarcDataFieldID = marcDataFieldID;
			MarcSubFieldID = marcSubFieldID;
			DataFieldTag = dataFieldTag;
			Indicator1 = indicator1;
			Indicator2 = indicator2;
			Code = code;
			SubFieldValue = subFieldValue;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__vwMarcDataField()
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
					case "MarcImportBatchID" :
					{
						_MarcImportBatchID = (int)column.Value;
						break;
					}
					case "MarcFileLocation" :
					{
						_MarcFileLocation = (string)column.Value;
						break;
					}
					case "InstitutionCode" :
					{
						_InstitutionCode = (string)column.Value;
						break;
					}
					case "MarcID" :
					{
						_MarcID = (int)column.Value;
						break;
					}
					case "MarcDataFieldID" :
					{
						_MarcDataFieldID = (int?)column.Value;
						break;
					}
					case "MarcSubFieldID" :
					{
						_MarcSubFieldID = (int?)column.Value;
						break;
					}
					case "DataFieldTag" :
					{
						_DataFieldTag = (string)column.Value;
						break;
					}
					case "Indicator1" :
					{
						_Indicator1 = (string)column.Value;
						break;
					}
					case "Indicator2" :
					{
						_Indicator2 = (string)column.Value;
						break;
					}
					case "Code" :
					{
						_Code = (string)column.Value;
						break;
					}
					case "SubFieldValue" :
					{
						_SubFieldValue = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region MarcImportBatchID
		
		private int _MarcImportBatchID = default(int);
		
		/// <summary>
		/// Column: MarcImportBatchID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcImportBatchID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10)]
		public int MarcImportBatchID
		{
			get
			{
				return _MarcImportBatchID;
			}
			set
			{
				if (_MarcImportBatchID != value)
				{
					_MarcImportBatchID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcImportBatchID
		
		#region MarcFileLocation
		
		private string _MarcFileLocation = string.Empty;
		
		/// <summary>
		/// Column: MarcFileLocation;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("MarcFileLocation", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=500)]
		public string MarcFileLocation
		{
			get
			{
				return _MarcFileLocation;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_MarcFileLocation != value)
				{
					_MarcFileLocation = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcFileLocation
		
		#region InstitutionCode
		
		private string _InstitutionCode = null;
		
		/// <summary>
		/// Column: InstitutionCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("InstitutionCode", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=10, IsNullable=true)]
		public string InstitutionCode
		{
			get
			{
				return _InstitutionCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_InstitutionCode != value)
				{
					_InstitutionCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion InstitutionCode
		
		#region MarcID
		
		private int _MarcID = default(int);
		
		/// <summary>
		/// Column: MarcID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10)]
		public int MarcID
		{
			get
			{
				return _MarcID;
			}
			set
			{
				if (_MarcID != value)
				{
					_MarcID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcID
		
		#region MarcDataFieldID
		
		private int? _MarcDataFieldID = null;
		
		/// <summary>
		/// Column: MarcDataFieldID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("MarcDataFieldID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsNullable=true)]
		public int? MarcDataFieldID
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
		
		#region MarcSubFieldID
		
		private int? _MarcSubFieldID = null;
		
		/// <summary>
		/// Column: MarcSubFieldID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("MarcSubFieldID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? MarcSubFieldID
		{
			get
			{
				return _MarcSubFieldID;
			}
			set
			{
				if (_MarcSubFieldID != value)
				{
					_MarcSubFieldID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcSubFieldID
		
		#region DataFieldTag
		
		private string _DataFieldTag = null;
		
		/// <summary>
		/// Column: DataFieldTag;
		/// DBMS data type: nchar(3); Nullable;
		/// </summary>
		[ColumnDefinition("DataFieldTag", DbTargetType=SqlDbType.NChar, Ordinal=7, CharacterMaxLength=3, IsNullable=true)]
		public string DataFieldTag
		{
			get
			{
				return _DataFieldTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 3);
				if (_DataFieldTag != value)
				{
					_DataFieldTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DataFieldTag
		
		#region Indicator1
		
		private string _Indicator1 = null;
		
		/// <summary>
		/// Column: Indicator1;
		/// DBMS data type: nchar(1); Nullable;
		/// </summary>
		[ColumnDefinition("Indicator1", DbTargetType=SqlDbType.NChar, Ordinal=8, CharacterMaxLength=1, IsNullable=true)]
		public string Indicator1
		{
			get
			{
				return _Indicator1;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1);
				if (_Indicator1 != value)
				{
					_Indicator1 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Indicator1
		
		#region Indicator2
		
		private string _Indicator2 = null;
		
		/// <summary>
		/// Column: Indicator2;
		/// DBMS data type: nchar(1); Nullable;
		/// </summary>
		[ColumnDefinition("Indicator2", DbTargetType=SqlDbType.NChar, Ordinal=9, CharacterMaxLength=1, IsNullable=true)]
		public string Indicator2
		{
			get
			{
				return _Indicator2;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1);
				if (_Indicator2 != value)
				{
					_Indicator2 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Indicator2
		
		#region Code
		
		private string _Code = null;
		
		/// <summary>
		/// Column: Code;
		/// DBMS data type: nchar(1); Nullable;
		/// </summary>
		[ColumnDefinition("Code", DbTargetType=SqlDbType.NChar, Ordinal=10, CharacterMaxLength=1, IsNullable=true)]
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
		
		#region SubFieldValue
		
		private string _SubFieldValue = null;
		
		/// <summary>
		/// Column: SubFieldValue;
		/// DBMS data type: nvarchar(200); Nullable;
		/// </summary>
		[ColumnDefinition("SubFieldValue", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=200, IsNullable=true)]
		public string SubFieldValue
		{
			get
			{
				return _SubFieldValue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_SubFieldValue != value)
				{
					_SubFieldValue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SubFieldValue
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__vwMarcDataField"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__vwMarcDataField"/>, 
		/// returns an instance of <see cref="__vwMarcDataField"/>; otherwise returns null.</returns>
		public static new __vwMarcDataField FromArray(byte[] byteArray)
		{
			__vwMarcDataField o = null;
			
			try
			{
				o = (__vwMarcDataField) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__vwMarcDataField"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__vwMarcDataField"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __vwMarcDataField)
			{
				__vwMarcDataField o = (__vwMarcDataField) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcImportBatchID == MarcImportBatchID &&
					GetComparisonString(o.MarcFileLocation) == GetComparisonString(MarcFileLocation) &&
					GetComparisonString(o.InstitutionCode) == GetComparisonString(InstitutionCode) &&
					o.MarcID == MarcID &&
					o.MarcDataFieldID == MarcDataFieldID &&
					o.MarcSubFieldID == MarcSubFieldID &&
					GetComparisonString(o.DataFieldTag) == GetComparisonString(DataFieldTag) &&
					GetComparisonString(o.Indicator1) == GetComparisonString(Indicator1) &&
					GetComparisonString(o.Indicator2) == GetComparisonString(Indicator2) &&
					GetComparisonString(o.Code) == GetComparisonString(Code) &&
					GetComparisonString(o.SubFieldValue) == GetComparisonString(SubFieldValue) 
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
				throw new ArgumentException("Argument is not of type __vwMarcDataField");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__vwMarcDataField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__vwMarcDataField"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__vwMarcDataField a, __vwMarcDataField b)
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
		/// <param name="a">The first <see cref="__vwMarcDataField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__vwMarcDataField"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__vwMarcDataField a, __vwMarcDataField b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__vwMarcDataField"/> object to compare with the current <see cref="__vwMarcDataField"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __vwMarcDataField))
			{
				return false;
			}
			
			return this == (__vwMarcDataField) obj;
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
		/// list.Sort(SortOrder.Ascending, __vwMarcDataField.SortColumn.MarcImportBatchID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcImportBatchID = "MarcImportBatchID";	
			public const string MarcFileLocation = "MarcFileLocation";	
			public const string InstitutionCode = "InstitutionCode";	
			public const string MarcID = "MarcID";	
			public const string MarcDataFieldID = "MarcDataFieldID";	
			public const string MarcSubFieldID = "MarcSubFieldID";	
			public const string DataFieldTag = "DataFieldTag";	
			public const string Indicator1 = "Indicator1";	
			public const string Indicator2 = "Indicator2";	
			public const string Code = "Code";	
			public const string SubFieldValue = "SubFieldValue";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
