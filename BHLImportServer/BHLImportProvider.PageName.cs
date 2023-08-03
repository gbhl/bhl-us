using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public PageName SavePageName(int importSourceID, string barCode,
            string fileNamePrefix, int? sequenceOrder, string source, string nameFound,
            string nameConfirmed, int? nameBankID, bool active, bool? isCommonName,
            DateTime? externalCreateDate, DateTime? externalLastUpdateDate)
        {
            PageNameDAL dal = new PageNameDAL();
            PageName savedPageName = dal.PageNameSelectNewByKeyValuesAndSource(null, null,
                barCode, fileNamePrefix, nameFound, importSourceID);

            if (savedPageName == null)
            {
                PageName newPageName = new PageName
                {
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    BarCode = barCode,
                    FileNamePrefix = fileNamePrefix,
                    SequenceOrder = sequenceOrder,
                    NameFound = nameFound,
                    Source = source,
                    NameConfirmed = nameConfirmed,
                    NameBankID = nameBankID,
                    Active = active,
                    IsCommonName = isCommonName,
                    ExternalCreateDate = externalCreateDate,
                    ExternalLastUpdateDate = externalLastUpdateDate
                };

                savedPageName = dal.PageNameInsertAuto(null, null, newPageName);
            }
            else
            {
                if (savedPageName.SequenceOrder != sequenceOrder ||
                    savedPageName.Source != source ||
                    savedPageName.NameConfirmed != nameConfirmed ||
                    savedPageName.NameBankID != nameBankID ||
                    savedPageName.Active != active ||
                    savedPageName.IsCommonName != isCommonName)
                {
                    savedPageName.SequenceOrder = sequenceOrder;
                    savedPageName.Source = source;
                    savedPageName.NameConfirmed = nameConfirmed;
                    savedPageName.NameBankID = nameBankID;
                    savedPageName.Active = active;
                    savedPageName.IsCommonName = isCommonName;
                    savedPageName.ExternalLastUpdateDate = externalLastUpdateDate;

                    dal.PageNameUpdateAuto(null, null, savedPageName);
                }
            }

            return savedPageName;
        }
    }
}
