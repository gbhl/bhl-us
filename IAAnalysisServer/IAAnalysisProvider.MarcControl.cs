using System;
using CustomDataAccess;
using MOBOT.IAAnalysis.DAL;
using MOBOT.IAAnalysis.DataObjects;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
	{
        public MarcControl MarcControlInsertAuto(int itemID, string tag, string value)
        {
            return (new MarcControlDAL().MarcControlInsertAuto(null, null, itemID, tag, value));
        }
	}
}
