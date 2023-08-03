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
                Item newItem = new Item
                {
                    ImportKey = marcBibID,
                    ImportStatusID = 10,
                    ImportSourceID = importSourceID,
                    BarCode = barCode,
                    MARCBibID = marcBibID,
                    ItemSequence = itemSequence,
                    MARCItemID = marcItemID,
                    CallNumber = callNumber,
                    Volume = volume,
                    InstitutionCode = institutionCode,
                    LanguageCode = languageCode,
                    Sponsor = sponsor,
                    ItemDescription = itemDescription,
                    ScannedBy = scannedBy,
                    PDFSize = pdfSize,
                    VaultID = vaultID,
                    NumberOfFiles = numberOfFiles,
                    Note = note,
                    ItemStatusID = itemStatusID,
                    ScanningUser = scanningUser,
                    ScanningDate = scanningDate,
                    PaginationCompleteUserID = paginationCompleteUserID,
                    PaginationCompleteDate = paginationCompleteDate,
                    PaginationStatusID = paginationStatusID,
                    PaginationStatusUserID = paginationStatusUserID,
                    PaginationStatusDate = paginationStatusDate,
                    LastPageNameLookupDate = lastPageNameLookupDate,
                    ExternalCreationDate = externalCreationDate,
                    ExternalLastModifiedDate = externalLastModifiedDate,
                    ExternalCreationUser = externalCreationUser,
                    ExternalLastModifiedUser = externalLastModifiedUser
                };

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
