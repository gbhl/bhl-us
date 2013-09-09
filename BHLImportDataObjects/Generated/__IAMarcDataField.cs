
// Generated 1/15/2008 11:27:51 AM
// Do not modify the contents of this code file.
// This abstract class __IAMarcDataField is based upon IAMarcDataField.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAMarcDataField : __IAMarcDataField
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
	public abstract class __IAMarcDataField : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAMarcDataField()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="marcDataFieldID"></param>
		/// <param name="marcID"></param>
		/// <param name="tag"></param>
		/// <param name="indicator1"></param>
		/// <param name="indicator2"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAMarcDataField(int marcDataFieldID, 
			int marcID, 
			string tag, 
			string indicator1, 
			string indicator2, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_MarcDataFieldID = marcDataFieldID;
			MarcID = marcID;
			Tag = tag;
			Indicator1 = indicator1;
			Indicator2 = indicator2;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAMarcDataField()
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
					case "MarcDataFieldID" :
					{
						_MarcDataFieldID = (int)column.Value;
						break;
					}
					case "MarcID" :
					{
						_MarcID = (int)column.Value;
						break;
					}
					case "Tag" :
					{
						_Tag = (string)column.Value;
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
		
		#region MarcDataFieldID
		
		private int _MarcDataFieldID = default(int);
		
		/// <summary>
		/// Column: MarcDataFieldID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("MarcDataFieldID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int MarcDataFieldID
		{
			get
			{
				return _MarcDataFieldID;
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
		
		#endregion MarcDataFieldID
		
		#region MarcID
		
		private int _MarcID = default(int);
		
		/// <summary>
		/// Column: MarcID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("MarcID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
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
		
		#region Tag
		
		private string _Tag = string.Empty;
		
		/// <summary>
		/// Column: Tag;
		/// DBMS data type: nchar(3);
		/// </summary>
		[ColumnDefinition("Tag", DbTargetType=SqlDbType.NChar, Ordinal=3, CharacterMaxLength=3)]
		public string Tag
		{
			get
			{
				return _Tag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 3);
				if (_Tag != value)
				{
					_Tag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Tag
		
		#region Indicator1
		
		private string _Indicator1 = string.Empty;
		
		/// <summary>
		/// Column: Indicator1;
		/// DBMS data type: nchar(1);
		/// </summary>
		[ColumnDefinition("Indicator1", DbTargetType=SqlDbType.NChar, Ordinal=4, CharacterMaxLength=1)]
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
		
		private string _Indicator2 = string.Empty;
		
		/// <summary>
		/// Column: Indicator2;
		/// DBMS data type: nchar(1);
		/// </summary>
		[ColumnDefinition("Indicator2", DbTargetType=SqlDbType.NChar, Ordinal=5, CharacterMaxLength=1)]
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
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=6)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAMarcDataField"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAMarcDataField"/>, 
		/// returns an instance of <see cref="__IAMarcDataField"/>; otherwise returns null.</returns>
		public static new __IAMarcDataField FromArray(byte[] byteArray)
		{
			__IAMarcDataField o = null;
			
			try
			{
				o = (__IAMarcDataField) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAMarcDataField"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAMarcDataField"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAMarcDataField)
			{
				__IAMarcDataField o = (__IAMarcDataField) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.MarcDataFieldID == MarcDataFieldID &&
					o.MarcID == MarcID &&
					GetComparisonString(o.Tag) == GetComparisonString(Tag) &&
					GetComparisonString(o.Indicator1) == GetComparisonString(Indicator1) &&
					GetComparisonString(o.Indicator2) == GetComparisonString(Indicator2) &&
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
				throw new ArgumentException("Argument is not of type __IAMarcDataField");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAMarcDataField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarcDataField"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAMarcDataField a, __IAMarcDataField b)
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
		/// <param name="a">The first <see cref="__IAMarcDataField"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAMarcDataField"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAMarcDataField a, __IAMarcDataField b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAMarcDataField"/> object to compare with the current <see cref="__IAMarcDataField"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAMarcDataField))
			{
				return false;
			}
			
			return this == (__IAMarcDataField) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAMarcDataField.SortColumn.MarcDataFieldID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MarcDataFieldID = "MarcDataFieldID";	
			public const string MarcID = "MarcID";	
			public const string Tag = "Tag";	
			public const string Indicator1 = "Indicator1";	
			public const string Indicator2 = "Indicator2";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
