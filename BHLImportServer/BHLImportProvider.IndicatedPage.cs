using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public IndicatedPage SaveIndicatedPage(int importSourceID, string barCode, string fileNamePrefix,
            int? sequenceOrder, short? sequence, string pagePrefix, string pageNumber, bool implied,
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate,
            int? externalCreationUser, int? externalLastModifiedUser)
        {
            IndicatedPageDAL dal = new IndicatedPageDAL();
            IndicatedPage savedIndicatedPage = dal.IndicatedPageSelectNewByKeyValuesAndSource(null, null, barCode,
                fileNamePrefix, sequence, importSourceID);

            if (savedIndicatedPage == null)
            {
                IndicatedPage newIndicatedPage = new IndicatedPage
                {
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    BarCode = barCode,
                    FileNamePrefix = fileNamePrefix,
                    SequenceOrder = sequenceOrder,
                    Sequence = sequence,
                    PagePrefix = pagePrefix,
                    PageNumber = pageNumber,
                    Implied = implied,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate,
                    ExternalCreationUser = externalCreationUser,
                    ExternalLastModifiedUser = externalLastModifiedUser
                };

                savedIndicatedPage = dal.IndicatedPageInsertAuto(null, null, newIndicatedPage);
            }
            else
            {
                if (savedIndicatedPage.SequenceOrder != sequenceOrder ||
                    savedIndicatedPage.PagePrefix != pagePrefix ||
                    savedIndicatedPage.PageNumber != pageNumber ||
                    savedIndicatedPage.Implied != implied)
                {
                    savedIndicatedPage.SequenceOrder = sequenceOrder;
                    savedIndicatedPage.PagePrefix = pagePrefix;
                    savedIndicatedPage.PageNumber = pageNumber;
                    savedIndicatedPage.Implied = implied;
                    savedIndicatedPage.ExternalLastModifiedDate = externalLastModifiedDate;
                    savedIndicatedPage.ExternalLastModifiedUser = externalLastModifiedUser;

                    dal.IndicatedPageUpdateAuto(null, null, savedIndicatedPage);
                }
            }

            return savedIndicatedPage;
        }
    }
}
