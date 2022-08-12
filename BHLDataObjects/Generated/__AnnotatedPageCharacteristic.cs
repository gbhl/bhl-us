
// Generated 1/5/2021 3:36:23 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotatedPageCharacteristic is based upon annotation.AnnotatedPageCharacteristic.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotatedPageCharacteristic : __AnnotatedPageCharacteristic
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
	public abstract class __AnnotatedPageCharacteristic : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotatedPageCharacteristic()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotatedPageCharacteristicID"></param>
		/// <param name="annotatedPageID"></param>
		/// <param name="characteristicDetail"></param>
		/// <param name="characteristicDetailClean"></param>
		/// <param name="characteristicDetailDisplay"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotatedPageCharacteristic(int annotatedPageCharacteristicID, 
			int? annotatedPageID, 
			string characteristicDetail, 
			string characteristicDetailClean, 
			string characteristicDetailDisplay, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotatedPageCharacteristicID = annotatedPageCharacteristicID;
			AnnotatedPageID = annotatedPageID;
			CharacteristicDetail = characteristicDetail;
			CharacteristicDetailClean = characteristicDetailClean;
			CharacteristicDetailDisplay = characteristicDetailDisplay;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotatedPageCharacteristic()
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
					case "AnnotatedPageCharacteristicID" :
					{
						_AnnotatedPageCharacteristicID = (int)column.Value;
						break;
					}
					case "AnnotatedPageID" :
					{
						_AnnotatedPageID = (int?)column.Value;
						break;
					}
					case "CharacteristicDetail" :
					{
						_CharacteristicDetail = (string)column.Value;
						break;
					}
					case "CharacteristicDetailClean" :
					{
						_CharacteristicDetailClean = (string)column.Value;
						break;
					}
					case "CharacteristicDetailDisplay" :
					{
						_CharacteristicDetailDisplay = (string)column.Value;
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
		
		#region AnnotatedPageCharacteristicID
		
		private int _AnnotatedPageCharacteristicID = default(int);
		
		/// <summary>
		/// Column: AnnotatedPageCharacteristicID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotatedPageCharacteristicID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int AnnotatedPageCharacteristicID
		{
			get
			{
				return _AnnotatedPageCharacteristicID;
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
		
		#endregion AnnotatedPageCharacteristicID
		
		#region AnnotatedPageID
		
		private int? _AnnotatedPageID = null;
		
		/// <summary>
		/// Column: AnnotatedPageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("AnnotatedPageID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? AnnotatedPageID
		{
			get
			{
				return _AnnotatedPageID;
			}
			set
			{
				if (_AnnotatedPageID != value)
				{
					_AnnotatedPageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedPageID
		
		#region CharacteristicDetail
		
		private string _CharacteristicDetail = string.Empty;
		
		/// <summary>
		/// Column: CharacteristicDetail;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CharacteristicDetail", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=1073741823)]
		public string CharacteristicDetail
		{
			get
			{
				return _CharacteristicDetail;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CharacteristicDetail != value)
				{
					_CharacteristicDetail = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CharacteristicDetail
		
		#region CharacteristicDetailClean
		
		private string _CharacteristicDetailClean = string.Empty;
		
		/// <summary>
		/// Column: CharacteristicDetailClean;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CharacteristicDetailClean", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=1073741823)]
		public string CharacteristicDetailClean
		{
			get
			{
				return _CharacteristicDetailClean;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CharacteristicDetailClean != value)
				{
					_CharacteristicDetailClean = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CharacteristicDetailClean
		
		#region CharacteristicDetailDisplay
		
		private string _CharacteristicDetailDisplay = string.Empty;
		
		/// <summary>
		/// Column: CharacteristicDetailDisplay;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CharacteristicDetailDisplay", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=1073741823)]
		public string CharacteristicDetailDisplay
		{
			get
			{
				return _CharacteristicDetailDisplay;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CharacteristicDetailDisplay != value)
				{
					_CharacteristicDetailDisplay = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CharacteristicDetailDisplay
		
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
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotatedPageCharacteristic"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotatedPageCharacteristic"/>, 
		/// returns an instance of <see cref="__AnnotatedPageCharacteristic"/>; otherwise returns null.</returns>
		public static new __AnnotatedPageCharacteristic FromArray(byte[] byteArray)
		{
			__AnnotatedPageCharacteristic o = null;
			
			try
			{
				o = (__AnnotatedPageCharacteristic) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotatedPageCharacteristic"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotatedPageCharacteristic"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotatedPageCharacteristic)
			{
				__AnnotatedPageCharacteristic o = (__AnnotatedPageCharacteristic) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotatedPageCharacteristicID == AnnotatedPageCharacteristicID &&
					o.AnnotatedPageID == AnnotatedPageID &&
					GetComparisonString(o.CharacteristicDetail) == GetComparisonString(CharacteristicDetail) &&
					GetComparisonString(o.CharacteristicDetailClean) == GetComparisonString(CharacteristicDetailClean) &&
					GetComparisonString(o.CharacteristicDetailDisplay) == GetComparisonString(CharacteristicDetailDisplay) &&
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
				throw new ArgumentException("Argument is not of type __AnnotatedPageCharacteristic");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotatedPageCharacteristic"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedPageCharacteristic"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotatedPageCharacteristic a, __AnnotatedPageCharacteristic b)
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
		/// <param name="a">The first <see cref="__AnnotatedPageCharacteristic"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedPageCharacteristic"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotatedPageCharacteristic a, __AnnotatedPageCharacteristic b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotatedPageCharacteristic"/> object to compare with the current <see cref="__AnnotatedPageCharacteristic"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotatedPageCharacteristic))
			{
				return false;
			}
			
			return this == (__AnnotatedPageCharacteristic) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotatedPageCharacteristic.SortColumn.AnnotatedPageCharacteristicID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotatedPageCharacteristicID = "AnnotatedPageCharacteristicID";	
			public const string AnnotatedPageID = "AnnotatedPageID";	
			public const string CharacteristicDetail = "CharacteristicDetail";	
			public const string CharacteristicDetailClean = "CharacteristicDetailClean";	
			public const string CharacteristicDetailDisplay = "CharacteristicDetailDisplay";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

