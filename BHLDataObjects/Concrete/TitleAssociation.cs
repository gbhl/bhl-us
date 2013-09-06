
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class TitleAssociation : __TitleAssociation
	{
        #region Properties

        private CustomGenericList<TitleAssociation_TitleIdentifier> _titleAssociationIdentifiers = new CustomGenericList<TitleAssociation_TitleIdentifier>();

        public CustomGenericList<TitleAssociation_TitleIdentifier> TitleAssociationIdentifiers
        {
            get { return _titleAssociationIdentifiers; }
            set { _titleAssociationIdentifiers = value; }
        }

        private string _titleAssociationLabel;

        public string TitleAssociationLabel
        {
            get { return this._titleAssociationLabel; }
            set { this._titleAssociationLabel = value; }
        }

        private string _titleAssociationName;

        public string TitleAssociationName
        {
            get { return _titleAssociationName; }
            set { _titleAssociationName = value; }
        }

        private int _marcDataFieldID;

        public int MarcDataFieldID
        {
            get { return _marcDataFieldID; }
            set { _marcDataFieldID = value; }
        }

        #endregion Properties

        #region ISet override

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "TitleAssociationLabel":
                        {
                            this._titleAssociationLabel = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "TitleAssociationName":
                        {
                            this._titleAssociationName = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "MARCDataFieldID":
                        {
                            this._marcDataFieldID = Utility.ZeroIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }

        #endregion

    }
}
