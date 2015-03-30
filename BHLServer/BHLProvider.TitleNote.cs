using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public CustomGenericList<TitleNote> TitleNoteSelectByTitleID(int titleID)
        {
            return new TitleNoteDAL().TitleNoteSelectByTitleID(null, null, titleID);
        }
    }
}
