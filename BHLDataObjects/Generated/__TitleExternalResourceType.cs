
// Generated 2/22/2023 5:08:40 PM
// Do not modify the contents of this code file.
// This abstract class __TitleExternalResourceType is based upon dbo.TitleExternalResourceType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class TitleExternalResourceType : __TitleExternalResourceType
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
	public abstract class __TitleExternalResourceType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __TitleExternalResourceType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="titleExternalResourceTypeID"></param>
		/// <param name="externalResourceTypeName"></param>
		/// <param name="externalResourceTypeLabel"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __TitleExternalResourceType(int titleExternalResourceTypeID, 
			string externalResourceTypeName, 
			string externalResourceTypeLabel, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int creationUserID, 
			int lastModifiedUserID) : this()
		{
			_TitleExternalResourceTypeID = titleExternalResourceTypeID;
			ExternalResourceTypeName = externalResourceTypeName;
			ExternalResourceTypeLabel = externalResourceTypeLabel;
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
		~__TitleExternalResourceType()
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
					case "TitleExternalResourceTypeID" :
					{
						_TitleExternalResourceTypeID = (int)column.Value;
						break;
					}
					case "ExternalResourceTypeName" :
					{
						_ExternalResourceTypeName = (string)column.Value;
						break;
					}
					case "ExternalResourceTypeLabel" :
					{
						_ExternalResourceTypeLabel = (string)column.Value;
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
		
		#region TitleExternalResourceTypeID
		
		private int _TitleExternalResourceTypeID = default(int);
		
		/// <summary>
		/// Column: TitleExternalResourceTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("TitleExternalResourceTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int TitleExternalResourceTypeID
		{
			get
			{
				return _TitleExternalResourceTypeID;
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
		
		#endregion TitleExternalResourceTypeID
		
		#region ExternalResourceTypeName
		
		private string _ExternalResourceTypeName = string.Empty;
		
		/// <summary>
		/// Column: ExternalResourceTypeName;
		/// DBMS data type: varchar(100);
		/// </summary>
		[ColumnDefinition("ExternalResourceTypeName", DbTargetType=SqlDbType.VarChar, Ordinal=2, CharacterMaxLength=100)]
		public string ExternalResourceTypeName
		{
			get
			{
				return _ExternalResourceTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ExternalResourceTypeName != value)
				{
					_ExternalResourceTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalResourceTypeName
		
		#region ExternalResourceTypeLabel
		
		private string _ExternalResourceTypeLabel = string.Empty;
		
		/// <summary>
		/// Column: ExternalResourceTypeLabel;
		/// DBMS data type: varchar(100);
		/// </summary>
		[ColumnDefinition("ExternalResourceTypeLabel", DbTargetType=SqlDbType.VarChar, Ordinal=3, CharacterMaxLength=100)]
		public string ExternalResourceTypeLabel
		{
			get
			{
				return _ExternalResourceTypeLabel;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ExternalResourceTypeLabel != value)
				{
					_ExternalResourceTypeLabel = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalResourceTypeLabel
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=5)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=7, NumericPrecision=10)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__TitleExternalResourceType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__TitleExternalResourceType"/>, 
		/// returns an instance of <see cref="__TitleExternalResourceType"/>; otherwise returns null.</returns>
		public static new __TitleExternalResourceType FromArray(byte[] byteArray)
		{
			__TitleExternalResourceType o = null;
			
			try
			{
				o = (__TitleExternalResourceType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__TitleExternalResourceType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__TitleExternalResourceType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __TitleExternalResourceType)
			{
				__TitleExternalResourceType o = (__TitleExternalResourceType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.TitleExternalResourceTypeID == TitleExternalResourceTypeID &&
					GetComparisonString(o.ExternalResourceTypeName) == GetComparisonString(ExternalResourceTypeName) &&
					GetComparisonString(o.ExternalResourceTypeLabel) == GetComparisonString(ExternalResourceTypeLabel) &&
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
				throw new ArgumentException("Argument is not of type __TitleExternalResourceType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__TitleExternalResourceType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleExternalResourceType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__TitleExternalResourceType a, __TitleExternalResourceType b)
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
		/// <param name="a">The first <see cref="__TitleExternalResourceType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__TitleExternalResourceType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__TitleExternalResourceType a, __TitleExternalResourceType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__TitleExternalResourceType"/> object to compare with the current <see cref="__TitleExternalResourceType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __TitleExternalResourceType))
			{
				return false;
			}
			
			return this == (__TitleExternalResourceType) obj;
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
		/// list.Sort(SortOrder.Ascending, __TitleExternalResourceType.SortColumn.TitleExternalResourceTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string TitleExternalResourceTypeID = "TitleExternalResourceTypeID";	
			public const string ExternalResourceTypeName = "ExternalResourceTypeName";	
			public const string ExternalResourceTypeLabel = "ExternalResourceTypeLabel";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

