
// Generated 11/23/2010 11:26:17 AM
// Do not modify the contents of this code file.
// This abstract class __IAScandataAltPageNumber is based upon IAScandataAltPageNumber.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAScandataAltPageNumber : __IAScandataAltPageNumber
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
	public abstract class __IAScandataAltPageNumber : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAScandataAltPageNumber()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="scandataAltPageNumberID"></param>
		/// <param name="scandataID"></param>
		/// <param name="sequence"></param>
		/// <param name="pagePrefix"></param>
		/// <param name="pageNumber"></param>
		/// <param name="implied"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IAScandataAltPageNumber(int scandataAltPageNumberID, 
			int scandataID, 
			int sequence, 
			string pagePrefix, 
			string pageNumber, 
			bool implied, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_ScandataAltPageNumberID = scandataAltPageNumberID;
			ScandataID = scandataID;
			Sequence = sequence;
			PagePrefix = pagePrefix;
			PageNumber = pageNumber;
			Implied = implied;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAScandataAltPageNumber()
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
					case "ScandataAltPageNumberID" :
					{
						_ScandataAltPageNumberID = (int)column.Value;
						break;
					}
					case "ScandataID" :
					{
						_ScandataID = (int)column.Value;
						break;
					}
					case "Sequence" :
					{
						_Sequence = (int)column.Value;
						break;
					}
					case "PagePrefix" :
					{
						_PagePrefix = (string)column.Value;
						break;
					}
					case "PageNumber" :
					{
						_PageNumber = (string)column.Value;
						break;
					}
					case "Implied" :
					{
						_Implied = (bool)column.Value;
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
		
		#region ScandataAltPageNumberID
		
		private int _ScandataAltPageNumberID = default(int);
		
		/// <summary>
		/// Column: ScandataAltPageNumberID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ScandataAltPageNumberID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInPrimaryKey=true)]
		public int ScandataAltPageNumberID
		{
			get
			{
				return _ScandataAltPageNumberID;
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
		
		#endregion ScandataAltPageNumberID
		
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
		
		#region Sequence
		
		private int _Sequence = default(int);
		
		/// <summary>
		/// Column: Sequence;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("Sequence", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10)]
		public int Sequence
		{
			get
			{
				return _Sequence;
			}
			set
			{
				if (_Sequence != value)
				{
					_Sequence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Sequence
		
		#region PagePrefix
		
		private string _PagePrefix = string.Empty;
		
		/// <summary>
		/// Column: PagePrefix;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("PagePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=40)]
		public string PagePrefix
		{
			get
			{
				return _PagePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_PagePrefix != value)
				{
					_PagePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PagePrefix
		
		#region PageNumber
		
		private string _PageNumber = string.Empty;
		
		/// <summary>
		/// Column: PageNumber;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("PageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=20)]
		public string PageNumber
		{
			get
			{
				return _PageNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_PageNumber != value)
				{
					_PageNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageNumber
		
		#region Implied
		
		private bool _Implied = false;
		
		/// <summary>
		/// Column: Implied;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("Implied", DbTargetType=SqlDbType.Bit, Ordinal=6)]
		public bool Implied
		{
			get
			{
				return _Implied;
			}
			set
			{
				if (_Implied != value)
				{
					_Implied = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Implied
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=7)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=8)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IAScandataAltPageNumber"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAScandataAltPageNumber"/>, 
		/// returns an instance of <see cref="__IAScandataAltPageNumber"/>; otherwise returns null.</returns>
		public static new __IAScandataAltPageNumber FromArray(byte[] byteArray)
		{
			__IAScandataAltPageNumber o = null;
			
			try
			{
				o = (__IAScandataAltPageNumber) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAScandataAltPageNumber"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAScandataAltPageNumber"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAScandataAltPageNumber)
			{
				__IAScandataAltPageNumber o = (__IAScandataAltPageNumber) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ScandataAltPageNumberID == ScandataAltPageNumberID &&
					o.ScandataID == ScandataID &&
					o.Sequence == Sequence &&
					GetComparisonString(o.PagePrefix) == GetComparisonString(PagePrefix) &&
					GetComparisonString(o.PageNumber) == GetComparisonString(PageNumber) &&
					o.Implied == Implied &&
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
				throw new ArgumentException("Argument is not of type __IAScandataAltPageNumber");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAScandataAltPageNumber"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAScandataAltPageNumber"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAScandataAltPageNumber a, __IAScandataAltPageNumber b)
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
		/// <param name="a">The first <see cref="__IAScandataAltPageNumber"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAScandataAltPageNumber"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAScandataAltPageNumber a, __IAScandataAltPageNumber b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAScandataAltPageNumber"/> object to compare with the current <see cref="__IAScandataAltPageNumber"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAScandataAltPageNumber))
			{
				return false;
			}
			
			return this == (__IAScandataAltPageNumber) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAScandataAltPageNumber.SortColumn.ScandataAltPageNumberID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ScandataAltPageNumberID = "ScandataAltPageNumberID";	
			public const string ScandataID = "ScandataID";	
			public const string Sequence = "Sequence";	
			public const string PagePrefix = "PagePrefix";	
			public const string PageNumber = "PageNumber";	
			public const string Implied = "Implied";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
