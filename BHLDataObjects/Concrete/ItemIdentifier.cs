
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class ItemIdentifier : __ItemIdentifier
	{
        private string _IdentifierName;

        public string IdentifierName
        {
            get { return this._IdentifierName; }
            set { this._IdentifierName = value; }
        }

        private string _identifierLabel;
        public string IdentifierLabel
        {
            get { return _identifierLabel; }
            set { _identifierLabel = value; }
        }

        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        public string IdentifierValueDisplay
        {
            get { return string.Format("{0}{1}", Prefix, IdentifierValue); }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "IdentifierName":
                        {
                            _IdentifierName = (string)column.Value;
                            break;
                        }
                    case "IdentifierLabel":
                        {
                            _identifierLabel = (string)column.Value;
                            break;
                        }
                    case "Prefix":
                        {
                            _prefix = (string)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}

