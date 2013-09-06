
#region Using

using System;

#endregion Using

namespace MOBOT.BHL.DataObjects
{
	[Serializable]
	public class PaginationStatus
	{
        public static int New
        {
            get
            {
                return 5;
            }
        }
        
        public static int Pending
        {
            get
            {
                return 10;
            }
        }

        public static int InProgress
        {
            get
            {
                return 20;
            }
        }

        public static int Complete
        {
            get
            {
                return 30;
            }
        }

        public static string GetStatusString(int? paginationStatusID)
        {
            if (paginationStatusID == PaginationStatus.New)
                return "New";
            else if (paginationStatusID == PaginationStatus.Pending)
                return "Incomplete";
            else if (paginationStatusID == PaginationStatus.InProgress)
                return "In Progress";
            else if (paginationStatusID == PaginationStatus.Complete)
                return "Complete";
            else
                return "New";
        }
	}
}
