using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Page_PageType SavePage_PageType(int importSourceID, string barCode, 
            string fileNamePrefix, int? sequenceOrder, int pageTypeID, bool verified,
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate,
            int? externalCreationUser, int? externalLastModifiedUser)
        {
            Page_PageTypeDAL dal = new Page_PageTypeDAL();
            Page_PageType savedPage_PageType = dal.Page_PageTypeSelectNewByKeyValuesAndSource(null, null, 
                barCode, fileNamePrefix, pageTypeID, importSourceID);

            if (savedPage_PageType == null)
            {
                Page_PageType newPage_PageType = new Page_PageType
                {
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    BarCode = barCode,
                    FileNamePrefix = fileNamePrefix,
                    SequenceOrder = sequenceOrder,
                    PageTypeID = pageTypeID,
                    Verified = verified,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate,
                    ExternalCreationUser = externalCreationUser,
                    ExternalLastModifiedUser = externalLastModifiedUser
                };

                savedPage_PageType = dal.Page_PageTypeInsertAuto(null, null, newPage_PageType);
            }
            else
            {
                if (savedPage_PageType.SequenceOrder != sequenceOrder ||
                    savedPage_PageType.PageTypeID != pageTypeID ||
                    savedPage_PageType.Verified != verified)
                {
                    savedPage_PageType.SequenceOrder = sequenceOrder;
                    savedPage_PageType.PageTypeID = pageTypeID;
                    savedPage_PageType.Verified = verified;
                    savedPage_PageType.ExternalLastModifiedDate = externalLastModifiedDate;
                    savedPage_PageType.ExternalLastModifiedUser = externalLastModifiedUser;

                    dal.Page_PageTypeUpdateAuto(null, null, savedPage_PageType);
                }
            }

            return savedPage_PageType;
        }
    }
}
