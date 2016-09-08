using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class Item : DataObjectBase, ISetValues
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Item()
		{
		}

		#endregion Constructors
		
		#region Properties		

        private int _ItemID = default(int);
        public int ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }

        // Primary title ID for the item
        private int? _PrimaryTitleID = null;
        public int? PrimaryTitleID
        {
            get { return _PrimaryTitleID; }
            set { _PrimaryTitleID = value; }
        }

        private int? _ThumbnailPageID = null;
        public int? ThumbnailPageID
        {
            get { return _ThumbnailPageID; }
            set { _ThumbnailPageID = value; }
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
		
        private string _Volume = null;
        public string Volume
        {
            get { return _Volume; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _Volume = value;
            }
        }

        private string _Year = null;
        public string Year
        {
            get { return _Year; }
            set 
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Year = value; 
            }
        }

        private string _CopySpecificInformation = null;
        public string CopySpecificInformation
        {
            get { return _CopySpecificInformation; }
            set { _CopySpecificInformation = value; }
        }

        private string _Contributor = null;
        public string Contributor
        {
            get { return _Contributor; }
            set 
            {
                if (value != null) value = CalibrateValue(value, 255);
                _Contributor = value; 
            }
        }

        private string _RightsHolder = null;
        public string RightsHolder
        {
            get { return _RightsHolder; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _RightsHolder = value;
            }
        }

        private string _ScanningInstitution = null;
        public string ScanningInstitution
        {
            get { return _ScanningInstitution; }
            set
            {
                if (value != null) value = CalibrateValue(value, 255);
                _ScanningInstitution = value;
            }
        }

        private string _Sponsor = null;
        public string Sponsor
        {
            get { return _Sponsor; }
            set
            {
                if (value != null) value = CalibrateValue(value, 100);
                _Sponsor = value;
            }
        }

        private string _Language = null;
        public string Language
        {
            get { return _Language; }
            set 
            {
                if (value != null) value = CalibrateValue(value, 20);
                _Language = value; 
            }
        }

        private string _LicenseUrl = null;
        public string LicenseUrl
        {
            get { return _LicenseUrl; }
            set { _LicenseUrl = value; }
        }

        private string _Rights = null;
        public string Rights
        {
            get { return _Rights; }
            set { _Rights = value; }
        }

        private string _DueDiligence = null;
        public string DueDiligence
        {
            get { return _DueDiligence; }
            set { _DueDiligence = value; }
        }

        private string _CopyrightStatus = null;
        public string CopyrightStatus
        {
            get { return _CopyrightStatus; }
            set { _CopyrightStatus = value; }
        }

        private string _CopyrightRegion = null;
        public string CopyrightRegion
        {
            get { return _CopyrightRegion; }
            set
            {
                if (value != null) value = CalibrateValue(value, 50);
                _CopyrightRegion = value;
            }
        }

        private string _ExternalUrl = null;
        public string ExternalUrl
        {
            get { return _ExternalUrl; }
            set { _ExternalUrl = value; }
        }

        private string _ItemUrl = null;
        public string ItemUrl
        {
            get { return _ItemUrl; }
            set { _ItemUrl = value; }
        }

        // Primary title URL for this item
        private string _TitleUrl = null;
        public string TitleUrl
        {
            get { return _TitleUrl; }
            set { _TitleUrl = value; }
        }

        private string _ItemThumbUrl = null;
        public string ItemThumbUrl
        {
            get { return _ItemThumbUrl; }
            set { _ItemThumbUrl = value; }
        }

        CustomGenericList<Page> _Pages = null;
        public CustomGenericList<Page> Pages
        {
            get { return _Pages; }
            set { _Pages = value; }
        }

        CustomGenericList<Part> _Parts = null;
        public CustomGenericList<Part> Parts
        {
            get { return _Parts; }
            set { _Parts = value; }
        }

        CustomGenericList<Collection> _Collections = null;
        public CustomGenericList<Collection> Collections
        {
            get { return _Collections; }
            set { _Collections = value; }
        }

		#endregion Properties

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ItemID":
                        {
                            _ItemID = (int)column.Value;
                            break;
                        }
                    case "PrimaryTitleID":
                        {
                            _PrimaryTitleID = (int)column.Value;
                            break;
                        }
                    case "ThumbnailPageID":
                        {
                            _ThumbnailPageID = (int?)column.Value;
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
                    case "Volume":
                        {
                            _Volume = (string)column.Value;
                            break;
                        }
                    case "Year":
                        {
                            _Year = (string)column.Value;
                            break;
                        }
                    case "ItemDescription":
                        {
                            _CopySpecificInformation = (string)column.Value;
                            break;
                        }
                    case "InstitutionName":
                        {
                            _Contributor = (string)column.Value;
                            break;
                        }
                    case "Sponsor":
                        {
                            _Sponsor = (string)column.Value;
                            break;
                        }
                    case "Language":
                        {
                            _Language = (string)column.Value;
                            break;
                        }
                    case "LicenseUrl":
                        {
                            _LicenseUrl = (string)column.Value;
                            break;
                        }
                    case "Rights":
                        {
                            _Rights = (string)column.Value;
                            break;
                        }
                    case "DueDiligence":
                        {
                            _DueDiligence = (string)column.Value;
                            break;
                        }
                    case "CopyrightStatus":
                        {
                            _CopyrightStatus = (string)column.Value;
                            break;
                        }
                    case "CopyrightRegion":
                        {
                            _CopyrightRegion = (string)column.Value;
                            break;
                        }
                    case "ExternalUrl":
                        {
                            _ExternalUrl = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
