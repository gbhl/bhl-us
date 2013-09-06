
// Generated 12/20/2010 4:06:17 PM
// Do not modify the contents of this code file.
// This abstract class __AnnotatedPageType is based upon AnnotatedPageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class AnnotatedPageType : __AnnotatedPageType
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
	public abstract class __AnnotatedPageType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __AnnotatedPageType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="annotatedPageTypeID"></param>
		/// <param name="annotatedPageTypeName"></param>
		/// <param name="annotatedPageTypeDescription"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __AnnotatedPageType(int annotatedPageTypeID, 
			string annotatedPageTypeName, 
			string annotatedPageTypeDescription, 
			DateTime creationDate, 
			DateTime lastModifiedDate) : this()
		{
			_AnnotatedPageTypeID = annotatedPageTypeID;
			AnnotatedPageTypeName = annotatedPageTypeName;
			AnnotatedPageTypeDescription = annotatedPageTypeDescription;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__AnnotatedPageType()
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
					case "AnnotatedPageTypeID" :
					{
						_AnnotatedPageTypeID = (int)column.Value;
						break;
					}
					case "AnnotatedPageTypeName" :
					{
						_AnnotatedPageTypeName = (string)column.Value;
						break;
					}
					case "AnnotatedPageTypeDescription" :
					{
						_AnnotatedPageTypeDescription = (string)column.Value;
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
		
		#region AnnotatedPageTypeID
		
		private int _AnnotatedPageTypeID = default(int);
		
		/// <summary>
		/// Column: AnnotatedPageTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("AnnotatedPageTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int AnnotatedPageTypeID
		{
			get
			{
				return _AnnotatedPageTypeID;
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
		
		#endregion AnnotatedPageTypeID
		
		#region AnnotatedPageTypeName
		
		private string _AnnotatedPageTypeName = string.Empty;
		
		/// <summary>
		/// Column: AnnotatedPageTypeName;
		/// DBMS data type: nvarchar(30);
		/// </summary>
		[ColumnDefinition("AnnotatedPageTypeName", DbTargetType=SqlDbType.NVarChar, Ordinal=2, CharacterMaxLength=30)]
		public string AnnotatedPageTypeName
		{
			get
			{
				return _AnnotatedPageTypeName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_AnnotatedPageTypeName != value)
				{
					_AnnotatedPageTypeName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedPageTypeName
		
		#region AnnotatedPageTypeDescription
		
		private string _AnnotatedPageTypeDescription = string.Empty;
		
		/// <summary>
		/// Column: AnnotatedPageTypeDescription;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("AnnotatedPageTypeDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=500)]
		public string AnnotatedPageTypeDescription
		{
			get
			{
				return _AnnotatedPageTypeDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_AnnotatedPageTypeDescription != value)
				{
					_AnnotatedPageTypeDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AnnotatedPageTypeDescription
		
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
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__AnnotatedPageType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__AnnotatedPageType"/>, 
		/// returns an instance of <see cref="__AnnotatedPageType"/>; otherwise returns null.</returns>
		public static new __AnnotatedPageType FromArray(byte[] byteArray)
		{
			__AnnotatedPageType o = null;
			
			try
			{
				o = (__AnnotatedPageType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__AnnotatedPageType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__AnnotatedPageType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __AnnotatedPageType)
			{
				__AnnotatedPageType o = (__AnnotatedPageType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.AnnotatedPageTypeID == AnnotatedPageTypeID &&
					GetComparisonString(o.AnnotatedPageTypeName) == GetComparisonString(AnnotatedPageTypeName) &&
					GetComparisonString(o.AnnotatedPageTypeDescription) == GetComparisonString(AnnotatedPageTypeDescription) &&
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
				throw new ArgumentException("Argument is not of type __AnnotatedPageType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__AnnotatedPageType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedPageType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__AnnotatedPageType a, __AnnotatedPageType b)
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
		/// <param name="a">The first <see cref="__AnnotatedPageType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__AnnotatedPageType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__AnnotatedPageType a, __AnnotatedPageType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__AnnotatedPageType"/> object to compare with the current <see cref="__AnnotatedPageType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __AnnotatedPageType))
			{
				return false;
			}
			
			return this == (__AnnotatedPageType) obj;
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
		/// list.Sort(SortOrder.Ascending, __AnnotatedPageType.SortColumn.AnnotatedPageTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string AnnotatedPageTypeID = "AnnotatedPageTypeID";	
			public const string AnnotatedPageTypeName = "AnnotatedPageTypeName";	
			public const string AnnotatedPageTypeDescription = "AnnotatedPageTypeDescription";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
