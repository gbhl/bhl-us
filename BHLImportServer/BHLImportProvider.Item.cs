using System;
using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public Item SaveItem(int importSourceID, string barCode, string marcBibID,
            short? itemSequence, string marcItemID, string callNumber, 
            string volume, string institutionCode, string languageCode, string sponsor,
            string itemDescription, int? scannedBy, int? pdfSize, int? vaultID, 
            short? numberOfFiles, string note, int itemStatusID, string scanningUser, 
            DateTime? scanningDate, int? paginationCompleteUserID, DateTime? paginationCompleteDate,
            int? paginationStatusID, int? paginationStatusUserID, 
            DateTime? paginationStatusDate, DateTime? lastPageNameLookupDate,
            DateTime? externalCreationDate, DateTime? externalLastModifiedDate,
            int? externalCreationUser, int? externalLastModifiedUser)
        {
            ItemDAL dal = new ItemDAL();
            Item savedItem = dal.ItemSelectNewByBarCodeAndSource(null, null, barCode, importSourceID);

            if (savedItem == null)
            {
                Item newItem = new Item();
                newItem.ImportKey = marcBibID;
                newItem.ImportStatusID = 10;
                newItem.ImportSourceID = importSourceID;
                newItem.BarCode = barCode;
                newItem.MARCBibID = marcBibID;
                newItem.ItemSequence = itemSequence;
                newItem.MARCItemID = marcItemID;
                newItem.CallNumber = callNumber;
                newItem.Volume = volume;
                newItem.InstitutionCode = institutionCode;
                newItem.LanguageCode = languageCode;
                newItem.Sponsor = sponsor;
                newItem.ItemDescription = itemDescription;
                newItem.ScannedBy = scannedBy;
                newItem.PDFSize = pdfSize;
                newItem.VaultID = vaultID;
                newItem.NumberOfFiles = numberOfFiles;
                newItem.Note = note;
                newItem.ItemStatusID = itemStatusID;
                newItem.ScanningUser = scanningUser;
                newItem.ScanningDate = scanningDate;
                newItem.PaginationCompleteUserID = paginationCompleteUserID;
                newItem.PaginationCompleteDate = paginationCompleteDate;
                newItem.PaginationStatusID = paginationStatusID;
                newItem.PaginationStatusUserID = paginationStatusUserID;
                newItem.PaginationStatusDate = paginationStatusDate;
                newItem.LastPageNameLookupDate = lastPageNameLookupDate;
                newItem.ExternalCreationDate = externalCreationDate;
                newItem.ExternalLastModifiedDate = externalLastModifiedDate;
                newItem.ExternalCreationUser = externalCreationUser;
                newItem.ExternalLastModifiedUser = externalLastModifiedUser;

                savedItem = dal.ItemInsertAuto(null, null, newItem);
            }
            else
            {
                if (savedItem.ItemSequence != itemSequence ||
                    savedItem.MARCItemID != marcItemID ||
                    savedItem.CallNumber != callNumber ||
                    savedItem.Volume != volume ||
                    savedItem.InstitutionCode != institutionCode ||
                    savedItem.LanguageCode != languageCode ||
                    savedItem.Sponsor != sponsor ||
                    savedItem.ItemDescription != itemDescription ||
                    savedItem.ScannedBy != scannedBy ||
                    savedItem.PDFSize != pdfSize ||
                    savedItem.VaultID != vaultID ||
                    savedItem.NumberOfFiles != numberOfFiles ||
                    savedItem.Note != note ||
                    savedItem.ItemStatusID != itemStatusID ||
                    savedItem.ScanningUser != scanningUser ||
                    savedItem.ScanningDate != scanningDate ||
                    savedItem.PaginationCompleteUserID != paginationCompleteUserID ||
                    savedItem.PaginationCompleteDate != paginationCompleteDate ||
                    savedItem.PaginationStatusID != paginationStatusID ||
                    savedItem.PaginationStatusUserID != paginationStatusUserID ||
                    savedItem.PaginationStatusDate != paginationStatusDate ||
                    savedItem.LastPageNameLookupDate != lastPageNameLookupDate
                    )
                {
                    savedItem.ItemSequence = itemSequence;
                    savedItem.MARCItemID = marcItemID;
                    savedItem.CallNumber = callNumber;
                    savedItem.Volume = volume;
                    savedItem.InstitutionCode = institutionCode;
                    savedItem.LanguageCode = languageCode;
                    savedItem.Sponsor = sponsor;
                    savedItem.ItemDescription = itemDescription;
                    savedItem.ScannedBy = scannedBy;
                    savedItem.PDFSize = pdfSize;
                    savedItem.VaultID = vaultID;
                    savedItem.NumberOfFiles = numberOfFiles;
                    savedItem.Note = note;
                    savedItem.ItemStatusID = itemStatusID;
                    savedItem.ScanningUser = scanningUser;
                    savedItem.ScanningDate = scanningDate;
                    savedItem.PaginationCompleteUserID = paginationCompleteUserID;
                    savedItem.PaginationCompleteDate = paginationCompleteDate;
                    savedItem.PaginationStatusID = paginationStatusID;
                    savedItem.PaginationStatusUserID = paginationStatusUserID;
                    savedItem.PaginationStatusDate = paginationStatusDate;
                    savedItem.LastPageNameLookupDate = lastPageNameLookupDate;
                    savedItem.ExternalLastModifiedDate = externalLastModifiedDate;
                    savedItem.ExternalLastModifiedUser = externalLastModifiedUser;

                    dal.ItemUpdateAuto(null, null, savedItem);
                }
            }

            return savedItem;
        }
    }
}
