using System;
using CustomDataAccess;

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class SegmentIdentifier : __SegmentIdentifier
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
                }
            }

            base.SetValues(row);
        }
    }
}
