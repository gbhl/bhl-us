using System;
using CustomDataAccess;

namespace MOBOT.BHL.RequestLog.DataObjects
{
	[Serializable]
	public class RequestLog : __RequestLog
	{
        private string userName = null;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string requestTypeName = null;
        public string RequestTypeName
        {
            get { return requestTypeName; }
            set { requestTypeName = value; }
        }

        public override void SetValues(CustomDataRow row)
        {

            foreach (CustomDataColumn column in row)
            {
                switch (column.Name)
                {
                    case "UserName":
                        userName = (string)column.Value;
                        break;
                    case "RequestTypeName":
                        requestTypeName = (string)column.Value;
                        break;
                }
            }

            base.SetValues(row);
        }
	}
}
