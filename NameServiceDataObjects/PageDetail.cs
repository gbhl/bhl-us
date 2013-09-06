using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects
{
    public class PageDetail : ISetValues
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageDetail()
		{
		}

		public PageDetail(string nameBankID, 
			string nameConfirmed,
            int titleID,
            string marcBibID, 
			string publicationTitle, 
			string publicationDetails,
            string author,
            string bph,
            string tl2,
            string abbreviation,
            string titleUrl,
            int itemID, 
			string barCode, 
			string marcItemID,
            string callNumber,
            string volumeInfo,
            string itemUrl,
            int pageID, 
			string year, 
			string volume,
            string issue,
            string prefix,
            string number,
            string pageUrl,
            string thumbnailUrl,
            string imageUrl,
            string pageTypeName) : this()
		{
            _NameBankID = nameBankID;
            _NameConfirmed = nameConfirmed;
            _TitleID = titleID;
            _MarcBibID = marcBibID;
            _PublicationTitle = publicationTitle;
            _PublicationDetails = publicationDetails;
            _Author = author;
            _BPH = bph;
            _TL2 = tl2;
            _Abbreviation = abbreviation;
            _TitleUrl = titleUrl;
            _ItemID = itemID;
            _BarCode = barCode;
            _MarcItemID = marcItemID;
            _CallNumber= callNumber;
            _VolumeInfo = volumeInfo;
            _ItemUrl = itemUrl;
            _PageID = pageID;
            _Year = year;
            _Volume = volume;
            _Issue = issue;
            _Prefix = prefix;
            _Number = number;
            _PageUrl = pageUrl;
            _ThumbnailUrl = thumbnailUrl;
            _ImageUrl = imageUrl;
            _PageTypeName = pageTypeName;
		}

		#endregion Constructors
		
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
                    case "NameBankID":
                        {
                            _NameBankID = (string)column.Value;
                            break;
                        }
                    case "ResolvedNameString":
                        {
                            _NameConfirmed = (string)column.Value;
                            break;
                        }
                    case "TitleID":
                        {
                            _TitleID = (int)column.Value;
                            break;
                        }
                    case "MARCBibID":
                        {
                            _MarcBibID = (string)column.Value;
                            break;
                        }
                    case "ShortTitle":
                        {
                            _PublicationTitle = (string)column.Value;
                            break;
                        }
                    case "PublicationDetails":
                        {
                            _PublicationDetails = (string)column.Value;
                            break;
                        }
                    case "TL2Author":
                        {
                            _Author = (string)column.Value;
                            break;
                        }
                    case "BPH":
                        {
                            _BPH = (string)column.Value;
                            break;
                        }
                    case "TL2":
                        {
                            _TL2 = (string)column.Value;
                            break;
                        }
                    case "Abbreviation":
                        {
                            _Abbreviation = (string)column.Value;
                            break;
                        }
                    case "TitleURL":
                        {
                            _TitleUrl = (string)column.Value;
                            break;
                        }
                    case "ItemID":
                        {
                            _ItemID = (int)column.Value;
                            break;
                        }
                    case "BarCode":
                        {
                            _BarCode = (string)column.Value;
                            break;
                        }
                    case "MARCItemID":
                        {
                            _MarcItemID = (string)column.Value;
                            break;
                        }
                    case "CallNumber":
                        {
                            _CallNumber = (string)column.Value;
                            break;
                        }
                    case "VolumeInfo":
                        {
                            _VolumeInfo = (string)column.Value;
                            break;
                        }
                    case "ItemURL":
                        {
                            _ItemUrl = (string)column.Value;
                            break;
                        }
                    case "PageID":
                        {
                            _PageID = (int)column.Value;
                            break;
                        }
                    case "Year":
                        {
                            _Year = (string)column.Value;
                            break;
                        }
                    case "Volume":
                        {
                            _Volume = (string)column.Value;
                            break;
                        }
                    case "Issue":
                        {
                            _Issue = (string)column.Value;
                            break;
                        }
                    case "PagePrefix":
                        {
                            _Prefix = (string)column.Value;
                            break;
                        }
                    case "PageNumber":
                        {
                            _Number = (string)column.Value;
                            break;
                        }
                    case "PageURL":
                        {
                            _PageUrl = (string)column.Value;
                            break;
                        }
                    case "ThumbnailURL":
                        {
                            _ThumbnailUrl = (string)column.Value;
                            break;
                        }
                    case "FullSizeImageURL":
                        {
                            _FullSizeImageUrl = (string)column.Value;
                            break;
                        }
                    case "ImageURL":
                        {
                            _ImageUrl = (string)column.Value;
                            break;
                        }
                    case "OcrURL":
                        {
                            _OcrUrl = (string)column.Value;
                            break;
                        }
                    case "PageTypeName":
                        {
                            _PageTypeName = (string)column.Value;
                            break;
                        }
                }
			}
		}
		
		#endregion Set Values
		
		#region Properties

        private string _NameBankID = string.Empty;
        public string NameBankID
        {
            get
            {
                return _NameBankID;
            }
            set
            {
                _NameBankID = value;
            }
        }

        private string _NameConfirmed = null;
        public string NameConfirmed
        {
            get
            {
                return _NameConfirmed;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _NameConfirmed = value;
            }
        }

        private int _TitleID = default(int);
        public int TitleID
        {
            get
            {
                return _TitleID;
            }
            set
            {
                _TitleID = value;
            }
        }

        private string _MarcBibID = null;
        public string MarcBibID
        {
            get
            {
                return _MarcBibID;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 11);
                _MarcBibID = value;
            }
        }

        private string _PublicationTitle = null;
        public string PublicationTitle
        {
            get
            {
                return _PublicationTitle;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _PublicationTitle = value;
            }
        }

        private string _PublicationDetails = null;
        public string PublicationDetails
        {
            get
            {
                return _PublicationDetails;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _PublicationDetails = value;
            }
        }

        private string _Author = null;
        public string Author
        {
            get
            {
                return _Author;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _Author = value;
            }
        }

        private string _BPH = null;
        public string BPH
        {
            get
            {
                return _BPH;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 50);
                _BPH = value;
            }
        }

        private string _TL2 = null;
        public string TL2
        {
            get
            {
                return _TL2;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 25);
                _TL2 = value;
            }
        }

        private string _Abbreviation = null;
        public string Abbreviation
        {
            get
            {
                return _Abbreviation;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 125);
                _Abbreviation = value;
            }
        }

        private string _TitleUrl = null;
        public string TitleUrl
        {
            get
            {
                return _TitleUrl;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _TitleUrl = value;
            }
        }

        private int _ItemID = default(int);
        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                _ItemID = value;
            }
        }

        private string _BarCode = null;
        public string BarCode
        {
            get
            {
                return _BarCode;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 19);
                _BarCode = value;
            }
        }

        private string _MarcItemID = null;
        public string MarcItemID
        {
            get
            {
                return _MarcItemID;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 50);
                _MarcItemID = value;
            }
        }

        private string _CallNumber = null;
        public string CallNumber
        {
            get
            {
                return _CallNumber;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _CallNumber = value;
            }
        }

        private string _VolumeInfo = null;
        public string VolumeInfo
        {
            get
            {
                return _VolumeInfo;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _VolumeInfo = value;
            }
        }

        private string _ItemUrl = null;
        public string ItemUrl
        {
            get
            {
                return _ItemUrl;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _ItemUrl = value;
            }
        }

        private int _PageID = default(int);
        public int PageID
        {
            get
            {
                return _PageID;
            }
            set
            {
                _PageID = value;
            }
        }
		
		private string _Year = null;
        public string Year
		{
			get
			{
                return _Year;
			}
			set
			{
				if (value != null) value = CalibrateValue(value, 20);
                _Year = value;
			}
		}

		private string _Volume = null;
        public string Volume
		{
			get
			{
                return _Volume;
			}
			set
			{
                if (value != null) value = CalibrateValue(value, 20);
                _Volume = value;
			}
		}

        private string _Issue = null;
        public string Issue
        {
            get
            {
                return _Issue;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Issue = value;
            }
        }

        private string _Prefix = null;
        public string Prefix
        {
            get
            {
                return _Prefix;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Prefix = value;
            }
        }

        private string _Number = null;
        public string Number
        {
            get
            {
                return _Number;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Number = value;
            }
        }

        private string _PageUrl = null;
        public string PageUrl
        {
            get
            {
                return _PageUrl;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _PageUrl = value;
            }
        }

        private string _ThumbnailUrl = null;
        public string ThumbnailUrl
        {
            get
            {
                return _ThumbnailUrl;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 150);
                _ThumbnailUrl = value;
            }
        }

        private string _FullSizeImageUrl = null;
        public string FullSizeImageUrl
        {
            get { return _FullSizeImageUrl; }
            set { _FullSizeImageUrl = value; }
        }

        private string _ImageUrl = null;
        public string ImageUrl
        {
            get
            {
                return _ImageUrl;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 150);
                _ImageUrl = value;
            }
        }

        private string _OcrUrl = null;
        public string OcrUrl
        {
            get { return _OcrUrl; }
            set { _OcrUrl = value; }
        }

        private string _PageTypeName = null;
        public string PageTypeName
        {
            get
            {
                return _PageTypeName;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 30);
                _PageTypeName = value;
            }
        }

		#endregion Properties
		
		#region SortColumn
		
		/// <summary>
		/// Use when defining sort columns for a collection sort request.
		/// For example where list is a instance of <see cref="CustomGenericList">, 
        /// list.Sort(SortOrder.Ascending, Name.SortColumn.NameID);
		/// </summary>
		[Serializable]
		public sealed class SortColumn
		{
            public const string NameBankID = "NameBankID";
            public const string NameConfirmed = "NameConfirmed";
            public const string TitleID = "TitleID";
            public const string MarcBibID = "MarcBibID";
            public const string PublicationTitle = "PublicationTitle";
            public const string MarcItemID = "MarcItemID";
            public const string Year = "Year";
            public const string Volume = "Volume";
            public const string Issue = "Issue";
            public const string Prefix = "Prefix";
            public const string Number = "Number";
            public const string PageTypeName = "PageTypeName";
        }
				
		#endregion SortColumn

        private string CalibrateValue(string value, int maximumCharacterLength)
        {
            value = value.Trim();
            if (value.Length > maximumCharacterLength)
            {
                value = value.Substring(0, maximumCharacterLength);
            }

            return value;
        }
    }
}
