
// Generated 4/6/2021 5:22:05 PM
// Do not modify the contents of this code file.
// This abstract class __BSSegment is based upon dbo.BSSegment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHLImport.DataObjects
// {
//		[Serializable]
// 		public class BSSegment : __BSSegment
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
	public abstract class __BSSegment : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __BSSegment()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="bioStorReferenceID"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="genre"></param>
		/// <param name="title"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="year"></param>
		/// <param name="date"></param>
		/// <param name="iSSN"></param>
		/// <param name="dOI"></param>
		/// <param name="oCLC"></param>
		/// <param name="jSTOR"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="startPageID"></param>
		/// <param name="contributorCreationDate"></param>
		/// <param name="contributorLastModifiedDate"></param>
		/// <param name="bHLSegmentID"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="contributorName"></param>
		/// <param name="segmentStatusID"></param>
		public __BSSegment(int segmentID, 
			int itemID, 
			string bioStorReferenceID, 
			short sequenceOrder, 
			string genre, 
			string title, 
			string containerTitle, 
			string publisherName, 
			string publisherPlace, 
			string volume, 
			string series, 
			string issue, 
			string year, 
			string date, 
			string iSSN, 
			string dOI, 
			string oCLC, 
			string jSTOR, 
			string startPageNumber, 
			string endPageNumber, 
			int? startPageID, 
			DateTime? contributorCreationDate, 
			DateTime? contributorLastModifiedDate, 
			int? bHLSegmentID, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			string contributorName, 
			int segmentStatusID) : this()
		{
			_SegmentID = segmentID;
			ItemID = itemID;
			BioStorReferenceID = bioStorReferenceID;
			SequenceOrder = sequenceOrder;
			Genre = genre;
			Title = title;
			ContainerTitle = containerTitle;
			PublisherName = publisherName;
			PublisherPlace = publisherPlace;
			Volume = volume;
			Series = series;
			Issue = issue;
			Year = year;
			Date = date;
			ISSN = iSSN;
			DOI = dOI;
			OCLC = oCLC;
			JSTOR = jSTOR;
			StartPageNumber = startPageNumber;
			EndPageNumber = endPageNumber;
			StartPageID = startPageID;
			ContributorCreationDate = contributorCreationDate;
			ContributorLastModifiedDate = contributorLastModifiedDate;
			BHLSegmentID = bHLSegmentID;
			CreationDate = creationDate;
			LastModifiedDate = lastModifiedDate;
			ContributorName = contributorName;
			SegmentStatusID = segmentStatusID;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__BSSegment()
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
					case "BioStorReferenceID" :
					{
						_BioStorReferenceID = (string)column.Value;
						break;
					}
					case "SequenceOrder" :
					{
						_SequenceOrder = (short)column.Value;
						break;
					}
					case "Genre" :
					{
						_Genre = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "ContainerTitle" :
					{
						_ContainerTitle = (string)column.Value;
						break;
					}
					case "PublisherName" :
					{
						_PublisherName = (string)column.Value;
						break;
					}
					case "PublisherPlace" :
					{
						_PublisherPlace = (string)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "Series" :
					{
						_Series = (string)column.Value;
						break;
					}
					case "Issue" :
					{
						_Issue = (string)column.Value;
						break;
					}
					case "Year" :
					{
						_Year = (string)column.Value;
						break;
					}
					case "Date" :
					{
						_Date = (string)column.Value;
						break;
					}
					case "ISSN" :
					{
						_ISSN = (string)column.Value;
						break;
					}
					case "DOI" :
					{
						_DOI = (string)column.Value;
						break;
					}
					case "OCLC" :
					{
						_OCLC = (string)column.Value;
						break;
					}
					case "JSTOR" :
					{
						_JSTOR = (string)column.Value;
						break;
					}
					case "StartPageNumber" :
					{
						_StartPageNumber = (string)column.Value;
						break;
					}
					case "EndPageNumber" :
					{
						_EndPageNumber = (string)column.Value;
						break;
					}
					case "StartPageID" :
					{
						_StartPageID = (int?)column.Value;
						break;
					}
					case "ContributorCreationDate" :
					{
						_ContributorCreationDate = (DateTime?)column.Value;
						break;
					}
					case "ContributorLastModifiedDate" :
					{
						_ContributorLastModifiedDate = (DateTime?)column.Value;
						break;
					}
					case "BHLSegmentID" :
					{
						_BHLSegmentID = (int?)column.Value;
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
					case "ContributorName" :
					{
						_ContributorName = (string)column.Value;
						break;
					}
					case "SegmentStatusID" :
					{
						_SegmentStatusID = (int)column.Value;
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
		
		#region BioStorReferenceID
		
		private string _BioStorReferenceID = string.Empty;
		
		/// <summary>
		/// Column: BioStorReferenceID;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("BioStorReferenceID", DbTargetType=SqlDbType.NVarChar, Ordinal=3, CharacterMaxLength=100)]
		public string BioStorReferenceID
		{
			get
			{
				return _BioStorReferenceID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_BioStorReferenceID != value)
				{
					_BioStorReferenceID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BioStorReferenceID
		
		#region SequenceOrder
		
		private short _SequenceOrder = default(short);
		
		/// <summary>
		/// Column: SequenceOrder;
		/// DBMS data type: smallint;
		/// </summary>
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.SmallInt, Ordinal=4, NumericPrecision=5)]
		public short SequenceOrder
		{
			get
			{
				return _SequenceOrder;
			}
			set
			{
				if (_SequenceOrder != value)
				{
					_SequenceOrder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SequenceOrder
		
		#region Genre
		
		private string _Genre = string.Empty;
		
		/// <summary>
		/// Column: Genre;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("Genre", DbTargetType=SqlDbType.NVarChar, Ordinal=5, CharacterMaxLength=50)]
		public string Genre
		{
			get
			{
				return _Genre;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_Genre != value)
				{
					_Genre = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Genre
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=2000)]
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
		
		#region ContainerTitle
		
		private string _ContainerTitle = string.Empty;
		
		/// <summary>
		/// Column: ContainerTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("ContainerTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=2000)]
		public string ContainerTitle
		{
			get
			{
				return _ContainerTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_ContainerTitle != value)
				{
					_ContainerTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContainerTitle
		
		#region PublisherName
		
		private string _PublisherName = string.Empty;
		
		/// <summary>
		/// Column: PublisherName;
		/// DBMS data type: nvarchar(250);
		/// </summary>
		[ColumnDefinition("PublisherName", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=250)]
		public string PublisherName
		{
			get
			{
				return _PublisherName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 250);
				if (_PublisherName != value)
				{
					_PublisherName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublisherName
		
		#region PublisherPlace
		
		private string _PublisherPlace = string.Empty;
		
		/// <summary>
		/// Column: PublisherPlace;
		/// DBMS data type: nvarchar(150);
		/// </summary>
		[ColumnDefinition("PublisherPlace", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=150)]
		public string PublisherPlace
		{
			get
			{
				return _PublisherPlace;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 150);
				if (_PublisherPlace != value)
				{
					_PublisherPlace = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublisherPlace
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=100)]
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
		
		#region Series
		
		private string _Series = string.Empty;
		
		/// <summary>
		/// Column: Series;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Series", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=100)]
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
		
		#region Issue
		
		private string _Issue = string.Empty;
		
		/// <summary>
		/// Column: Issue;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=100)]
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
		
		#region Year
		
		private string _Year = string.Empty;
		
		/// <summary>
		/// Column: Year;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Year", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=20)]
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
		
		#region Date
		
		private string _Date = string.Empty;
		
		/// <summary>
		/// Column: Date;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Date", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=20)]
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
		
		#region ISSN
		
		private string _ISSN = string.Empty;
		
		/// <summary>
		/// Column: ISSN;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("ISSN", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=125)]
		public string ISSN
		{
			get
			{
				return _ISSN;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_ISSN != value)
				{
					_ISSN = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ISSN
		
		#region DOI
		
		private string _DOI = string.Empty;
		
		/// <summary>
		/// Column: DOI;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("DOI", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=50)]
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
		
		#region OCLC
		
		private string _OCLC = string.Empty;
		
		/// <summary>
		/// Column: OCLC;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("OCLC", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=125)]
		public string OCLC
		{
			get
			{
				return _OCLC;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_OCLC != value)
				{
					_OCLC = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OCLC
		
		#region JSTOR
		
		private string _JSTOR = string.Empty;
		
		/// <summary>
		/// Column: JSTOR;
		/// DBMS data type: nvarchar(125);
		/// </summary>
		[ColumnDefinition("JSTOR", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=125)]
		public string JSTOR
		{
			get
			{
				return _JSTOR;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 125);
				if (_JSTOR != value)
				{
					_JSTOR = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion JSTOR
		
		#region StartPageNumber
		
		private string _StartPageNumber = string.Empty;
		
		/// <summary>
		/// Column: StartPageNumber;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("StartPageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=20)]
		public string StartPageNumber
		{
			get
			{
				return _StartPageNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_StartPageNumber != value)
				{
					_StartPageNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartPageNumber
		
		#region EndPageNumber
		
		private string _EndPageNumber = string.Empty;
		
		/// <summary>
		/// Column: EndPageNumber;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("EndPageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=20)]
		public string EndPageNumber
		{
			get
			{
				return _EndPageNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
				if (_EndPageNumber != value)
				{
					_EndPageNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion EndPageNumber
		
		#region StartPageID
		
		private int? _StartPageID = null;
		
		/// <summary>
		/// Column: StartPageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("StartPageID", DbTargetType=SqlDbType.Int, Ordinal=21, NumericPrecision=10, IsNullable=true)]
		public int? StartPageID
		{
			get
			{
				return _StartPageID;
			}
			set
			{
				if (_StartPageID != value)
				{
					_StartPageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion StartPageID
		
		#region ContributorCreationDate
		
		private DateTime? _ContributorCreationDate = null;
		
		/// <summary>
		/// Column: ContributorCreationDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ContributorCreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=22, IsNullable=true)]
		public DateTime? ContributorCreationDate
		{
			get
			{
				return _ContributorCreationDate;
			}
			set
			{
				if (_ContributorCreationDate != value)
				{
					_ContributorCreationDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContributorCreationDate
		
		#region ContributorLastModifiedDate
		
		private DateTime? _ContributorLastModifiedDate = null;
		
		/// <summary>
		/// Column: ContributorLastModifiedDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ContributorLastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=23, IsNullable=true)]
		public DateTime? ContributorLastModifiedDate
		{
			get
			{
				return _ContributorLastModifiedDate;
			}
			set
			{
				if (_ContributorLastModifiedDate != value)
				{
					_ContributorLastModifiedDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContributorLastModifiedDate
		
		#region BHLSegmentID
		
		private int? _BHLSegmentID = null;
		
		/// <summary>
		/// Column: BHLSegmentID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("BHLSegmentID", DbTargetType=SqlDbType.Int, Ordinal=24, NumericPrecision=10, IsNullable=true)]
		public int? BHLSegmentID
		{
			get
			{
				return _BHLSegmentID;
			}
			set
			{
				if (_BHLSegmentID != value)
				{
					_BHLSegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BHLSegmentID
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=25)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=26)]
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
		
		#region ContributorName
		
		private string _ContributorName = string.Empty;
		
		/// <summary>
		/// Column: ContributorName;
		/// DBMS data type: nvarchar(255);
		/// </summary>
		[ColumnDefinition("ContributorName", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=255)]
		public string ContributorName
		{
			get
			{
				return _ContributorName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_ContributorName != value)
				{
					_ContributorName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ContributorName
		
		#region SegmentStatusID
		
		private int _SegmentStatusID = default(int);
		
		/// <summary>
		/// Column: SegmentStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentStatusID", DbTargetType=SqlDbType.Int, Ordinal=28, NumericPrecision=10, IsInForeignKey=true)]
		public int SegmentStatusID
		{
			get
			{
				return _SegmentStatusID;
			}
			set
			{
				if (_SegmentStatusID != value)
				{
					_SegmentStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentStatusID
			
		#endregion Properties

		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__BSSegment"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__BSSegment"/>, 
		/// returns an instance of <see cref="__BSSegment"/>; otherwise returns null.</returns>
		public static new __BSSegment FromArray(byte[] byteArray)
		{
			__BSSegment o = null;
			
			try
			{
				o = (__BSSegment) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__BSSegment"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__BSSegment"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __BSSegment)
			{
				__BSSegment o = (__BSSegment) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentID == SegmentID &&
					o.ItemID == ItemID &&
					GetComparisonString(o.BioStorReferenceID) == GetComparisonString(BioStorReferenceID) &&
					o.SequenceOrder == SequenceOrder &&
					GetComparisonString(o.Genre) == GetComparisonString(Genre) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.ContainerTitle) == GetComparisonString(ContainerTitle) &&
					GetComparisonString(o.PublisherName) == GetComparisonString(PublisherName) &&
					GetComparisonString(o.PublisherPlace) == GetComparisonString(PublisherPlace) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Series) == GetComparisonString(Series) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.Year) == GetComparisonString(Year) &&
					GetComparisonString(o.Date) == GetComparisonString(Date) &&
					GetComparisonString(o.ISSN) == GetComparisonString(ISSN) &&
					GetComparisonString(o.DOI) == GetComparisonString(DOI) &&
					GetComparisonString(o.OCLC) == GetComparisonString(OCLC) &&
					GetComparisonString(o.JSTOR) == GetComparisonString(JSTOR) &&
					GetComparisonString(o.StartPageNumber) == GetComparisonString(StartPageNumber) &&
					GetComparisonString(o.EndPageNumber) == GetComparisonString(EndPageNumber) &&
					o.StartPageID == StartPageID &&
					o.ContributorCreationDate == ContributorCreationDate &&
					o.ContributorLastModifiedDate == ContributorLastModifiedDate &&
					o.BHLSegmentID == BHLSegmentID &&
					o.CreationDate == CreationDate &&
					o.LastModifiedDate == LastModifiedDate &&
					GetComparisonString(o.ContributorName) == GetComparisonString(ContributorName) &&
					o.SegmentStatusID == SegmentStatusID 
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
				throw new ArgumentException("Argument is not of type __BSSegment");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__BSSegment"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BSSegment"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__BSSegment a, __BSSegment b)
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
		/// <param name="a">The first <see cref="__BSSegment"/> object to compare.</param>
		/// <param name="b">The second <see cref="__BSSegment"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__BSSegment a, __BSSegment b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__BSSegment"/> object to compare with the current <see cref="__BSSegment"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __BSSegment))
			{
				return false;
			}
			
			return this == (__BSSegment) obj;
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
		/// list.Sort(SortOrder.Ascending, __BSSegment.SortColumn.SegmentID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentID = "SegmentID";	
			public const string ItemID = "ItemID";	
			public const string BioStorReferenceID = "BioStorReferenceID";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string Genre = "Genre";	
			public const string Title = "Title";	
			public const string ContainerTitle = "ContainerTitle";	
			public const string PublisherName = "PublisherName";	
			public const string PublisherPlace = "PublisherPlace";	
			public const string Volume = "Volume";	
			public const string Series = "Series";	
			public const string Issue = "Issue";	
			public const string Year = "Year";	
			public const string Date = "Date";	
			public const string ISSN = "ISSN";	
			public const string DOI = "DOI";	
			public const string OCLC = "OCLC";	
			public const string JSTOR = "JSTOR";	
			public const string StartPageNumber = "StartPageNumber";	
			public const string EndPageNumber = "EndPageNumber";	
			public const string StartPageID = "StartPageID";	
			public const string ContributorCreationDate = "ContributorCreationDate";	
			public const string ContributorLastModifiedDate = "ContributorLastModifiedDate";	
			public const string BHLSegmentID = "BHLSegmentID";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string ContributorName = "ContributorName";	
			public const string SegmentStatusID = "SegmentStatusID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

