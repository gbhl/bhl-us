
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemPage : __ItemPage
	{
        #region Properties

        private int _pageSequenceOrder;

        public int PageSequenceOrder
        {
            get { return _pageSequenceOrder; }
            set { _pageSequenceOrder = value; }
        }

        private string _pageTypes = string.Empty;

        public string PageTypes
        {
            get { return _pageTypes; }
            set { _pageTypes = value; }
        }

        private string _IndicatedPages = string.Empty;

        public string IndicatedPages
        {
            get { return _IndicatedPages; }
            set { _IndicatedPages = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "PageSequenceOrder":
                        {
                            _pageSequenceOrder = (int)(column.Value ?? 0);
                            break;
                        }
                    case "PageTypes":
                        {
                            _pageTypes = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "IndicatedPages":
                        {
                            _IndicatedPages = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);

        }

        #endregion
    }
}

