
// Generated 1/5/2021 2:15:43 PM
// Do not modify the contents of this code file.
// This abstract class __IASegment is based upon dbo.IASegment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class IASegment : __IASegment
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
	public abstract class __IASegment : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __IASegment()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="sequence"></param>
		/// <param name="title"></param>
		/// <param name="volume"></param>
		/// <param name="issue"></param>
		/// <param name="series"></param>
		/// <param name="date"></param>
		/// <param name="languageCode"></param>
		/// <param name="bHLSegmentGenreID"></param>
		/// <param name="bHLSegmentGenreName"></param>
		/// <param name="dOI"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastModifiedDate"></param>
		public __IASegment(int segmentID, 
			int itemID, 
			int sequence, 
			string title, 
			string volume, 
			string issue, 
			string series, 
			string date, 
			string languageCode, 
			int bHLSegmentGenreID, 
			string bHLSegmentGenreName, 
			string dOI, 
			DateTime createdDate, 
			DateTime lastModifiedDate) : this()
		{
			_SegmentID = segmentID;
			ItemID = itemID;
			Sequence = sequence;
			Title = title;
			Volume = volume;
			Issue = issue;
			Series = series;
			Date = date;
			LanguageCode = languageCode;
			BHLSegmentGenreID = bHLSegmentGenreID;
			BHLSegmentGenreName = bHLSegmentGenreName;
			DOI = dOI;
			CreatedDate = createdDate;
			LastModifiedDate = lastModifiedDate;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__IASegment()
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
					case "SegmentID" :
					{
						_SegmentID = (int)column.Value;
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
					case "Title" :
					{
						_Title = (string)column.Value;
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
					case "Series" :
					{
						_Series = (string)column.Value;
						break;
					}
					case "Date" :
					{
						_Date = (string)column.Value;
						break;
					}
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
						break;
					}
					case "BHLSegmentGenreID" :
					{
						_BHLSegmentGenreID = (int)column.Value;
						break;
					}
					case "BHLSegmentGenreName" :
					{
						_BHLSegmentGenreName = (string)column.Value;
						break;
					}
					case "DOI" :
					{
						_DOI = (string)column.Value;
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
		
		#region SegmentID
		
		private int _SegmentID = default(int);
		
		/// <summary>
		/// Column: SegmentID;
		/// DBMS data type: int; Auto key;
		/// </summary>
		[ColumnDefinition("SegmentID", DbTargetType=SqlDbType.Int, Ordinal=1, NumericPrecision=10, IsAutoKey=true, IsInForeignKey=true, IsInPrimaryKey=true)]
		public int SegmentID
		{
			get
			{
				return _SegmentID;
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
		
		#endregion SegmentID
		
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
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=2000)]
		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_Title != value)
				{
					_Title = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Title
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=100)]
		public string Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Volume != value)
				{
					_Volume = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Volume
		
		#region Issue
		
		private string _Issue = string.Empty;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=100)]
		public string Issue
		{
			get
			{
				return _Issue;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Issue != value)
				{
					_Issue = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Issue
		
		#region Series
		
		private string _Series = string.Empty;
		
		/// <summary>
		/// Column: Series;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Series", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=100)]
		public string Series
		{
			get
			{
				return _Series;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_Series != value)
				{
					_Series = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Series
		
		#region Date
		
		private string _Date = string.Empty;
		
		/// <summary>
		/// Column: Date;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Date", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=20)]
		public string Date
		{
			get
			{
				return _Date;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_Date != value)
				{
					_Date = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Date
		
		#region LanguageCode
		
		private string _LanguageCode = string.Empty;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10);
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=10)]
		public string LanguageCode
		{
			get
			{
				return _LanguageCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 10);
				if (_LanguageCode != value)
				{
					_LanguageCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LanguageCode
		
		#region BHLSegmentGenreID
		
		private int _BHLSegmentGenreID = default(int);
		
		/// <summary>
		/// Column: BHLSegmentGenreID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("BHLSegmentGenreID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
		public int BHLSegmentGenreID
		{
			get
			{
				return _BHLSegmentGenreID;
			}
			set
			{
				if (_BHLSegmentGenreID != value)
				{
					_BHLSegmentGenreID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLSegmentGenreID
		
		#region BHLSegmentGenreName
		
		private string _BHLSegmentGenreName = string.Empty;
		
		/// <summary>
		/// Column: BHLSegmentGenreName;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("BHLSegmentGenreName", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=50)]
		public string BHLSegmentGenreName
		{
			get
			{
				return _BHLSegmentGenreName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_BHLSegmentGenreName != value)
				{
					_BHLSegmentGenreName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLSegmentGenreName
		
		#region DOI
		
		private string _DOI = string.Empty;
		
		/// <summary>
		/// Column: DOI;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("DOI", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=50)]
		public string DOI
		{
			get
			{
				return _DOI;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_DOI != value)
				{
					_DOI = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DOI
		
		#region CreatedDate
		
		private DateTime _CreatedDate;
		
		/// <summary>
		/// Column: CreatedDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreatedDate", DbTargetType=SqlDbType.DateTime, Ordinal=13)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=14)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__IASegment"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__IASegment"/>, 
		/// returns an instance of <see cref="__IASegment"/>; otherwise returns null.</returns>
		public static new __IASegment FromArray(byte[] byteArray)
		{
			__IASegment o = null;
			
			try
			{
				o = (__IASegment) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__IASegment"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__IASegment"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __IASegment)
			{
				__IASegment o = (__IASegment) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentID == SegmentID &&
					o.ItemID == ItemID &&
					o.Sequence == Sequence &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.Series) == GetComparisonString(Series) &&
					GetComparisonString(o.Date) == GetComparisonString(Date) &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					o.BHLSegmentGenreID == BHLSegmentGenreID &&
					GetComparisonString(o.BHLSegmentGenreName) == GetComparisonString(BHLSegmentGenreName) &&
					GetComparisonString(o.DOI) == GetComparisonString(DOI) &&
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
				throw new ArgumentException("Argument is not of type __IASegment");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__IASegment"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IASegment"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__IASegment a, __IASegment b)
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
		/// <param name="a">The first <see cref="__IASegment"/> object to compare.</param>
		/// <param name="b">The second <see cref="__IASegment"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__IASegment a, __IASegment b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__IASegment"/> object to compare with the current <see cref="__IASegment"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __IASegment))
			{
				return false;
			}
			
			return this == (__IASegment) obj;
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
		/// list.Sort(SortOrder.Ascending, __IASegment.SortColumn.SegmentID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentID = "SegmentID";	
			public const string ItemID = "ItemID";	
			public const string Sequence = "Sequence";	
			public const string Title = "Title";	
			public const string Volume = "Volume";	
			public const string Issue = "Issue";	
			public const string Series = "Series";	
			public const string Date = "Date";	
			public const string LanguageCode = "LanguageCode";	
			public const string BHLSegmentGenreID = "BHLSegmentGenreID";	
			public const string BHLSegmentGenreName = "BHLSegmentGenreName";	
			public const string DOI = "DOI";	
			public const string CreatedDate = "CreatedDate";	
			public const string LastModifiedDate = "LastModifiedDate";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

