using CustomDataAccess;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        public List<Item> ItemSelectForPublishToProduction(string barCode)
        {
            ItemDAL dal = new ItemDAL();
            return dal.ItemSelectForPublishToProduction(null, null, barCode);
        }

        public IAPublishResult ItemPublishToProductionIA(string barCode)
        {
            ItemDAL dal = new ItemDAL();
            CustomDataRow row = dal.ItemPublishToProductionIA(null, null, barCode);
            if (row != null)
            {
                bool success = row["ItemType"].Value != null;
                string itemType = success ? row["ItemType"].Value.ToString() : null;
                int id = success ? Convert.ToInt32(row["ID"].Value) : 0;
                return new IAPublishResult(success, itemType, id, barCode);
            }
            else
            {
                return new IAPublishResult(false, null, 0, barCode);
            }

        }

        /// <summary>
        /// Data struture returned by the ItemPublishToProductionIA method.  It contains the result of the attempt to publish an item to production.
        /// </summary>
        public readonly struct IAPublishResult
        {
            public IAPublishResult(bool success, string itemType, int id, string barcode)
            {
                Success = success;
                ItemType = itemType;
                ID = id;
                BarCode = barcode;
            }

            public bool Success { get; }
            public string ItemType { get; }
            public int? ID { get; }
            public string BarCode { get; }

        }

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
