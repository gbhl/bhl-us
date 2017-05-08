using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<PageFlickrNote> PageFlickrNoteSelectForPageID(int pageID)
        {
            return new PageFlickrNoteDAL().PageFlickrNoteSelectForPageID(null, null, pageID);
        }

        public void PageFlickrNoteUpdateForPageID(int pageID, PageFlickrNote[] notes)
        {
            PageFlickrNoteDAL dal = new PageFlickrNoteDAL();

            foreach (PageFlickrNote note in notes)
            {
                if (note.PageFlickrNoteID == default(int))
                {
                    dal.PageFlickrNoteInsertAuto(null, null, note);
                }
                else
                {
                    dal.PageFlickrNoteUpdateAuto(null, null, note);
                }
            }
        }
    }
}
