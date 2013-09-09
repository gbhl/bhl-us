
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class IAItemError : __IAItemError
	{
        #region Properties

        private string _iaIdentifier;
        public string IAIdentifier
        {
            get { return this._iaIdentifier; }
            set { this._iaIdentifier = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "IAIdentifier":
                        {
                            _iaIdentifier = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion ISet override
    }
}
