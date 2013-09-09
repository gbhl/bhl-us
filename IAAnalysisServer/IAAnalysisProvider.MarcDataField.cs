using System;
using CustomDataAccess;
using MOBOT.IAAnalysis.DAL;
using MOBOT.IAAnalysis.DataObjects;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
    {
        public MarcDataField MarcDataFieldInsertAuto(int itemID, string tag, string indicator1, string indicator2)
        {
            return (new MarcDataFieldDAL().MarcDataFieldInsertAuto(null, null, itemID, tag, indicator1, indicator2));
        }
    }
}
