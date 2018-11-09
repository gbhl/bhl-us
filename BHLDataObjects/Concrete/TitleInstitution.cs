
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleInstitution : __TitleInstitution
	{
        #region Properties

        private string _fullTitle;
        private string _institutionName;

        public string FullTitle
        {
            get { return _fullTitle; }
            set { _fullTitle = value; }
        }

        public string InstitutionName
        {
            get { return _institutionName; }
            set { _institutionName = value; }
        }

        #endregion

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "FullTitle":
                        {
                            _fullTitle = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "InstitutionName":
                        {
                            _institutionName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}

