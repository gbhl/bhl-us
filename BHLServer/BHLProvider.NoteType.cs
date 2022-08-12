using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public NoteType NoteTypeSelectAuto(int noteTypeID)
        {
            return new NoteTypeDAL().NoteTypeSelectAuto(null, null, noteTypeID);
        }

        public List<NoteType> NoteTypeSelectAll()
        {
            return new NoteTypeDAL().NoteTypeSelectAll(null, null);
        }

        public void SaveNoteType(NoteType noteType, int userID)
        {
            new NoteTypeDAL().Save(null, null, noteType, userID);
        }

    }
}
