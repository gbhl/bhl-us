using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class SegmentKeyword : __SegmentKeyword
	{
        #region Properties

        private string _keyword = string.Empty;

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "Keyword":
                        {
                            _keyword = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);

        }

        #endregion

    }
}
