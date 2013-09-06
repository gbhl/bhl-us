using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public MarcDataField MarcDataFieldInsertAuto(int marcID, String tag, String indicator1, String indicator2)
        {
            return new MarcDataFieldDAL().MarcDataFieldInsertAuto(null, null, marcID, tag, 
                indicator1, indicator2);
        }
    }
}
