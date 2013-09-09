
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class ImportLog : __ImportLog
    {
        #region Properties

        private string _importSource;
        public string ImportSource
        {
            get { return this._importSource; }
            set { this._importSource = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "ImportSource":
                        {
                            _importSource = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion ISet override
    }
}
