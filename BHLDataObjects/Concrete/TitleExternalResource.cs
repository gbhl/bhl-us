
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleExternalResource : __TitleExternalResource
	{
        #region Properties

        private string _externalResourceTypeLabel;
        public string ExternalResourceTypeLabel
        {
            get { return _externalResourceTypeLabel; }
            set { _externalResourceTypeLabel = value; }
        }

        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ExternalResourceTypeLabel":
                        {
                            this._externalResourceTypeLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion
    }
}

