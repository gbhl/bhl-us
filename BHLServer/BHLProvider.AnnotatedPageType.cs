using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public AnnotatedPageType AnnotatedPageTypeSelectByPageID(int pageID)
        {
            return new AnnotatedPageTypeDAL().AnnotatedPageTypeSelectByPageID(null, null, pageID);
        }
    }
}
