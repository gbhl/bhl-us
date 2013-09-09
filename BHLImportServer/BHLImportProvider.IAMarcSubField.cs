using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IAMarcSubField IAMarcSubFieldInsertAuto(int marcDataFieldID, string code, string value)
        {
            return (new IAMarcSubFieldDAL().IAMarcSubFieldInsertAuto(null, null, marcDataFieldID, code, value));
        }
    }
}
