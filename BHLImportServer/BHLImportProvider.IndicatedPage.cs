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
                IndicatedPage newIndicatedPage = new IndicatedPage();
                newIndicatedPage.ImportStatusID = 10;
                newIndicatedPage.ImportSourceID = importSourceID;
                newIndicatedPage.BarCode = barCode;
                newIndicatedPage.FileNamePrefix = fileNamePrefix;
                newIndicatedPage.SequenceOrder = sequenceOrder;
                newIndicatedPage.Sequence = sequence;
                newIndicatedPage.PagePrefix = pagePrefix;
                newIndicatedPage.PageNumber = pageNumber;
                newIndicatedPage.Implied = implied;
                newIndicatedPage.ExternalCreationDate = externalCreationDate;
                newIndicatedPage.ExternalLastModifiedDate = externalLastModifiedDate;
                newIndicatedPage.ExternalCreationUser = externalCreationUser;
                newIndicatedPage.ExternalLastModifiedUser = externalLastModifiedUser;

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
