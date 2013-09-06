using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects
{
    [Serializable]
    public class Title
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Title()
		{
		}

		#endregion Constructors
		
		#region Properties

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
                if (value != null) value = CalibrateValue(value, 50);
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
                _TitleUrl = value;
            }
        }

        CustomGenericList<Item> _Items;
        public CustomGenericList<Item> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
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
