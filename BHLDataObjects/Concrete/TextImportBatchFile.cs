
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TextImportBatchFile : __TextImportBatchFile
	{
        #region Properties

        private string _statusName;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "StatusName":
                        {
                            _statusName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}

