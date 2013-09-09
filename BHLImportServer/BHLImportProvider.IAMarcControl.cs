using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
	public partial class BHLImportProvider
	{
        public void IAMarcControlDeleteByMarcID(int marcID)
        {
            new IAMarcControlDAL().IAMarcControlDeleteByMarcID(null, null, marcID);
        }

        public IAMarcControl IAMarcControlInsertAuto(int MarcID, string tag, string value)
        {
            return (new IAMarcControlDAL().IAMarcControlInsertAuto(null, null, MarcID, tag, value));
        }
	}
}
