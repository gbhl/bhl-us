using MOBOT.BHL.Utility;
using MOBOT.BHLImport.DAL;
using MOBOT.BHLImport.DataObjects;
using System;
using System.Collections.Generic;

namespace MOBOT.BHLImport.Server
{
    public partial class BHLImportProvider
    {
        private const int ITEMSTATUS_NEW = 10;
        private const int ITEMSTATUS_PENDINGAPPROVAL = 20;
        private const int ITEMSTATUS_APPROVED = 30;
        private const int ITEMSTATUS_COMPLETE = 40;
        private const int ITEMSTATUS_MARCMISSINGNEW = 80;
        private const int ITEMSTATUS_MARCMISSINGAPPROVED = 81;
        private const int ITEMSTATUS_MARCMISSINGONHOLD = 82;
        private const int ITEMSTATUS_ERROR = 99;

        public IAItem SaveIAItemID(string iaIdentifier, string localFileFolder, DateTime? dateStamp, bool noMarcOK, int userId = 1)
        {
            IAItemDAL dal = new IAItemDAL();
            IAItem savedItem = dal.IAItemSelectByIAIdentifier(null, null, iaIdentifier);

            if (savedItem == null)
            {
                IAItem newItem = new IAItem
                {
                    IAIdentifier = iaIdentifier,
                    ItemStatusID = ITEMSTATUS_NEW,
                    LocalFileFolder = localFileFolder,
                    IADateStamp = dateStamp,
                    NoMARCOk = (byte)(noMarcOK ? 1 : 0),
                    CreatedUserID = userId,
                    LastModifiedUserID = userId
                };
                savedItem = dal.IAItemInsertAuto(null, null, newItem);
            }
            else
            {
                savedItem.IADateStamp = dateStamp;
                dal.IAItemUpdateAuto(null, null, savedItem);
            }

            return savedItem;
        }

        public IAItem IAItemSelectAuto(int itemID)
        {
            return (new IAItemDAL().IAItemSelectAuto(null, null, itemID));
        }

        public List<IAItem> IAItemSelectForXMLDownload(String iaIdentifier)
        {
            return (new IAItemDAL().IAItemSelectForXMLDownload(null, null, iaIdentifier));
        }

        public List<IAItem> IAItemSelectForPublishToImportTables(String iaIdentifier)
        {
            return (new IAItemDAL().IAItemSelectForPublishToImportTables(null, null, iaIdentifier));
        }

        public bool IAItemPublishToProduction(int itemID)
        {
            return (new IAItemDAL().IAItemPublishToProduction(null, null, itemID));
        }

        public bool IAItemPublishToImportTables(int itemID)
        {
            return (new IAItemDAL().IAItemPublishToImportTables(null, null, itemID));
        }

        public IAItem IAItemUpdateLastXMLDataHarvestDate(int itemID, int userId = 1)
        {
            IAItemDAL dal = new IAItemDAL();
            IAItem savedItem = dal.IAItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.LastXMLDataHarvestDate = DateTime.Now;
                savedItem.LastModifiedDate = DateTime.Now;
                savedItem.LastModifiedUserID = userId;
                savedItem = dal.IAItemUpdateAuto(null, null, savedItem);
            }
            else
            {
                throw new Exception("Could not find existing Item record.");
            }
            return savedItem;
        }

        public void IAItemUpdateItemStatusSetError(int itemID,
            int statusID, string procedure, string message, int userId = 1)
        {
            // Simple error logging, so skipping the use of a transaction here.
            IAItemError itemError = new IAItemError
            {
                ItemID = itemID,
                ErrorDate = DateTime.Now,
                Procedure = procedure,
                Message = message,
                IsNew = true
            };
            new IAItemErrorDAL().IAItemErrorInsertAuto(null, null, itemError);

            IAItemDAL itemDAL = new IAItemDAL();
            IAItem item = itemDAL.IAItemSelectAuto(null, null, itemID);
            if (item != null)
            {
                item.ItemStatusID = statusID;
                item.LastModifiedDate = DateTime.Now;
                item.LastModifiedUserID = userId;
                itemDAL.IAItemUpdateAuto(null, null, item);
            }
        }

        public IAItem IAItemUpdateItemStatusIDAfterDataHarvest(int itemID, 
            bool allowUnapproved, int minDaysBeforeAllowUnapproved, int userId = 1)
        {
            IAItemDAL dal = new IAItemDAL();
            IAItem savedItem = dal.IAItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                // Don't update the item status if this item has already been loaded into
                // the production database, or if the xml data harvest has not been
                // completed.
                if ((savedItem.LastProductionDate == null) && (savedItem.LastXMLDataHarvestDate != null))
                {
                    // - If the external (Internet Archive) status is "Approved" and
                    // the XML data harvest is complete, then set this item's status 
                    // to Approved.
                    // - Alternately, if it is OK to load "unapproved" items, then
                    // check if the item is ok to publish (even if not formally 
                    // approved by IA).
                    // - Otherwise, set the item to Pending Approval.
                    // - Approved items will be loaded into the production database.
                    if (String.Compare(savedItem.ExternalStatus, "approved", true) == 0)
                        savedItem.ItemStatusID = ITEMSTATUS_APPROVED;
                    else if (allowUnapproved && this.IAItemSelectOKToPublish(itemID, minDaysBeforeAllowUnapproved) &&
                        (String.Compare(savedItem.ExternalStatus, "freeze", true) != 0))
                        savedItem.ItemStatusID = ITEMSTATUS_APPROVED;
                    else
                        savedItem.ItemStatusID = ITEMSTATUS_PENDINGAPPROVAL;

                    savedItem.LastModifiedDate = DateTime.Now;
                    savedItem.LastModifiedUserID = userId;

                    savedItem = dal.IAItemUpdateAuto(null, null, savedItem);
                }
            }
            else
            {
                throw new Exception("Could not find existing Item record.");
            }
            return savedItem;
        }

        private bool IAItemSelectOKToPublish(int itemID, int minDaysBeforeAllowUnapproved)
        {
            IAItemDAL dal = new IAItemDAL();
            List<IAItem> items = dal.IAItemSelectOKToPublish(null, null, itemID, minDaysBeforeAllowUnapproved);
            if (items.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Update the item with metadata information.
        /// </summary>
        public IAItem IAItemUpdateMetadata(int itemID, string sponsor, string sponsorDate, 
            string scanningCenter, string callNumber, int imageCount, string identifierAccessUrl, 
            string volume, string issue, string note, string scanOperator, string scanDate, 
            DateTime? addedDate, string externalStatus, string titleID, string year, string identifierBib,
            string licenseUrl, string rights, string dueDiligence, string possibleCopyrightStatus,
            string copyrightRegion, string copyrightComment, string copyrightEvidence,
            string copyrightEvidenceOperator, string copyrightEvidenceDate, string scanningInstitution,
            string rightsHolder, string itemDescription, string pageProgression, string virtualVolume,
            int? virtualTitleID, string summary, string genre, int userId = 1)
        {
            // Standardize the format of the year value
            year = DataCleaner.CleanYear(year);

            if (!DataCleaner.ValidateItemYear(year))
            {
                throw new Exception(string.Format("Invalid Year format in metadata file: {0}", year));
            }

            // Parse the year and volume into their component parts.
            // If a Virtual Volume value is provided, parse it.  Otherwise, use the "regular" volume value.
            YearData yearData = DataCleaner.ParseYearString(year);
            VolumeData volumeData = DataCleaner.ParseVolumeString(string.IsNullOrWhiteSpace(virtualVolume) ? volume : virtualVolume);

            IAItemDAL dal = new IAItemDAL();
            IAItem savedItem = dal.IAItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.Sponsor = sponsor;
                savedItem.SponsorDate = sponsorDate;
                savedItem.ScanningCenter = scanningCenter;
                savedItem.CallNumber = callNumber;
                savedItem.ImageCount = imageCount;
                savedItem.IdentifierAccessUrl = identifierAccessUrl;
                savedItem.Volume = volume;
                savedItem.Issue = issue;
                savedItem.Note = note;
                savedItem.ScanOperator = scanOperator;
                savedItem.ScanDate = scanDate;
                savedItem.ExternalStatus = externalStatus;
                savedItem.TitleID = titleID;
                savedItem.Year = string.IsNullOrWhiteSpace(year) ? volumeData.StartYear : yearData.StartYear;
                savedItem.IdentifierBib = identifierBib;
                savedItem.LicenseUrl = licenseUrl;
                savedItem.Rights = rights;
                savedItem.DueDiligence = dueDiligence;
                savedItem.PossibleCopyrightStatus = possibleCopyrightStatus;
                savedItem.CopyrightRegion = copyrightRegion;
                savedItem.CopyrightComment = copyrightComment;
                savedItem.CopyrightEvidence = copyrightEvidence;
                savedItem.CopyrightEvidenceOperator = copyrightEvidenceOperator;
                savedItem.CopyrightEvidenceDate = copyrightEvidenceDate;
                savedItem.IAAddedDate = addedDate;
                savedItem.ScanningInstitution = scanningInstitution;
                savedItem.RightsHolder = rightsHolder;
                savedItem.ItemDescription = itemDescription;
                savedItem.PageProgression = pageProgression;
                savedItem.EndYear = string.IsNullOrWhiteSpace(year) ? volumeData.EndYear : yearData.EndYear;
                savedItem.StartVolume = volumeData.StartVolume;
                savedItem.EndVolume = volumeData.EndVolume;
                savedItem.StartIssue = volumeData.StartIssue;
                savedItem.EndIssue = volumeData.EndIssue;
                savedItem.StartNumber = volumeData.StartNumber;
                savedItem.EndNumber = volumeData.EndNumber;
                savedItem.StartSeries = volumeData.StartSeries;
                savedItem.EndSeries = volumeData.EndSeries;
                savedItem.StartPart = volumeData.StartPart;
                savedItem.EndPart = volumeData.EndPart;
                savedItem.VirtualTitleID = virtualTitleID;
                savedItem.VirtualVolume = virtualVolume;
                savedItem.Summary = summary;
                savedItem.Genre = genre;
                savedItem.LastModifiedDate = DateTime.Now;
                savedItem.LastModifiedUserID = userId;
                savedItem = dal.IAItemUpdateAuto(null, null, savedItem);
            }
            else
            {
                throw new Exception("Could not find existing Item record.");
            }
            return savedItem;
        }

        /// <summary>
        /// Update the item with metadata source information
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="sponsorName"></param>
        /// <returns></returns>
        public IAItem IAItemUpdateMetadataSource(int itemID, string sponsorName, string zQuery, int userId = 1)
        {
            IAItemDAL dal = new IAItemDAL();
            IAItem savedItem = dal.IAItemSelectAuto(null, null, itemID);
            if (savedItem != null)
            {
                savedItem.SponsorName = sponsorName;
                savedItem.ZQuery = zQuery;
                savedItem.LastModifiedDate = DateTime.Now;
                savedItem.LastModifiedUserID = userId;
                savedItem = dal.IAItemUpdateAuto(null, null, savedItem);
            }
            else
            {
                throw new Exception("Could not find existing Item record.");
            }
            return savedItem;
        }

        public List<IAItem> IAItemSelectPendingApproval(int ageInDays)
        {
            return (new IAItemDAL().IAItemSelectPendingApproval(null, null, ageInDays));
        }

        public void IAItemInsertFromIAAnalysis(String localFileFolder)
        {
            new IAItemDAL().IAItemInsertFromIAAnalysis(null, null, localFileFolder);
        }

        public List<IAItem> IAItemSelectByStatus(int itemStatusID, string iaId, int numberOfRows, int pageNumber,
            string sortColumn, string sortDirection)
        {
            return new IAItemDAL().IAItemSelectByStatus(null, null, itemStatusID, iaId, numberOfRows, pageNumber, sortColumn, sortDirection);
        }

        /// <summary>
        /// Checks if the specified identifier is already in the import database.  If not, it is added.
        /// If so, and it is not "Approved" or "Complete", it is set to "New" status.  If it is already
        /// "Approved" or "Complete", it is NOT queued for download.
        /// </summary>
        /// <param name="iaIdentifier"></param>
        /// <param name="localFileFolder"></param>
        /// <param name="userId"></param>
        /// <returns>Array of two elements. First element contains "true" or "false".  Second element 
        /// contains message detailing the success or error.</returns>
        public string[] IAItemQueueForDownload(string iaIdentifier, string localFileFolder, int userId)
        {
            string[] results = new string[2];
            string returnValue = "true";
            string returnMessage;
            IAItemDAL itemDAL = new IAItemDAL();
            IAItem item;

            try
            {
                item = itemDAL.IAItemSelectByIAIdentifier(null, null, iaIdentifier);

                if (item == null)
                {
                    // Add new item
                    item = new IAItem
                    {
                        IAIdentifier = iaIdentifier,
                        ItemStatusID = ITEMSTATUS_NEW,
                        LocalFileFolder = localFileFolder,
                        CreatedUserID = userId,
                        LastModifiedUserID = userId
                    };
                    item = itemDAL.IAItemInsertAuto(null, null, item);
                    returnMessage = "'" + iaIdentifier + "' has been queued for download.";
                }
                else
                {
                    switch(item.ItemStatusID)
                    {
                        case 10:
                            returnValue = "false";
                            returnMessage = "'" + iaIdentifier + "' is already queued for download.";
                            break;
                        case 30:
                            returnValue = "false";
                            returnMessage = "'" + iaIdentifier + "' has already been downloaded and is in 'Approved' status.  It will be moved to be production within 24 hours.";
                            break;
                        case 40:
                            returnValue = "false";
                            returnMessage = "'" + iaIdentifier + "' has already been downloaded and is in 'Complete' status.  If not already in production, it will be moved to be production within 24 hours.";
                            break;
                        default:
                            // Reset item for download
                            itemDAL.IAItemResetForDownload(null, null, iaIdentifier, userId);
                            returnMessage = "'" + iaIdentifier + "' has been queued for download.";
                            break;
                    }
                }

                results[0] = returnValue;
                results[1] = returnMessage;
            }
            catch (Exception ex)
            {
                results[0] = "false";
                results[1] = ex.Message;
            }

            return results;
        }

        /// <summary>
        /// Update the specified item to the specified status.  If setting to NEW, then item is fully
        /// set for re-download.  If setting to or from MARC MISSING - APPROVED, the flag to load 
        /// without a MARC record is set accordingly.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemStatusId"></param>
        /// <returns>Array of three elements. First element contains "true" or "false".  Second element 
        /// contains message detailing the success or error.  Third element contains the IA Identifier
        /// for the updated item.</returns>
        public string[] IAItemUpdateStatus(int itemId, int itemStatusId, int userId)
        {
            string[] results = new string[3];
            string returnValue = "true";
            string returnMessage = string.Empty;
            string returnIdentifier = string.Empty;

            try
            {
                IAItemDAL itemDal = new IAItemDAL();
                IAItem item = itemDal.IAItemSelectAuto(null, null, itemId);

                if (item != null)
                {
                    returnIdentifier = item.IAIdentifier;

                    // If status changing FROM "MARC Missing - Approved", then turn off NoMARCOk flag
                    if (item.ItemStatusID == ITEMSTATUS_MARCMISSINGAPPROVED) item.NoMARCOk = 0;

                    if (itemStatusId == ITEMSTATUS_NEW)
                    {
                        // Setting item for re-download
                        itemDal.IAItemResetForDownload(null, null, item.IAIdentifier, userId);
                    }
                    else 
                    {
                        // If status changing TO "MARC Missing - Approved", then turn on NoMARCOk flag
                        if (itemStatusId == ITEMSTATUS_MARCMISSINGAPPROVED) item.NoMARCOk = 1;

                        item.ItemStatusID = itemStatusId;
                        item.LastModifiedDate = DateTime.Now;
                        item.LastModifiedUserID = userId;
                        itemDal.IAItemUpdateAuto(null, null, item);
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = "false";
                returnMessage = ex.Message;
            }

            results[0] = returnValue;
            results[1] = returnMessage;
            results[2] = returnIdentifier;
            return results;
        }
    }
}
