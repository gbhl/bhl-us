
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleVariant : __TitleVariant
	{
        #region Properties

        private string _titleVariantLabel;
        public string TitleVariantLabel
        {
            get { return this._titleVariantLabel; }
            set { this._titleVariantLabel = value; }
        }

        private string _titleVariantTypeName;
        public string TitleVariantTypeName
        {
            get { return _titleVariantTypeName; }
            set { _titleVariantTypeName = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleVariantLabel":
                        {
                            this._titleVariantLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TitleVariantTypeName":
                        {
                            this._titleVariantTypeName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion
    }
}
