using CustomDataAccess;
using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using MOBOT.BHL.Utility;
using System;
using System.Text.RegularExpressions;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public ImportRecordStatus ImportRecordStatusSelectAuto(int importRecordStatusID)
        {
            return new ImportRecordStatusDAL().ImportRecordStatusSelectAuto(null, null, importRecordStatusID);
        }

        public CustomGenericList<ImportRecordStatus> ImportRecordStatusSelectAll()
        {
            return new ImportRecordStatusDAL().SelectAll(null, null);
        }

        public CustomGenericList<ImportRecord> ImportRecordSelectByImportFileID(int importFileID, int numRows, int startRow,
            string sortColumn, string sortDirection, int extended = 0)
        {
            return new ImportRecordDAL().ImportRecordSelectByImportFileID(null, null, importFileID, numRows, startRow,
                sortColumn, sortDirection, extended);
        }

        public void ImportRecordSave(ImportRecord citation, int userID)
        {
            // Perform the following actions if the record is new
            if (citation.ImportRecordID == default(int))
            {
                // Get production author IDs
                foreach(ImportRecordCreator author in citation.Authors)
                {
                    CustomGenericList<Author> authors = AuthorResolve(author.FullName, author.LastName,
                        author.FirstName, author.StartYear, author.EndYear);
                    if (authors.Count == 1) author.AuthorID = authors[0].AuthorID;
                }

                // Validate the imported record
                if (!IsImportRecordValid(citation))
                {
                    citation.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Invalid;
                }

                if (citation.ImportRecordStatusID == default(int))
                {
                    // Check for completeness of the imported record.
                    if (!IsImportRecordComplete(citation))
                    {
                        citation.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Incomplete;
                    }
                }

                if (citation.ImportRecordStatusID == default(int))
                {

                    // Resolve with production segments.  If duplicates found, set status to Duplicate.
                    CustomGenericList<Segment> segments = SegmentResolve(citation.DOI, citation.StartPageID ?? 0);
                    if (segments.Count > 0)
                    {
                        citation.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Duplicate;
                    }
                }
            }

            // If no status set, set it to New
            if (citation.ImportRecordStatusID == default(int))
            {
                citation.ImportRecordStatusID = (int)BHL.DataObjects.Enum.ImportRecordStatus.New;
            }

            new ImportRecordDAL().ImportRecordSave(null, null, citation, userID);
        }

        public ImportRecord ImportRecordUpdateRecordStatus(int importRecordID, int importRecordStatusID, int userID)
		{
            ImportRecordDAL dal = new ImportRecordDAL();
			ImportRecord savedImportRecord = dal.ImportRecordSelectAuto(null, null, importRecordID);
			if ( savedImportRecord != null )
			{
                // If changing 'Incomplete' or 'Rejected' status to 'New', then resolve this record 
                // against production data to determine if it is a duplicate of an existing segment.
                if (importRecordStatusID == (int)DataObjects.Enum.ImportRecordStatus.New &&
                    (savedImportRecord.ImportRecordStatusID == (int)DataObjects.Enum.ImportRecordStatus.Incomplete ||
                    savedImportRecord.ImportRecordStatusID == (int)DataObjects.Enum.ImportRecordStatus.Rejected))
                {
                    // Resolve with production segments.  If duplicates found, set status to Duplicate.
                    CustomGenericList<Segment> segments = SegmentResolve(savedImportRecord.DOI,
                        savedImportRecord.StartPageID ?? 0);
                    if (segments.Count > 0)
                    {
                        savedImportRecord.ImportRecordStatusID = (int)DataObjects.Enum.ImportRecordStatus.Duplicate;
                    }
                }

                savedImportRecord.ImportRecordStatusID = importRecordStatusID;
                savedImportRecord.LastModifiedDate = DateTime.Now;
				savedImportRecord.LastModifiedUserID = userID;
				savedImportRecord = dal.ImportRecordUpdateAuto(null, null, savedImportRecord);
			}
			else
			{
				throw new Exception( "Could not find existing ImportRecord." );
			}
			return savedImportRecord;
		}

        public ImportRecordCreator ImportRecordCreatorUpdateAuthorID(int importRecordCreatorID, int? AuthorID, int userID)
        {
            ImportRecordCreatorDAL dal = new ImportRecordCreatorDAL();
            ImportRecordCreator savedImportRecordCreator = dal.ImportRecordCreatorSelectAuto(null, null, importRecordCreatorID);
            if (savedImportRecordCreator != null)
            {
                savedImportRecordCreator.AuthorID = AuthorID;
                savedImportRecordCreator.LastModifiedDate = DateTime.Now;
                savedImportRecordCreator.LastModifiedUserID = userID;
                savedImportRecordCreator = dal.ImportRecordCreatorUpdateAuto(null, null, savedImportRecordCreator);
            }
            else
            {
                throw new Exception("Could not find existing ImportRecordCreator.");
            }
            return savedImportRecordCreator;
        }

        /// <summary>
        /// Ensure that the imported record has valid Title, Item, and Page IDs (if they were provided).
        /// Also look for invalid author names (ex. "et al") and mal-formed DOIs.
        /// 
        /// The "final" fields for all valid identifiers should be populated (ex. ItemID, TitleID, list 
        /// of ImportRecordPage records).
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        private bool IsImportRecordValid (ImportRecord citation)
        {
            bool isValid = true;

            // Check data type of item identifier
            if (string.IsNullOrWhiteSpace(citation.ItemIDString))
            {
                citation.ItemID = null;
            }
            else
            {
                if (Int32.TryParse(citation.ItemIDString, out int itemID))
                {
                    citation.ItemID = itemID;
                }
                else
                {
                    citation.ItemID = null;
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Item ID {0}", citation.ItemIDString)));
                    isValid = false;
                }
            }

            // Make sure Item ID is valid BHL identifier
            if (citation.ItemID != null)
            {
                if (ItemSelectAuto((int)citation.ItemID) == null)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Item {0} not found", citation.ItemIDString)));
                    isValid = false;
                }
            }

            // Verify the start and end page identifiers
            int startPageID = -1;
            int endPageID = -1;
            if (!string.IsNullOrWhiteSpace(citation.StartPageIDString))
            {
                if (!Int32.TryParse(citation.StartPageIDString, out startPageID))
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Start Page ID {0}", citation.StartPageID)));
                    isValid = false;
                }
                else
                {
                    citation.StartPageID = startPageID;
                    Page page = PageSelectAuto(startPageID);
                    if (page == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("Start Page {0} not found", citation.StartPageID)));
                        isValid = false;
                    }
                    else
                    {
                        if (citation.ItemID == null) citation.ItemID = page.ItemID;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(citation.EndPageIDString))
            {
                if (!Int32.TryParse(citation.EndPageIDString, out endPageID))
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid End Page ID {0}", citation.EndPageID)));
                    isValid = false;
                }
                else
                {
                    citation.EndPageID = endPageID;
                    Page page = PageSelectAuto(endPageID);
                    if (page == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("End Page {0} not found", citation.EndPageID)));
                        isValid = false;
                    }
                    else
                    {
                        if (citation.ItemID == null) citation.ItemID = page.ItemID;
                    }
                }
            }

            // Given metadata and StartPage/EndPage (no IDs), try to find a single matching Item ID.  
            // If no item is found, this citation is still valid, but incomplete.
            if (citation.ItemID == null && startPageID == -1 && endPageID == -1)
            {
                CustomGenericList<Item> items = ItemResolve(citation.JournalTitle, citation.ISSN,
                    citation.ISBN, citation.OCLC, citation.Volume, citation.Issue, citation.Year);
                if (items.Count == 1) citation.ItemID = items[0].ItemID;
            }

            // Given an ItemID and StartPage/EndPage, try to find unique IDs for the Start and End page.
            // If the IDs are not found, this citation is valid, but incomplete.
            if (citation.ItemID != null && startPageID == -1 && !string.IsNullOrWhiteSpace(citation.StartPage))
            {
                CustomGenericList<Page> startPageIDs = PageSelectByItemAndPageNumber(
                    (int)citation.ItemID, citation.Volume, citation.Issue, citation.StartPage);
                if (startPageIDs.Count == 1)
                {
                    startPageID = startPageIDs[0].PageID;
                    citation.StartPageID = startPageID;
                }
            }

            if (citation.ItemID != null && endPageID == -1 && !string.IsNullOrWhiteSpace(citation.EndPage))
            {
                CustomGenericList<Page> endPageIDs = PageSelectByItemAndPageNumber(
                    (int)citation.ItemID, citation.Volume, citation.Issue, citation.EndPage);
                if (endPageIDs.Count == 1)
                {
                    endPageID = endPageIDs[0].PageID;
                    citation.EndPageID = endPageID;
                }
            }

            if (isValid && startPageID != -1 && endPageID != -1)
            {
                // Make sure the start page, end page, and item identifiers all belong to the same item.
                CustomGenericList<Page> pageRange = PageSelectRangeForPagesAndItem(startPageID, endPageID, citation.ItemID);
                if (pageRange.Count == 0)
                {
                    citation.Errors.Add(
                        GetNewImportRecordError(
                            string.Format("Item {0}, Start Page {1}, and End Page {2} are mismatched", 
                                citation.ItemIDString, citation.StartPageID.ToString(), citation.EndPageID.ToString())
                            )
                        );
                    isValid = false;
                }
                else
                {
                    // Add ImportRecordPage records for the range of page ids
                    foreach(Page page in pageRange)
                    {
                        ImportRecordPage irPage = GetNewImportRecordPage(page.PageID);
                        irPage.SequenceOrder = (short)(citation.Pages.Count + 1);
                        citation.Pages.Add(irPage);
                    }
                }
            }

            // Verify any additional page IDs
            foreach(string pageID in citation.PageIDs)
            {
                // Check data type of page id
                if (Int32.TryParse(pageID, out int pid))
                {
                    // Make sure page id is valid BHL identifier
                    Page page = PageSelectAuto(pid);
                    if (page == null)
                    {
                        citation.Errors.Add(GetNewImportRecordError(string.Format("Page {0} not found", pid.ToString())));
                        isValid = false;
                    }
                    else
                    {
                        if (page.ItemID != citation.ItemID && !string.IsNullOrEmpty(citation.ItemIDString))
                        {
                            citation.Errors.Add(GetNewImportRecordError(string.Format("Page {0} not part of Item {1}", pid.ToString(), citation.ItemID)));
                            isValid = false;
                        }
                        else
                        {
                            ImportRecordPage irPage = GetNewImportRecordPage(pid);
                            irPage.SequenceOrder = (short)(citation.Pages.Count + 1);
                            citation.Pages.Add(irPage);
                        }
                    }
                }
                else
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Page ID {0}", pageID)));
                    isValid = false;
                }
            }

            // Make sure author names are valid
            foreach (ImportRecordCreator author in citation.Authors)
            {
                if (author.FullName.ToLower().Contains("et al") ||
                    author.LastName.ToLower().Contains("et al") ||
                    author.FirstName.ToLower().Contains("et al"))
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Author {0}", author.FullName)));
                    isValid = false;
                }
            }

            // Make sure that DOIs are syntactically correct            
            if (!string.IsNullOrWhiteSpace(citation.DOI))
            {
                // Strip off doi.org prefixes
                citation.DOI = citation.DOI.Replace("http://dx.doi.org/", "").Replace("https://dx.doi.org/", "")
                    .Replace("dx.doi.org/", "").Replace("http://doi.org/", "").Replace("https://doi.org", "")
                    .Replace("doi.org/", "");

                // Make sure the remaining DOI value is properly formatted
                Regex regex = new Regex("^10.\\d{4,9}/[-._;()/:a-zA-Z0-9]+$");
                if (!regex.IsMatch(citation.DOI))
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid DOI {0}", citation.DOI)));
                    isValid = false;
                }
            }

            // Make sure that the date value is formatted correctly.
            // Only YYYY, YYYY-MM, YYYY-MM-DD, and YYYY-YYYY are accepted.
            if (!string.IsNullOrWhiteSpace(citation.Year))
            {
                Regex regexYYYY = new Regex("^[0-9]{4}$");
                Regex regexYYYYMM = new Regex("^[0-9]{4}-[0-9]{2}$");
                Regex regexYYYYMMDD = new Regex("^[0-9]{4}-[0-9]{2}-[0-9]{2}$");
                Regex regexYYYYYYYY = new Regex("^[0-9]{4}-[0-9]{4}$");
                if (!(regexYYYY.Match(citation.Year).Success ||
                    regexYYYYMM.Match(citation.Year).Success ||
                    regexYYYYMMDD.Match(citation.Year).Success ||
                    regexYYYYYYYY.Match(citation.Year).Success))
                {
                    citation.Errors.Add(GetNewImportRecordError(
                        string.Format("Date {0} must be YYYY, YYYY-MM, YYYY-MM-DD, or YYYY-YYYY.", citation.Year)));
                    isValid = false;
                }

            }

            // Check data type of title identifier
            if (string.IsNullOrWhiteSpace(citation.TitleIDString))
            {
                citation.TitleID = null;
            }
            else
            {
                if (Int32.TryParse(citation.TitleIDString, out int titleID))
                {
                    citation.TitleID = titleID;
                }
                else
                {
                    citation.TitleID = null;
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Invalid Title ID {0}", citation.TitleIDString)));
                    isValid = false;
                }
            }

            // Make sure Title ID is valid BHL identifier
            if (citation.TitleID != null)
            {
                if (TitleSelectAuto((int)citation.TitleID) == null)
                {
                    citation.Errors.Add(GetNewImportRecordError(string.Format("Title {0} not found", citation.TitleIDString)));
                    isValid = false;
                }
            }

            return isValid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="citation"></param>
        /// <returns></returns>
        private bool IsImportRecordComplete(ImportRecord citation)
        {
            bool isComplete = true;

            if (string.IsNullOrWhiteSpace(citation.ItemID.ToString()))
            {
                citation.Errors.Add(GetNewImportRecordError(string.Format("No item can be identified from the supplied metadata.")));
                isComplete = false;
            }

            if (citation.Pages.Count == 0)
            {
                citation.Errors.Add(GetNewImportRecordError(string.Format("No pages supplied, or pages could not be identified.")));
                isComplete = false;
            }

            return isComplete;
        }

        private ImportRecordErrorLog GetNewImportRecordError(string message)
        {
            ImportRecordErrorLog importRecordErrorLog = new ImportRecordErrorLog();
            importRecordErrorLog.ErrorDate = DateTime.Now;
            importRecordErrorLog.ErrorMessage = message;
            return importRecordErrorLog;
        }

        private ImportRecordPage GetNewImportRecordPage(int pageID)
        {
            ImportRecordPage importRecordPage = new ImportRecordPage();
            importRecordPage.PageID = pageID;
            return importRecordPage;
        }
    }
}
