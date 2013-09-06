
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class AuthorIdentifier : __AuthorIdentifier
	{
        private string _IdentifierName;

        public string IdentifierName
        {
            get { return this._IdentifierName; }
            set { this._IdentifierName = value; }
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
                }
            }

            base.SetValues(row);
        }
    }
}
