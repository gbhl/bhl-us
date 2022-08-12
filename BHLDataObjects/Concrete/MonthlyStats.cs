
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class MonthlyStats : __MonthlyStats
	{
        private string _institutionName = string.Empty;

        public string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        private string _institutionGroupNames = string.Empty;

        public string InstitutionGroupNames
        { 
            get { return _institutionGroupNames; } 
            set { _institutionGroupNames = value; }
        }

        private int _cumulativeValue = 0;

        public int CumulativeValue
        {
            get { return _cumulativeValue; }
            set { _cumulativeValue = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "InstitutionName")
                {
                    _institutionName = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "InstitutionGroupNames")
                {
                    _institutionGroupNames = Utility.EmptyIfNull(column.Value);
                }
                if (column.Name == "CumulativeValue")
                {
                    _cumulativeValue = Utility.ZeroIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
    }
}
