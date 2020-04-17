
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class MonthlyStats : __MonthlyStats
	{
        private string _institutionCode = string.Empty;

        public string InstitutionCode
        {
            get { return _institutionCode; }
            set { _institutionCode = value; }
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
                if (column.Name == "InstitutionCode")
                {
                    _institutionCode = Utility.EmptyIfNull(column.Value);
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
