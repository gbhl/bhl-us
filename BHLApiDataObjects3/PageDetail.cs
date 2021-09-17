﻿using CustomDataAccess;
using System;

namespace MOBOT.BHL.API.BHLApiDataObjects3
{
    public class PageDetail : DataObjectBase, ISetValues
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PageDetail()
        {
        }

        public PageDetail(string nameBankID,
            int nameResolvedID,
            string nameConfirmed,
            string nameCanonical,
            int titleID,
            string publicationTitle,
            string publisherPlace,
            string publisherName,
            string publicationDate,
            string titleUrl,
            int itemID,
            string callNumber,
            string volumeInfo,
            string itemUrl,
            int? segmentID,
            string title,
            string startPageNumber,
            string endPageNumber,
            string segmentUrl,
            int pageID,
            string year,
            string volume,
            string issue,
            string prefix,
            string number,
            string textSource,
            string pageUrl,
            string thumbnailUrl,
            string pageTypeName,
            string creationDate) : this()
        {
            _NameBankID = nameBankID;
            _NameResolvedID = nameResolvedID;
            _NameConfirmed = nameConfirmed;
            _NameCanonical = nameCanonical;
            _TitleID = titleID;
            _PublicationTitle = publicationTitle;
            _PublisherPlace = publisherPlace;
            _PublisherName = publisherName;
            _PublicationDate = publicationDate;
            _TitleUrl = titleUrl;
            _ItemID = itemID;
            _CallNumber = callNumber;
            _VolumeInfo = volumeInfo;
            _ItemUrl = itemUrl;
            SegmentID = segmentID;
            Title = title;
            StartPageNumber = startPageNumber;
            EndPageNumber = endPageNumber;
            SegmentUrl = segmentUrl;
            _PageID = pageID;
            _Year = year;
            _Volume = volume;
            _Issue = issue;
            _Prefix = prefix;
            _Number = number;
            _TextSource = textSource;
            _PageUrl = pageUrl;
            _ThumbnailUrl = thumbnailUrl;
            _PageTypeName = pageTypeName;
            _creationDate = creationDate;
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
                    case "NameResolvedID":
                        {
                            _NameResolvedID = (int)column.Value;
                            break;
                        }
                    case "ResolvedNameString":
                        {
                            _NameConfirmed = (string)column.Value;
                            break;
                        }
                    case "CanonicalNameString":
                        {
                            _NameCanonical = (string)column.Value;
                            break;
                        }
                    case "TitleID":
                        {
                            _TitleID = (int)column.Value;
                            break;
                        }
                    case "ShortTitle":
                        {
                            _PublicationTitle = (string)column.Value;
                            break;
                        }
                    case "PublisherName":
                        {
                            _PublisherName = (string)column.Value;
                            break;
                        }
                    case "PublisherPlace":
                        {
                            _PublisherPlace = (string)column.Value;
                            break;
                        }
                    case "PublicationDate":
                        {
                            _PublicationDate = (string)column.Value;
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
                    case "SourceName":
                        {
                            _Source = (string)column.Value;
                            break;
                        }
                    case "Barcode":
                        {
                            _SourceIdentifier = (string)column.Value;
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
                    case "InstitutionName":
                        {
                            _HoldingInstitution = (string)column.Value;
                            break;
                        }
                    case "ItemURL":
                        {
                            _ItemUrl = (string)column.Value;
                            break;
                        }
                    case "SegmentID":
                        {
                            _SegmentID = (int?)column.Value;
                            break;
                        }
                    case "Title":
                        {
                            _Title = (string)column.Value;
                            break;
                        }
                    case "SegmentSourceName":
                        {
                            _SegmentSourceName = (string)column.Value;
                            break;
                        }
                    case "SegmentBarcode":
                        {
                            _SegmentSourceIdentifier = (string)column.Value;
                            break;
                        }
                    case "SegmentContributors":
                        {
                            _SegmentContributors = (string)column.Value;
                            break;
                        }
                    case "StartPageNumber":
                        {
                            _StartPageNumber = (string)column.Value;
                            break;
                        }
                    case "EndPageNumber":
                        {
                            _EndPageNumber = (string)column.Value;
                            break;
                        }
                    case "SegmentURL":
                        {
                            _SegmentUrl = (string)column.Value;
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
                    case "TextSource":
                        {
                            _TextSource = (string)column.Value;
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
                    case "PageNumbers":
                        {
                            _PageNumbers = (string)column.Value;
                            break;
                        }
                    case "CreationDate":
                        {
                            _creationDate = column.Value == null ? null : ((DateTime)column.Value).ToString("yyyy/MM/dd HH:mm:ss");
                            break;
                        }
                }
            }
        }

        #endregion Set Values

        #region Properties

        private string _NameBankID = null;
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

        private int _NameResolvedID = default(int);
        public int NameResolvedID
        {
            get
            {
                return _NameResolvedID;
            }
            set
            {
                _NameResolvedID = value;
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

        private string _NameCanonical = null;
        public string NameCanonical
        {
            get { return _NameCanonical; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _NameCanonical = value;
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

        private string _PublisherPlace = null;
        public string PublisherPlace
        {
            get
            {
                return _PublisherPlace;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 150);
                _PublisherPlace = value;
            }
        }

        private string _PublisherName = null;
        public string PublisherName
        {
            get
            {
                return _PublisherName;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _PublisherName = value;
            }
        }

        private string _PublicationDate = null;
        public string PublicationDate
        {
            get
            {
                return _PublicationDate;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _PublicationDate = value;
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

        private string _Source = null;
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        private string _SourceIdentifier = null;
        public string SourceIdentifier
        {
            get { return _SourceIdentifier; }
            set { _SourceIdentifier = value; }
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

        private string _HoldingInstitution = null;
        public string HoldingInstitution
        {
            get { return _HoldingInstitution; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _HoldingInstitution = value;
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

        private int? _SegmentID = null;
        public int? SegmentID { get => _SegmentID; set => _SegmentID = value; }

        private string _Title = null;
        public string Title { get => _Title; set => _Title = value; }

        private string _SegmentSourceName = null;
        public string SegmentSourceName { get => _SegmentSourceName; set => _SegmentSourceName = value; }

        private string _SegmentSourceIdentifier = null;
        public string SegmentSourceIdentifier { get => _SegmentSourceIdentifier; set => _SegmentSourceIdentifier = value; }

        private string _SegmentContributors = null;
        public string SegmentContributors { get => _SegmentContributors; set => _SegmentContributors = value; }

        private string _StartPageNumber = null;
        public string StartPageNumber { get => _StartPageNumber; set => _StartPageNumber = value; }

        private string _EndPageNumber = null;
        public string EndPageNumber { get => _EndPageNumber; set => _EndPageNumber = value; }

        private string _SegmentUrl = null;
        public string SegmentUrl { get => _SegmentUrl; set => _SegmentUrl = value; }


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

        private string _TextSource = null;
        public string TextSource
        {
            get
            {
                return _TextSource;
            }
            set
            {
                if (value != null) value = CalibrateValue(value, 50);
                _TextSource = value;
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

        private string _OcrUrl = null;
        public string OcrUrl
        {
            get { return _OcrUrl; }
            set { _OcrUrl = value; }
        }

        private string _PageTypeName = null;
        public string PageTypeName
        {
            get { return _PageTypeName; }
            set { _PageTypeName = value; }
        }

        private string _PageNumbers = null;
        public string PageNumbers
        {
            get { return _PageNumbers; }
            set { _PageNumbers = value; }
        }

        private string _creationDate = null;
        public string CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        #endregion Properties
    }
}
