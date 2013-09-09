using System;
using CustomDataAccess;
using MOBOT.IAAnalysis.DAL;
using MOBOT.IAAnalysis.DataObjects;

namespace MOBOT.IAAnalysis.Server
{
    public partial class IAAnalysisProvider
    {
        public MarcSubField MarcSubFieldInsertAuto(int marcDataFieldID, string code, string value)
        {
            return (new MarcSubFieldDAL().MarcSubFieldInsertAuto(null, null, marcDataFieldID, code, value));
        }
    }
}
