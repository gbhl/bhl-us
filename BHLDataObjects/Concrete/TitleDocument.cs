
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleDocument : __TitleDocument
	{
        #region Properties

        private string _typeLabel;
        public string TypeLabel
        {
            get { return _typeLabel; }
            set { _typeLabel = value; }
        }

        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TypeLabel":
                        {
                            this._typeLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion
    }
}
