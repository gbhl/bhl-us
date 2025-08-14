
#region Using

using CustomDataAccess;
using System;

#endregion Using

namespace MOBOT.BHLImport.DataObjects
{
	[Serializable]
	public class WDEntityIdentifier : __WDEntityIdentifier
	{
		private string _entityDescription;
		private string _message;

		public string EntityDescription
		{
			get { return _entityDescription; }
			set { _entityDescription = value; }
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

        public override void SetValues(CustomDataRow row)
        {
            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "EntityDescription":
                        {
                            _entityDescription = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                    case "Message":
                        {
                            _message = Utility.EmptyIfNull(column.Value);
                            break;
                        }
                }
            }

            base.SetValues(row);
        }
    }
}

