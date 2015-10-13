using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public CustomGenericList<PageFlickrTag> PageFlickrTagSelectForPageID(int pageID)
        {
            return new PageFlickrTagDAL().PageFlickrTagSelectForPageID(null, null, pageID);
        }

        public void PageFlickrTagUpdateForPageID(int pageID, PageFlickrTag[] tags)
        {
            PageFlickrTagDAL dal = new PageFlickrTagDAL();

            foreach (PageFlickrTag tag in tags)
            {
                if (tag.PageFlickrTagID == default(int))
                {
                    dal.PageFlickrTagInsertAuto(null, null, tag);
                }
                else
                {
                    dal.PageFlickrTagUpdateAuto(null, null, tag);
                }
            }
        }
    }
}
