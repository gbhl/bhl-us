using System;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using CustomDataAccess;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public AnnotatedItem AnnotatedItemSave(int annotatedTitleId, string externalIdentifier, string volume)
        {
            AnnotatedItemDAL dal = new AnnotatedItemDAL();
            AnnotatedItem item = dal.AnnotatedItemSelectByExternalIdentifer(null, null,
                externalIdentifier, annotatedTitleId);

            if (item == null)
            {
                item = new AnnotatedItem();
                item.ExternalIdentifier = externalIdentifier;
                item.AnnotatedTitleID = annotatedTitleId;
                item.Volume = volume;
                item = dal.AnnotatedItemInsertAuto(null, null, item);
            }
            return item;
        }

        public bool AnnotatedItemCheckForSurrogate(int annotatedItemID)
        {
            AnnotatedItemDAL dal = new AnnotatedItemDAL();
            return dal.AnnotatedItemCheckForSurrogate(null, null, annotatedItemID);
        }
    }
}
