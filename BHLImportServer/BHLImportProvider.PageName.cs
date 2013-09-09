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
                PageName newPageName = new PageName();
                newPageName.ImportStatusID = 10;
                newPageName.ImportSourceID = importSourceID;
                newPageName.BarCode = barCode;
                newPageName.FileNamePrefix = fileNamePrefix;
                newPageName.SequenceOrder = sequenceOrder;
                newPageName.NameFound = nameFound;
                newPageName.Source = source;
                newPageName.NameConfirmed = nameConfirmed;
                newPageName.NameBankID = nameBankID;
                newPageName.Active = active;
                newPageName.IsCommonName = isCommonName;
                newPageName.ExternalCreateDate = externalCreateDate;
                newPageName.ExternalLastUpdateDate = externalLastUpdateDate;

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
