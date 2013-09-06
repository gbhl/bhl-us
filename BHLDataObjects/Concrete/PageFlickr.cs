using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class PageFlickr : __PageFlickr
	{
        private string _shortTitle = string.Empty;
        public string ShortTitle
        {
            get { return _shortTitle; }
            set { _shortTitle = value; }
        }

        private string _indicatedPage = string.Empty;
        public string IndicatedPage
        {
            get { return _indicatedPage; }
            set { _indicatedPage = value; }
        }

        private string _pageType = string.Empty;
        public string PageType
        {
            get { return _pageType; }
            set { _pageType = value; }
        }

        public override void SetValues(CustomDataRow row)
        {

            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ShortTitle":
                        {
                            _shortTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "IndicatedPage":
                        {
                            _indicatedPage = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "PageType":
                        {
                            _pageType = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

	}
}
