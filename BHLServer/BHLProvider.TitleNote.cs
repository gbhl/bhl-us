using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public List<TitleNote> TitleNoteSelectByTitleID(int titleID)
        {
            return new TitleNoteDAL().TitleNoteSelectByTitleID(null, null, titleID);
        }
    }
}
