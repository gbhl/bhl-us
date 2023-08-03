using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<PageFlickrTag> PageFlickrTagSelectForPageID(int pageID)
        {
            return new PageFlickrTagDAL().PageFlickrTagSelectForPageID(null, null, pageID);
        }

        public void PageFlickrTagUpdateForPageID(int pageID, PageFlickrTag[] tags)
        {
            PageFlickrTagDAL dal = new PageFlickrTagDAL();

            foreach (PageFlickrTag tag in tags)
            {
                if (tag.PageFlickrTagID == default)
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
