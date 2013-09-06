using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects
{
    [Serializable]
    public class Item
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
				if (value != null) value = CalibrateValue(value, 40);
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
                _ItemUrl = value;
            }
        }

        CustomGenericList<Page> _Pages;
        public CustomGenericList<Page> Pages
        {
            get
            {
                return _Pages;
            }
            set
            {
                _Pages = value;
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
