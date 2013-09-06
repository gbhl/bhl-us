
// Generated 6/10/2011 1:45:34 PM
// Do not modify the contents of this code file.
// This abstract class __PageSummaryView is based upon PageSummaryView.

#region How To Implement

// To implement, create another code file as outlined in the following example.
// It is recommended the code file you create be in the same project as this code file.
// Example:
// using System;
//
// namespace MOBOT.BHL.DataObjects
// {
//		[Serializable]
// 		public class PageSummaryView : __PageSummaryView
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
	public abstract class __PageSummaryView : CustomObjectBase, ICloneable, IComparable, IDisposable, ISetValues
	{
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public __PageSummaryView()
		{
		}

		/// <summary>
		/// Overloaded constructor specifying each column value.
		/// </summary>
		/// <param name="mARCBibID"></param>
		/// <param name="titleID"></param>
		/// <param name="redirectTitleID"></param>
		/// <param name="fullTitle"></param>
		/// <param name="rareBooks"></param>
		/// <param name="shortTitle"></param>
		/// <param name="sortTitle"></param>
		/// <param name="partNumber"></param>
		/// <param name="partName"></param>
		/// <param name="itemStatusID"></param>
		/// <param name="itemID"></param>
		/// <param name="redirectItemID"></param>
		/// <param name="primaryTitleID"></param>
		/// <param name="barCode"></param>
		/// <param name="pDFSize"></param>
		/// <param name="volume"></param>
		/// <param name="fileRootFolder"></param>
		/// <param name="itemSequence"></param>
		/// <param name="pageID"></param>
		/// <param name="fileNamePrefix"></param>
		/// <param name="pageDescription"></param>
		/// <param name="sequenceOrder"></param>
		/// <param name="illustration"></param>
		/// <param name="active"></param>
		/// <param name="externalURL"></param>
		/// <param name="externalBaseURL"></param>
		/// <param name="altExternalURL"></param>
		/// <param name="webVirtualDirectory"></param>
		/// <param name="oCRFolderShare"></param>
		/// <param name="downloadUrl"></param>
		/// <param name="imageServerUrlFormat"></param>
		public __PageSummaryView(string mARCBibID, 
			int titleID, 
			int? redirectTitleID, 
			string fullTitle, 
			bool rareBooks, 
			string shortTitle, 
			string sortTitle, 
			string partNumber, 
			string partName, 
			int itemStatusID, 
			int itemID, 
			int? redirectItemID, 
			int primaryTitleID, 
			string barCode, 
			int? pDFSize, 
			string volume, 
			string fileRootFolder, 
			short? itemSequence, 
			int pageID, 
			string fileNamePrefix, 
			string pageDescription, 
			int? sequenceOrder, 
			bool illustration, 
			bool active, 
			string externalURL, 
			string externalBaseURL, 
			string altExternalURL, 
			string webVirtualDirectory, 
			string oCRFolderShare, 
			string downloadUrl, 
			string imageServerUrlFormat) : this()
		{
			MARCBibID = mARCBibID;
			TitleID = titleID;
			RedirectTitleID = redirectTitleID;
			FullTitle = fullTitle;
			RareBooks = rareBooks;
			ShortTitle = shortTitle;
			SortTitle = sortTitle;
			PartNumber = partNumber;
			PartName = partName;
			ItemStatusID = itemStatusID;
			ItemID = itemID;
			RedirectItemID = redirectItemID;
			PrimaryTitleID = primaryTitleID;
			BarCode = barCode;
			PDFSize = pDFSize;
			Volume = volume;
			FileRootFolder = fileRootFolder;
			ItemSequence = itemSequence;
			PageID = pageID;
			FileNamePrefix = fileNamePrefix;
			PageDescription = pageDescription;
			SequenceOrder = sequenceOrder;
			Illustration = illustration;
			Active = active;
			ExternalURL = externalURL;
			ExternalBaseURL = externalBaseURL;
			AltExternalURL = altExternalURL;
			WebVirtualDirectory = webVirtualDirectory;
			OCRFolderShare = oCRFolderShare;
			DownloadUrl = downloadUrl;
			ImageServerUrlFormat = imageServerUrlFormat;
		}
		
		#endregion Constructors
		
		#region Destructor
		
		/// <summary>
		///
		/// </summary>
		~__PageSummaryView()
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
					case "MARCBibID" :
					{
						_MARCBibID = (string)column.Value;
						break;
					}
					case "TitleID" :
					{
						_TitleID = (int)column.Value;
						break;
					}
					case "RedirectTitleID" :
					{
						_RedirectTitleID = (int?)column.Value;
						break;
					}
					case "FullTitle" :
					{
						_FullTitle = (string)column.Value;
						break;
					}
					case "RareBooks" :
					{
						_RareBooks = (bool)column.Value;
						break;
					}
					case "ShortTitle" :
					{
						_ShortTitle = (string)column.Value;
						break;
					}
					case "SortTitle" :
					{
						_SortTitle = (string)column.Value;
						break;
					}
					case "PartNumber" :
					{
						_PartNumber = (string)column.Value;
						break;
					}
					case "PartName" :
					{
						_PartName = (string)column.Value;
						break;
					}
					case "ItemStatusID" :
					{
						_ItemStatusID = (int)column.Value;
						break;
					}
					case "ItemID" :
					{
						_ItemID = (int)column.Value;
						break;
					}
					case "RedirectItemID" :
					{
						_RedirectItemID = (int?)column.Value;
						break;
					}
					case "PrimaryTitleID" :
					{
						_PrimaryTitleID = (int)column.Value;
						break;
					}
					case "BarCode" :
					{
						_BarCode = (string)column.Value;
						break;
					}
					case "PDFSize" :
					{
						_PDFSize = (int?)column.Value;
						break;
					}
					case "Volume" :
					{
						_Volume = (string)column.Value;
						break;
					}
					case "FileRootFolder" :
					{
						_FileRootFolder = (string)column.Value;
						break;
					}
					case "ItemSequence" :
					{
						_ItemSequence = (short?)column.Value;
						break;
					}
					case "PageID" :
					{
						_PageID = (int)column.Value;
						break;
					}
					case "FileNamePrefix" :
					{
						_FileNamePrefix = (string)column.Value;
						break;
					}
					case "PageDescription" :
					{
						_PageDescription = (string)column.Value;
						break;
					}
					case "SequenceOrder" :
					{
						_SequenceOrder = (int?)column.Value;
						break;
					}
					case "Illustration" :
					{
						_Illustration = (bool)column.Value;
						break;
					}
					case "Active" :
					{
						_Active = (bool)column.Value;
						break;
					}
					case "ExternalURL" :
					{
						_ExternalURL = (string)column.Value;
						break;
					}
					case "ExternalBaseURL" :
					{
						_ExternalBaseURL = (string)column.Value;
						break;
					}
					case "AltExternalURL" :
					{
						_AltExternalURL = (string)column.Value;
						break;
					}
					case "WebVirtualDirectory" :
					{
						_WebVirtualDirectory = (string)column.Value;
						break;
					}
					case "OCRFolderShare" :
					{
						_OCRFolderShare = (string)column.Value;
						break;
					}
					case "DownloadUrl" :
					{
						_DownloadUrl = (string)column.Value;
						break;
					}
					case "ImageServerUrlFormat" :
					{
						_ImageServerUrlFormat = (string)column.Value;
						break;
					}
				}
			}
			
			IsNew = false;
		}
		
		#endregion Set Values
		
		#region Properties		
		
		#region MARCBibID
		
		private string _MARCBibID = string.Empty;
		
		/// <summary>
		/// Column: MARCBibID;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("MARCBibID", DbTargetType=SqlDbType.NVarChar, Ordinal=1, CharacterMaxLength=50)]
		public string MARCBibID
		{
			get
			{
				return _MARCBibID;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_MARCBibID != value)
				{
					_MARCBibID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion MARCBibID
		
		#region TitleID
		
		private int _TitleID = default(int);
		
		/// <summary>
		/// Column: TitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("TitleID", DbTargetType=SqlDbType.Int, Ordinal=2, NumericPrecision=10)]
		public int TitleID
		{
			get
			{
				return _TitleID;
			}
			set
			{
				if (_TitleID != value)
				{
					_TitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion TitleID
		
		#region RedirectTitleID
		
		private int? _RedirectTitleID = null;
		
		/// <summary>
		/// Column: RedirectTitleID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectTitleID", DbTargetType=SqlDbType.Int, Ordinal=3, NumericPrecision=10, IsNullable=true)]
		public int? RedirectTitleID
		{
			get
			{
				return _RedirectTitleID;
			}
			set
			{
				if (_RedirectTitleID != value)
				{
					_RedirectTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectTitleID
		
		#region FullTitle
		
		private string _FullTitle = string.Empty;
		
		/// <summary>
		/// Column: FullTitle;
		/// DBMS data type: nvarchar(2000);
		/// </summary>
		[ColumnDefinition("FullTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=4, CharacterMaxLength=2000)]
		public string FullTitle
		{
			get
			{
				return _FullTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 2000);
				if (_FullTitle != value)
				{
					_FullTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FullTitle
		
		#region RareBooks
		
		private bool _RareBooks = false;
		
		/// <summary>
		/// Column: RareBooks;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("RareBooks", DbTargetType=SqlDbType.Bit, Ordinal=5)]
		public bool RareBooks
		{
			get
			{
				return _RareBooks;
			}
			set
			{
				if (_RareBooks != value)
				{
					_RareBooks = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RareBooks
		
		#region ShortTitle
		
		private string _ShortTitle = null;
		
		/// <summary>
		/// Column: ShortTitle;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("ShortTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=6, CharacterMaxLength=255, IsNullable=true)]
		public string ShortTitle
		{
			get
			{
				return _ShortTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_ShortTitle != value)
				{
					_ShortTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ShortTitle
		
		#region SortTitle
		
		private string _SortTitle = null;
		
		/// <summary>
		/// Column: SortTitle;
		/// DBMS data type: nvarchar(60); Nullable;
		/// </summary>
		[ColumnDefinition("SortTitle", DbTargetType=SqlDbType.NVarChar, Ordinal=7, CharacterMaxLength=60, IsNullable=true)]
		public string SortTitle
		{
			get
			{
				return _SortTitle;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 60);
				if (_SortTitle != value)
				{
					_SortTitle = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion SortTitle
		
		#region PartNumber
		
		private string _PartNumber = null;
		
		/// <summary>
		/// Column: PartNumber;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PartNumber", DbTargetType=SqlDbType.NVarChar, Ordinal=8, CharacterMaxLength=255, IsNullable=true)]
		public string PartNumber
		{
			get
			{
				return _PartNumber;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PartNumber != value)
				{
					_PartNumber = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PartNumber
		
		#region PartName
		
		private string _PartName = null;
		
		/// <summary>
		/// Column: PartName;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PartName", DbTargetType=SqlDbType.NVarChar, Ordinal=9, CharacterMaxLength=255, IsNullable=true)]
		public string PartName
		{
			get
			{
				return _PartName;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PartName != value)
				{
					_PartName = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PartName
		
		#region ItemStatusID
		
		private int _ItemStatusID = default(int);
		
		/// <summary>
		/// Column: ItemStatusID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemStatusID", DbTargetType=SqlDbType.Int, Ordinal=10, NumericPrecision=10)]
		public int ItemStatusID
		{
			get
			{
				return _ItemStatusID;
			}
			set
			{
				if (_ItemStatusID != value)
				{
					_ItemStatusID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemStatusID
		
		#region ItemID
		
		private int _ItemID = default(int);
		
		/// <summary>
		/// Column: ItemID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("ItemID", DbTargetType=SqlDbType.Int, Ordinal=11, NumericPrecision=10)]
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
		
		#region RedirectItemID
		
		private int? _RedirectItemID = null;
		
		/// <summary>
		/// Column: RedirectItemID;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("RedirectItemID", DbTargetType=SqlDbType.Int, Ordinal=12, NumericPrecision=10, IsNullable=true)]
		public int? RedirectItemID
		{
			get
			{
				return _RedirectItemID;
			}
			set
			{
				if (_RedirectItemID != value)
				{
					_RedirectItemID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion RedirectItemID
		
		#region PrimaryTitleID
		
		private int _PrimaryTitleID = default(int);
		
		/// <summary>
		/// Column: PrimaryTitleID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PrimaryTitleID", DbTargetType=SqlDbType.Int, Ordinal=13, NumericPrecision=10)]
		public int PrimaryTitleID
		{
			get
			{
				return _PrimaryTitleID;
			}
			set
			{
				if (_PrimaryTitleID != value)
				{
					_PrimaryTitleID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PrimaryTitleID
		
		#region BarCode
		
		private string _BarCode = string.Empty;
		
		/// <summary>
		/// Column: BarCode;
		/// DBMS data type: nvarchar(40);
		/// </summary>
		[ColumnDefinition("BarCode", DbTargetType=SqlDbType.NVarChar, Ordinal=14, CharacterMaxLength=40)]
		public string BarCode
		{
			get
			{
				return _BarCode;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 40);
				if (_BarCode != value)
				{
					_BarCode = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion BarCode
		
		#region PDFSize
		
		private int? _PDFSize = null;
		
		/// <summary>
		/// Column: PDFSize;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("PDFSize", DbTargetType=SqlDbType.Int, Ordinal=15, NumericPrecision=10, IsNullable=true)]
		public int? PDFSize
		{
			get
			{
				return _PDFSize;
			}
			set
			{
				if (_PDFSize != value)
				{
					_PDFSize = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PDFSize
		
		#region Volume
		
		private string _Volume = null;
		
		/// <summary>
		/// Column: Volume;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("Volume", DbTargetType=SqlDbType.NVarChar, Ordinal=16, CharacterMaxLength=100, IsNullable=true)]
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
		
		#region FileRootFolder
		
		private string _FileRootFolder = null;
		
		/// <summary>
		/// Column: FileRootFolder;
		/// DBMS data type: nvarchar(250); Nullable;
		/// </summary>
		[ColumnDefinition("FileRootFolder", DbTargetType=SqlDbType.NVarChar, Ordinal=17, CharacterMaxLength=250, IsNullable=true)]
		public string FileRootFolder
		{
			get
			{
				return _FileRootFolder;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 250);
				if (_FileRootFolder != value)
				{
					_FileRootFolder = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileRootFolder
		
		#region ItemSequence
		
		private short? _ItemSequence = null;
		
		/// <summary>
		/// Column: ItemSequence;
		/// DBMS data type: smallint; Nullable;
		/// </summary>
		[ColumnDefinition("ItemSequence", DbTargetType=SqlDbType.SmallInt, Ordinal=18, NumericPrecision=5, IsNullable=true)]
		public short? ItemSequence
		{
			get
			{
				return _ItemSequence;
			}
			set
			{
				if (_ItemSequence != value)
				{
					_ItemSequence = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ItemSequence
		
		#region PageID
		
		private int _PageID = default(int);
		
		/// <summary>
		/// Column: PageID;
		/// DBMS data type: int;
		/// </summary>
		[ColumnDefinition("PageID", DbTargetType=SqlDbType.Int, Ordinal=19, NumericPrecision=10)]
		public int PageID
		{
			get
			{
				return _PageID;
			}
			set
			{
				if (_PageID != value)
				{
					_PageID = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageID
		
		#region FileNamePrefix
		
		private string _FileNamePrefix = string.Empty;
		
		/// <summary>
		/// Column: FileNamePrefix;
		/// DBMS data type: nvarchar(50);
		/// </summary>
		[ColumnDefinition("FileNamePrefix", DbTargetType=SqlDbType.NVarChar, Ordinal=20, CharacterMaxLength=50)]
		public string FileNamePrefix
		{
			get
			{
				return _FileNamePrefix;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 50);
				if (_FileNamePrefix != value)
				{
					_FileNamePrefix = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion FileNamePrefix
		
		#region PageDescription
		
		private string _PageDescription = null;
		
		/// <summary>
		/// Column: PageDescription;
		/// DBMS data type: nvarchar(255); Nullable;
		/// </summary>
		[ColumnDefinition("PageDescription", DbTargetType=SqlDbType.NVarChar, Ordinal=21, CharacterMaxLength=255, IsNullable=true)]
		public string PageDescription
		{
			get
			{
				return _PageDescription;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 255);
				if (_PageDescription != value)
				{
					_PageDescription = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion PageDescription
		
		#region SequenceOrder
		
		private int? _SequenceOrder = null;
		
		/// <summary>
		/// Column: SequenceOrder;
		/// DBMS data type: int; Nullable;
		/// </summary>
		[ColumnDefinition("SequenceOrder", DbTargetType=SqlDbType.Int, Ordinal=22, NumericPrecision=10, IsNullable=true)]
		public int? SequenceOrder
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
		
		#region Illustration
		
		private bool _Illustration = false;
		
		/// <summary>
		/// Column: Illustration;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("Illustration", DbTargetType=SqlDbType.Bit, Ordinal=23)]
		public bool Illustration
		{
			get
			{
				return _Illustration;
			}
			set
			{
				if (_Illustration != value)
				{
					_Illustration = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Illustration
		
		#region Active
		
		private bool _Active = false;
		
		/// <summary>
		/// Column: Active;
		/// DBMS data type: bit;
		/// </summary>
		[ColumnDefinition("Active", DbTargetType=SqlDbType.Bit, Ordinal=24)]
		public bool Active
		{
			get
			{
				return _Active;
			}
			set
			{
				if (_Active != value)
				{
					_Active = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion Active
		
		#region ExternalURL
		
		private string _ExternalURL = null;
		
		/// <summary>
		/// Column: ExternalURL;
		/// DBMS data type: nvarchar(500); Nullable;
		/// </summary>
		[ColumnDefinition("ExternalURL", DbTargetType=SqlDbType.NVarChar, Ordinal=25, CharacterMaxLength=500, IsNullable=true)]
		public string ExternalURL
		{
			get
			{
				return _ExternalURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 500);
				if (_ExternalURL != value)
				{
					_ExternalURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalURL
		
		#region ExternalBaseURL
		
		private string _ExternalBaseURL = null;
		
		/// <summary>
		/// Column: ExternalBaseURL;
		/// DBMS data type: nvarchar(1000); Nullable;
		/// </summary>
		[ColumnDefinition("ExternalBaseURL", DbTargetType=SqlDbType.NVarChar, Ordinal=26, CharacterMaxLength=1000, IsNullable=true)]
		public string ExternalBaseURL
		{
			get
			{
				return _ExternalBaseURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1000);
				if (_ExternalBaseURL != value)
				{
					_ExternalBaseURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ExternalBaseURL
		
		#region AltExternalURL
		
		private string _AltExternalURL = null;
		
		/// <summary>
		/// Column: AltExternalURL;
		/// DBMS data type: nvarchar(1500); Nullable;
		/// </summary>
		[ColumnDefinition("AltExternalURL", DbTargetType=SqlDbType.NVarChar, Ordinal=27, CharacterMaxLength=1500, IsNullable=true)]
		public string AltExternalURL
		{
			get
			{
				return _AltExternalURL;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 1500);
				if (_AltExternalURL != value)
				{
					_AltExternalURL = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion AltExternalURL
		
		#region WebVirtualDirectory
		
		private string _WebVirtualDirectory = null;
		
		/// <summary>
		/// Column: WebVirtualDirectory;
		/// DBMS data type: nvarchar(30); Nullable;
		/// </summary>
		[ColumnDefinition("WebVirtualDirectory", DbTargetType=SqlDbType.NVarChar, Ordinal=28, CharacterMaxLength=30, IsNullable=true)]
		public string WebVirtualDirectory
		{
			get
			{
				return _WebVirtualDirectory;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 30);
				if (_WebVirtualDirectory != value)
				{
					_WebVirtualDirectory = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion WebVirtualDirectory
		
		#region OCRFolderShare
		
		private string _OCRFolderShare = null;
		
		/// <summary>
		/// Column: OCRFolderShare;
		/// DBMS data type: nvarchar(100); Nullable;
		/// </summary>
		[ColumnDefinition("OCRFolderShare", DbTargetType=SqlDbType.NVarChar, Ordinal=29, CharacterMaxLength=100, IsNullable=true)]
		public string OCRFolderShare
		{
			get
			{
				return _OCRFolderShare;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_OCRFolderShare != value)
				{
					_OCRFolderShare = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion OCRFolderShare
		
		#region DownloadUrl
		
		private string _DownloadUrl = string.Empty;
		
		/// <summary>
		/// Column: DownloadUrl;
		/// DBMS data type: nvarchar(100);
		/// </summary>
		[ColumnDefinition("DownloadUrl", DbTargetType=SqlDbType.NVarChar, Ordinal=30, CharacterMaxLength=100)]
		public string DownloadUrl
		{
			get
			{
				return _DownloadUrl;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 100);
				if (_DownloadUrl != value)
				{
					_DownloadUrl = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion DownloadUrl
		
		#region ImageServerUrlFormat
		
		private string _ImageServerUrlFormat = string.Empty;
		
		/// <summary>
		/// Column: ImageServerUrlFormat;
		/// DBMS data type: nvarchar(200);
		/// </summary>
		[ColumnDefinition("ImageServerUrlFormat", DbTargetType=SqlDbType.NVarChar, Ordinal=31, CharacterMaxLength=200)]
		public string ImageServerUrlFormat
		{
			get
			{
				return _ImageServerUrlFormat;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 200);
				if (_ImageServerUrlFormat != value)
				{
					_ImageServerUrlFormat = value;
					_IsDirty = true;
				}
			}
		}
		
		#endregion ImageServerUrlFormat
			
		#endregion Properties
				
		#region From Array serialization
		
		/// <summary>
		/// Deserializes the byte array and returns an instance of <see cref="__PageSummaryView"/>.
		/// </summary>
		/// <returns>If the byte array can be deserialized and cast to an instance of <see cref="__PageSummaryView"/>, 
		/// returns an instance of <see cref="__PageSummaryView"/>; otherwise returns null.</returns>
		public static new __PageSummaryView FromArray(byte[] byteArray)
		{
			__PageSummaryView o = null;
			
			try
			{
				o = (__PageSummaryView) CustomObjectBase.FromArray(byteArray);
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
		/// Compares this instance with a specified object. Throws an ArgumentException if the specified object is not of type <see cref="__PageSummaryView"/>.
		/// </summary>
		/// <param name="obj">An <see cref="__PageSummaryView"/> object to compare with this instance.</param>
		/// <returns>0 if the specified object equals this instance; -1 if the specified object does not equal this instance.</returns>
		public virtual int CompareTo(Object obj)
		{
			if (obj is __PageSummaryView)
			{
				__PageSummaryView o = (__PageSummaryView) obj;
				
				if (
					o.IsNew == IsNew &&
					o.IsDeleted == IsDeleted &&
					GetComparisonString(o.MARCBibID) == GetComparisonString(MARCBibID) &&
					o.TitleID == TitleID &&
					o.RedirectTitleID == RedirectTitleID &&
					GetComparisonString(o.FullTitle) == GetComparisonString(FullTitle) &&
					o.RareBooks == RareBooks &&
					GetComparisonString(o.ShortTitle) == GetComparisonString(ShortTitle) &&
					GetComparisonString(o.SortTitle) == GetComparisonString(SortTitle) &&
					GetComparisonString(o.PartNumber) == GetComparisonString(PartNumber) &&
					GetComparisonString(o.PartName) == GetComparisonString(PartName) &&
					o.ItemStatusID == ItemStatusID &&
					o.ItemID == ItemID &&
					o.RedirectItemID == RedirectItemID &&
					o.PrimaryTitleID == PrimaryTitleID &&
					GetComparisonString(o.BarCode) == GetComparisonString(BarCode) &&
					o.PDFSize == PDFSize &&
					GetComparisonString(o.Volume) == GetComparisonString(Volume) &&
					GetComparisonString(o.FileRootFolder) == GetComparisonString(FileRootFolder) &&
					o.ItemSequence == ItemSequence &&
					o.PageID == PageID &&
					GetComparisonString(o.FileNamePrefix) == GetComparisonString(FileNamePrefix) &&
					GetComparisonString(o.PageDescription) == GetComparisonString(PageDescription) &&
					o.SequenceOrder == SequenceOrder &&
					o.Illustration == Illustration &&
					o.Active == Active &&
					GetComparisonString(o.ExternalURL) == GetComparisonString(ExternalURL) &&
					GetComparisonString(o.ExternalBaseURL) == GetComparisonString(ExternalBaseURL) &&
					GetComparisonString(o.AltExternalURL) == GetComparisonString(AltExternalURL) &&
					GetComparisonString(o.WebVirtualDirectory) == GetComparisonString(WebVirtualDirectory) &&
					GetComparisonString(o.OCRFolderShare) == GetComparisonString(OCRFolderShare) &&
					GetComparisonString(o.DownloadUrl) == GetComparisonString(DownloadUrl) &&
					GetComparisonString(o.ImageServerUrlFormat) == GetComparisonString(ImageServerUrlFormat) 
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
				throw new ArgumentException("Argument is not of type __PageSummaryView");
			}
		}
 		
		#endregion CompareTo
		
		#region Operators
		
		/// <summary>
		/// Equality operator (==) returns true if the values of its operands are equal, false otherwise.
		/// </summary>
		/// <param name="a">The first <see cref="__PageSummaryView"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageSummaryView"/> object to compare.</param>
		/// <returns>true if values of operands are equal, false otherwise.</returns>
		public static bool operator == (__PageSummaryView a, __PageSummaryView b)
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
		/// <param name="a">The first <see cref="__PageSummaryView"/> object to compare.</param>
		/// <param name="b">The second <see cref="__PageSummaryView"/> object to compare.</param>
		/// <returns>false if values of operands are equal, false otherwise.</returns>
		public static bool operator !=(__PageSummaryView a, __PageSummaryView b)
		{
			return !(a == b);
		}
		
		/// <summary>
		/// Returns true the specified object is equal to this object instance, false otherwise.
		/// </summary>
		/// <param name="obj">The <see cref="__PageSummaryView"/> object to compare with the current <see cref="__PageSummaryView"/>.</param>
		/// <returns>true if specified object is equal to the instance of this object, false otherwise.</returns>
		public override bool Equals(Object obj)
		{
			if (!(obj is __PageSummaryView))
			{
				return false;
			}
			
			return this == (__PageSummaryView) obj;
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
		/// list.Sort(SortOrder.Ascending, __PageSummaryView.SortColumn.MARCBibID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{	
			public const string MARCBibID = "MARCBibID";	
			public const string TitleID = "TitleID";	
			public const string RedirectTitleID = "RedirectTitleID";	
			public const string FullTitle = "FullTitle";	
			public const string RareBooks = "RareBooks";	
			public const string ShortTitle = "ShortTitle";	
			public const string SortTitle = "SortTitle";	
			public const string PartNumber = "PartNumber";	
			public const string PartName = "PartName";	
			public const string ItemStatusID = "ItemStatusID";	
			public const string ItemID = "ItemID";	
			public const string RedirectItemID = "RedirectItemID";	
			public const string PrimaryTitleID = "PrimaryTitleID";	
			public const string BarCode = "BarCode";	
			public const string PDFSize = "PDFSize";	
			public const string Volume = "Volume";	
			public const string FileRootFolder = "FileRootFolder";	
			public const string ItemSequence = "ItemSequence";	
			public const string PageID = "PageID";	
			public const string FileNamePrefix = "FileNamePrefix";	
			public const string PageDescription = "PageDescription";	
			public const string SequenceOrder = "SequenceOrder";	
			public const string Illustration = "Illustration";	
			public const string Active = "Active";	
			public const string ExternalURL = "ExternalURL";	
			public const string ExternalBaseURL = "ExternalBaseURL";	
			public const string AltExternalURL = "AltExternalURL";	
			public const string WebVirtualDirectory = "WebVirtualDirectory";	
			public const string OCRFolderShare = "OCRFolderShare";	
			public const string DownloadUrl = "DownloadUrl";	
			public const string ImageServerUrlFormat = "ImageServerUrlFormat";
		}
				
		#endregion SortColumn
	}
}
// end of source generation
