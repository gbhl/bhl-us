using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public MarcSubField MarcSubFieldInsertAuto(int marcDataFieldID, String code, String value)
        {
            return new MarcSubFieldDAL().MarcSubFieldInsertAuto(null, null, marcDataFieldID, code, value);
        }
    }
}
