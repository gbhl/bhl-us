using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class DOIStatus : __DOIStatus
	{
        private int _numberOfDOIs = 0;

        public int NumberOfDOIs
        {
            get { return _numberOfDOIs; }
            set { _numberOfDOIs = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "NumberDOIs":
                        {
                            _numberOfDOIs = (int)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
	}
}
