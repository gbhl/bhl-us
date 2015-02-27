
// Generated 2/27/2015 2:20:32 PM
// Do not modify the contents of this code file.
// This abstract class __NoteType is based upon NoteType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class NoteType : __NoteType
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
	public abstract class __NoteType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __NoteType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="noteTypeID"></param>
		/// <param name="noteTypeName"></param>
		/// <param name="noteTypeDisplay"></param>
		/// <param name="marcDataFieldTag"></param>
		/// <param name="marcIndicator1"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __NoteType(int noteTypeID, 
			string noteTypeName, 
			string noteTypeDisplay, 
			string marcDataFieldTag, 
			string marcIndicator1, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_NoteTypeID = noteTypeID;
			NoteTypeName = noteTypeName;
			NoteTypeDisplay = noteTypeDisplay;
			MarcDataFieldTag = marcDataFieldTag;
			MarcIndicator1 = marcIndicator1;
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
		~__NoteType()
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
					case "NoteTypeID" :
					{
						_NoteTypeID = (int)column.Value;
						break;
					}
					case "NoteTypeName" :
					{
						_NoteTypeName = (string)column.Value;
						break;
					}
					case "NoteTypeDisplay" :
					{
						_NoteTypeDisplay = (string)column.Value;
						break;
					}
					case "MarcDataFieldTag" :
					{
						_MarcDataFieldTag = (string)column.Value;
						break;
					}
					case "MarcIndicator1" :
					{
						_MarcIndicator1 = (string)column.Value;
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
						_CreationUserID = (int?)column.Value;
						break;
					}
					case "LastModifiedUserID" :
					{
						_LastModifiedUserID = (int?)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region NoteTypeID
		
		private int _NoteTypeID = default(int);
		
		/// <summary>
		/// Column: NoteTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("NoteTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int NoteTypeID
		{
			get
			{
				return _NoteTypeID;
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
		
		#endregion NoteTypeID
		
		#region NoteTypeName
		
		private string _NoteTypeName = string.Empty;
		
		/// <summary>
		/// Column: NoteTypeName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("NoteTypeName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=50)]
		public string NoteTypeName
		{
			get
			{
				return _NoteTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_NoteTypeName != value)
				{
					_NoteTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteTypeName
		
		#region NoteTypeDisplay
		
		private string _NoteTypeDisplay = string.Empty;
		
		/// <summary>
		/// Column: NoteTypeDisplay;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("NoteTypeDisplay", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string NoteTypeDisplay
		{
			get
			{
				return _NoteTypeDisplay;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_NoteTypeDisplay != value)
				{
					_NoteTypeDisplay = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion NoteTypeDisplay
		
		#region MarcDataFieldTag
		
		private string _MarcDataFieldTag = string.Empty;
		
		/// <summary>
		/// Column: MarcDataFieldTag;
		/// DBMS data type: nvarchar(5);
		/// </summary>
		[ColumnDefinition("MarcDataFieldTag", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=5)]
		public string MarcDataFieldTag
		{
			get
			{
				return _MarcDataFieldTag;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 5);
				if (_MarcDataFieldTag != value)
				{
					_MarcDataFieldTag = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcDataFieldTag
		
		#region MarcIndicator1
		
		private string _MarcIndicator1 = string.Empty;
		
		/// <summary>
		/// Column: MarcIndicator1;
		/// DBMS data type: nvarchar(5);
		/// </summary>
		[ColumnDefinition("MarcIndicator1", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=5)]
		public string MarcIndicator1
		{
			get
			{
				return _MarcIndicator1;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 5);
				if (_MarcIndicator1 != value)
				{
					_MarcIndicator1 = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MarcIndicator1
		
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
		
		private int? _CreationUserID = null;
		
		/// <summary>
		/// Column: CreationUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=8, NumericPrecision=10, IsNullable=true)]
		public int? CreationUserID
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
		
		private int? _LastModifiedUserID = null;
		
		/// <summary>
		/// Column: LastModifiedUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=9, NumericPrecision=10, IsNullable=true)]
		public int? LastModifiedUserID
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
		/// Deserializes the byte array and returns an instance of <see cref="__NoteType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__NoteType"/>, 
		/// returns an instance of <see cref="__NoteType"/>; otherwise returns null.</returns>
		public static new __NoteType FromArray(byte[] byteArray)
		{
			__NoteType o = null;
			
			try
			{
				o = (__NoteType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__NoteType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__NoteType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __NoteType)
			{
				__NoteType o = (__NoteType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.NoteTypeID == NoteTypeID &&
					GetComparisonString(o.NoteTypeName) == GetComparisonString(NoteTypeName) &&
					GetComparisonString(o.NoteTypeDisplay) == GetComparisonString(NoteTypeDisplay) &&
					GetComparisonString(o.MarcDataFieldTag) == GetComparisonString(MarcDataFieldTag) &&
					GetComparisonString(o.MarcIndicator1) == GetComparisonString(MarcIndicator1) &&
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
				throw new ArgumentException("Argument is not of type __NoteType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__NoteType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__NoteType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__NoteType a, __NoteType b)
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
		/// <param name="a">The first <see cref="__NoteType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__NoteType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__NoteType a, __NoteType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__NoteType"/> object to compare with the current <see cref="__NoteType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __NoteType))
			{
				return false;
			}
			
			return this == (__NoteType) obj;
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
		/// list.Sort(SortOrder.Ascending, __NoteType.SortColumn.NoteTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string NoteTypeID = "NoteTypeID";	
			public const string NoteTypeName = "NoteTypeName";	
			public const string NoteTypeDisplay = "NoteTypeDisplay";	
			public const string MarcDataFieldTag = "MarcDataFieldTag";	
			public const string MarcIndicator1 = "MarcIndicator1";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
