
// Generated 11/23/2010 11:26:17 AM
// Do not modify the contents of this code file.
// This abstract class __IAScandataAltPageType is based upon IAScandataAltPageType.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAScandataAltPageType : __IAScandataAltPageType
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
	public abstract class __IAScandataAltPageType : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAScandataAltPageType()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="scandataAltPageTypeID"></param>
		/// <param name="scandataID"></param>
		/// <param name="pageType"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAScandataAltPageType(int scandataAltPageTypeID, 
			int scandataID, 
			string pageType, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_ScandataAltPageTypeID = scandataAltPageTypeID;
			ScandataID = scandataID;
			PageType = pageType;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAScandataAltPageType()
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
					case "ScandataAltPageTypeID" :
					{
						_ScandataAltPageTypeID = (int)column.Value;
						break;
					}
					case "ScandataID" :
					{
						_ScandataID = (int)column.Value;
						break;
					}
					case "PageType" :
					{
						_PageType = (string)column.Value;
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
		
		#region ScandataAltPageTypeID
		
		private int _ScandataAltPageTypeID = default(int);
		
		/// <summary>
		/// Column: ScandataAltPageTypeID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ScandataAltPageTypeID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ScandataAltPageTypeID
		{
			get
			{
				return _ScandataAltPageTypeID;
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
		
		#endregion ScandataAltPageTypeID
		
		#region ScandataID
		
		private int _ScandataID = default(int);
		
		/// <summary>
		/// Column: ScandataID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ScandataID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ScandataID
		{
			get
			{
				return _ScandataID;
			}
			set
			{
				if (_ScandataID != value)
				{
					_ScandataID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScandataID
		
		#region PageType
		
		private string _PageType = string.Empty;
		
		/// <summary>
		/// Column: PageType;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("PageType", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=50)]
		public string PageType
		{
			get
			{
				return _PageType;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_PageType != value)
				{
					_PageType = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageType
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=4)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAScandataAltPageType"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAScandataAltPageType"/>, 
		/// returns an instance of <see cref="__IAScandataAltPageType"/>; otherwise returns null.</returns>
		public static new __IAScandataAltPageType FromArray(byte[] byteArray)
		{
			__IAScandataAltPageType o = null;
			
			try
			{
				o = (__IAScandataAltPageType) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAScandataAltPageType"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAScandataAltPageType"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAScandataAltPageType)
			{
				__IAScandataAltPageType o = (__IAScandataAltPageType) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ScandataAltPageTypeID == ScandataAltPageTypeID &&
					o.ScandataID == ScandataID &&
					GetComparisonString(o.PageType) == GetComparisonString(PageType) &&
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
				throw new ArgumentException("Argument is not of type __IAScandataAltPageType");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAScandataAltPageType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAScandataAltPageType"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAScandataAltPageType a, __IAScandataAltPageType b)
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
		/// <param name="a">The first <see cref="__IAScandataAltPageType"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAScandataAltPageType"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAScandataAltPageType a, __IAScandataAltPageType b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAScandataAltPageType"/> object to compare with the current <see cref="__IAScandataAltPageType"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAScandataAltPageType))
			{
				return false;
			}
			
			return this == (__IAScandataAltPageType) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAScandataAltPageType.SortColumn.ScandataAltPageTypeID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ScandataAltPageTypeID = "ScandataAltPageTypeID";	
			public const string ScandataID = "ScandataID";	
			public const string PageType = "PageType";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
