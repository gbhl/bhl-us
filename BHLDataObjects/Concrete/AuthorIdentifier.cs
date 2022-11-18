
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

        private string _IdentifierLabel;

        public string IdentifierLabel
        {
            get { return this._IdentifierLabel; }
            set { this._IdentifierLabel = value; }
        }

        private string _Prefix = string.Empty;
        
        public string IdentifierValueUrl
        {
            get
            {
                string url = string.Empty;
                if (!string.IsNullOrWhiteSpace(_Prefix))
                {
                    url = this._Prefix + this.IdentifierValue;
                }
                else if (this.IdentifierValue.StartsWith("http://") || this.IdentifierValue.StartsWith("https://"))
                {
                    url = this.IdentifierValue;
                }
                return url;
            }
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
                            _IdentifierLabel = (string)column.Value;
                            break;
                        }
                    case "Prefix":
                        {
                            _Prefix = (string)column.Value;
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}
