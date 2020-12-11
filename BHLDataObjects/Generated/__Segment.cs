
// Generated 10/28/2020 3:25:03 PM
// Do not modify the contents of this code file.
// This abstract class __Segment is based upon dbo.Segment.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class Segment : __Segment
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
	public abstract class __Segment : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __Segment()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="segmentID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectSegmentID"></param>
		/// <param name="segmentGenreID"></param>
		/// <param name="startPageID"></param>
		/// <param name="thumbnailPageID"></param>
		/// <param name="languageCode"></param>
		/// <param name="barCode"></param>
		/// <param name="mARCItemID"></param>
		/// <param name="title"></param>
		/// <param name="sortTitle"></param>
		/// <param name="translatedTitle"></param>
		/// <param name="containerTitle"></param>
		/// <param name="publicationDetails"></param>
		/// <param name="publisherName"></param>
		/// <param name="publisherPlace"></param>
		/// <param name="summary"></param>
		/// <param name="volume"></param>
		/// <param name="series"></param>
		/// <param name="issue"></param>
		/// <param name="edition"></param>
		/// <param name="date"></param>
		/// <param name="pageRange"></param>
		/// <param name="startPageNumber"></param>
		/// <param name="endPageNumber"></param>
		/// <param name="url"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="licenseName"></param>
		/// <param name="licenseUrl"></param>
		/// <param name="rightsStatus"></param>
		/// <param name="rightsStatement"></param>
		/// <param name="copyrightStatus"></param>
		/// <param name="copyrightRegion"></param>
		/// <param name="copyrightComment"></param>
		/// <param name="copyrightEvidence"></param>
		/// <param name="scanningUser"></param>
		/// <param name="scanningDate"></param>
		/// <param name="paginationStatusID"></param>
		/// <param name="paginationStatusDate"></param>
		/// <param name="paginationStatusUserID"></param>
		/// <param name="paginationCompleteDate"></param>
		/// <param name="paginationCompleteUserID"></param>
		/// <param name="lastPageNameLookupDate"></param>
		/// <param name="creationDate"></param>
		/// <param name="lastModifiedDate"></param>
		/// <param name="creationUserID"></param>
		/// <param name="lastModifiedUserID"></param>
		public __Segment(int segmentID, 
			int itemID, 
			int? redirectSegmentID, 
			int segmentGenreID, 
			int? startPageID, 
			int? thumbnailPageID, 
			string languageCode, 
			string barCode, 
			string mARCItemID, 
			string title, 
			string sortTitle, 
			string translatedTitle, 
			string containerTitle, 
			string publicationDetails, 
			string publisherName, 
			string publisherPlace, 
			string summary, 
			string volume, 
			string series, 
			string issue, 
			string edition, 
			string date, 
			string pageRange, 
			string startPageNumber, 
			string endPageNumber, 
			string url, 
			string downloadUrl, 
			string licenseName, 
			string licenseUrl, 
			string rightsStatus, 
			string rightsStatement, 
			string copyrightStatus, 
			string copyrightRegion, 
			string copyrightComment, 
			string copyrightEvidence, 
			string scanningUser, 
			DateTime? scanningDate, 
			int? paginationStatusID, 
			DateTime? paginationStatusDate, 
			int? paginationStatusUserID, 
			DateTime? paginationCompleteDate, 
			int? paginationCompleteUserID, 
			DateTime? lastPageNameLookupDate, 
			DateTime creationDate, 
			DateTime lastModifiedDate, 
			int? creationUserID, 
			int? lastModifiedUserID) : this()
		{
			_SegmentID = segmentID;
			ItemID = itemID;
			RedirectSegmentID = redirectSegmentID;
			SegmentGenreID = segmentGenreID;
			StartPageID = startPageID;
			ThumbnailPageID = thumbnailPageID;
			LanguageCode = languageCode;
			BarCode = barCode;
			MARCItemID = mARCItemID;
			Title = title;
			SortTitle = sortTitle;
			TranslatedTitle = translatedTitle;
			ContainerTitle = containerTitle;
			PublicationDetails = publicationDetails;
			PublisherName = publisherName;
			PublisherPlace = publisherPlace;
			Summary = summary;
			Volume = volume;
			Series = series;
			Issue = issue;
			Edition = edition;
			Date = date;
			PageRange = pageRange;
			StartPageNumber = startPageNumber;
			EndPageNumber = endPageNumber;
			Url = url;
			DownloadUrl = downloadUrl;
			LicenseName = licenseName;
			LicenseUrl = licenseUrl;
			RightsStatus = rightsStatus;
			RightsStatement = rightsStatement;
			CopyrightStatus = copyrightStatus;
			CopyrightRegion = copyrightRegion;
			CopyrightComment = copyrightComment;
			CopyrightEvidence = copyrightEvidence;
			ScanningUser = scanningUser;
			ScanningDate = scanningDate;
			PaginationStatusID = paginationStatusID;
			PaginationStatusDate = paginationStatusDate;
			PaginationStatusUserID = paginationStatusUserID;
			PaginationCompleteDate = paginationCompleteDate;
			PaginationCompleteUserID = paginationCompleteUserID;
			LastPageNameLookupDate = lastPageNameLookupDate;
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
		~__Segment()
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
						_ItemID = Utility.ZeroIfNull(column.Value);
						break;
					}
					case "RedirectSegmentID" :
					{
						_RedirectSegmentID = (int?)column.Value;
						break;
					}
					case "SegmentGenreID" :
					{
						_SegmentGenreID = (int)column.Value;
						break;
					}
					case "StartPageID" :
					{
						_StartPageID = (int?)column.Value;
						break;
					}
					case "ThumbnailPageID" :
					{
						_ThumbnailPageID = (int?)column.Value;
						break;
					}
					case "LanguageCode" :
					{
						_LanguageCode = (string)column.Value;
						break;
					}
					case "BarCode" :
					{
						_BarCode = (string)column.Value;
						break;
					}
					case "MARCItemID" :
					{
						_MARCItemID = (string)column.Value;
						break;
					}
					case "Title" :
					{
						_Title = (string)column.Value;
						break;
					}
					case "SortTitle" :
					{
						_SortTitle = (string)column.Value;
						break;
					}
					case "TranslatedTitle" :
					{
						_TranslatedTitle = (string)column.Value;
						break;
					}
					case "ContainerTitle" :
					{
						_ContainerTitle = (string)column.Value;
						break;
					}
					case "PublicationDetails" :
					{
						_PublicationDetails = (string)column.Value;
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
					case "Summary" :
					{
						_Summary = (string)column.Value;
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
					case "Edition" :
					{
						_Edition = (string)column.Value;
						break;
					}
					case "Date" :
					{
						_Date = (string)column.Value;
						break;
					}
					case "PageRange" :
					{
						_PageRange = (string)column.Value;
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
					case "Url" :
					{
						_Url = (string)column.Value;
						break;
					}
					case "DownloadUrl" :
					{
						_DownloadUrl = (string)column.Value;
						break;
					}
					case "LicenseName" :
					{
						_LicenseName = (string)column.Value;
						break;
					}
					case "LicenseUrl" :
					{
						_LicenseUrl = (string)column.Value;
						break;
					}
					case "RightsStatus" :
					{
						_RightsStatus = (string)column.Value;
						break;
					}
					case "RightsStatement" :
					{
						_RightsStatement = (string)column.Value;
						break;
					}
					case "CopyrightStatus" :
					{
						_CopyrightStatus = (string)column.Value;
						break;
					}
					case "CopyrightRegion" :
					{
						_CopyrightRegion = (string)column.Value;
						break;
					}
					case "CopyrightComment" :
					{
						_CopyrightComment = (string)column.Value;
						break;
					}
					case "CopyrightEvidence" :
					{
						_CopyrightEvidence = (string)column.Value;
						break;
					}
					case "ScanningUser" :
					{
						_ScanningUser = (string)column.Value;
						break;
					}
					case "ScanningDate" :
					{
						_ScanningDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationStatusID" :
					{
						_PaginationStatusID = (int?)column.Value;
						break;
					}
					case "PaginationStatusDate" :
					{
						_PaginationStatusDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationStatusUserID" :
					{
						_PaginationStatusUserID = (int?)column.Value;
						break;
					}
					case "PaginationCompleteDate" :
					{
						_PaginationCompleteDate = (DateTime?)column.Value;
						break;
					}
					case "PaginationCompleteUserID" :
					{
						_PaginationCompleteUserID = (int?)column.Value;
						break;
					}
					case "LastPageNameLookupDate" :
					{
						_LastPageNameLookupDate = (DateTime?)column.Value;
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
		
		#region RedirectSegmentID
		
		private int? _RedirectSegmentID = null;
		
		/// <summary>
		/// Column: RedirectSegmentID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectSegmentID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? RedirectSegmentID
		{
			get
			{
				return _RedirectSegmentID;
			}
			set
			{
				if (_RedirectSegmentID != value)
				{
					_RedirectSegmentID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectSegmentID
		
		#region SegmentGenreID
		
		private int _SegmentGenreID = default(int);
		
		/// <summary>
		/// Column: SegmentGenreID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("SegmentGenreID", DbTargetType=SqlDbType.Int, Ordinal=4, NumericPrecision=10, IsInForeignKey=true)]
		public int SegmentGenreID
		{
			get
			{
				return _SegmentGenreID;
			}
			set
			{
				if (_SegmentGenreID != value)
				{
					_SegmentGenreID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SegmentGenreID
		
		#region StartPageID
		
		private int? _StartPageID = null;
		
		/// <summary>
		/// Column: StartPageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("StartPageID", DbTargetType=SqlDbType.Int, Ordinal=5, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region ThumbnailPageID
		
		private int? _ThumbnailPageID = null;
		
		/// <summary>
		/// Column: ThumbnailPageID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("ThumbnailPageID", DbTargetType=SqlDbType.Int, Ordinal=6, NumericPrecision=10, IsNullable=true)]
		public int? ThumbnailPageID
		{
			get
			{
				return _ThumbnailPageID;
			}
			set
			{
				if (_ThumbnailPageID != value)
				{
					_ThumbnailPageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ThumbnailPageID
		
		#region LanguageCode
		
		private string _LanguageCode = null;
		
		/// <summary>
		/// Column: LanguageCode;
		/// DBMS data type: nvarchar(10); Nullable;
		/// </summary>
		[ColumnDefinition("LanguageCode", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=10, IsInForeignKey=true, IsNullable=true)]
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
		
		#region BarCode
		
		private string _BarCode = null;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(200); Nullable;
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=200, IsNullable=true)]
		public string BarCode
		{
			get
			{
				return _BarCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_BarCode != value)
				{
					_BarCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BarCode
		
		#region MARCItemID
		
		private string _MARCItemID = null;
		
		/// <summary>
		/// Column: MARCItemID;
		/// DBMS data type: nvarchar(50); Nullable;
		/// </summary>
		[ColumnDefinition("MARCItemID", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=50, IsNullable=true)]
		public string MARCItemID
		{
			get
			{
				return _MARCItemID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MARCItemID != value)
				{
					_MARCItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCItemID
		
		#region Title
		
		private string _Title = string.Empty;
		
		/// <summary>
		/// Column: Title;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("Title", DbTargetType=SqlDbType.NVarChar, Ordinal=10, CharacterMaxLength=2000)]
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
		
		#region SortTitle
		
		private string _SortTitle = string.Empty;
		
		/// <summary>
		/// Column: SortTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("SortTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=11, CharacterMaxLength=2000)]
		public string SortTitle
		{
			get
			{
				return _SortTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_SortTitle != value)
				{
					_SortTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SortTitle
		
		#region TranslatedTitle
		
		private string _TranslatedTitle = string.Empty;
		
		/// <summary>
		/// Column: TranslatedTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("TranslatedTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=12, CharacterMaxLength=2000)]
		public string TranslatedTitle
		{
			get
			{
				return _TranslatedTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_TranslatedTitle != value)
				{
					_TranslatedTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TranslatedTitle
		
		#region ContainerTitle
		
		private string _ContainerTitle = string.Empty;
		
		/// <summary>
		/// Column: ContainerTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("ContainerTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=13, CharacterMaxLength=2000)]
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
		
		#region PublicationDetails
		
		private string _PublicationDetails = string.Empty;
		
		/// <summary>
		/// Column: PublicationDetails;
		/// DBMS data type: nvarchar(400);
		/// </summary>
		[ColumnDefinition("PublicationDetails", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=400)]
		public string PublicationDetails
		{
			get
			{
				return _PublicationDetails;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 400);
				if (_PublicationDetails != value)
				{
					_PublicationDetails = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PublicationDetails
		
		#region PublisherName
		
		private string _PublisherName = string.Empty;
		
		/// <summary>
		/// Column: PublisherName;
		/// DBMS data type: nvarchar(250);
		/// </summary>
		[ColumnDefinition("PublisherName", DbTargetType=SqlDbType.NVarChar, Ordinal=15, CharacterMaxLength=250)]
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
		[ColumnDefinition("PublisherPlace", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=150)]
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
		
		#region Summary
		
		private string _Summary = string.Empty;
		
		/// <summary>
		/// Column: Summary;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("Summary", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=1073741823)]
		public string Summary
		{
			get
			{
				return _Summary;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_Summary != value)
				{
					_Summary = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Summary
		
		#region Volume
		
		private string _Volume = string.Empty;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=18, CharacterMaxLength=100)]
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
		[ColumnDefinition("Series", DbTargetType=SqlDbType.NVarChar, Ordinal=19, CharacterMaxLength=100)]
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
		[ColumnDefinition("Issue", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=100)]
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
		
		#region Edition
		
		private string _Edition = string.Empty;
		
		/// <summary>
		/// Column: Edition;
		/// DBMS data type: nvarchar(400);
		/// </summary>
		[ColumnDefinition("Edition", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=400)]
		public string Edition
		{
			get
			{
				return _Edition;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 400);
				if (_Edition != value)
				{
					_Edition = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Edition
		
		#region Date
		
		private string _Date = string.Empty;
		
		/// <summary>
		/// Column: Date;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("Date", DbTargetType=SqlDbType.NVarChar, Ordinal=22, CharacterMaxLength=20)]
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
		
		#region PageRange
		
		private string _PageRange = string.Empty;
		
		/// <summary>
		/// Column: PageRange;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("PageRange", DbTargetType=SqlDbType.NVarChar, Ordinal=23, CharacterMaxLength=50)]
		public string PageRange
		{
			get
			{
				return _PageRange;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_PageRange != value)
				{
					_PageRange = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageRange
		
		#region StartPageNumber
		
		private string _StartPageNumber = string.Empty;
		
		/// <summary>
		/// Column: StartPageNumber;
		/// DBMS data type: nvarchar(20);
		/// </summary>
		[ColumnDefinition("StartPageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=24, CharacterMaxLength=20)]
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
		[ColumnDefinition("EndPageNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=20)]
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
		
		#region Url
		
		private string _Url = string.Empty;
		
		/// <summary>
		/// Column: Url;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("Url", DbTargetType=SqlDbType.NVarChar, Ordinal=26, CharacterMaxLength=200)]
		public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_Url != value)
				{
					_Url = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Url
		
		#region DownloadUrl
		
		private string _DownloadUrl = string.Empty;
		
		/// <summary>
		/// Column: DownloadUrl;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("DownloadUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=200)]
		public string DownloadUrl
		{
			get
			{
				return _DownloadUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_DownloadUrl != value)
				{
					_DownloadUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DownloadUrl
		
		#region LicenseName
		
		private string _LicenseName = string.Empty;
		
		/// <summary>
		/// Column: LicenseName;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("LicenseName", DbTargetType=SqlDbType.NVarChar, Ordinal=28, CharacterMaxLength=200)]
		public string LicenseName
		{
			get
			{
				return _LicenseName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_LicenseName != value)
				{
					_LicenseName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LicenseName
		
		#region LicenseUrl
		
		private string _LicenseUrl = string.Empty;
		
		/// <summary>
		/// Column: LicenseUrl;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("LicenseUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=200)]
		public string LicenseUrl
		{
			get
			{
				return _LicenseUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_LicenseUrl != value)
				{
					_LicenseUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LicenseUrl
		
		#region RightsStatus
		
		private string _RightsStatus = string.Empty;
		
		/// <summary>
		/// Column: RightsStatus;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("RightsStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=500)]
		public string RightsStatus
		{
			get
			{
				return _RightsStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_RightsStatus != value)
				{
					_RightsStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RightsStatus
		
		#region RightsStatement
		
		private string _RightsStatement = string.Empty;
		
		/// <summary>
		/// Column: RightsStatement;
		/// DBMS data type: nvarchar(500);
		/// </summary>
		[ColumnDefinition("RightsStatement", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=500)]
		public string RightsStatement
		{
			get
			{
				return _RightsStatement;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_RightsStatement != value)
				{
					_RightsStatement = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RightsStatement
		
		#region CopyrightStatus
		
		private string _CopyrightStatus = string.Empty;
		
		/// <summary>
		/// Column: CopyrightStatus;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CopyrightStatus", DbTargetType=SqlDbType.NVarChar, Ordinal=32, CharacterMaxLength=1073741823)]
		public string CopyrightStatus
		{
			get
			{
				return _CopyrightStatus;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CopyrightStatus != value)
				{
					_CopyrightStatus = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightStatus
		
		#region CopyrightRegion
		
		private string _CopyrightRegion = string.Empty;
		
		/// <summary>
		/// Column: CopyrightRegion;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("CopyrightRegion", DbTargetType=SqlDbType.NVarChar, Ordinal=33, CharacterMaxLength=50)]
		public string CopyrightRegion
		{
			get
			{
				return _CopyrightRegion;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_CopyrightRegion != value)
				{
					_CopyrightRegion = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightRegion
		
		#region CopyrightComment
		
		private string _CopyrightComment = string.Empty;
		
		/// <summary>
		/// Column: CopyrightComment;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CopyrightComment", DbTargetType=SqlDbType.NVarChar, Ordinal=34, CharacterMaxLength=1073741823)]
		public string CopyrightComment
		{
			get
			{
				return _CopyrightComment;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CopyrightComment != value)
				{
					_CopyrightComment = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightComment
		
		#region CopyrightEvidence
		
		private string _CopyrightEvidence = string.Empty;
		
		/// <summary>
		/// Column: CopyrightEvidence;
		/// DBMS data type: nvarchar(MAX);
		/// </summary>
		[ColumnDefinition("CopyrightEvidence", DbTargetType=SqlDbType.NVarChar, Ordinal=35, CharacterMaxLength=1073741823)]
		public string CopyrightEvidence
		{
			get
			{
				return _CopyrightEvidence;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1073741823);
				if (_CopyrightEvidence != value)
				{
					_CopyrightEvidence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion CopyrightEvidence
		
		#region ScanningUser
		
		private string _ScanningUser = null;
		
		/// <summary>
		/// Column: ScanningUser;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("ScanningUser", DbTargetType=SqlDbType.NVarChar, Ordinal=36, CharacterMaxLength=100, IsNullable=true)]
		public string ScanningUser
		{
			get
			{
				return _ScanningUser;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_ScanningUser != value)
				{
					_ScanningUser = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningUser
		
		#region ScanningDate
		
		private DateTime? _ScanningDate = null;
		
		/// <summary>
		/// Column: ScanningDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("ScanningDate", DbTargetType=SqlDbType.DateTime, Ordinal=37, IsNullable=true)]
		public DateTime? ScanningDate
		{
			get
			{
				return _ScanningDate;
			}
			set
			{
				if (_ScanningDate != value)
				{
					_ScanningDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ScanningDate
		
		#region PaginationStatusID
		
		private int? _PaginationStatusID = null;
		
		/// <summary>
		/// Column: PaginationStatusID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusID", DbTargetType=SqlDbType.Int, Ordinal=38, NumericPrecision=10, IsInForeignKey=true, IsNullable=true)]
		public int? PaginationStatusID
		{
			get
			{
				return _PaginationStatusID;
			}
			set
			{
				if (_PaginationStatusID != value)
				{
					_PaginationStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationStatusID
		
		#region PaginationStatusDate
		
		private DateTime? _PaginationStatusDate = null;
		
		/// <summary>
		/// Column: PaginationStatusDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusDate", DbTargetType=SqlDbType.DateTime, Ordinal=39, IsNullable=true)]
		public DateTime? PaginationStatusDate
		{
			get
			{
				return _PaginationStatusDate;
			}
			set
			{
				if (_PaginationStatusDate != value)
				{
					_PaginationStatusDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationStatusDate
		
		#region PaginationStatusUserID
		
		private int? _PaginationStatusUserID = null;
		
		/// <summary>
		/// Column: PaginationStatusUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationStatusUserID", DbTargetType=SqlDbType.Int, Ordinal=40, NumericPrecision=10, IsNullable=true)]
		public int? PaginationStatusUserID
		{
			get
			{
				return _PaginationStatusUserID;
			}
			set
			{
				if (_PaginationStatusUserID != value)
				{
					_PaginationStatusUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationStatusUserID
		
		#region PaginationCompleteDate
		
		private DateTime? _PaginationCompleteDate = null;
		
		/// <summary>
		/// Column: PaginationCompleteDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteDate", DbTargetType=SqlDbType.DateTime, Ordinal=41, IsNullable=true)]
		public DateTime? PaginationCompleteDate
		{
			get
			{
				return _PaginationCompleteDate;
			}
			set
			{
				if (_PaginationCompleteDate != value)
				{
					_PaginationCompleteDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationCompleteDate
		
		#region PaginationCompleteUserID
		
		private int? _PaginationCompleteUserID = null;
		
		/// <summary>
		/// Column: PaginationCompleteUserID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PaginationCompleteUserID", DbTargetType=SqlDbType.Int, Ordinal=42, NumericPrecision=10, IsNullable=true)]
		public int? PaginationCompleteUserID
		{
			get
			{
				return _PaginationCompleteUserID;
			}
			set
			{
				if (_PaginationCompleteUserID != value)
				{
					_PaginationCompleteUserID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PaginationCompleteUserID
		
		#region LastPageNameLookupDate
		
		private DateTime? _LastPageNameLookupDate = null;
		
		/// <summary>
		/// Column: LastPageNameLookupDate;
		/// DBMS data type: datetime; Nullable;
		/// </summary>
		[ColumnDefinition("LastPageNameLookupDate", DbTargetType=SqlDbType.DateTime, Ordinal=43, IsNullable=true)]
		public DateTime? LastPageNameLookupDate
		{
			get
			{
				return _LastPageNameLookupDate;
			}
			set
			{
				if (_LastPageNameLookupDate != value)
				{
					_LastPageNameLookupDate = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion LastPageNameLookupDate
		
		#region CreationDate
		
		private DateTime _CreationDate;
		
		/// <summary>
		/// Column: CreationDate;
		/// DBMS data type: datetime;
		/// </summary>
		[ColumnDefinition("CreationDate", DbTargetType=SqlDbType.DateTime, Ordinal=44)]
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
		[ColumnDefinition("LastModifiedDate", DbTargetType=SqlDbType.DateTime, Ordinal=45)]
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
		[ColumnDefinition("CreationUserID", DbTargetType=SqlDbType.Int, Ordinal=46, NumericPrecision=10, IsNullable=true)]
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
		[ColumnDefinition("LastModifiedUserID", DbTargetType=SqlDbType.Int, Ordinal=47, NumericPrecision=10, IsNullable=true)]
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
		/// Deserializes the byte array and returns an instance of <see cref="__Segment"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__Segment"/>, 
		/// returns an instance of <see cref="__Segment"/>; otherwise returns null.</returns>
		public static new __Segment FromArray(byte[] byteArray)
		{
			__Segment o = null;
			
			try
			{
				o = (__Segment) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__Segment"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__Segment"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __Segment)
			{
				__Segment o = (__Segment) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					o.SegmentID == SegmentID &&
					o.ItemID == ItemID &&
					o.RedirectSegmentID == RedirectSegmentID &&
					o.SegmentGenreID == SegmentGenreID &&
					o.StartPageID == StartPageID &&
					o.ThumbnailPageID == ThumbnailPageID &&
					GetComparisonString(o.LanguageCode) == GetComparisonString(LanguageCode) &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					GetComparisonString(o.MARCItemID) == GetComparisonString(MARCItemID) &&
					GetComparisonString(o.Title) == GetComparisonString(Title) &&
					GetComparisonString(o.SortTitle) == GetComparisonString(SortTitle) &&
					GetComparisonString(o.TranslatedTitle) == GetComparisonString(TranslatedTitle) &&
					GetComparisonString(o.ContainerTitle) == GetComparisonString(ContainerTitle) &&
					GetComparisonString(o.PublicationDetails) == GetComparisonString(PublicationDetails) &&
					GetComparisonString(o.PublisherName) == GetComparisonString(PublisherName) &&
					GetComparisonString(o.PublisherPlace) == GetComparisonString(PublisherPlace) &&
					GetComparisonString(o.Summary) == GetComparisonString(Summary) &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.Series) == GetComparisonString(Series) &&
					GetComparisonString(o.Issue) == GetComparisonString(Issue) &&
					GetComparisonString(o.Edition) == GetComparisonString(Edition) &&
					GetComparisonString(o.Date) == GetComparisonString(Date) &&
					GetComparisonString(o.PageRange) == GetComparisonString(PageRange) &&
					GetComparisonString(o.StartPageNumber) == GetComparisonString(StartPageNumber) &&
					GetComparisonString(o.EndPageNumber) == GetComparisonString(EndPageNumber) &&
					GetComparisonString(o.Url) == GetComparisonString(Url) &&
					GetComparisonString(o.DownloadUrl) == GetComparisonString(DownloadUrl) &&
					GetComparisonString(o.LicenseName) == GetComparisonString(LicenseName) &&
					GetComparisonString(o.LicenseUrl) == GetComparisonString(LicenseUrl) &&
					GetComparisonString(o.RightsStatus) == GetComparisonString(RightsStatus) &&
					GetComparisonString(o.RightsStatement) == GetComparisonString(RightsStatement) &&
					GetComparisonString(o.CopyrightStatus) == GetComparisonString(CopyrightStatus) &&
					GetComparisonString(o.CopyrightRegion) == GetComparisonString(CopyrightRegion) &&
					GetComparisonString(o.CopyrightComment) == GetComparisonString(CopyrightComment) &&
					GetComparisonString(o.CopyrightEvidence) == GetComparisonString(CopyrightEvidence) &&
					GetComparisonString(o.ScanningUser) == GetComparisonString(ScanningUser) &&
					o.ScanningDate == ScanningDate &&
					o.PaginationStatusID == PaginationStatusID &&
					o.PaginationStatusDate == PaginationStatusDate &&
					o.PaginationStatusUserID == PaginationStatusUserID &&
					o.PaginationCompleteDate == PaginationCompleteDate &&
					o.PaginationCompleteUserID == PaginationCompleteUserID &&
					o.LastPageNameLookupDate == LastPageNameLookupDate &&
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
				throw new ArgumentException("Argument is not of type __Segment");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__Segment"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Segment"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__Segment a, __Segment b)
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
		/// <param name="a">The first <see cref="__Segment"/> object to compare.</param>
		/// <param name="b">The second <see cref="__Segment"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__Segment a, __Segment b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__Segment"/> object to compare with the current <see cref="__Segment"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __Segment))
			{
				return false;
			}
			
			return this == (__Segment) obj;
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
		/// list.Sort(SortOrder.Ascending, __Segment.SortColumn.SegmentID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string SegmentID = "SegmentID";	
			public const string ItemID = "ItemID";	
			public const string RedirectSegmentID = "RedirectSegmentID";	
			public const string SegmentGenreID = "SegmentGenreID";	
			public const string StartPageID = "StartPageID";	
			public const string ThumbnailPageID = "ThumbnailPageID";	
			public const string LanguageCode = "LanguageCode";	
			public const string BarCode = "BarCode";	
			public const string MARCItemID = "MARCItemID";	
			public const string Title = "Title";	
			public const string SortTitle = "SortTitle";	
			public const string TranslatedTitle = "TranslatedTitle";	
			public const string ContainerTitle = "ContainerTitle";	
			public const string PublicationDetails = "PublicationDetails";	
			public const string PublisherName = "PublisherName";	
			public const string PublisherPlace = "PublisherPlace";	
			public const string Summary = "Summary";	
			public const string Volume = "Volume";	
			public const string Series = "Series";	
			public const string Issue = "Issue";	
			public const string Edition = "Edition";	
			public const string Date = "Date";	
			public const string PageRange = "PageRange";	
			public const string StartPageNumber = "StartPageNumber";	
			public const string EndPageNumber = "EndPageNumber";	
			public const string Url = "Url";	
			public const string DownloadUrl = "DownloadUrl";	
			public const string LicenseName = "LicenseName";	
			public const string LicenseUrl = "LicenseUrl";	
			public const string RightsStatus = "RightsStatus";	
			public const string RightsStatement = "RightsStatement";	
			public const string CopyrightStatus = "CopyrightStatus";	
			public const string CopyrightRegion = "CopyrightRegion";	
			public const string CopyrightComment = "CopyrightComment";	
			public const string CopyrightEvidence = "CopyrightEvidence";	
			public const string ScanningUser = "ScanningUser";	
			public const string ScanningDate = "ScanningDate";	
			public const string PaginationStatusID = "PaginationStatusID";	
			public const string PaginationStatusDate = "PaginationStatusDate";	
			public const string PaginationStatusUserID = "PaginationStatusUserID";	
			public const string PaginationCompleteDate = "PaginationCompleteDate";	
			public const string PaginationCompleteUserID = "PaginationCompleteUserID";	
			public const string LastPageNameLookupDate = "LastPageNameLookupDate";	
			public const string CreationDate = "CreationDate";	
			public const string LastModifiedDate = "LastModifiedDate";	
			public const string CreationUserID = "CreationUserID";	
			public const string LastModifiedUserID = "LastModifiedUserID";
		}
				
		#endregion SortColumn
	}
}
// end of source generation

