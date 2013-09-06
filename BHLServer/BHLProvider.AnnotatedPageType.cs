using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public AnnotatedPageType AnnotatedPageTypeSelectAuto(int annotatedPageTypeID)
        {
            return new AnnotatedPageTypeDAL().AnnotatedPageTypeSelectAuto(null, null, annotatedPageTypeID);
        }

        public AnnotatedPageType AnnotatedPageTypeSelectByPageID(int pageID)
        {
            return new AnnotatedPageTypeDAL().AnnotatedPageTypeSelectByPageID(null, null, pageID);
        }
    }
}
