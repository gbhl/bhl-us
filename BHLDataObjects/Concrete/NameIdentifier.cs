
#region Using

using System;
using CustomDataAccess;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class NameIdentifier : __NameIdentifier
	{
        private string _identifierName;

        public string IdentifierName
        {
            get { return _identifierName; }
            set { _identifierName = value; }
        }

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                if (column.Name == "IdentifierName")
                {
                    _identifierName = Utility.EmptyIfNull(column.Value);
                }
            }
            base.SetValues(row);
        }
	}
}
