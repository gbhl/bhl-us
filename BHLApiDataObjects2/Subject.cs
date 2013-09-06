using System;
using CustomDataAccess;

namespace MOBOT.BHL.API.BHLApiDataObjects2
{
    [Serializable]
    public class Subject : DataObjectBase, ISetValues
    {
		#region Constructors
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Subject()
		{
		}

		#endregion Constructors
		
		#region Properties

        private string _SubjectText = null;
        public string SubjectText
        {
            get { return _SubjectText; }
            set
            {
                if (value != null) value = this.CalibrateValue(value, 50);
                _SubjectText = value;
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
                    case "Keyword":
                        {
                            _SubjectText = (string)column.Value;
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
