
// Generated 1/5/2021 2:15:15 PM
// Do not modify the contents of this code file.
// This abstract class __IAScandata is based upon dbo.IAScandata.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IAScandata : __IAScandata
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
	public abstract class __IAScandata : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IAScandata()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="scandataID"></param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="pageType"></param>
		/// <param name="pageNumber"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="year"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="issuePrefix"></param>
		public __IAScandata(int scandataID, 
			int itemID, 
			int sequence, 
			string pageType, 
			string pageNumber, 
			DateTime createdDate, 
			DateTime lastModifiedDate, 
			string year, 
			string volume, 
			string issue, 
			string issuePrefix) : this()
		{
			_ScandataID = scandataID;
			ItemID = itemID;
			Sequence = sequence;
			PageType = pageType;
			PageNumber = pageNumber;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
			Year = year;
			Volume = volume;
			Issue = issue;
			IssuePrefix = issuePrefix;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IAScandata()
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
					case "ScandataID" :
					{
						_ScandataID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "Sequence" :
					{
						_Sequence = (int)column.Value;
						break;
					}
					case "PageType" :
					{
						_PageType = (string)column.Value;
						break;
					}
					case "PageNumber" :
					{
						_PageNumber = (string)column.Value;
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
					case "Year" :
					{
						_Year = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "Issue" :
					{
						_Issue = (string)column.Value;
						break;
					}
					case "IssuePrefix" :
					{
						_IssuePrefix = (string)column.Value;
						break;
					}
								}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties
		
		#region ScandataID
		
		private int _ScandataID = default(int);
		
		/// <summary>
		/// Column: ScandataID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("ScandataID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int ScandataID
		{
			get
			{
				return _ScandataID;
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
		
		#endregion ScandataID
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10, IsInForeignKey=true)]
		public int ItemID
		{
			get
			{
				return _ItemID;
			}
			set
			{
				if (_ItemID != value)
				{
					_ItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemID
		
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
		
		#region PageType
		
		private string _PageType = string.Empty;
		
		/// <summary>
		/// Column: PageType;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("PageType", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=50)]
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
		
		#region Year
		
		private string _Year = null;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=20, IsNullable=true)]
		public string Year
		{
			get
			{
				return _Year;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Year != value)
				{
					_Year = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Year
		
		#region Volume
		
		private string _Volume = null;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=20, IsNullable=true)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region Issue
		
		private string _Issue = null;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=20, IsNullable=true)]
		public string Issue
		{
			get
			{
				return _Issue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Issue != value)
				{
					_Issue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Issue
		
		#region IssuePrefix
		
		private string _IssuePrefix = null;
		
		/// <summary>
		/// Column: IssuePrefix;
		/// DBMS data type: nvarchar(20); Nullable;
		/// </summary>
		[ColumnDefinition("IssuePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=20, IsNullable=true)]
		public string IssuePrefix
		{
			get
			{
				return _IssuePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_IssuePrefix != value)
				{
					_IssuePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion IssuePrefix
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__IAScandata"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IAScandata"/>, 
		/// returns an instance of <see cref="__IAScandata"/>; otherwise returns null.</returns>
		public static new __IAScandata FromArray(byte[] byteArray)
		{
			__IAScandata o = null;
			
			try
			{
				o = (__IAScandata) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IAScandata"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IAScandata"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IAScandata)
			{
				__IAScandata o = (__IAScandata) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.ScandataID == ScandataID &&
					o.ItemID == ItemID &&
					o.Sequence == Sequence &&
					GetComparisonString(o.PageType) == GetComparisonString(PageType) &&
					GetComparisonString(o.PageNumber) == GetComparisonString(PageNumber) &&
					o.CreatedDate == CreatedDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.IssuePrefix) == GetComparisonString(IssuePrefix) 
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
				throw new ArgumentException("Argument is not of type __IAScandata");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IAScandata"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAScandata"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IAScandata a, __IAScandata b)
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
		/// <param name="a">The first <see cref="__IAScandata"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IAScandata"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IAScandata a, __IAScandata b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IAScandata"/> object to compare with the current <see cref="__IAScandata"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IAScandata))
			{
				return false;
			}
			
			return this == (__IAScandata) obj;
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
		/// list.Sort(SortOrder.Ascending, __IAScandata.SortColumn.ScandataID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string ScandataID = "ScandataID";	
			public const string ItemID = "ItemID";	
			public const string Sequence = "Sequence";	
			public const string PageType = "PageType";	
			public const string PageNumber = "PageNumber";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string Year = "Year";	
			public const string Volume = "Volume";	
			public const string Issue = "Issue";	
			public const string IssuePrefix = "IssuePrefix";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

