using System;

namespace MOBOT.BHL.API.BHLApiDataObjects
{
    [Serializable]
    public class PageType
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PageType()
		{
		}

		#endregion Constructors
		
		#region Properties

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
