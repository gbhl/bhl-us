using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public NoteType NoteTypeSelectAuto(int noteTypeID)
        {
            return new NoteTypeDAL().NoteTypeSelectAuto(null, null, noteTypeID);
        }

        public CustomGenericList<NoteType> NoteTypeSelectAll()
        {
            return new NoteTypeDAL().NoteTypeSelectAll(null, null);
        }

        public void SaveNoteType(NoteType noteType, int userID)
        {
            new NoteTypeDAL().Save(null, null, noteType, userID);
        }

    }
}
