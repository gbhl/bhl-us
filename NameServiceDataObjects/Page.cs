using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects
{
    [Serializable]
    public class Page
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Page()
		{
		}

		#endregion Constructors
		
		#region Properties

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
                _ImageUrl = value;
            }
        }

        private string _OcrUrl = null;
        public string OcrUrl
        {
            get { return _OcrUrl; }
            set { _OcrUrl = value; }
        }

        CustomGenericList<PageType> _PageTypes;
        public CustomGenericList<PageType> PageTypes
        {
            get
            {
                return _PageTypes;
            }
            set
            {
                _PageTypes = value;
            }
        }

		#endregion Properties
		
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
