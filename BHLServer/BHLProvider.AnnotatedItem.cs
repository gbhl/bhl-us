using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public bool AnnotatedItemCheckForSurrogate(int annotatedItemID)
        {
            AnnotatedItemDAL dal = new AnnotatedItemDAL();
            return dal.AnnotatedItemCheckForSurrogate(null, null, annotatedItemID);
        }
    }
}
