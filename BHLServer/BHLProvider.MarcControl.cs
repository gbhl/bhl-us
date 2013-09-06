using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public MarcControl MarcControlInsertAuto(int marcID, String tag, String value)
        {
            return new MarcControlDAL().MarcControlInsertAuto(null, null, marcID, tag, value);
        }
    }
}
