
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class IAItem : __IAItem
	{

        private int _totalItems;

        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TotalItems":
                        {
                            _totalItems = (int)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

	}
}
