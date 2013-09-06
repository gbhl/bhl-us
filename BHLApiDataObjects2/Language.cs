using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class Language : DataObjectBase, ISetValues
    {
        #region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Language()
		{
		}

		#endregion Constructors
		
        #region Properties

        private string _languageCode = null;
        public string LanguageCode
        {
            get { return _languageCode; }
            set {
                if (value != null) value = CalibrateValue(value, 10);
                _languageCode = value; 
            }
        }

        private string _languageName = null;
        public string LanguageName
        {
            get { return _languageName; }
            set {
                if (value != null) value = CalibrateValue(value, 20);
                _languageName = value; 
            }
        }

        #endregion

        #region ISetValues Members

        public void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "LanguageCode":
                        {
                            _languageCode = (string)column.Value;
                            break;
                        }
                    case "LanguageName":
                        {
                            _languageName = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
